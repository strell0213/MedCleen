using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MaksDiplom
{
    class RegistryPerson
    {
        [Key]
        public int RPID { get; set; }
        private string Name, Surname, Patronomic, Login, Password;
        public string name { get { return Name; } set { Name = value; } }
        public string surname { get { return Surname; } set { Surname = value; } }
        public string patronomic { get { return Patronomic; } set { Patronomic = value; } }
        public string login { get { return Login; } set { Login = value; } }
        public string password { get { return Password; } set { Password = value; } }

        public RegistryPerson() { }
        public RegistryPerson(string Name, string Surname, string Patronomic, string Login, string Password)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Patronomic = Patronomic;
            this.Login = Login;
            this.Password = Password;
        }
    }
}
