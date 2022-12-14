using Outlook_Work.Models.Entities;
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

namespace Outlook_Work.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ApplicationControl.xaml
    /// </summary>
    public partial class ApplicationControl : UserControl
    {
        public OutlookWorkDatabaseContext context;
        public Order order;
        public int OrderId;
        int whichIsRole;

        public ApplicationControl(Order onOrder,int idRole)
        {
            whichIsRole = idRole;
            order = onOrder;
            context = OutlookWorkDatabaseContext.DbContext;
            OrderId = onOrder.IdOrder;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCurrentOrder();
        }

        private void CheckCurrentOrder()
        {

            if (whichIsRole == 1 || whichIsRole == 3)
            {
                Users user = context.Users.Where(u => u.Iduser == order.CodeEmployeer).FirstOrDefault();
                hideLabel.Content = "Сотрудник:" + user.UserName + " " + user.UserSurname;
            }
            nameLabel.Content = order.OrderName;
            dateLabel.Content = order.OrderDate.ToShortDateString();

            Status status = context.Status.Where(w => w.Idstatus == order.CodeStatus).FirstOrDefault();
            statusLabel.Content = status.StatusName;
        }
    }
}
