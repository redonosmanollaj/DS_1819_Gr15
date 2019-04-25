using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 12000);
            Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socketServer.Bind(ip);
            socketServer.Listen(10);

            Console.WriteLine("Serveri eshte duke pritur kliente ...");
            
            while (true)
            {
                Socket connSocket = socketServer.Accept();
                Thread th = new Thread(() => Handle_Connection(connSocket));
                th.Start();
                

            }
        }


        private static void Handle_Connection(Socket connSocket)
        {
          
                IPEndPoint clientip = (IPEndPoint)connSocket.RemoteEndPoint;
                Console.WriteLine("Serveri u lidh me hostin {0} ne portin {1}", clientip.Address, clientip.Port);
            while (true)
            {
                byte[] byteArdhura = new byte[1024];
                int length  = connSocket.Receive(byteArdhura);
                string strKerkesa = Encoding.UTF8.GetString(byteArdhura);
                Console.WriteLine("Nga klienti ka ardhur kjo kerkese e enkriptuar: {0}", strKerkesa);

                string kerkesaDekriptuar = Dekripto(strKerkesa);
                Console.WriteLine("Kerkesa e dekriptuar: {0}", kerkesaDekriptuar);

                string strPergjigja = "Kjo eshte pergjigja e serverit";
                
                
                connSocket.Send(Encoding.UTF8.GetBytes(Enkripto(strPergjigja)));
                Console.WriteLine("Nga serveri ka shkuar kjo pergjigje e enkriptuar: {0}", Encoding.UTF8.GetBytes(Enkripto(strPergjigja)));
            }
        }

        private static string Dekripto(string strKerkesaEnkriptuar)
        {

            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;

            byte[] byteKerkesaEnkriptuar = Convert.FromBase64String(strKerkesaEnkriptuar);

            MemoryStream ms = new MemoryStream(byteKerkesaEnkriptuar);
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] byteKerkesaDekriptuar = new byte[ms.Length];
            cs.Read(byteKerkesaDekriptuar, 0, byteKerkesaDekriptuar.Length);
            cs.Close();

            string strKerkesaDekriptuar = Encoding.UTF8.GetString(byteKerkesaDekriptuar);
            return strKerkesaDekriptuar;
        }

        private static string Enkripto(string strPergjigja)
        {
            byte[] bytePergjigja = Encoding.UTF8.GetBytes(strPergjigja);

            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(bytePergjigja, 0, bytePergjigja.Length);
            cs.Close();

            byte[] bytePergjigjaEnkriptuar = ms.ToArray();
            string strPergjigjaEnkriptuar = Convert.ToBase64String(bytePergjigjaEnkriptuar);
            return strPergjigjaEnkriptuar;
        }

    }
}
