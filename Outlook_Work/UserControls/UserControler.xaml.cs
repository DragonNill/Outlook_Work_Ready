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

namespace Outlook_Work.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserControler.xaml
    /// </summary>
    public partial class UserControler : UserControl
    {
        OutlookWorkDatabaseContext context;
        public Users selectUser;

        public UserControler(Users selectUser)
        {
            InitializeComponent();
            context = OutlookWorkDatabaseContext.DbContext;
            this.selectUser = selectUser;
        }

        private void CheckCurrentUser()
        {
            idLabel.Content += selectUser.Iduser.ToString();
            loginLable.Content += selectUser.UserLogin;
            nspLabel.Content += selectUser.UserName + " " + selectUser.UserSurname + " " + selectUser.UserPatronomyc;
            roleLabel.Content += selectUser.UserCodeRoleNavigation.RoleName;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCurrentUser();
        }
    }
}
