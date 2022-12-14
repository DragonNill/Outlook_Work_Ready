using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Outlook_Work.Models.Entities
{
    public partial class Order
    {
        public Order()
        {
            Feedback = new HashSet<Feedback>();
            Report = new HashSet<Report>();
        }

        public int IdOrder { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderContent { get; set; }
        public int? CodeHeadDepartament { get; set; }
        public int CodeStatus { get; set; }
        public int CodeEmployeer { get; set; }

        public virtual Users CodeEmployeerNavigation { get; set; }
        public virtual Users CodeHeadDepartamentNavigation { get; set; }
        public virtual Status CodeStatusNavigation { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
