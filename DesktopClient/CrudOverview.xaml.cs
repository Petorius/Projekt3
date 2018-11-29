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
using Client.Domain;
using Client.ControlLayer;

namespace DesktopClient {
    /// <summary>
    /// Interaction logic for CrudOverview.xaml
    /// </summary>

    public partial class CrudOverview : Window {
        public string ImageURL { get; set; }
        public string ImageName { get; set; }
        private ProductController productController;
        private OrderController orderController;
        private List<Orderline> orderlines;
        private UserController userController;
       
        private List<string> userOrderListWithID;

        public CrudOverview() {
            InitializeComponent();
            productController = new ProductController();
            orderController = new OrderController();
            orderlines = new List<Orderline>();
            userController = new UserController();

            userOrderListWithID = new List<string>();
        }



        private void Produkt_Opret_OpretProdukt_Clicked(object sender, RoutedEventArgs e) {
            try {
                string name = nameTextBox.Text;
                decimal price = decimal.Parse(priceTextBox.Text);
                int stock = Int32.Parse(stockTextBox.Text);
                int minStock = Int32.Parse(minStockTextBox.Text);
                int maxStock = Int32.Parse(maxStockTextBox.Text);
                string description = descriptionTextBox.Text;
                bool res = false;
                if (ImageName != "" && ImageURL != "") {
                    res = productController.CreateProduct(name, price, stock, minStock, maxStock, description, ImageURL, ImageName);
                }
                if (res) {
                    addProductText.Content = "Produktet blev tilføjet";
                    clearFields();
                }
                else {
                    addProductText.Content = "Der skete en fejl";
                }


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
            stockTextBox.Text = "";
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
            if (res == true) {
                deleteStatusLabel.Content = "Produktet blev slettet";
                deleteTextBox.Text = "";
            }
            else {
                deleteStatusLabel.Content = "Der opstod en fejl. Prøv igen";
            }

        }

        private void Søgbutton_Click(object sender, RoutedEventArgs e) {
            Product p = productController.Find(Int32.Parse(_inputIDtextBox.Text));
            try {
                IsActiveButton.IsChecked = p.IsActive;
                updateNameTextBox.Text = p.Name;
                updatePriceTextBox.Text = p.Price.ToString();
                updateStockTextBox.Text = p.Stock.ToString();
                updateDescriptionTextBox.Text = p.Description;
                updateMinStockTextBox.Text = p.MinStock.ToString();
                updateMaxStockTextBox.Text = p.MaxStock.ToString();
                _inputIDtextBox.IsEnabled = false;
            }
            catch (NullReferenceException) {
                MessageBox.Show("Produktet findes ikke");
            }
        }

        private void OKUpdateButton_Click(object sender, RoutedEventArgs e) {
            try {
                bool isActive = (bool)IsActiveButton.IsChecked;
                int id = Int32.Parse(_inputIDtextBox.Text);
                string name = updateNameTextBox.Text;
                decimal price = decimal.Parse(updatePriceTextBox.Text);
                int stock = Int32.Parse(updateStockTextBox.Text);
                int minStock = Int32.Parse(updateMinStockTextBox.Text);
                int maxStock = Int32.Parse(updateMaxStockTextBox.Text);
                string description = updateDescriptionTextBox.Text;
                bool isUpdated = productController.Update(id, name, price, stock, minStock, maxStock, description, isActive);
                if (isUpdated) {
                    updateProductText1.Content = "Produktet blev opdateret";
                    ProductClearUpdateFields();
                    _inputIDtextBox.IsEnabled = true;
                }
                else {
                    updateProductText1.Content = "Der opstod en fejl. Prøv igen";
                }

            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void ProductClearUpdateFields() {
            IsActiveButton.IsChecked = false;
            _inputIDtextBox.Text = "";
            updateNameTextBox.Text = "";
            updatePriceTextBox.Text = "";
            updateStockTextBox.Text = "";
            updateMaxStockTextBox.Text = "";
            updateMinStockTextBox.Text = "";
            updateDescriptionTextBox.Text = "";
        }

        private void CancelUpdateButton_Click(object sender, RoutedEventArgs e) {
            _inputIDtextBox.IsEnabled = true;
            ProductClearUpdateFields();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e) {
            AddImagesWindow addImagesWindow = new AddImagesWindow();
            addImagesWindow.Show();
        }

        private void OrdrelineClearFields() {
            findProductTextBox.Text = "";
            inputQuantityTextBox.Text = "";
        }

        private void OrderClearFields() {
            findCustomerTextBox.Text = "";
            findProductTextBox.Text = "";
            inputQuantityTextBox.Text = "";
        }

        private void findProductTextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void addOrderlineButton_Click(object sender, RoutedEventArgs e) {

            Product p = productController.FindByName(findProductTextBox.Text);
            int quantity = Int32.Parse(inputQuantityTextBox.Text);
            decimal subTotal = p.Price * quantity;
            Orderline ol = new Orderline(quantity, subTotal, p);
            bool result = orderController.CreateOrderLine(quantity, subTotal, p.ID);

            if (result) {
                orderlines.Add(ol);
                //updateProductText.Content = "Ordrelinjen blev oprettet";
                OrdrelineClearFields();

            }
            else {
                //updateProductText.Content = "Der opstod en fejl. Prøv igen";
            }
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e) {
            Customer c = userController.GetCustomerByMail(findCustomerTextBox.Text);

            if (c != null) {
                Order order = orderController.CreateOrder(c.FirstName, c.LastName, c.Address, c.ZipCode, c.City, c.Email, c.Phone, orderlines);
                //updateProductText.Content = "Ordren blev oprettet";
                OrderClearFields();
                orderlines = new List<Orderline>();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            OrderClearFields();
        }

        private void Kunde_Søg_Ok_Click(object sender, RoutedEventArgs e) {
            User user = userController.GetUserWithOrders(findUserByMailTextBox1.Text);
            if (user != null) {
                userFindFirstNameLabel.Content = user.FirstName;
                userFindLastNameLabel.Content = user.LastName;
                userFindAddressLabel.Content = user.Address;
                userFindZipCodeLabel.Content = user.ZipCode;
                userFindCityLabel.Content = user.City;
                userFindPhoneLabel.Content = user.Phone;
                foreach (Order order in user.OrderList) {
                    userOrderListWithID.Add("Ordrenummer #" + order.ID);

                }
                userOrderListWithID.Reverse();
                Kunde_Søg_Orderbox.ItemsSource = userOrderListWithID;
            }
        }

        private void Kunde_Opdater_OK_Click(object sender, RoutedEventArgs e) {
            User user = userController.GetUser(Kunde_Opdater_SøgEmail_TextBox.Text);
            if (user != null) {
                Kunde_Opdater_FirstName_Label_Display.Content = user.FirstName;
                Kunde_Opdater_LastName_Label_Display.Content = user.LastName;
                Kunde_Opdater_Address_Label_Display.Content = user.Address;
                Kunde_Opdater_ZipCode_Label_Display.Content = user.ZipCode;
                Kunde_Opdater_City_Label_Display.Content = user.City;
                Kunde_Opdater_Phone_Label_Display.Content = user.Phone;
                Kunde_Opdater_Email_Label_Display.Content = user.Email;
            }

            userController.UpdateCustomer(user.FirstName, user.LastName, user.Phone, user.Email, user.Address, user.ZipCode, user.City);
        }

        private void Kunde_Slet_Find_Clicked(object sender, RoutedEventArgs e) {
            Customer customer = userController.GetCustomerByMail(findUserByMailDeleteTextBox.Text);
            findUserByMailDeleteTextBox.Text = "";
            userDeleteUserDontExLabel.Content = "";
            userDeleteCompleteLabel.Content = "";
            if(customer.ID > 0) {
                userDeleteFirstNameLabel.Content = customer.FirstName;
                userDeleteLastNameLabel.Content = customer.LastName;
                userDeleteAddressLabel.Content = customer.Address;
                userDeleteZipCodeLabel.Content = customer.ZipCode;
                userDeleteCityLabel.Content = customer.City;
                userDeletePhoneLabel.Content = customer.Phone;
                userDeleteMailLabel.Content = customer.Email;
            } 
            else {
                userDeleteUserDontExLabel.Content = "Brugeren findes ikke";
            }
        }

        private void Kunde_Slet_SletKunde_Clicked(object sender, RoutedEventArgs e) {
            bool res = userController.DeleteUser(userDeleteMailLabel.Content.ToString());
            if (res) {
                userDeleteCompleteLabel.Content = "Brugeren blev slettet";
                Kunde_Slet_ClearFields();
            }
            else {
                userDeleteCompleteLabel.Content = "Der skete en fejl. Prøv igen";
                Kunde_Slet_ClearFields();
            }
        }

        private void  Kunde_Slet_ClearFields() {
            userDeleteFirstNameLabel.Content = "";
            userDeleteLastNameLabel.Content = "";
            userDeleteAddressLabel.Content = "";
            userDeleteZipCodeLabel.Content = "";
            userDeleteCityLabel.Content = "";
            userDeletePhoneLabel.Content = "";
            userDeleteMailLabel.Content = "";
        }
    }
}
