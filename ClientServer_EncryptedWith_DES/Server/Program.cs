using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.IO;

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
                IPEndPoint clientip = (IPEndPoint)connSocket.RemoteEndPoint;
                Console.WriteLine("Serveri u lidh me hostin {0} ne portin {1}", clientip.Address, clientip.Port);

                byte[] byteArdhura = new byte[1024];
                connSocket.Receive(byteArdhura);

                Console.WriteLine("Nga klienti ka ardhur kjo kerkese e enkriptuar: {0}", Encoding.UTF8.GetString(byteArdhura));

                Dekripto(ref byteArdhura);
                Console.WriteLine("Kerkesa e dekriptuar: {0}", Encoding.UTF8.GetString(byteArdhura));

                string strPergjigja = "Kjo eshte pergjigja e serverit";
                byte[] bytePergjigja = Encoding.UTF8.GetBytes(strPergjigja);
                Enkripto(ref bytePergjigja);
                connSocket.Send(bytePergjigja);
                Console.WriteLine("Nga serveri ka shkuar kjo pergjigje e enkriptuar: {0}", Encoding.UTF8.GetString(bytePergjigja));

                connSocket.Close();
            }
            
        }


        private static void Dekripto(ref byte[] byteArdhura)
        {
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;


            MemoryStream ms = new MemoryStream(byteArdhura);
            CryptoStream cs = new CryptoStream(ms, objDES.CreateDecryptor(), CryptoStreamMode.Read);
            byteArdhura = new byte[ms.Length];
            cs.Read(byteArdhura, 0, byteArdhura.Length);
            cs.Close();      

        }

        private static void Enkripto(ref byte[] bytePergjigja)
        {
            DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, objDES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(bytePergjigja, 0, bytePergjigja.Length);
            cs.Close();

            bytePergjigja = ms.ToArray();
        }

    }
}
