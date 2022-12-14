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
using Outlook_Work.Windows.Menus;

namespace Outlook_Work.Windows.Menus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public OutlookWorkDatabaseContext context;
        public Users user;

        public Authorization()
        {
            InitializeComponent();
            context = OutlookWorkDatabaseContext.DbContext;
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            if(showPasswordcheckBox.IsChecked != true)
            {
                passwordTextBox.Text = passwordPasswordBox.Password;
            }

            if(!string.IsNullOrWhiteSpace(passwordTextBox.Text) && !string.IsNullOrWhiteSpace(loginTextBox.Text))
            {
                user = context.Users.FirstOrDefault(l => l.UserLogin.Trim() == loginTextBox.Text.Trim()
                    && l.UserPassword.Trim() == passwordTextBox.Text.Trim());

                if(user == null)
                {
                    MessageBox.Show("Неправильный логин или пароль", "Предупреждение"
                        ,MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if(user.UserCodeRole == 1)
                    {
                        MessageBox.Show("Вы успешно вошли как Админ", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if(user.UserCodeRole == 2)
                        {
                            MessageBox.Show("Вы успешно вошли как Сотрудник компании", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            if (user.UserCodeRole == 3)
                            {
                                MessageBox.Show("Вы успешно вошли как Директор", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                if(user.UserCodeRole == 4)
                                {
                                    MessageBox.Show("Вы успешно вошли как Начальник отдела", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                    }
                    Hide();

                    Window nextWindow = new MenuForAll(user.UserCodeRole,user);
                    nextWindow.ShowDialog();
                    passwordTextBox.Text = "";
                    passwordPasswordBox.Password = "";
                    loginTextBox.Text = "";
                    Show();
                }
            }
            else
            {
                MessageBox.Show("Заполните поля пожалуйста", "Предупреждение"
                       , MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void showPasswordcheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Text = passwordPasswordBox.Password;

            passwordPasswordBox.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Visible;
        }

        private void showPasswordcheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordPasswordBox.Password = passwordTextBox.Text;

            passwordPasswordBox.Visibility = Visibility.Visible;
            passwordTextBox.Visibility = Visibility.Hidden;
        }
    }
}
