using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Principal : Form
    {

//PARAMETROS FORM E INICIALIZACION

        string IP = "192.168.25.172";
        int puerto = 7829;
        bool thread_start;
        Socket server;
        bool registrado;
        int cont = 0;
        public Usuario user = new Usuario();
        Thread atender;
        public Principal()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Para poder avanzar sin el problema de cross-threading
        }

//FUNCIONES DE INTERACCIÓN CON EL FORM

        private void button2_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse(IP);
            IPEndPoint ipep = new IPEndPoint(direc, puerto);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //Intentamos conectar al socket
                server.Connect(ipep);
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            if (!thread_start)
            {
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
                thread_start = true;
            }
            
            if (!registrado)
            {
                //Preparamos el mensaje que vamos a enviar
                string mensaje = "0/" + usuario.Text + "/" + password.Text;
                this.user.nombre = usuario.Text;
                //Enviamos nuestra consulta y recibimos del servidor la respuesta
                Enviar(mensaje);
            }

            if (registrado)
            {
                panel1.Visible = true;
            }
        }

        private void registrarbtn_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse(IP);
            IPEndPoint ipep = new IPEndPoint(direc, puerto);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //Intentamos conectar al socket
                server.Connect(ipep);
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            string registro = "4/" + usuario.Text + "/" + password.Text;
            //Lo enviamos
            Enviar(registro);
            if (!thread_start)
            {
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
                thread_start = true;
            }
        }
        
        private void enviar_Click(object sender, EventArgs e)
        {
            if (registrado)
            {
                //Si nos hemos conectado, realizamos la consulta seleccionada

                if (consulta1.Checked)
                {
                    Consulta1Envio();
                }
                if (consulta2.Checked)
                {
                    Consulta2Envio();
                }
                if (consulta3.Checked)
                {
                    Consulta3Envio();
                }
            }
        }

        private void desconectar_Click(object sender, EventArgs e)
        {
            Desconectar();
            this.BackColor = Color.WhiteSmoke;
            this.registrado = false;
            thread_start = false;
        }

        private void listaConBtn_Click(object sender, EventArgs e)
        {
            if (registrado)
            {
                VerConectados TablaUsuarios = new VerConectados();
                TablaUsuarios.SetIP(this.IP);
                TablaUsuarios.SetPuerto(this.puerto);
                TablaUsuarios.ShowDialog();
            }
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (registrado)
            {
                desconectar_Click(sender, e);
            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            //tablaUsuarios.ColumnCount = 2;
            //tablaUsuarios.Columns[0].HeaderText = "Usuarios";
            //tablaUsuarios.Columns[1].HeaderText = "Socket";
        }

//FUNCIONES COMUNICACIÓN SERVIDOR/CLIENTE

        private IPEndPoint PrepararIPEndPoint()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse(IP);
            IPEndPoint ipep = new IPEndPoint(direc, puerto);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            return ipep;
        }

        private void Conectar(IPEndPoint ipep)
        {
            try
            {
                //Intentamos conectar al socket
                server.Connect(ipep);
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
        }

        private void Enviar(string mensaje)
        {
            try
            {
                // Enviamos al servidor el mensaje
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            catch
            {
                MessageBox.Show("No he podido conectar con el servidor");
            }
        }

        private void Desconectar()
        {
            if (registrado)
            {
                //Prepara la petición de desconexión
                string mensaje = "5/" + this.user.nombre;
                listBox1.Items.Clear();

                //Envia la petición de desconexión para ese usuario (para que nos borre de la lista)
                Enviar(mensaje);
                MessageBox.Show("Desconectado correctamente");
            }
        }

        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string respuesta = trozos[1].Split('\0')[0];

                switch (codigo)
                {
                    case 0: //Iniciar sesión
                        if (respuesta == "correcto")
                        {
                            if (!registrado)
                            {
                                MessageBox.Show("Credenciales correctas");
                                this.BackColor = Color.Green;
                                this.registrado = true;

                                //Enviar(mensaje);
                                this.usuario.Text = "";
                                this.password.Text = "";
                            }
                        }
                        else if (respuesta == "conectado")
                        {
                            MessageBox.Show("Ya estás conectado al servidor");
                        }
                        else
                        {
                            MessageBox.Show("Error al iniciar sesión");
                            password.Text = "";
                        }

                        break;

                    case 1: //Consulta 1

                        MessageBox.Show("La primera vez que jugaron fue: " + respuesta);
                        break;

                    case 2: //Consulta 2


                        MessageBox.Show("La experiencia del jugador que ganó la última partida es: " + respuesta + " puntos");
                        break;

                    case 3: //Consulta 3

                        MessageBox.Show("El jugador " + textBox1.Text + " ha ganado " + respuesta + " partidas");
                        break;

                    case 4: //Registro

                        if (respuesta == "correcto")
                        {
                            MessageBox.Show("Te has dado de alta correctamente en la base de datos");
                        }
                        else if (respuesta == "ya registrado")
                        {
                            MessageBox.Show("Ya hay un usuario con ese nombre de usuario");
                        }
                        else
                        {
                            MessageBox.Show("Se ha producido un error. Vuelve a intentarlo.");
                        }
                        break;

                    case 5: //Desconectar no necesita código porque no recibe nada

                        //Se desconecta el cliente
                        atender.Abort();
                        server.Shutdown(SocketShutdown.Both);
                        server.Close();
                        this.registrado = false;
                        listBox1.Items.Clear();
                        MessageBox.Show("Desconectado correctamente");

                        break;
                    case 6: //Lista de Conectados

                        if (!registrado)
                        {
                            this.BackColor = Color.Green;
                            this.user.nombre = usuario.Text;

                            usuario.Text = "";
                            password.Text = "";
                        }
                        List<Usuario> ListaUsuarios = new List<Usuario>();
                        //Adaptamos la respuesta a nuestro formato de datos (Lista)
                        string[] prov = respuesta.Split('_');
                        int i = 0;
                        while (i < prov.Length - 1)
                        {
                            Usuario u = new Usuario();
                            u.nombre = prov[i];
                            u.socket = Convert.ToInt32(prov[i + 1]);
                            ListaUsuarios.Add(u);
                            i = i + 2;
                        }

                        listBox1.Items.Clear();
                        //tablaUsuarios.Rows.Clear();
                        for (int j = 0; j < ListaUsuarios.Count; j++)
                        {
                            listBox1.Items.Add(ListaUsuarios[j].nombre);
                            //tablaUsuarios.Rows.Add(ListaUsuarios[j].nombre, ListaUsuarios[j].socket);
                        }

                        break;

                    case 7: //Recibir Socket

                        this.user.socket = Convert.ToInt32(respuesta);
                        break;

                    case 8: //Invitacion a jugar conmigo
                        if (MessageBox.Show("¡El usuario " + respuesta + " te está invitando a una partida privada! ¿Aceptas?", "Invitación!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string respuesta2 = "9/" + this.user.nombre + "/" + respuesta + "/si";
                            Enviar(respuesta2);
                        }
                        else if (MessageBox.Show("¡El usuario " + respuesta + " te está invitando a una partida privada! ¿Aceptas?", "Invitación!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            string respuesta2 = "9/" + this.user.nombre + "/" + respuesta + "/no";
                            Enviar(respuesta2);
                        }
                        break;
                    case 9: //Respuesta a la invitación
                        string[] confirmacion = respuesta.Split('_');
                        if (confirmacion[1] == "si")
                            MessageBox.Show("El usuario " + confirmacion[0] + " ha aceptado la invitacion!");
                        else if (confirmacion[1] == "no")
                            MessageBox.Show("El usuario " + confirmacion[0] + " ha rechazado la invitacion!");
                        break;
                    case 10: //Mensaje recibido en el chat
                        string[] mensaje_recibido = respuesta.Split('_');
                        listBox2.Items.Add(mensaje_recibido[0] + " : " + mensaje_recibido[1]);

                        if (listBox2.Items.Count >= 10) //Establecemos un numero maximo de mensajes que podemos tener en el chat
                        {
                            listBox2.Items.RemoveAt(0);
                        }
                        break;
                }
            }
        }
//CONSULTAS
        
        public void Consulta1Envio()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "1/" + textBox1.Text + "/" + textBox2.Text;

            //Enviamos nuestra consulta y recibimos del servidor la respuesta
            Enviar(mensaje);
        }
        
        public void Consulta2Envio()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "2/";

            //Enviamos nuestra consulta y recibimos del servidor la respuesta
            Enviar(mensaje);

        }
        
        public void Consulta3Envio()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "3/" + textBox1.Text;

            //Enviamos nuestra consulta y recibimos del servidor la respuesta
            Enviar(mensaje);
        }


//INTERFAZ GRÁFICA

        private void SeleccionarCarta(object sender, EventArgs e)
        {
            if (registrado)
            {
                PictureBox cartasel = (PictureBox)sender;
                cartasel.Location = new Point(200 + 10 * cont, 200);
                cartasel.Image = new Bitmap("WhatsApp Image 2019-10-21 at 17.49.50.jpeg");
                cartasel.SizeMode = PictureBoxSizeMode.StretchImage;
                cont++;
            }
        }

        private void invitar_Click(object sender, EventArgs e)
        {
            string sel = Convert.ToString(listBox1.SelectedIndex);
            string invitacion = "8/" + this.user.nombre + "/" + sel;
            MessageBox.Show(invitacion);
            Enviar(invitacion);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void enviar_mensaje_Click(object sender, EventArgs e)
        {
            string mensajechat = "10/" + this.user.nombre + "/" + mensaje.Text;
            Enviar(mensajechat);
        }
    }
}