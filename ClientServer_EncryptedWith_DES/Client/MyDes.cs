using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MyDes
    {
        public DESCryptoServiceProvider objDES;


        public MyDes()
        {
            objDES = new DESCryptoServiceProvider();
            objDES.IV = Encoding.UTF8.GetBytes("12345678");
            objDES.Padding = PaddingMode.Zeros;
            objDES.Mode = CipherMode.CBC;
            objDES.Key = Encoding.UTF8.GetBytes("27651409");
        }
    }
}
