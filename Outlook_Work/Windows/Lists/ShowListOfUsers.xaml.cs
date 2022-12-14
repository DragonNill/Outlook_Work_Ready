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

namespace Outlook_Work.Windows.Lists
{
    /// <summary>
    /// Логика взаимодействия для ShowListOfUsers.xaml
    /// </summary>
    public partial class ShowListOfUsers : Window
    {
        OutlookWorkDatabaseContext context;


        public ShowListOfUsers()
        {
            context = OutlookWorkDatabaseContext.DbContext;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FullList();
            FullComboBox();
        }

        private void FullList()
        {
            usersListView.Items.Clear();

            List<Users> usersList = context.Users.ToList();

            if (!string.IsNullOrWhiteSpace(searchTextBox.Text.ToLower()))
            {
                usersList = usersList.Where(x => x.UserName.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            }

            switch (filterComboBox.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    usersList = usersList.Where(u => u.UserCodeRole == 1).ToList();
                    break;
                case 2:
                    usersList = usersList.Where(u => u.UserCodeRole == 2).ToList();
                    break;
                case 3:
                    usersList = usersList.Where(u => u.UserCodeRole == 3).ToList();
                    break;
                case 4:
                    usersList = usersList.Where(u => u.UserCodeRole == 4).ToList();
                    break;
            }
            switch (sortingComboBox.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    usersList = usersList.OrderBy(u => u.UserName).ToList();
                    break;
                case 2:
                    usersList = usersList.OrderByDescending(u => u.UserName).ToList();
                    break;
            }
            foreach (Users user in usersList)
            {
                usersListView.Items.Add(new UserControler(user));
            }
        }

        private void FullComboBox()
        {
            filterComboBox.Items.Add("Всё");
            List<Roles> rolesList = context.Roles.ToList();
            foreach(Roles role in rolesList)
            {
                filterComboBox.Items.Add(role.RoleName);
            }
            sortingComboBox.Items.Add("Без сортировки");
            sortingComboBox.Items.Add("По алфавиту ↑");
            sortingComboBox.Items.Add("По алфавиту ↓");
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
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

        private void editorinfoButton_Click(object sender, RoutedEventArgs e)
        {
            if(usersListView.SelectedItem != null)
            {
                Users user = ((UserControler)usersListView.SelectedItem).selectUser;
                InfoAboutUser.creatingWindow = this;
                new InfoAboutUser(user,1).ShowDialog();

            }
            else
            {
                MessageBox.Show("Вы не выбрали пользователя","Предупреждение",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            FullList();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new InfoAboutUser();
            nextWindow.ShowDialog();
            FullList();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void usersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (usersListView.SelectedItem != null)
            {
                Users user = ((UserControler)usersListView.SelectedItem).selectUser;
                InfoAboutUser.creatingWindow = this;
                new InfoAboutUser(user,1).ShowDialog();

            }
            else
            {
                MessageBox.Show("Вы не выбрали пользователя", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            FullList();
        }

    }
}
