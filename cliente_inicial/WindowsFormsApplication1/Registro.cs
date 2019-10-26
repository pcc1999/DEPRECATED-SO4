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
    public partial class Registro : Form
    {
        Socket server;
        string IP;
        int puerto;
        public Registro()
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
        private void button1_Click(object sender, EventArgs e)
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
            //Preparamos el mensaje de registro en la base de datos
            string registro = "4/" + usuario.Text + "/" + contraseña.Text;
            //Lo enviamos
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(registro);
            server.Send(msg);
            //Lo recibimos
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            string respuesta = Encoding.ASCII.GetString(msg2).Split('\0')[0];
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
        }

      
    }
}
