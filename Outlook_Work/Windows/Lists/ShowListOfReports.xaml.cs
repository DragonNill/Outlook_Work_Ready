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
using Outlook_Work.UserControls;
using Outlook_Work.Windows.Infomation;
using Outlook_Work.Windows.Menus;

namespace Outlook_Work.Windows.Lists
{
    /// <summary>
    /// Логика взаимодействия для ShowListOfReports.xaml
    /// </summary>
    public partial class ShowListOfReports : Window
    {
        public static OutlookWorkDatabaseContext context;
        Users user;
        int whichIsRole;
        int Itys = 0;

        public ShowListOfReports(Users liveUser)
        {
            user = liveUser;
            whichIsRole = user.UserCodeRole;
            context = OutlookWorkDatabaseContext.DbContext;

            InitializeComponent();
        }

        public ShowListOfReports(Users liveUser, int Itys)
        {
            this.Itys = Itys;
            user = liveUser;
            whichIsRole = user.UserCodeRole;
            context = OutlookWorkDatabaseContext.DbContext;

            InitializeComponent();
        }


        private void FullList()
        {
            if (Itys == 0)
            {
                reportListView.Items.Clear();
                List<Report> reports = new List<Report>();
                reports = context.Report.ToList();
                switch(sortingComboBox.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        reports = reports.OrderBy(r => r.CodeOrderNavigation.OrderDate).ToList();
                        break;
                    case 2:
                        reports = reports.OrderByDescending(r => r.CodeOrderNavigation.OrderDate).ToList();
                        break;
                }
                foreach (Report obj in reports)
                {
                    reportListView.Items.Add(new ReportControl(obj, user));
                }
            }
            else
            {
                reportListView.Items.Clear();
                List<Feedback> feedbacks = new List<Feedback>();
                feedbacks = context.Feedback.ToList();
                switch (sortingComboBox.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        feedbacks = feedbacks.OrderBy(r => r.CodeOrderNavigation.OrderDate).ToList();
                        break;
                    case 2:
                        feedbacks = feedbacks.OrderByDescending(r => r.CodeOrderNavigation.OrderDate).ToList();
                        break;
                }
                foreach (Feedback obj in feedbacks)
                {
                    reportListView.Items.Add(new ReportControl(obj, user,1));
                }
            }
        }


        private void editorinfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Itys == 0)
            {
                if (reportListView.SelectedItem != null)
                {
                    Report report = ((ReportControl)reportListView.SelectedItem).report;
                    InfoAboutReport.creatingForm = this;
                    new InfoAboutReport(report, user).ShowDialog();
                    FullList();
                }
                else
                {
                    MessageBox.Show("Пожалуйста выберите отчет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (reportListView.SelectedItem != null)
                {
                    Feedback feedback = ((ReportControl)reportListView.SelectedItem).feedback;
                    InfoAboutReport.creatingForm = this;
                    new InfoAboutReport(feedback, user,1).ShowDialog();
                    FullList();
                }
                else
                {
                    MessageBox.Show("Пожалуйста выберите отчет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void reportListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Itys == 0)
            {
                Report report = ((ReportControl)reportListView.SelectedItem).report;
                InfoAboutReport.creatingForm = this;
                new InfoAboutReport(report, user).ShowDialog();
                FullList();
            }
            else
            {
                Feedback feedback = ((ReportControl)reportListView.SelectedItem).feedback;
                InfoAboutReport.creatingForm = this;
                new InfoAboutReport(feedback, user,1).ShowDialog();
                FullList();
            }
        }
        private void CheckCurrentUser()
        {
            if (Itys == 0)
            {
                if (user.UserCodeRole == 1)
                {
                    deleteButton.IsEnabled = true;
                    deleteButton.Visibility = Visibility.Visible;
                    deleteButton.Content = "Удалить";
                }
            }
            else
            {
                if (user.UserCodeRole == 1)
                {
                    deleteButton.IsEnabled = true;
                    deleteButton.Visibility = Visibility.Visible;
                    deleteButton.Content = "Удалить";
                    editorinfoButton.Content = "Информация отзыва";
                    Title = "Список отзывов";
                }
            }
        }

        private void FullComboBox()
        {
            sortingComboBox.Items.Add("Без сортировки");
            sortingComboBox.Items.Add("По дате ↑");
            sortingComboBox.Items.Add("По дате ↓");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FullList();
            FullComboBox();
            CheckCurrentUser();
        }

        private void backButton_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Itys == 0)
            {
                if (reportListView.SelectedItem != null)
                {
                    Report reporT = ((ReportControl)reportListView.SelectedItem).report;
                    context.Report.Remove(reporT);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Пожалуйста выберите отчет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (reportListView.SelectedItem != null)
                {
                    Feedback feedback = ((ReportControl)reportListView.SelectedItem).feedback;
                    context.Feedback.Remove(feedback);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Пожалуйста выберите отчет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            FullList();
        }

        private void sortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FullList();
        }
    }
}
