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

namespace WindowsFormsApplication1
{
    public partial class Principal : Form
    {

//PARAMETROS FORM E INICIALIZACION

        string IP = "192.168.25.144";
        int puerto = 9020;
        Socket server;
        bool registrado;
        int cont = 0;
        public Usuario user = new Usuario();
        public Principal()
        {
            InitializeComponent();
        }

//FUNCIONES DE INTERACCIÓN CON EL FORM

        private void button2_Click(object sender, EventArgs e)
        {
            if (!registrado)
            {
                //Preparamos el IPEndPoint y nos conectamos al socket
                IPEndPoint ipep = PrepararIPEndPoint();

                //Intentamos conectarnos al servidor
                Conectar(ipep);

                //Preparamos el mensaje que vamos a enviar
                string mensaje = "0/" + usuario.Text + "/" + password.Text;

                //Enviamos nuestra consulta y recibimos del servidor la respuesta
                string respuesta = EnviarYRecibir(mensaje);

                if (respuesta == "correcto")
                {
                    MessageBox.Show("Credenciales correctas");
                    this.BackColor = Color.Green;
                    registrado = true;
                    this.user.nombre = usuario.Text;
                    mensaje = "7/" + this.user.nombre;

                    //Preparamos el IPEndPoint y nos conectamos al socket
                    IPEndPoint ipep2 = PrepararIPEndPoint();

                    //Intentamos conectarnos al servidor
                    Conectar(ipep2);

                    string socket_usuario = EnviarYRecibir(mensaje);
                    this.user.socket = Convert.ToInt32(socket_usuario);
                    usuario.Text = "";
                    password.Text = "";
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
            }
            if (registrado)
            {
                panel1.Visible = true;
            }
        }

        private void registrarbtn_Click(object sender, EventArgs e)
        {
            Registro form = new Registro();
            form.SetIP(this.IP);
            form.SetPuerto(this.puerto);
            form.ShowDialog();
        }
        
        private void enviar_Click(object sender, EventArgs e)
        {
            if (registrado)
            {
                //Preparamos el IPEndPoint y nos conectamos al socket
                IPEndPoint ipep = PrepararIPEndPoint();

                //Intentamos conectarnos al servidor
                Conectar(ipep);

                //Si nos hemos conectado, realizamos la consulta seleccionada

                if (consulta1.Checked)
                {
                    Consulta1();
                }
                if (consulta2.Checked)
                {
                    Consulta2();
                }
                if (consulta3.Checked)
                {
                    Consulta3();
                }

                //Vaciamos los textbox
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void desconectar_Click(object sender, EventArgs e)
        {
            //Preparamos el IPEndPoint y nos conectamos al socket
            IPEndPoint ipep = PrepararIPEndPoint();

            //Intentamos conectarnos al servidor
            Conectar(ipep);

            Desconectar();
            this.BackColor = Color.WhiteSmoke;
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
            desconectar_Click(sender, e);
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

        private string EnviarYRecibir(string mensaje)
        {
            try
            {
                // Enviamos al servidor el mensaje
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                return Encoding.ASCII.GetString(msg2).Split('\0')[0];
            }
            catch
            {
                MessageBox.Show("No he podido conectar con el servidor");
                return null;
            }
        }

        private void Desconectar()
        {
            if (registrado)
            {
                //Prepara la petición de desconexión
                string mensaje = "5/" + this.user.nombre;

                //Envia la petición de desconexión para ese usuario (para que nos borre de la lista)
                EnviarYRecibir(mensaje);

                //Se desconecta el cliente
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                registrado = false;
                MessageBox.Show("Desconectado correctamente");
            }
        }

//CONSULTAS
        
        public void Consulta1()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "1/" + textBox1.Text + "/" + textBox2.Text;

            //Enviamos nuestra consulta y recibimos del servidor la respuesta
            string respuesta = EnviarYRecibir(mensaje);

            MessageBox.Show("La primera vez que jugaron fue: " + respuesta);
        }
        
        public void Consulta2()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "2/";

            //Enviamos nuestra consulta y recibimos del servidor la respuesta
            string respuesta = EnviarYRecibir(mensaje);

            MessageBox.Show("La experiencia del jugador que ganó la última partida es: " + respuesta + " puntos");
        }
        
        public void Consulta3()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "3/" + textBox1.Text;

            //Enviamos nuestra consulta y recibimos del servidor la respuesta
            string respuesta = EnviarYRecibir(mensaje);

            MessageBox.Show("El jugador " + textBox1.Text + " ha ganado " + respuesta + " partidas");
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
    }
}