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
    public partial class Inicio : Form
    {
        string IP = "192.168.25.132";
        int puerto = 9230;
        Socket server;
        public Inicio()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
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
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
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
                Consulta form = new Consulta();
                form.SetIP(this.IP);
                form.SetPuerto(this.puerto);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error al iniciar sesión");
            }
        }

        private void registrarbtn_Click(object sender, EventArgs e)
        {
            Registro form = new Registro();
            form.SetIP(this.IP);
            form.SetPuerto(this.puerto);
            form.ShowDialog();
        }
    }
}
