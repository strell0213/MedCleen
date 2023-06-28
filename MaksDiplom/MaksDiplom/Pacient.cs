using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaksDiplom
{
    internal class Pacient
    {
        [Key]
        public int PacientID { get; set; }

        private string Name, Surname, Patronomic, PolicyNumber, PhoneNumber;
        public string name { get { return Name; } set { Name = value; } }
        public string surname { get { return Surname; } set { Surname = value; } }
        public string patronomic { get { return Patronomic; } set { Patronomic = value; } }
        public string policyNumber { get { return PolicyNumber; } set { PolicyNumber = value; } }
        public string phoneNumber { get { return PhoneNumber; } set { PhoneNumber = value; } }

        public Pacient() { }

        public Pacient(string Name, string Surname, string Patronomic, string PolicyNumber, string PhoneNumber)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Patronomic = Patronomic;
            this.PolicyNumber = PolicyNumber;
            this.PhoneNumber = PhoneNumber;
        }
    }
}
