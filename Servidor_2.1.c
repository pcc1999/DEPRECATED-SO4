#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>

typedef struct{
	char usuario[20];
	int socket;
}TUsuario;

typedef struct{
	int num;
	TUsuario usuarios[100];
}TLista;	

TLista lista;

int AddUsuario(char user[20], int socket)
{
	if (lista.num < 100)
	{
		strcpy(lista.usuarios[lista.num].usuario, user);
		lista.usuarios[lista.num].socket = socket;
		lista.num = lista.num + 1;
		return 0;
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
		for(i; i < lista.num; i++)
		{
			lista.usuarios[i]=lista.usuarios[i+1];
		}
		lista.num = lista.num - 1;
	}
}

void *AtenderCliente(void *socket)
{
	int *s;
	s = (int *) socket;
	int terminar = 0;
	int sock_conn, sock_listen, ret;
	sock_conn = *s;
	struct sockaddr_in serv_adr;
	char buff[512];
	char respuesta[20];
	char experiencia[64];
	char consulta2[512];
	
	MYSQL *conn;
	int err;
	int err1;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	//for consultas
	while(terminar == 0)
	{
		conn = mysql_init(NULL);
		if (conn==NULL)
		{
			printf ("Error al crear la conexion: %u %s\n", 
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		conn = mysql_real_connect (conn, "localhost","root", "mysql", "Juego",0, NULL, 0);
		if (conn==NULL)
		{
			printf ("Error al inicializar la conexion: %u %s\n", 
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		printf ("Escuchando\n");
		
		
		//sock_conn es el socket que usaremos para este cliente
		
		// Ahora recibimos su nombre, que dejamos en 
		ret=read(sock_conn,buff, sizeof(buff));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		buff[ret]='\0';
		
		// Separamos usuario de contraseña ya que lo hemos pasado
		// todo con una barra como delimitador
		
		char *p = strtok(buff, "/");
		int consulta = atoi(p);	
		
		if (consulta == 4)  //Registro
		{
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
					strcpy(respuesta, "ya registrado");
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
				strcpy(respuesta, "correcto");
				printf("El usuario %s se ha dado de alta correctamente\n", nuevo);
			}
			write (sock_conn, respuesta, strlen(respuesta));
		}
		if (consulta == 0)  //Log in
		{
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
						strcpy(respuesta, "correcto");
						encontrado = 1;
						printf("El usuario %s ha iniciado sesion correctamente\n", nombre);
						//añadir usuario a lista usuarios
						AddUsuario(nombre, socket);
					}
					else if (strcmp(row[2], password)!=0)
					{
						strcpy(respuesta, "no_contraseña");
						encontrado = 1;
					}
				}
				row = mysql_fetch_row (resultado);
			}
			if (encontrado == 0)
			{
				strcpy(respuesta, "no_usuario");
			}
			write (sock_conn, respuesta, strlen(respuesta));
		}
		if (consulta == 1)  //Consulta 1 del cliente
		{
			printf("Se está efectuando la primera consulta\n");
			
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
			
			err1 = mysql_query (conn, consulta1);
			
			if ((err1!=0)) 
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
				strcpy(respuesta, "no");
			}
			else
			{
				// El resultado debe ser una matriz con una sola fila y una columna que contiene la fecha
				printf ("Primera fecha en la que dos jugadores han jugado juntos: %s\n", row[0] );
				strcpy(respuesta, row[0]);
			}
			write (sock_conn, respuesta, strlen(respuesta));
		}
		if (consulta == 2)  //Consulta 2 del cliente
		{
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
				strcpy (respuesta, row[0]);
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la segunda consulta\n");
					strcpy(respuesta, "no");
				}
				else
				{
					strcpy(respuesta, row[0]);
				}
			}
			write (sock_conn, respuesta, strlen(respuesta));
		}
		if (consulta == 3)  //Consulta 3 del cliente
		{
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
				sprintf(respuesta, "%d", ganadas);
			}
			write (sock_conn, respuesta, strlen(respuesta));
		}
		if (consulta == 6)
		{
			char listausuarios[2000];
			int i;
			for(i = 0; i < lista.num; i++)
			{
				strcat(listausuarios, ("%s", lista.usuarios[i].usuario));
				strcat(listausuarios, "_");
			}
			strcat(listausuarios, "0");
			printf(listausuarios);
			write (sock_conn, listausuarios, strlen(listausuarios));
		}
		if(consulta == 5)
		{
			char nombre[20];
			p = strtok(NULL, "/");
			sprintf(nombre, p);
			BorrarUsuario(nombre);
			terminar = 1;
		}
		close(sock_conn);
	}
	
	//mysql_close(conn);
}

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
	{
		printf("Error creant socket");
	}
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	// htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el port 9230
	serv_adr.sin_port = htons(9230);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
	{
		printf ("Error al bind");
	}
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 2) < 0)
	{
		printf("Error en el Listen");
	}
	
	int i;
	pthread_t threads[100];
	int sockets[100];
	for(i = 0; i<100; i++)
	{
		printf("Escuchando\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("He recibido conexion\n");
		sockets[i] = sock_conn;
		pthread_create(&threads[i], NULL, AtenderCliente, &sockets[i]);
	}
	//Creamos una conexion al servidor MYSQL 
	
}
