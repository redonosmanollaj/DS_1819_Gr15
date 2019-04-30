

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
                    strPergjigja = NUMRIIPORTIT(ref connSocket);
                else if (kerkesaDekriptuar.Contains("KOHA"))
                    strPergjigja = KOHA();
                else if (kerkesaDekriptuar.Contains("FIBONACCI"))
                    strPergjigja = FIBONACCI(ref connSocket);
                else if (kerkesaDekriptuar.Contains("KONVERTIMI"))
                    strPergjigja = KONVERTIMI(ref connSocket);
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
            return "Emri i hostit me te cilin jeni lidhur eshte "+hostName;
        }
        private static string IPADRESA() //emri i hostit
        {
            string hostName = Dns.GetHostName();

            string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return "IP Adresa juaj eshte "+IP;
        }           
        private static string NUMRIIPORTIT(ref Socket socketi)
        {
            IPEndPoint clientip = (IPEndPoint)socketi.RemoteEndPoint;
            int porti = clientip.Port;
            return "Numri i portit me te cilin ju ka lidhur serveri eshte "+porti;
        }

        

        private static string KOHA()
        {

            return "Koha aktuale sipas sistemit te serverit eshte "+DateTime.Now.ToString("h:mm:ss tt");
        
        }

        private static string FIBONACCI(ref Socket socketi)
        {
            string strJepeNumrin = "Keni kerkuar kerkesen FIBONACCI. \nShkruani nje numer ...";
            socketi.Send(Encoding.UTF8.GetBytes(Enkripto(strJepeNumrin)));

            

            byte[] byteArdhura = new byte[1024];
            int length = socketi.Receive(byteArdhura);
            string strNumri= Encoding.UTF8.GetString(byteArdhura, 0, length);
            Console.WriteLine("Nga klienti ka ardhur ky numer i enkriptuar: {0}", strNumri);

            string strNumriDekriptuar = Dekripto(strNumri);


            int numri = int.Parse(strNumriDekriptuar);
            int fibonacci;

            int a=0;
            int b=1;
            int c;
            if (numri == 0 || numri == 1)
                fibonacci = numri;
            else
            {
                for (int i = 2; i <= numri; i++)
                {
                    c = a + b;
                    a = b;
                    b = c;
                }
                fibonacci = b;
            }
               


            return "Fibonacci i numrit "+numri+" eshte "+fibonacci;
        }

        private static string KONVERTIMI(ref Socket socketi)
        {
            string strJepeModin = "Keni kerkuar kerkesen KONVERTIMI. \n Zgjedhni njeren nga modet e meposhtme: \n GallonsToLiters \n LitersToGallons \n " +
                "DegreesToRadians \n RadiansToDegrees \n KilowattToHorsepower \n HorsepowerToKilowatt";
            socketi.Send(Encoding.UTF8.GetBytes(Enkripto(strJepeModin)));


            byte[] byteArdhura = new byte[1024];
            int length = socketi.Receive(byteArdhura);
            string modiEnkriptuar = Encoding.UTF8.GetString(byteArdhura, 0, length);
            Console.WriteLine("Nga klienti ka ardhur ky tekst i enkriptuar: {0}", modiEnkriptuar);
            string modi = Dekripto(modiEnkriptuar);

            // --------------------------------------------------------------

            string strJepeNumrin = "Shkruani numrin ...";
            socketi.Send(Encoding.UTF8.GetBytes(Enkripto(strJepeNumrin)));

            byte[] byteArdhura1 = new byte[1024];
            int length1 = socketi.Receive(byteArdhura1);
            string strNumriEnkriptuar = Encoding.UTF8.GetString(byteArdhura1, 0, length1);
            string strNumri = Dekripto(strNumriEnkriptuar);

            double numri = double.Parse(strNumri);
            Console.WriteLine("Nga klienti ka ardhur ky tekst i enkriptuar: {0}", strNumriEnkriptuar);

            double rezultati = 0;
            modi = modi.ToLower();
            if (modi.Contains("gallonstoliters"))
                rezultati = numri * 3.785;
            else if (modi.Contains("literstogallons"))
                rezultati = numri / 3.785;
            else if (modi.Contains("degreestoradians"))
                rezultati = numri * 3.14 / 180;
            else if (modi.Contains("radianstodegrees"))
                rezultati = numri * 180 / 3.14;
            else if (modi.Contains("kilowatttohorsepower"))
                rezultati = numri / 0.7457;
            else if (modi.Contains("horsepowertokilowatt"))
                rezultati = numri * 0.7457;
            else
                return "Modi nuk eshte valid!";


            string njesia1 = modi.Substring(0, modi.IndexOf("to"));
            string njesia2 = modi.Substring(modi.LastIndexOf("to")+2, modi.Length-njesia1.Length-2);

            string strReturn = numri + " " + njesia1 + " = " + rezultati.ToString("##.##") + " " + njesia2;

            return strReturn;
        }

        

    }
}
