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

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ProductController productController;
        public MainWindow() {
            InitializeComponent();
            productController = new ProductController();

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            try {
                string name = nameTextBox.Text;
                double price = double.Parse(priceTextBox.Text);
                int stock = Int32.Parse(stockTextBox.Text);
                int minStock = Int32.Parse(minStockTextBox.Text);
                int maxStock = Int32.Parse(maxStockTextBox.Text);
                string description = descriptionTextBox.Text;
                productController.CreateProduct(name, price, stock, minStock, maxStock, description);
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            

        }

        private void inputIDtextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OKbutton_Click(object sender, RoutedEventArgs e)
        {
           

            Product p = productController.Find(Int32.Parse(inputIDtextBox.Text));

            try
            {
                foundNamelabel.Content = p._Name;
                foundPricelabel.Content = p._Price;
                foundStocklabel.Content = p._Stock;
                foundDescriptionlabel.Content = p._Description;
                foundRatinglabel.Content = p._Rating;
                
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Product does not exist");
            }
        }
    }
}
