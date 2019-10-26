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
    public partial class Consulta : Form
    {
        string IP = "192.168.25.132";
        int puerto = 9230;
        Socket server;
        public Consulta()
        {
            InitializeComponent();
        }
        public void SetPuerto(int p)
        {
            this.puerto = p;
        }
        public void SetIP(string ip)
        {
            this.IP = ip;
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
        }



        private void Consulta_Load(object sender, EventArgs e)
        {

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

            ////Recibimos la respuesta del servidor
            //byte[] msg2 = new byte[80];
            //server.Receive(msg2);
            //mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            //MessageBox.Show("La longitud de tu nombre es: " + mensaje);

            // Se terminó el servicio. 
            // Nos desconectamos
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            this.Close();
            Inicio form2 = new Inicio();
            form2.ShowDialog();

        }
    }
}

