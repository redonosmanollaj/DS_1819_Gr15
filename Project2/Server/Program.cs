using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;

namespace Server
{
    class Program
    {
        public static XmlDocument objXml = new XmlDocument();


        public static XmlElement rootNode;
                                              
        public static XmlElement profesorNode;
        public static XmlElement nameNode;
        public static XmlElement surnameNode;
        public static XmlElement degreeNode;
        public static XmlElement salaryNode;
        public static XmlElement emailNode;
        public static XmlElement usernameNode;
        public static XmlElement passwordNode;



        public static DESCryptoServiceProvider objDes;
        public static RSACryptoServiceProvider objRsa;
        public static string strCelesiCipher = "";
        public static Socket serverSocket;
        public static byte[] desKey = new byte[8];
        public static byte[] desIV = new byte[8];
        static void Main(string[] args)
        {

            //createRsa();
            createXmlDb();
            createServerSocket();



            while (true)
            {
                Socket connSocket = serverSocket.Accept();
                Thread th = new Thread(() => handleConnection(ref connSocket));
                th.Start();
            }

        }

        private static void createServerSocket()
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 12000);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(ip);
            serverSocket.Listen(10);
            Console.WriteLine("Server is waiting for connections ...");
        }

        private static void handleConnection(ref Socket connSocket)
        {
            IPEndPoint clientIp = (IPEndPoint)connSocket.RemoteEndPoint;
            Console.WriteLine("Serveri u lidh me hostin {0} ne portin {1}", clientIp.Address, clientIp.Port);


            
            connSocket.Receive(desKey);
            connSocket.Receive(desIV);
            createDes();




            while (true)
            {
                byte[] byteFromClient = new byte[1024];
                int length = connSocket.Receive(byteFromClient);
                string strFromClient = Encoding.UTF8.GetString(byteFromClient, 0, length);

                Console.WriteLine(Encoding.UTF8.GetString(desKey));
         
                Console.WriteLine(Encoding.UTF8.GetString(desIV));

                Console.WriteLine("Client: (Ciphertext) {0} ", strFromClient);
                string fromClientDecrypted = dekriptoDes(strFromClient);
                Console.WriteLine("Client: (Plaintext) {0} ", fromClientDecrypted);

                string[] tokens = fromClientDecrypted.Split('%');
                string username = tokens[0];
                string password = tokens[1];

                string strFromServer="Username: "+username+" \n"+"Password: "+password;
                //
                // Serveri kthen diqka ...
                //


                Console.WriteLine("Server: (Plaintext) {0} ", strFromServer);
                string strFromServerEncrypted = enkriptoDes(strFromServer);
                connSocket.Send(Encoding.UTF8.GetBytes(strFromServerEncrypted));
                Console.WriteLine("Server: (Ciphertext) {0} ", strFromServerEncrypted);
            }
        }

        private static void createDes()
        {
            objDes = new DESCryptoServiceProvider();
            objDes.IV = desIV;
            objDes.Padding = PaddingMode.Zeros;
            objDes.Mode = CipherMode.CBC;
            objDes.Key = desKey;
        }

        private static string enkriptoDes(string strFromServer)
        {
            byte[] byteFromServer = Encoding.UTF8.GetBytes(strFromServer);

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, objDes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(byteFromServer, 0, byteFromServer.Length);
            cs.Close();

            byte[] byteFromServerEncrypted = ms.ToArray();
            string strFromServerEncrypted = Convert.ToBase64String(byteFromServerEncrypted);
            return strFromServerEncrypted;
        }

        private static string dekriptoDes(string strFromClient)
        {
            byte[] byteFromClient = Convert.FromBase64String(strFromClient);

            MemoryStream ms = new MemoryStream(byteFromClient);
            CryptoStream cs = new CryptoStream(ms, objDes.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] byteFromClientDecrypted = new byte[ms.Length];
            cs.Read(byteFromClientDecrypted, 0, byteFromClientDecrypted.Length);
            cs.Close();
            ms.Close();

            string strFromClientDecrypted = Encoding.UTF8.GetString(byteFromClientDecrypted);
            return strFromClientDecrypted;

        }

        private static void createRsa()
        {
            objRsa = new RSACryptoServiceProvider();

            File.WriteAllText("public-key.xml", objRsa.ToXmlString(false));
            File.WriteAllText("private-key.xml", objRsa.ToXmlString(true));

            // Duhet me ja jep celsin publ
        }

        public static void createXmlDb()
        {
            if (File.Exists("mesimdhenesi.xml") == false)
            {
                XmlTextWriter xmlTw = new XmlTextWriter("mesimdhenesi.xml", Encoding.UTF8);
                xmlTw.WriteStartElement("mesimdhenesi");
                xmlTw.Close();

            }

            objXml.Load("mesimdhenesi.xml");

            rootNode = objXml.DocumentElement;

            profesorNode = objXml.CreateElement("profesor");
            nameNode = objXml.CreateElement("name");
            surnameNode = objXml.CreateElement("surname");
            degreeNode = objXml.CreateElement("degree");
            salaryNode = objXml.CreateElement("salary");
            emailNode = objXml.CreateElement("email");
            usernameNode = objXml.CreateElement("valuta");
            passwordNode = objXml.CreateElement("sasia");

            /*  nameNode.InnerText = txtCmimi.Text;
            valutaNode.InnerText = txtValuta.Text;

            sasiaNode.InnerText = txtSasia.Text;

            emriNode.InnerText = txtEmri.Text;

            cmimiNode.AppendChild(vleraNode);
            cmimiNode.AppendChild(valutaNode);  */

            profesorNode.AppendChild(nameNode);
            profesorNode.AppendChild(surnameNode);
            profesorNode.AppendChild(degreeNode);
            profesorNode.AppendChild(salaryNode);
            profesorNode.AppendChild(emailNode);
            profesorNode.AppendChild(usernameNode);
            profesorNode.AppendChild(passwordNode);


            rootNode.AppendChild(profesorNode);

            objXml.Save("mesimdhenesi.xml");
        }

        private static byte[] decryptKey()
        {
            byte[] byteCelesiDekriptuar = objRsa.Decrypt(desKey,true);

            return byteCelesiDekriptuar;
        }
    }
}
