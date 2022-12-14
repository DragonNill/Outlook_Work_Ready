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

namespace Outlook_Work.Windows.CreateApplication
{
    /// <summary>
    /// Логика взаимодействия для CreateApplicationWindow.xaml
    /// </summary>
    public partial class CreateApplicationWindow : Window
    {
        OutlookWorkDatabaseContext context;
        Users user;
        Order order;

        public CreateApplicationWindow(Users liveUser)
        {
            context = OutlookWorkDatabaseContext.DbContext;

            user = liveUser;
            InitializeComponent();
        }

        private void CreateOrder()
        {
            TextRange content = new TextRange(applicationContentRichTextBox.Document.ContentStart,
               applicationContentRichTextBox.Document.ContentEnd);
            if (!string.IsNullOrWhiteSpace(content.Text) && !string.IsNullOrWhiteSpace(nameOrdertextBox.Text))
            {
                Order order = new Order();

                order.OrderContent = content.Text;

                order.OrderDate = DateTime.Now;
                order.OrderName = nameOrdertextBox.Text;
                order.CodeStatus = 4;
                order.CodeEmployeer = user.Iduser;

                context.Order.Add(order);
                context.SaveChanges();
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста заполните поля");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void makeButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrder();
        }
    }
}
