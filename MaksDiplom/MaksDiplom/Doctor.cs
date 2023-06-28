using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaksDiplom
{
    internal class Doctor
    {
        [Key]
        public int DoctorID { get; set; }
        private string Name, Surname, Patronomic, JobTitle, Login, Password;
        public string name { get { return Name; } set { Name = value; } }
        public string surname { get { return Surname; } set { Surname = value; } }
        public string patronomic { get { return Patronomic; } set { Patronomic = value; } }
        public string jobTitle { get { return JobTitle; } set { JobTitle = value; } }
        public string login { get { return Login; } set { Login = value; } }
        public string password { get { return Password; } set { Password = value; } }

        public Doctor() { }
        public Doctor(string Name, string Surname, string Patronomic, string JobTitle, string Login, string Password)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Patronomic = Patronomic;
            this.JobTitle = JobTitle;
            this.Login = Login;
            this.Password = Password;
        }
    }
}
