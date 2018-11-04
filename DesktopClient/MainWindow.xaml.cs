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
        private ProductController productController;
        public MainWindow() {
            InitializeComponent();
            productController = new ProductController();

        }
        private void Produkt_Opret_OpretProdukt_Clicked(object sender, RoutedEventArgs e) {
            try {
                string name = nameTextBox.Text;
                decimal price = decimal.Parse(priceTextBox.Text);
                int stock = Int32.Parse(stockTextBox.Text);
                int minStock = Int32.Parse(minStockTextBox.Text);
                int maxStock = Int32.Parse(maxStockTextBox.Text);
                string description = descriptionTextBox.Text;
                productController.CreateProduct(name, price, stock, minStock, maxStock, description);
                addProductText.Content = "Produktet blev tilføjet";
                clearFields();
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void Produkt_Opret_Fortryd_Clicked(object sender, RoutedEventArgs e) {
            clearFields();
        }

        private void clearFields() {
            nameTextBox.Text = "";
            priceTextBox.Text = "";
            minStockTextBox.Text = "";
            maxStockTextBox.Text = "";
            descriptionTextBox.Text = "";
        }

        private void Produkt_Søg_Ok_Clicked(object sender, RoutedEventArgs e) {
            Product p = productController.Find(Int32.Parse(inputIDtextBox.Text));
            try {
                foundNamelabel.Content = p.Name;
                foundPricelabel.Content = p.Price;
                foundStocklabel.Content = p.Stock;
                foundDescriptionlabel.Content = p.Description;
                foundRatinglabel.Content = p.Rating;
            }
            catch (NullReferenceException) {
                MessageBox.Show("Produktet findes ikke");
            }
        }

        private void Produkt_Slet_SletProdukt_Clicked_Click(object sender, RoutedEventArgs e) {
            int value = Int32.Parse(deleteTextBox.Text);
            bool res = productController.DeleteProduct(value);
            if(res == true) {
                deleteStatusLabel.Content = "Produktet blev slettet";
                deleteTextBox.Text = "";
            }
            else {
                deleteStatusLabel.Content = "Der opstod en fejl. Prøv igen";
            }
            
        }
    }
}
