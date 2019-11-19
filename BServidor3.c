#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>
#include <my_global.h>

typedef struct{
	char usuario[20];
	int socket;
}TUsuario;

typedef struct{
	int num;
	TUsuario usuarios[100];
}TLista;	

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int l;
int sockets[100];
TLista lista;

int AddUsuario(char user[20], int s)
{
	int i;
	int YaIntroducido = 0;
	for (i = 0; i < lista.num; i++)
	{
		if (strcmp(lista.usuarios[i].usuario, user)==0)
		{
			YaIntroducido = 1;
		}
	}
	if (!YaIntroducido)
	{
		if (lista.num < 100)
		{
			//pthread_mutex_lock(&mutex);
			
			strcpy(lista.usuarios[lista.num].usuario, user);
			lista.usuarios[lista.num].socket = s;
			lista.num = lista.num + 1;
			return 0;
			
			//pthread_mutex_unlock(&mutex);
		}
		
		else 
		{
			return -1;
		}
	}
	else
	{
		return -1;
	}
}

void BorrarUsuario(char nombre[20])
{
	int encontrado = 0;
	int i;
	for (i = 0; i < lista.num && !encontrado; i++)
	{
		if (strcmp(lista.usuarios[i].usuario, nombre) == 0)
		{
			encontrado = 1;
		}
	}
	if(encontrado)
	{
		//pthread_mutex_lock(&mutex);
		int j;
		for(j = i; j < lista.num; j++)
		{
			lista.usuarios[j - 1]=lista.usuarios[j];
		}
		lista.num = lista.num - 1;
		
		//pthread_mutex_unlock(&mutex);
	}
}

void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	//int socket_conn = * (int *) socket;
	
	char peticion[512];
	char respuesta[512];
	int ret;
	
	MYSQL *conn;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	int terminar =0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar ==0)
	{
		// Ahora recibimos la petici?n
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		// Ya tenemos el codigo de la peticion
		char nombre[20];
		printf("Peticion numero %d\n", codigo);
		if (codigo ==0) //peticion de conexion
		{
			conn = mysql_init(NULL);
			if (conn==NULL)
			{
				printf ("Error al crear la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "TG4XXX",0, NULL, 0);
			if (conn==NULL)
			{
				printf ("Error al inicializar la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			int err;
			p = strtok(NULL, "/");
			char nombre[20];
			strcpy (nombre, p);
			
			printf("%s está intentando iniciar sesión\n", nombre);
			
			p = strtok(NULL, "/");
			char password[20];
			strcpy (password, p);
			
			err=mysql_query (conn, "SELECT * FROM Jugador");
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//recogemos el resultado de la consulta. El resultado de la
			//consulta se devuelve en una variable del tipo puntero a
			//MYSQL_RES tal y como hemos declarado anteriormente.
			//Se trata de una tabla virtual en memoria que es la copia
			//de la tabla real en disco.

			resultado = mysql_store_result (conn);
			// El resultado es una estructura matricial en memoria
			// en la que cada fila contiene los datos de una persona.
			
			
			int encontrado = 0;
			row = mysql_fetch_row (resultado);
			while (!encontrado && row != NULL)
			{
				// Ahora obtenemos la primera fila que se almacena en una
				// variable de tipo MYSQL_ROW
				if (strcmp(row[1], nombre) == 0)
				{	
					if (strcmp(row[2], password)==0)
					{
						int hapodido = AddUsuario(nombre, sock_conn);
						if (hapodido == 0)
						{
							strcpy(respuesta, "0/correcto");
							encontrado = 1;
							printf("El usuario %s ha iniciado sesion correctamente\n", nombre);
							//añadir usuario a lista usuarios
							
							//Notificar a todos los clientes conectados
							//Creo el string que enviaré
							char listausuarios[2000];
							//Relleno el string de la lista de usuarios que enviaré a los clientes
							int i;
							strcpy(listausuarios, "6/");
							for(i = 0; i < lista.num; i++)
							{
								printf("Nombre del usuario en la posicion %d: %s. Su socket: %d\n", lista.num, lista.usuarios[i].usuario, lista.usuarios[i].socket);
								strcat(listausuarios, ("%s", lista.usuarios[i].usuario));
								strcat(listausuarios, "_");
								
								char socket_usuario[100];
								sprintf(socket_usuario, "%d", lista.usuarios[i].socket);
								strcat(listausuarios, socket_usuario);
								strcat(listausuarios, "_");
							}
							//Envio la lista de usuarios a todos los clientes conectados (por el socket que tienen asociado en la lista de usuarios)
							int j;
							printf("%s\n", listausuarios);
							for (j = 0; j < l; j++)
							{
								write (sockets[j], listausuarios, strlen(listausuarios));
							}
							write (sock_conn, respuesta, strlen(respuesta));
						}
						else
						{
							printf("Ya esta conectado\n");
							encontrado = 1;
							strcpy(respuesta, "0/conectado");
							write (sock_conn,respuesta, strlen(respuesta));
						}
					}
					else if (strcmp(row[2], password)!=0)
					{
						strcpy(respuesta, "0/no_contrasena");
						encontrado = 1;
						write (sock_conn,respuesta, strlen(respuesta));
					}
				}
				row = mysql_fetch_row (resultado);
			}
			if (encontrado == 0)
			{
				strcpy(respuesta, "0/no_usuario");
				write (sock_conn,respuesta, strlen(respuesta));
			}
			mysql_close(conn);
		}
		
		else if (codigo == 1)  //Consulta 1 del cliente
		{
			conn = mysql_init(NULL);
			if (conn==NULL)
			{
				printf ("Error al crear la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			conn = mysql_real_connect (conn, "shiva.upc.es","root", "mysql", "TG4XXX",0, NULL, 0);
			if (conn==NULL)
			{
				printf ("Error al inicializar la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			printf("Se está efectuando la primera consulta\n");
			int err;
			p = strtok( NULL, "/");
			
			char nombre1[20];
			strcpy (nombre1, p);
			p = strtok( NULL, "/");
			
			char nombre2[20];
			strcpy (nombre2, p);
			
			char consulta1[512];
			strcpy(consulta1, ("SELECT Partida.FechaFinal FROM Partida WHERE Partida.Identificador IN (SELECT Relacion.Partida FROM Jugador, Relacion WHERE Jugador.Username = '"));
			strcat(consulta1, nombre1);
			strcat(consulta1, ("' AND Jugador.Identificador = Relacion.Jugador AND Relacion.Partida IN( SELECT Relacion.Partida FROM Jugador, Relacion WHERE Jugador.Username = '"));
			strcat(consulta1, nombre2);
			strcat(consulta1, ("' AND Jugador.Identificador = Relacion.Jugador))"));
			
			err = mysql_query (conn, consulta1);
			
			if ((err!=0)) 
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}

			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la primera consulta\n");
				strcpy(respuesta, "1/");
				strcat(respuesta, "no");
			}
			else
			{
				// El resultado debe ser una matriz con una sola fila y una columna que contiene la fecha
				printf ("Primera fecha en la que dos jugadores han jugado juntos: %s\n", row[0] );
				strcpy(respuesta, "1/");
				strcat(respuesta, row[0]);
			}
			write (sock_conn,respuesta, strlen(respuesta));
			mysql_close(conn);
		}
		
		else if (codigo == 2)  //Consulta 2 del cliente
		{
			conn = mysql_init(NULL);
			if (conn==NULL)
			{
				printf ("Error al crear la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "TG4XXX",0, NULL, 0);
			if (conn==NULL)
			{
				printf ("Error al inicializar la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			char consulta2[512];
			printf("Se está efectuando la segunda consulta\n");
			strcpy (consulta2, "SELECT Jugador.Experiencia FROM Jugador WHERE Jugador.Identificador = (SELECT Relacion.Jugador FROM Relacion WHERE Relacion.Ganador = '1' AND Relacion.Partida = (SELECT Partida.Identificador FROM Partida WHERE Partida.Identificador = (SELECT MAX(Partida.Identificador) FROM Partida)))");
			int err2 = mysql_query (conn, consulta2);
			if (err2!=0) 
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit(1);
			}
			else
			{
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la segunda consulta\n");
					strcpy(respuesta, "2/no");
				}
				else
				{
					strcpy(respuesta, "2/");
					strcat(respuesta, row[0]);
				}
			}
			write (sock_conn,respuesta, strlen(respuesta));
			mysql_close(conn);
		}
		
		else if (codigo == 3)  //Consulta 3 del cliente
		{
			conn = mysql_init(NULL);
			if (conn==NULL)
			{
				printf ("Error al crear la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "TG4XXX",0, NULL, 0);
			if (conn==NULL)
			{
				printf ("Error al inicializar la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			printf("Se está efectuando la tercera consulta\n");
			char consulta3[512];
			strcpy (consulta3, "SELECT Partida.Ganador FROM Partida");
			printf("%s\n", consulta3);
			int err3 = mysql_query(conn, consulta3);
			if (err3 != 0)
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit(1);
			}
			else
			{
				p = strtok(NULL, "/");
				char nombre1[20];
				strcpy (nombre1, p);
				resultado = mysql_store_result(conn);
				row = mysql_fetch_row(resultado);
				int ganadas = 0;
				while (row != NULL)
				{
					if (strcmp(row[0], nombre1) == 0)
					{
						ganadas = ganadas + 1;
					}
					row = mysql_fetch_row(resultado);
				}
				sprintf(respuesta, "3/");
				char ganadas_string[20];
				sprintf(ganadas_string, "%d", ganadas);
				
				strcat(respuesta, ("%s", ganadas_string));
			}
			write (sock_conn,respuesta, strlen(respuesta));
			mysql_close(conn);
		}
		
		else if (codigo == 4)  //Registro
		{
			conn = mysql_init(NULL);
			if (conn==NULL)
			{
				printf ("Error al crear la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "TG4XXX",0, NULL, 0);
			if (conn==NULL)
			{
				printf ("Error al inicializar la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}			

			p = strtok(NULL, "/");
			char nuevo[20];
			strcpy (nuevo, p);
			
			printf("%s se está intentando dar de alta en la base de datos\n", nuevo);
			
			p = strtok(NULL, "/");
			char contrasena[20];
			strcpy(contrasena, p);
			
			int err4 = mysql_query(conn, "SELECT * FROM Jugador");
			if (err4 != 0)
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result(conn);
			int encontrado4 = 0;
			int jugadores = 0;
			row = mysql_fetch_row(resultado);
			while (!encontrado4 && row != NULL)
			{
				if (strcmp(row[1], nuevo) == 0)
				{
					strcpy(respuesta, "4/ya registrado");
					encontrado4 = 1;
					printf("Ya está en la lista de jugadores\n");
				}
				jugadores++;
				row = mysql_fetch_row(resultado);
			}
			if (encontrado4 == 0)
			{
				jugadores++;
				char nuevoID[20];
				sprintf(nuevoID, "%d", jugadores);
				char consulta4[512]= "INSERT INTO Jugador VALUES ('";
				strcat(consulta4, nuevoID);
				strcat(consulta4, "', '");
				strcat(consulta4, nuevo);
				strcat(consulta4, "', '");
				strcat(consulta4, contrasena);
				strcat(consulta4, "', 0)");
				int err5 = mysql_query(conn, consulta4);
				if (err5 != 0)
				{
					printf ("Error al insertar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				strcpy(respuesta, "4/correcto");
				printf("El usuario %s se ha dado de alta correctamente\n", nuevo);
			}
			write (sock_conn,respuesta, strlen(respuesta));
			mysql_close(conn);
		}

		else if(codigo == 5) //Desconectar
		{
			char nombre[20];
			p = strtok(NULL, "/");
			sprintf(nombre, p);
			printf("El usuario %s se está desconectando\n", nombre);
			BorrarUsuario(nombre);
			strcpy(respuesta, "5/");
			write (sock_conn,respuesta, strlen(respuesta));
			char listausuarios[2000];
			//Relleno el string de la lista de usuarios que enviaré a los clientes
			int i;
			strcpy(listausuarios, "6/");
			for(i = 0; i < lista.num; i++)
			{
				printf("Nombre del usuario en la posicion %d: %s. Su socket: %d\n", i, lista.usuarios[i].usuario, lista.usuarios[i].socket);
				strcat(listausuarios, ("%s", lista.usuarios[i].usuario));
				strcat(listausuarios, "_");
				
				char socket_usuario[100];
				sprintf(socket_usuario, "%d", lista.usuarios[i].socket);
				strcat(listausuarios, socket_usuario);
				strcat(listausuarios, "_");
			}
			//Envio la lista de usuarios a todos los clientes conectados (por el socket que tienen asociado en la lista de usuarios)
			int j;
			printf("%s\n", listausuarios);
			for (j = 0; j < l; j++)
			{
				write (sockets[j], listausuarios, strlen(listausuarios));
			}
			terminar = 1;
		}
		
		else if(codigo == 7)
		{
			char nombre[20];
			p = strtok(NULL, "/");
			sprintf(nombre, p);
			int j;
			int encontrado6 = 0;
			for (j = 0; j < lista.num && !encontrado6; j++)
			{
				if (strcmp(lista.usuarios[j].usuario, nombre) == 0)
				{
					sprintf(respuesta, "7/%d", &lista.usuarios[j].socket);
					printf("Encontrado!\n");
					encontrado6 = 1;
				}
			}
			write (sock_conn,respuesta, strlen(respuesta));
		}
		printf("Petición atendida\n");
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
}


int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(50061);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind\n");
	
	if (listen(sock_listen, 2) < 0)
		printf("Error en el Listen\n");
	
	
	pthread_t thread;
	l=0;
	// Bucle para atender a n clientes
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[l] =sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacer
		
		pthread_create (&thread, NULL, AtenderCliente,&sockets[l]);
		l=l+1;
		
	}
	
	//for (i=0; i<5; i++)
	//pthread_join (thread[i], NULL);
	
	
	
}
