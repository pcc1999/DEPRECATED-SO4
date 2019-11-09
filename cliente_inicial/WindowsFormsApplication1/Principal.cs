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
        string IP = "192.168.25.141";
        int puerto = 9230;
        Socket server;
        bool registrado;
        int cont = 0;
        public Principal()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!registrado)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse(IP);
                IPEndPoint ipep = new IPEndPoint(direc, puerto);


                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket
                }
                catch (SocketException)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }

                //Preparamos el mensaje que vamos a enviar
                string mensaje = "0/" + usuario.Text + "/" + password.Text;
                // Enviamos al servidor el mensaje
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                if (mensaje == "correcto")
                {
                    MessageBox.Show("Credenciales correctas");
                    this.BackColor = Color.Green;
                    registrado = true;
                    usuario.Text = "";
                    password.Text = "";
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
        public void Consulta2()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "2/";
            //Enviamos la consulta al servidor
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            MessageBox.Show("La experiencia del jugador que ganó la última partida es: " + mensaje + " puntos");
        }
        public void Consulta1()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "1/" + textBox1.Text + "/" + textBox2.Text;
            //Enviamos la consulta al servidor
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            MessageBox.Show("La primera vez que jugaron fue: " + mensaje);
        }
        public void Consulta3()
        {
            //Preparamos el mensaje que queremos consultar
            string mensaje = "3/" + textBox1.Text;
            //Enviamos la consulta al servidor
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            MessageBox.Show("El jugador " + textBox1.Text + " ha ganado " + mensaje + " partidas");
        }
        private void enviar_Click(object sender, EventArgs e)
        {
            if (registrado)
            {
                //Creamos la conexión
                IPAddress direc = IPAddress.Parse(IP);
                IPEndPoint ipep = new IPEndPoint(direc, puerto);

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //Intentamos conectar con el servidor
                try
                {
                    server.Connect(ipep);
                }
                catch (SocketException)
                {
                    //Mensaje de error en caso de no poder establecer la conexión
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
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
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
        private void desconectar_Click(object sender, EventArgs e)
        {

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse(IP);
            IPEndPoint ipep = new IPEndPoint(direc, puerto);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.WhiteSmoke;
            }
            catch (SocketException)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            // Quiere saber la longitud
            string mensaje = "5/" + "Desconectar";
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Se terminó el servicio. 
            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            registrado = false;
        }
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