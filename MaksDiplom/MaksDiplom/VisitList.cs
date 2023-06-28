using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaksDiplom
{
    internal class VisitList
    {
        [Key]
        public int VLID { get; set; }
        public int PacientID { get; set; }
        public int DoctorID { get; set; }
        public int Cabinet { get; set; }
        private string Date,Time, Discriptions;
        public string date { get { return Date; } set { Date = value; } }
        public string time { get { return Time; } set { Time = value; } }
        public string discriptions { get { return Discriptions; } set { Discriptions = value; } }

        public VisitList() { }
        public VisitList(int PacientID, int DoctorID, int Cabinet, string Date,string Time, string Discriptions)
        {
            this.PacientID = PacientID;
            this.DoctorID = DoctorID;
            this.Cabinet = Cabinet;
            this.Date = Date;
            this.Time = Time;
            this.Discriptions = Discriptions;
        }
    }
}
