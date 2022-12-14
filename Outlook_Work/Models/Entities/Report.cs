using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Outlook_Work.Models.Entities
{
    public partial class Report
    {
        public int Idreport { get; set; }
        public string ReportContent { get; set; }
        public int CodeHeadDepartament { get; set; }
        public int CodeOrder { get; set; }

        public virtual Users CodeHeadDepartamentNavigation { get; set; }
        public virtual Order CodeOrderNavigation { get; set; }
    }
}
