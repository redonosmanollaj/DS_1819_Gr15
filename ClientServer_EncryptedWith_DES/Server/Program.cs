

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
using System.IO.Ports;
using System.Management;
using System.Windows.Forms;
using System.ComponentModel;




namespace Server
{
    class Program
    {
        public static MyDes objMyDes;

        static void Main(string[] args)
        {
            objMyDes = new MyDes();

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
                string strKerkesa = Encoding.UTF8.GetString(byteArdhura,0,length);
                Console.WriteLine("Nga klienti ka ardhur kjo kerkese e enkriptuar: {0}", strKerkesa);

                string kerkesaDekriptuar = Dekripto(strKerkesa);
                Console.WriteLine("Kerkesa e dekriptuar: {0}", kerkesaDekriptuar);

                string strPergjigja = "";
                if (kerkesaDekriptuar.Contains("EMRIIHOSTIT"))
                    strPergjigja = EMRIIHOSTIT();
                else if (kerkesaDekriptuar.Contains("IPADRESA"))
                    strPergjigja = IPADRESA();
                else if (kerkesaDekriptuar.Contains("NUMRIIPORTIT"))
                    strPergjigja = NUMRIIPORTIT();
                else if (kerkesaDekriptuar.Contains("KOHA"))
                    strPergjigja = KOHA();
                else if (kerkesaDekriptuar.Contains("FIBONACCI"))
                    strPergjigja = FIBONACCI();
                else if (kerkesaDekriptuar.Contains("KONVERTIMI"))
                    strPergjigja = KONVERTIMI();
                else
                    strPergjigja = "Kerkesa eshte jo valide!";

                Console.WriteLine("Pergjigja e serverit: {0}", strPergjigja);
                
                connSocket.Send(Encoding.UTF8.GetBytes(Enkripto(strPergjigja)));
                Console.WriteLine("Nga serveri ka shkuar kjo pergjigje e enkriptuar: {0}", Enkripto(strPergjigja));
                Console.WriteLine("================================================================================");
            }
        }

        private static string Dekripto(string strKerkesaEnkriptuar)
        {
            byte[] byteKerkesaEnkriptuar = Convert.FromBase64String(strKerkesaEnkriptuar);

            MemoryStream ms = new MemoryStream(byteKerkesaEnkriptuar);
            CryptoStream cs = new CryptoStream(ms, objMyDes.objDES.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] byteKerkesaDekriptuar = new byte[ms.Length];
            cs.Read(byteKerkesaDekriptuar, 0, byteKerkesaDekriptuar.Length);
            cs.Close();

            string strKerkesaDekriptuar = Encoding.UTF8.GetString(byteKerkesaDekriptuar);
            return strKerkesaDekriptuar;
        }

        private static string Enkripto(string strPergjigja)
        {
            byte[] bytePergjigja = Encoding.UTF8.GetBytes(strPergjigja);

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, objMyDes.objDES.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(bytePergjigja, 0, bytePergjigja.Length);
            cs.Close();

            byte[] bytePergjigjaEnkriptuar = ms.ToArray();
            string strPergjigjaEnkriptuar = Convert.ToBase64String(bytePergjigjaEnkriptuar);
            return strPergjigjaEnkriptuar;
        }


        private static string EMRIIHOSTIT()
        {
            string hostName = Dns.GetHostName();
            hostName.ToString();
            return hostName;
        }
        private static string IPADRESA() //emri i hostit
        {
            string hostName = Dns.GetHostName();

            string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return IP;
        }           
        private static string NUMRIIPORTIT()
        {
            return "";
        }

        

        private static string KOHA()
        {

            return DateTime.Now.ToString("h:mm:ss tt");
        
        }

        private static string FIBONACCI()
        {
            return "";
        }

        private static string KONVERTIMI()
        {
            return "";
        }

        

    }
}
