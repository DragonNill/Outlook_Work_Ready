using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Outlook_Work.Models.Entities
{
    public partial class Orderreportaccounting
    {
        public int IdAccounting { get; set; }
        public int IdReport { get; set; }
        public int IdOrder { get; set; }

        public virtual Accounting IdAccountingNavigation { get; set; }
        public virtual Order IdOrderNavigation { get; set; }
        public virtual Report IdReportNavigation { get; set; }
    }
}
