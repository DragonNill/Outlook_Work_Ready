using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Outlook_Work.Models.Entities;
using Outlook_Work.Windows.Infomation;

namespace Outlook_Work.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ReportControl.xaml
    /// </summary>
    public partial class ReportControl : UserControl
    {
        public OutlookWorkDatabaseContext context;
        public Report report;
        public Feedback feedback;
        Users user;
        int Itys = 0;

        public ReportControl(Report reort, Users userLive)
        {
            context = OutlookWorkDatabaseContext.DbContext;
            
            user = userLive;
            report = reort;
            InitializeComponent();
            
        }

        public ReportControl(Feedback feedbacks, Users userLive,int Itys5)
        {
            context = OutlookWorkDatabaseContext.DbContext;
            this.Itys = Itys5;
            user = userLive;
            feedback = feedbacks;
            InitializeComponent();

        }

        private void CheckCurrentReport()
        {
            if (Itys == 0)
            {
                Users usery = context.Users.Where(u => u.Iduser == report.CodeHeadDepartament).FirstOrDefault();
                Order order = context.Order.Where(o => o.IdOrder == report.CodeOrder).FirstOrDefault();

                nameLabel.Content = user.UserName + " " + user.UserSurname[0] + ".";
                dateLabel.Content = order.OrderDate.ToShortDateString();
                nameOrder.Content = order.OrderName;
            }
            else
            {
                nsLabel.Content = "Сотрудник: ";
                nameLabel.Content = feedback.CodeCompanyEmployeeNavigation.UserName + " " + feedback.CodeCompanyEmployeeNavigation.UserSurname[0] + ".";
                dateLabel.Content = feedback.CodeOrderNavigation.OrderDate.ToShortDateString();
                nameOrder.Content = feedback.CodeOrderNavigation.OrderName;
                
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCurrentReport();
        }
    }
}
