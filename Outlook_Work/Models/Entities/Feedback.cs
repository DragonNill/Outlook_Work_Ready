using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Outlook_Work.Models.Entities
{
    public partial class Feedback
    {
        public int Idfeedback { get; set; }
        public string FeedbackContent { get; set; }
        public int CodeCompanyEmployee { get; set; }
        public int CodeOrder { get; set; }

        public virtual Users CodeCompanyEmployeeNavigation { get; set; }
        public virtual Order CodeOrderNavigation { get; set; }
    }
}
