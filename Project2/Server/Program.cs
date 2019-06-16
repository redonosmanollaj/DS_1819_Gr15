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
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.Data;

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
        public static XmlElement saltedHashNode;
        public static XmlElement saltNode;



        public static DESCryptoServiceProvider objDes;
        public static RSACryptoServiceProvider objRsa;
        public static RSACryptoServiceProvider objRsaForSign;
        public static string strCelesiCipher = "";
        public static Socket serverSocket;
        public static byte[] desKey = new byte[8];
        public static byte[] desIV = new byte[8];

        public static X509Certificate2 certificate;
        static void Main(string[] args)
        {

            //createRsa();
            //createXmlDb();
            createServerSocket();
            certificate = GetCertificateFromStore("CN=Admin");


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


                Console.WriteLine("Client: (Ciphertext) {0} ", strFromClient);
                string fromClientDecrypted = dekriptoDes(strFromClient);
                Console.WriteLine("Client: (Plaintext) {0} ", fromClientDecrypted);

                string[] tokens = fromClientDecrypted.Split('%');
                Console.WriteLine(tokens.Length);

                string strFromServer = "";
                if (tokens.Length == 2)
                {
                    string username = tokens[0].Trim();
                    string password = tokens[1].Trim();

                    if (isValidLogin(username, password))
                    {
                        strFromServer = "You are logged in!";
                        DataSet dsProfesors = new DataSet();
                        Profesor profesor = getProfesor(username);
                        string jsonWebToken = generateJWT(profesor.name, profesor.surname, profesor.degree, profesor.salary, profesor.email, profesor.username);
                        string signature = signData(jsonWebToken);

                        strFromServer = signature+' '+jsonWebToken;
                        
                    }
                    else
                    {
                        strFromServer = "false";
                        
                    }

                }
                else
                {
                    string name = tokens[0];
                    string surname = tokens[1];
                    string email = tokens[2];
                    string degree = tokens[3];
                    double salary = Double.Parse(tokens[4]);
                    string username = tokens[5].Trim();
                    string password = tokens[6].Trim();
                    //string saltedHash = tokens[7];

                    try
                    {
                        addProfesor(name, surname, degree, salary, email, username, password);
                        strFromServer = "Profesor has been added successfuly!";
                        //Console.WriteLine(strFromServer);
                    }
                    catch (Exception ex)
                    {
                        strFromServer = "Profesor has not been added!! \n" + ex.Message;
                        //Console.WriteLine(strFromServer);
                    }
                }

                
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

        public static void addProfesor(string name, string surname, string degree, double salary,string email,string username,string password)
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
            usernameNode = objXml.CreateElement("username");
            saltedHashNode = objXml.CreateElement("saltedHashPassword");
            saltNode = objXml.CreateElement("salt");


            nameNode.InnerText = name;
            surnameNode.InnerText = surname;
            degreeNode.InnerText = degree;
            salaryNode.InnerText = salary+"";
            emailNode.InnerText = email;
            usernameNode.InnerText = username;
            Random random = new Random(DateTime.Now.Millisecond);
            string salt = random.Next(100000, 1000000).ToString();
            saltNode.InnerText = salt;
            saltedHashNode.InnerText = getSaltedHash(salt,password);


            profesorNode.AppendChild(nameNode);
            profesorNode.AppendChild(surnameNode);
            profesorNode.AppendChild(degreeNode);
            profesorNode.AppendChild(salaryNode);
            profesorNode.AppendChild(emailNode);
            profesorNode.AppendChild(usernameNode);
            profesorNode.AppendChild(saltNode);
            profesorNode.AppendChild(saltedHashNode);


            rootNode.AppendChild(profesorNode);

            objXml.Save("mesimdhenesi.xml");
        }

        public static SHA1CryptoServiceProvider objHash = new SHA1CryptoServiceProvider();
        private static string getSaltedHash(string salt, string password)
        {
            string saltedPassword = password+salt;
             

            byte[] byteSaltedPassword = Encoding.UTF8.GetBytes(saltedPassword);
            byte[] byteSaltedHash = objHash.ComputeHash(byteSaltedPassword);

            return Convert.ToBase64String(byteSaltedHash);
        }

        public static bool isValidLogin(string username,string password)
        {
            objXml.Load("mesimdhenesi.xml");


            /*  foreach(XmlNode node in objXml.SelectNodes("//profesor"))
              {
                  string usernameXml = node.SelectSingleNode("username").InnerText;
                  string saltedHashXml = node.SelectSingleNode("saltedHashPassword").InnerText;
                  string saltXml = node.SelectSingleNode("salt").InnerText; ;

                  string saltedPasswordLogin = password + saltXml;

                  SHA1CryptoServiceProvider objHash = new SHA1CryptoServiceProvider();

                  byte[] byteSaltedPasswordLogin = Encoding.UTF8.GetBytes(saltedPasswordLogin);
                  byte[] byteSaltedHashLogin = objHash.ComputeHash(byteSaltedPasswordLogin);
                  string saltedHashLogin = Convert.ToBase64String(byteSaltedHashLogin);


                  if(usernameXml == username && saltedHashXml == saltedHashLogin)
                  {
                      return true;
                  }

              } */

            XmlNodeList profesorElements = objXml.GetElementsByTagName("profesor");


            for (int i = 0; i < profesorElements.Count; i++)
            {
                string usernameXml = profesorElements[i].SelectSingleNode("username").InnerText;
                string saltedHashXml = profesorElements[i].SelectSingleNode("saltedHashPassword").InnerText;
                string saltXml = profesorElements[i].SelectSingleNode("salt").InnerText;

                Console.WriteLine("usernameXml: " + usernameXml);
                Console.WriteLine("Salt: " + saltXml);

                Console.WriteLine("SaltedHashXml: " + saltedHashXml);
                Console.WriteLine(usernameXml + username);

                Console.WriteLine(usernameXml + password);
                Console.WriteLine(username + saltXml);
                Console.WriteLine(password + saltXml);

                string saltedPasswordLogin = saltXml + password;
                Console.WriteLine("usernameLogin: " + username);
                Console.WriteLine("\n\nPlain Password: " + password);
                Console.WriteLine("SaltedPassword: " + saltedPasswordLogin);
                //SHA1CryptoServiceProvider objHash = new SHA1CryptoServiceProvider();

                //byte[] byteSaltedPasswordLogin = Encoding.UTF8.GetBytes(saltedPasswordLogin);
                //byte[] byteSaltedHashLogin = objHash.ComputeHash(byteSaltedPasswordLogin);
                string saltedHashLogin = getSaltedHash(saltXml, password);

                Console.WriteLine("SaltedHashFromLogin : " + saltedHashLogin);
                Console.WriteLine("====================================");

                if (usernameXml.Equals(username) || saltedHashXml.Equals(saltedHashLogin))
                {
                    return true;
                }
            }

            return false; 
        }

        private static Profesor getProfesor(string username)
        {
            objXml.Load("mesimdhenesi.xml");
            XmlNodeList profesorElements = objXml.GetElementsByTagName("profesor");

            Profesor profesor = new Profesor();

            for(int i = 0; i < profesorElements.Count; i++)
            {
                string usernameXml = profesorElements[i].SelectSingleNode("username").InnerText;
                string nameXml = profesorElements[i].SelectSingleNode("name").InnerText;
                string surnameXml = profesorElements[i].SelectSingleNode("surname").InnerText;
                string degreXml = profesorElements[i].SelectSingleNode("degree").InnerText;
                double salaryXml = Double.Parse(profesorElements[i].SelectSingleNode("salary").InnerText);
                string emailXml = profesorElements[i].SelectSingleNode("email").InnerText;

                if (username.Equals(usernameXml))
                {
                    profesor = new Profesor(nameXml, surnameXml, degreXml, salaryXml, emailXml, usernameXml);
                }
            }



            return profesor;
        }

        private static byte[] decryptKey()
        {
            byte[] byteCelesiDekriptuar = objRsa.Decrypt(desKey,true);

            return byteCelesiDekriptuar;
        }

        private static string signData(string data)
        {

            objRsaForSign = (RSACryptoServiceProvider)certificate.PrivateKey;
            

            File.WriteAllText("objRsaForSignKey.xml", objRsaForSign.ToXmlString(false));

            byte[] byteData = Encoding.UTF8.GetBytes(data);
  
            byte[] byteSignature = objRsaForSign.SignData(byteData, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(byteSignature);
        }

        private static X509Certificate2 GetCertificateFromStore(string certName)
        {

            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                // currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, true);
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, false);
                if (signingCert.Count == 0)
                    return null;
                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];
            }
            finally
            {
                store.Close();
            }

        }

        private static string generateJWT(string name,string surname,string degree,double salary,string email,string username)
        {
            var payload = new Dictionary<string, object>
                {
                    { "name", name },
                    { "surname", surname },
                    {"degree", degree},
                    {"salary",salary },
                    {"email",email },
                    {"username",username }
                };
            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            Console.WriteLine(token);

            //rtResult.Text = token;
            return token;
        }
    }
}
