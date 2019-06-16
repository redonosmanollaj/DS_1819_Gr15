using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Profesor
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string degree { get; set; }
        public double salary { get; set; }
        public string email { get; set; }
        public string username { get; set; }

        public Profesor(string name,string surname,string degree,double salary,string email,string username)
        {
            this.name = name;
            this.surname = surname;
            this.degree = degree;
            this.salary = salary;
            this.email = email;
            this.username = username;
        }

        public Profesor()
        {

        }

    }
}
