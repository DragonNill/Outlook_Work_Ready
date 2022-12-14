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

namespace Outlook_Work.Windows.Infomation
{
    /// <summary>
    /// Логика взаимодействия для InfoAboutUser.xaml
    /// </summary>
    public partial class InfoAboutUser : Window
    {
        OutlookWorkDatabaseContext context;
        Users user;

        int Itys = 0;

        public static Window creatingWindow { get; set; }

        public InfoAboutUser(Users user)
        {
            InitializeComponent();
            context = OutlookWorkDatabaseContext.DbContext;
            this.user = user;
        }

        public InfoAboutUser(Users user,int Itys2)
        {
            this.Itys = Itys2;
            InitializeComponent();
            context = OutlookWorkDatabaseContext.DbContext;
            this.user = user;
        }


        public InfoAboutUser()
        {
            InitializeComponent();
            context = OutlookWorkDatabaseContext.DbContext;
        }


        private void CheckCurrentUser()
        {
            List<Roles> rolesList = context.Roles.ToList();
            foreach(Roles role in rolesList)
            {
                roleComboBox.Items.Add(role.RoleName);
            }

            if (user == null)
            {
                nameTextBox.IsEnabled = true;
                loginTextBox.IsEnabled = true;
                emailTextBox.IsEnabled = true;
                patronomycTextBox.IsEnabled = true;
                telephoneTextBox.IsEnabled = true;
                surnameTextBox.IsEnabled = true;
                passwordPasswordBox.Visibility = Visibility.Visible;
                passwordPasswordBox.IsEnabled = true;
                roleComboBox.IsEnabled = true;
                roleComboBox.Visibility = Visibility.Visible;
                editButton.IsEnabled = true;
                editButton.Visibility = Visibility.Visible;
                editButton.Content = "Добавить";
                passwordLabel.Visibility = Visibility.Visible;
                roleLabel.Visibility = Visibility.Visible;
                backButton.Content = "Назад";

            }
            else if (Itys != 0)
            {
                nameTextBox.Text = user.UserName;
                loginTextBox.Text = user.UserLogin;
                emailTextBox.Text = user.UserEmail;
                patronomycTextBox.Text = user.UserPatronomyc;
                telephoneTextBox.Text = user.UserTelephone;
                surnameTextBox.Text = user.UserSurname;
                passwordPasswordBox.Password = user.UserPassword;
                roleComboBox.SelectedItem = user.UserCodeRoleNavigation.RoleName;
                nameTextBox.IsEnabled = true;
                loginTextBox.IsEnabled = true;
                emailTextBox.IsEnabled = true;
                patronomycTextBox.IsEnabled = true;
                telephoneTextBox.IsEnabled = true;
                surnameTextBox.IsEnabled = true;
                passwordPasswordBox.Visibility = Visibility.Visible;
                passwordPasswordBox.IsEnabled = true;
                roleComboBox.IsEnabled = true;
                roleComboBox.Visibility = Visibility.Visible;
                editButton.IsEnabled = true;
                editButton.Visibility = Visibility.Visible;
                deleteButton.IsEnabled = true;
                deleteButton.Visibility = Visibility.Visible;
                passwordLabel.Visibility = Visibility.Visible;
                roleLabel.Visibility = Visibility.Visible;
                backButton.Content = "Назад";

            }
            else
            {
                nameTextBox.Text = user.UserName;
                loginTextBox.Text = user.UserLogin;
                emailTextBox.Text = user.UserEmail;
                patronomycTextBox.Text = user.UserPatronomyc;
                telephoneTextBox.Text = user.UserTelephone;
                surnameTextBox.Text = user.UserSurname;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCurrentUser();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void showPasswordcheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordPasswordBox.Password = passwordTextBox.Text;

            passwordPasswordBox.Visibility = Visibility.Visible;
            passwordTextBox.Visibility = Visibility.Hidden;
        }

        private void showPasswordcheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Text = passwordPasswordBox.Password;

            passwordPasswordBox.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Visible;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (showPasswordcheckBox.IsChecked != true)
            {
                passwordTextBox.Text = passwordPasswordBox.Password;
            }

            if (!string.IsNullOrWhiteSpace(loginTextBox.Text) && !string.IsNullOrWhiteSpace(nameTextBox.Text) && !string.IsNullOrWhiteSpace(surnameTextBox.Text)
                && !string.IsNullOrWhiteSpace(telephoneTextBox.Text) && !string.IsNullOrWhiteSpace(emailTextBox.Text) && !string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                if (user == null)
                {
                    Users newUser = new Users();
                    newUser.UserLogin = loginTextBox.Text;
                    newUser.UserName = nameTextBox.Text;
                    newUser.UserSurname = surnameTextBox.Text;
                    newUser.UserPatronomyc = patronomycTextBox.Text;
                    newUser.UserEmail = emailTextBox.Text;
                    newUser.UserTelephone = telephoneTextBox.Text;
                    newUser.UserPassword = passwordTextBox.Text;
                    newUser.UserCodeRole = context.Roles.Where(r => r.RoleName == roleComboBox.SelectedItem).FirstOrDefault().IdRole;

                    context.Users.Add(newUser);
                    context.SaveChanges();
                    Close();
                }
                else
                {
                    user.UserLogin = loginTextBox.Text;
                    user.UserName = nameTextBox.Text;
                    user.UserSurname = surnameTextBox.Text;
                    user.UserPatronomyc = patronomycTextBox.Text;
                    user.UserEmail = emailTextBox.Text;
                    user.UserTelephone = telephoneTextBox.Text;
                    user.UserPassword = passwordTextBox.Text;
                    user.UserCodeRole = context.Roles.Where(r => r.RoleName == roleComboBox.SelectedItem).FirstOrDefault().IdRole;

                    context.SaveChanges();
                    Close();
                }
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            context.Users.Remove(user);
            context.SaveChanges();
            Close();
        }


        private void telephoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            {
                e.Handled = !(Char.IsDigit(e.Text, 0));
            }
        }
    }
}
