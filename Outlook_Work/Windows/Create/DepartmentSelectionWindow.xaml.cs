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

namespace Outlook_Work.Windows.Create
{
    /// <summary>
    /// Логика взаимодействия для DepartmentSelectionWindow.xaml
    /// </summary>
    public partial class DepartmentSelectionWindow : Window
    {
        OutlookWorkDatabaseContext context;
        Users user;
        Order order;
        public static int Itys = 0;
        List<Users> users;


        public DepartmentSelectionWindow(Order currentOrder)
        {
            order = currentOrder;
            context = OutlookWorkDatabaseContext.DbContext;
            InitializeComponent();
        }

        private void FullComboBox()
        {
            users = context.Users.Where(u => u.UserCodeRole == 4).ToList();
            foreach(Users obj in users)
                {
                selectionComboBox.Items.Add($"{obj.UserName} {obj.UserSurname}");
            }
        }


        private void selectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void sendapplicationButton_Click(object sender, RoutedEventArgs e)
        {
            order.CodeHeadDepartament = users[selectionComboBox.SelectedIndex].Iduser;
            order.CodeStatus = 3;
            context.SaveChanges();
            Itys = 1;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FullComboBox();
        }
    }
}
