using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Outlook_Work.Models.Entities
{
    public partial class Users
    {
        public Users()
        {
            Feedback = new HashSet<Feedback>();
            OrderCodeEmployeerNavigation = new HashSet<Order>();
            OrderCodeHeadDepartamentNavigation = new HashSet<Order>();
            Report = new HashSet<Report>();
        }

        public int Iduser { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronomyc { get; set; }
        public string UserTelephone { get; set; }
        public string UserEmail { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public int UserCodeRole { get; set; }

        public virtual Roles UserCodeRoleNavigation { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Order> OrderCodeEmployeerNavigation { get; set; }
        public virtual ICollection<Order> OrderCodeHeadDepartamentNavigation { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
