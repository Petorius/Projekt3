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
using System.Configuration;
using System.Data.SqlClient;
using Client.Domain;
using Client.ControlLayer;

namespace DesktopClient {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private AdminController ac;
        public MainWindow() {
            InitializeComponent();
            ac = new AdminController();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            string email = emploEmail.Text;
            string pass = employPassword.Password.ToString();
            if(ac.ValidateAdminLogin(email, pass)) {
                CrudOverview crudView = new CrudOverview();
                crudView.Show();
            }
            else {
                wrongLogin.Content = "Email eller password er forkert";
            }
            
        }
    }
}
