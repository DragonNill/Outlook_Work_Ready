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
using Outlook_Work.Windows.Create;
using Outlook_Work.Windows.CreateApplication;
using Outlook_Work.Windows.InfoApplication;
using Outlook_Work.Windows.Menus;

namespace Outlook_Work.Windows.Lists
{
    /// <summary>
    /// Логика взаимодействия для ShowListOfApplications.xaml
    /// </summary>
    public partial class ShowListOfApplications : Window
    {
        public OutlookWorkDatabaseContext context;
        public Users user;
        public int whichIsRole;

        public ShowListOfApplications(int id, Users onUser)
        {
            context = OutlookWorkDatabaseContext.DbContext;
            user = onUser;
            whichIsRole = id;
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FullList()
        {
            applicationListView.Items.Clear();
            List<Order> ordersList = new List<Order>();
            ordersList = context.Order.ToList();
            if (whichIsRole == 2 || whichIsRole == 4)
            {
                if (whichIsRole == 2)
                {
                    ordersList = ordersList.Where(u => u.CodeEmployeer == user.Iduser).ToList();
                }
                else
                {
                    ordersList = ordersList.Where(u => u.CodeHeadDepartament == user.Iduser).ToList();
                }
            }
            if(!string.IsNullOrWhiteSpace(searchTextBox.Text.ToLower()))
            {
                ordersList = ordersList.Where(x => x.OrderName.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            }

            switch(filterComboBox.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    ordersList = ordersList.Where(u => u.CodeStatus == 1).ToList();
                    break;
                case 2:
                    ordersList = ordersList.Where(u => u.CodeStatus == 2).ToList();
                    break;
                case 3:
                    ordersList = ordersList.Where(u => u.CodeStatus == 3).ToList();
                    break;
            }
            switch(sortingComboBox.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    ordersList = ordersList.OrderBy(u => u.OrderDate).ToList();
                    break;
                case 2:
                    ordersList = ordersList.OrderByDescending(u => u.OrderDate).ToList();
                    break;
            }
            foreach(Order order in ordersList)
            {
                applicationListView.Items.Add(new ApplicationControl(order,user.UserCodeRole));
            }
        }

        private void FullComboBoxes()
        {
            filterComboBox.Items.Add("Все");
            filterComboBox.Items.Add("Готово");
            filterComboBox.Items.Add("Отказано");
            filterComboBox.Items.Add("В процессе");

            sortingComboBox.Items.Add("Без сортировки");
            sortingComboBox.Items.Add("По дате ↑");
            sortingComboBox.Items.Add("По дате ↓");
        }

        private void CheckCurrentUser()
        {
            if (whichIsRole == 2)
            {
                addButton.Visibility = Visibility.Visible;
                addButton.IsEnabled = true;
                deleteButton.Visibility = Visibility.Visible;
                deleteButton.IsEnabled = true;
            }
            else if(whichIsRole == 4)
            {
                addButton.Visibility = Visibility;
                addButton.IsEnabled = true;
                addButton.Content = "Написать отчет";
            }
            else if( whichIsRole == 1)
            {
                deleteButton.Visibility = Visibility.Visible;
                deleteButton.IsEnabled = true;
            }
           

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCurrentUser();
            FullComboBoxes();
            FullList();
        }

        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FullList();
        }

        private void sortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FullList();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            FullList();
        }

        private void applicationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (whichIsRole == 2)
            {
                Order order = ((ApplicationControl)applicationListView.SelectedItem).order;
                InfoAboutApplication.creatingForm = this;
                new InfoAboutApplication(order, user.UserCodeRole, user).ShowDialog();
            }
            else
            {
                Order order = ((ApplicationControl)applicationListView.SelectedItem).order;
                InfoAboutApplication.creatingForm = this;
                new InfoAboutApplication(order, user.UserCodeRole).ShowDialog();
            }
            FullList();
        }

        private void editorinfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (applicationListView.SelectedItem != null)
            {
                if (whichIsRole == 2)
                {
                    Order order = ((ApplicationControl)applicationListView.SelectedItem).order;
                    InfoAboutApplication.creatingForm = this;
                    new InfoAboutApplication(order, user.UserCodeRole,user).ShowDialog();
                }
                else
                {
                    Order order = ((ApplicationControl)applicationListView.SelectedItem).order;
                    InfoAboutApplication.creatingForm = this;
                    new InfoAboutApplication(order, user.UserCodeRole).ShowDialog();
                }
                FullList();
            }
            else
            {
                MessageBox.Show("Выберите пожалуйста заявку");
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (whichIsRole == 2)
            {
                Window nextWindow = new CreateApplicationWindow(user);
                nextWindow.ShowDialog();
            }
            else
            {
                if (applicationListView.SelectedItem != null)
                {
                    Order order = ((ApplicationControl)applicationListView.SelectedItem).order;
                    if (order.CodeStatus == 1 || order.CodeStatus == 2)
                    {
                        MessageBox.Show("На эти заявки нельзя написать отчет", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        if (whichIsRole == 4)
                        {
                            CreateReportWindow.currentWindow = this;
                            new CreateReportWindow(order, user).ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите пожалуйста заявку", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            FullList();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (applicationListView.SelectedItem != null)
            {
                Order order = ((ApplicationControl)applicationListView.SelectedItem).order;
                if (whichIsRole == 1)
                {
                    context.Order.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    if (order.CodeStatus == 1)
                    {
                        MessageBox.Show("Вы не можете удалить заявку,\nтак как на неё написан отчет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        context.Order.Remove(order);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пожалуйста заявку", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            FullList();
        }
    }
}
