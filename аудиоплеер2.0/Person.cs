using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace аудиоплеер2._0
{
    public class Person
    {
        private string name;
        private string surname;
        private string patr;
        public Person(string name, string _surname)
        {
            this.name = name;
            this.surname = surname;
        }
        public Person(string name, string _surname, string _patr)
        {
            this.name = name;
            patr = _patr;
            this.surname = surname;
        }

    }
}
