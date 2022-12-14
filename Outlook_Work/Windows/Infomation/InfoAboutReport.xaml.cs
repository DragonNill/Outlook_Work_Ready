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
using System.Windows.Shapes;
using Outlook_Work.Models.Entities;
using Outlook_Work.Windows.Create;

namespace Outlook_Work.Windows.Infomation
{
    /// <summary>
    /// Логика взаимодействия для InfoAboutReport.xaml
    /// </summary>
    public partial class InfoAboutReport : Window
    {
        public Report report { get; set; }
        public Feedback feedback { get; set; }

        Users selectUser;

        int Itys = 0;
        OutlookWorkDatabaseContext context;
        public static Window creatingForm { get; set; }  

        public InfoAboutReport(Report reportT,Users user)
        {
            context = OutlookWorkDatabaseContext.DbContext;
            report = reportT;
            selectUser = user;
            InitializeComponent();
        }

        public InfoAboutReport(Feedback feedbackT, Users user,int ItysR)
        {
            context = OutlookWorkDatabaseContext.DbContext;
            Itys = ItysR;
            feedback = feedbackT;
            selectUser = user;
            InitializeComponent();
        }

        private void CheckCurrentReport()
        {
            if (Itys == 0)
            {
                Users user = context.Users.Where(u => u.Iduser == report.CodeHeadDepartament).FirstOrDefault();

                headDepartLabel.Content = user.UserName + " " + user.UserSurname[0] + ".";

                contentRichTextBox.AppendText(report.ReportContent);
                if (selectUser.UserCodeRole == 1)
                {
                    deleteButton.Visibility = Visibility.Visible;
                    deleteButton.IsEnabled = true;
                    contentHeadLabel.Content = "Информация отзыва";
                }
            }
            else
            {
                headDepartLabel.Content = feedback.CodeCompanyEmployeeNavigation.UserName + " " + feedback.CodeCompanyEmployeeNavigation.UserSurname[0] + ".";
                contentRichTextBox.AppendText(feedback.FeedbackContent);
                if (selectUser.UserCodeRole == 1)
                {
                    deleteButton.Visibility = Visibility.Visible;
                    deleteButton.IsEnabled = true;
                    contentHeadLabel.Content = "Информация отзыва";
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Itys == 0)
            {
                context.Report.Remove(report);
                context.SaveChanges();
                Close();
            }
            else
            {
                context.Feedback.Remove(feedback);
                context.SaveChanges();
                Close();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCurrentReport();
        }
    }
}
