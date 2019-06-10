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

namespace Server
{
    class Program
    {
        public static DESCryptoServiceProvider objDes;
        public static RSACryptoServiceProvider objRsa;
        public static string strCelesiCipher = "";
        public static Socket serverSocket;

        static void Main(string[] args)
        {

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

            byte[] byteCelesiCipher = new byte[128];
            int celesiLength = connSocket.Receive(byteCelesiCipher);
            strCelesiCipher = Encoding.UTF8.GetString(byteCelesiCipher, 0, celesiLength);

            createRsa();
            createDes();

            while (true)
            {
                byte[] byteFromClient = new byte[1024];
                int length = connSocket.Receive(byteFromClient);
                string strFromClient = Encoding.UTF8.GetString(byteFromClient, 0, length);

                Console.WriteLine("Client: (Ciphertext) {0} ", strFromClient);
                string fromClientDecrypted = dekriptoDes(strFromClient);
                Console.WriteLine("Client: (Plaintext) {0} ", fromClientDecrypted);

                string strFromServer="";
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
            objDes.GenerateIV();
            objDes.Padding = PaddingMode.Zeros;
            objDes.Mode = CipherMode.CBC;
            objDes.Key = Encoding.UTF8.GetBytes(dekriptoRsa());
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
            string xmlRsaParametrat = objRsa.ToXmlString(false);

            StreamWriter sw = new StreamWriter("ServerPublicKey.xml");
            sw.Write(xmlRsaParametrat);
            sw.Close();
            // Duhet me ja jep celsin publ
        }

        private static string dekriptoRsa()
        {
            byte[] byteCelesiCipher = Convert.FromBase64String(strCelesiCipher);
            byte[] byteCelesiDekriptuar = objRsa.Decrypt(byteCelesiCipher,true);

            return Encoding.UTF8.GetString(byteCelesiDekriptuar);
        }
    }
}
