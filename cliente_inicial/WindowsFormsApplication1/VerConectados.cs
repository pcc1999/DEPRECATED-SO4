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
    public partial class VerConectados : Form
    {
        
//PARÁMETROS DEL FORM, INICIALIZACIÓN Y MÉTODOS SET
        
        Socket server;
        int puerto;
        string IP;

        public VerConectados()
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

//FUNCIONES DE INTERACCIÓN CON EL FORM

        private void Form1_Load(object sender, EventArgs e)
        {
            //Preparamos el IPEndPoint y nos conectamos al socket
            IPEndPoint ipep = PrepararIPEndPoint();

            //Intentamos conectarnos al servidor
            Conectar(ipep);

            List<string> ListaUsuarios = ObtenerLista();
            tablaUsuarios.RowCount = ListaUsuarios.Count + 1 ;
            tablaUsuarios.ColumnCount = 1;
            tablaUsuarios[0,0].Value = "Usuarios";
            for (int i = 0; i < ListaUsuarios.Count; i++)
            {
                tablaUsuarios[0, i + 1].Value = ListaUsuarios[i];
            }
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
                this.BackColor = Color.WhiteSmoke;
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
            // Enviamos al servidor el mensaje
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            return Encoding.ASCII.GetString(msg2).Split('\0')[0];
        }

//CONSULTAS

        public List<string> ObtenerLista()
        {
            List<string> ListaUsuarios = new List<string>();
            string mensaje = "6/";

            //Enviamos nuestra consulta y recibimos del servidor la respuesta
            string respuesta = EnviarYRecibir(mensaje);
            
            //Adaptamos la respuesta a nuestro formato de datos (Lista)
            string[] prov = respuesta.Split('_');
            int i = 0;
            while (prov[i] != "0")
            {
                ListaUsuarios.Add(prov[i]);
                i++;
            }
            return ListaUsuarios;
        }

    }
}
