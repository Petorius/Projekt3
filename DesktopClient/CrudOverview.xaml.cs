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
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DesktopClient {
    /// <summary>
    /// Interaction logic for CrudOverview.xaml
    /// </summary>

    public partial class CrudOverview : Window {
        public static string ImageURL { get; set; }
        public static string ImageName { get; set; }
        private ProductController productController;
        private OrderController orderController;
        private List<Orderline> orderlines;
        private UserController userController;
        private List<string> userOrderListWithID;
        private List<string> orderlineItems;
        private List<string> showReviewList;
        private int selectedListItemOrderLineID;
        private string selectedListItem = "";

        public CrudOverview() {
            InitializeComponent();
            productController = new ProductController();
            orderController = new OrderController();
            orderlines = new List<Orderline>();
            userController = new UserController();
            showReviewList = new List<string>();
            userOrderListWithID = new List<string>();
            orderlineItems = new List<string>();
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
                if (ImageName != null && ImageURL != null) {
                    res = productController.CreateProduct(name, price, stock, minStock, maxStock, description, ImageURL, ImageName);
                }
                else {
                    addProductText.Content = "Du skal tilføje et billede";
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
            ImageURL = null;
            ImageName = null;
        }

        private void clearFields() {
            nameTextBox.Text = "";
            priceTextBox.Text = "";
            stockTextBox.Text = "";
            minStockTextBox.Text = "";
            maxStockTextBox.Text = "";
            descriptionTextBox.Text = "";
            addProductText.Content = "";
        }

        private void Produkt_Søg_Ok_Clicked(object sender, RoutedEventArgs e) {
            try {
                Product p = new Product();
                if (Int32.TryParse(inputIDtextBox.Text, out int i)) {
                    p = productController.Find(i);
                }
                else {
                    p = productController.FindByName(inputIDtextBox.Text);
                }
                if (p.ID > 0) {
                    productShowID.Content = p.ID;
                    foundNamelabel.Content = p.Name;
                    foundPricelabel.Content = p.Price;
                    foundStocklabel.Content = p.Stock;
                    foundDescriptionlabel.Content = p.Description;
                    foundRatinglabel.Content = p.Rating;
                    ProductSøgLabel.Content = "";
                    showReviewList = new List<string>();
                    foreach (Review review in p.Reviews) {
                        showReviewList.Add("Skrevet af " + review.User.FirstName + ": " + review.Text);
                    }
                    showReviewList.Reverse();
                    ProductSøgShowReviews.ItemsSource = showReviewList;
                }
                else {
                    productShowID.Content = "";
                    foundNamelabel.Content = "";
                    foundPricelabel.Content = "";
                    foundStocklabel.Content = "";
                    foundDescriptionlabel.Content = "";
                    foundRatinglabel.Content = "";
                    showReviewList = new List<string>();
                    ProductSøgShowReviews.ItemsSource = showReviewList;
                    ProductSøgLabel.Content = "Produktet findes ikke";
                }

            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void Produkt_Slet_SletProdukt_Clicked_Click(object sender, RoutedEventArgs e) {
            try {
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
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void Søgbutton_Click(object sender, RoutedEventArgs e) {

            try {
                Product p = new Product();
                if (Int32.TryParse(_inputIDtextBox.Text, out int i)) {
                    p = productController.Find(i);
                }
                else {
                    p = productController.FindByName(_inputIDtextBox.Text);
                }
                IsActiveButton.IsChecked = p.IsActive;
                updateNameTextBox.Text = p.Name;
                updatePriceTextBox.Text = p.Price.ToString();
                updateStockTextBox.Text = p.Stock.ToString();
                updateDescriptionTextBox.Text = p.Description;
                updateMinStockTextBox.Text = p.MinStock.ToString();
                updateMaxStockTextBox.Text = p.MaxStock.ToString();
                _inputIDtextBox.IsEnabled = false;
                updateProductText1.Content = "";
            }
            catch (NullReferenceException) {
                MessageBox.Show("Produktet findes ikke");
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
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
            updateProductText1.Content = "";
            ProductClearUpdateFields();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e) {
            AddImagesWindow addImagesWindow = new AddImagesWindow();
            addImagesWindow.Show();
        }

        private void OrdrelineClearFields() {
            Ordre_Opret_Find_Product_TextBox.Text = "";
            Ordre_Opret_Antal_TextBox.Text = "";
        }

        private void OrderClearFields() {
            Ordre_Opret_Find_Kunde_TextBox.Text = "";
            Ordre_Opret_Find_Product_TextBox.Text = "";
            Ordre_Opret_Antal_TextBox.Text = "";
        }

        private void addOrderlineButton_Click(object sender, RoutedEventArgs e) {
            try {
                Product p = new Product();
                if (Int32.TryParse(Ordre_Opret_Find_Product_TextBox.Text, out int i)) {
                    p = productController.Find(i);
                }
                else {
                    p = productController.FindByName(Ordre_Opret_Find_Product_TextBox.Text);
                }
                int quantity = Int32.Parse(Ordre_Opret_Antal_TextBox.Text);
                decimal subTotal = p.Price * quantity;
                Orderline ol = new Orderline(quantity, subTotal, p);
                bool result = orderController.CreateOrderLine(quantity, subTotal, p.ID);

                if (result) {
                    orderlines.Add(ol);
                    Ordre_Opret_Tilføjet_Label.Content = "Ordrelinjen blev oprettet";
                    OrdrelineClearFields();

                }
                else {
                    Ordre_Opret_Tilføjet_Label.Content = "Der opstod en fejl. Prøv igen";
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e) {

            try {
                Customer c = userController.GetCustomerByMail(Ordre_Opret_Find_Kunde_TextBox.Text);

                if (c.ID > 0) {
                    Order order = orderController.CreateOrder(c.FirstName, c.LastName, c.Address, c.ZipCode, c.City, c.Email, c.Phone, orderlines);
                    Ordre_Opret_Tilføjet_Label.Content = "Ordren blev oprettet";
                    OrderClearFields();
                    orderlines = new List<Orderline>();
                }
                else {
                    Ordre_Opret_Tilføjet_Label.Content = "Der opstod en fejl. Prøv igen";
                }
            }

            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            OrderClearFields();
        }

        private void Ordre_Søg_Ok_Knap_Click(object sender, RoutedEventArgs e) {

            try {

                int id = Int32.Parse(Ordre_Søg_Find_Ordre_TextBox.Text);
                Order o = orderController.FindOrder(id);
                if (o.ID > 0) {
                    Ordre_Søg_Kunde_Email_Label_Output.Content = o.Customer.Email;
                    Ordre_Søg_Total_Label_Output.Content = o.Total;
                    Ordre_Søg_Købsdato_Label_Output.Content = o.DateCreated;

                    foreach (Orderline ol in o.Orderlines) {
                        orderlineItems.Add("Orderlinje ID: " + ol.ID.ToString() + " " + "Antal: " + ol.Quantity.ToString() + " " + "Sub-total: " + ol.SubTotal.ToString() + " " + "Product ID: " + ol.Product.ID.ToString());
                    }

                    Ordre_Søg_Ordrelinjer_ListBox.ItemsSource = orderlineItems;
                    orderlineItems = new List<string>();
                }
                else {
                    MessageBox.Show("Ordren findes ikke");
                }
            }
            catch (NullReferenceException) {
                MessageBox.Show("Ordren findes ikke");
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }

        }

        private void Ordre_Søg_Annuller_Knap_Click(object sender, RoutedEventArgs e) {
            Ordre_Søg_Find_Ordre_TextBox.Text = "";
            Ordre_Søg_Kunde_Email_Label_Output.Content = "";
            Ordre_Søg_Total_Label_Output.Content = "";
            Ordre_Søg_Købsdato_Label_Output.Content = "";
            Ordre_Søg_Ordrelinjer_ListBox.ItemsSource = new List<string>();
        }

        private void Kunde_Søg_Ok_Click(object sender, RoutedEventArgs e) {
            try {
                User user = userController.GetUserWithOrders(findUserByMailTextBox2.Text);
                if (user.ID > 0) {
                    userFindFirstNameLabel1.Content = user.FirstName;
                    userFindLastNameLabel1.Content = user.LastName;
                    userFindAddressLabel1.Content = user.Address;
                    userFindZipCodeLabel1.Content = user.ZipCode;
                    userFindCityLabel1.Content = user.City;
                    userFindPhoneLabel1.Content = user.Phone;
                    userOrderListWithID = new List<string>();
                    foreach (Order order in user.Orders) {
                        userOrderListWithID.Add("Ordrenummer #" + order.ID);
                    }
                    userOrderListWithID.Reverse();
                    Kunde_Søg_Orderbox1.ItemsSource = userOrderListWithID;
                    UserSøgLabel.Content = "";
                }
                else {
                    UserSøgLabel.Content = "Kunden findes ikke";
                    userFindFirstNameLabel1.Content = "";
                    userFindLastNameLabel1.Content = "";
                    userFindAddressLabel1.Content = "";
                    userFindZipCodeLabel1.Content = "";
                    userFindCityLabel1.Content = "";
                    userFindPhoneLabel1.Content = "";
                    userOrderListWithID = new List<string>();
                }
            }
            catch (NullReferenceException) {
                MessageBox.Show("Der skete en fejl");
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void Kunde_Opdater_OK_Click(object sender, RoutedEventArgs e) {
            try {
                User user = userController.GetUser(Kunde_Opdater_SøgEmail_TextBox.Text);
                user.FirstName = Kunde_Opdater_FirstName_TextBox.Text;
                user.LastName = Kunde_Opdater_LastName_TextBox.Text;
                user.Address = Kunde_Opdater_Address_TextBox.Text;
                user.ZipCode = Int32.Parse(Kunde_Opdater_ZipCode_TextBox.Text);
                user.City = Kunde_Opdater_City_TextBox.Text;
                user.Phone = Int32.Parse(Kunde_Opdater_Phone_TextBox.Text);
                user.Email = Kunde_Opdater_Email_TextBox.Text;

                bool res = userController.UpdateCustomer(user.FirstName, user.LastName, user.Phone, user.Email, user.Address, user.ZipCode, user.City);
                if (res) {
                    Kunde_Opdater_Result_Label.Content = "Kunden blev opdateret!";
                }
                else {
                    Kunde_Opdater_Result_Label.Content = "Der skete en fejl! Prøv igen.";
                }
                ClearCustomerFields();
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void Kunde_Opdater_Anuller_Click(object sender, RoutedEventArgs e) {
            ClearCustomerFields();
        }

        private void ClearCustomerFields() {
            Kunde_Opdater_FirstName_TextBox.Text = "";
            Kunde_Opdater_LastName_TextBox.Text = "";
            Kunde_Opdater_Address_TextBox.Text = "";
            Kunde_Opdater_ZipCode_TextBox.Text = "";
            Kunde_Opdater_City_TextBox.Text = "";
            Kunde_Opdater_Phone_TextBox.Text = "";
            Kunde_Opdater_Email_TextBox.Text = "";
            Kunde_Opdater_SøgEmail_TextBox.Text = "";
            Kunde_Opdater_SøgEmail_TextBox.IsEnabled = true;
            Kunde_Opdater_FindKunde_Resultat_Label.Content = "";
        }

        private void Kunde_Opdater_FindKunde_Click(object sender, RoutedEventArgs e) {
            User user = userController.GetUser(Kunde_Opdater_SøgEmail_TextBox.Text);
            if (user.ID > 0) {
                SetCustomerFields(user);
                Kunde_Opdater_FindKunde_Resultat_Label.Content = "";
            }
            else {
                Kunde_Opdater_FindKunde_Resultat_Label.Content = "Kunden findes ikke!";
            }
            Kunde_Opdater_Result_Label.Content = "";
        }

        private void SetCustomerFields(User user) {
            Kunde_Opdater_FirstName_TextBox.Text = user.FirstName;
            Kunde_Opdater_LastName_TextBox.Text = user.LastName;
            Kunde_Opdater_Address_TextBox.Text = user.Address;
            Kunde_Opdater_ZipCode_TextBox.Text = user.ZipCode.ToString();
            Kunde_Opdater_City_TextBox.Text = user.City;
            Kunde_Opdater_Phone_TextBox.Text = user.Phone.ToString();
            Kunde_Opdater_Email_TextBox.Text = user.Email;
            Kunde_Opdater_SøgEmail_TextBox.IsEnabled = false;
        }

        private void Kunde_Slet_Find_Clicked(object sender, RoutedEventArgs e) {
            Customer customer = userController.GetCustomerByMail(findUserByMailDeleteTextBox.Text);
            findUserByMailDeleteTextBox.Text = "";
            userDeleteUserDontExLabel.Content = "";
            userDeleteCompleteLabel.Content = "";
            try {
                if (customer.ID > 0) {
                    userDeleteFirstNameLabel.Content = customer.FirstName;
                    userDeleteLastNameLabel.Content = customer.LastName;
                    userDeleteAddressLabel.Content = customer.Address;
                    userDeleteZipCodeLabel.Content = customer.ZipCode;
                    userDeleteCityLabel.Content = customer.City;
                    userDeletePhoneLabel.Content = customer.Phone;
                    userDeleteMailLabel.Content = customer.Email;
                }
                else {
                    userDeleteUserDontExLabel.Content = "Kundens findes ikke";
                }
            }
            catch (NullReferenceException) {
                MessageBox.Show("Der skete en fejl");
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

        private void Kunde_Slet_ClearFields() {
            userDeleteFirstNameLabel.Content = "";
            userDeleteLastNameLabel.Content = "";
            userDeleteAddressLabel.Content = "";
            userDeleteZipCodeLabel.Content = "";
            userDeleteCityLabel.Content = "";
            userDeletePhoneLabel.Content = "";
            userDeleteMailLabel.Content = "";
        }

        private void Ordre_Opdater_Søg_Knap_Click(object sender, RoutedEventArgs e) {
            Ordre_Opdater_Find_Ordre_TextBox.IsEnabled = false;
            CreateOrderlineHandler();
        }

        private void CreateOrderlineHandler() {
            try {
                int id = Int32.Parse(Ordre_Opdater_Find_Ordre_TextBox.Text);
                Order o = orderController.FindOrder(id);

                if (o != null) {

                    foreach (Orderline ol in o.Orderlines) {
                        orderlineItems.Add("Orderlinje ID: " + ol.ID.ToString() + " " + "Antal: " + ol.Quantity.ToString() + " " + "Sub-total: " + ol.SubTotal.ToString() + " " + "Product ID: " + ol.Product.ID.ToString());

                        
                        UpdateOrderlineListBox();
                    }
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (NullReferenceException) {
                MessageBox.Show("Ordren findes ikke");
            }
        }

        private void UpdateOrderlineListBox() {
            Ordre_Opdater_ListBox.ItemsSource = orderlineItems;
            orderlineItems = new List<string>();

        }

        private void Ordre_Slet_SletOrdre_Button_Click(object sender, RoutedEventArgs e) {
            try {
                int value = Int32.Parse(Ordre_Slet_FindOrdre_Input.Text);
                bool res = orderController.DeleteOrder(value);
                if (res == true) {
                    Ordre_Slet_SletStatus_Label.Content = "Ordren blev slettet";
                    Ordre_Slet_FindOrdre_Input.Text = "";
                }
                else {
                    Ordre_Slet_SletStatus_Label.Content = "Der opstod en fejl. Prøv igen";
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void ClearOrderLineFields() {
            Ordre_Opdater_ProductName_TextBox.Text = "";
            Ordre_Opdater_Antal_TextBox.Text = "";

        }

        private void Ordre_Opdater_Tilføj_Ordrelinje_Knap_Click(object sender, RoutedEventArgs e) {
            try {
                Product p = new Product();
                if (Int32.TryParse(Ordre_Opdater_ProductName_TextBox.Text, out int i)) {
                    p = productController.Find(i);
                }
                else {
                    p = productController.FindByName(Ordre_Opdater_ProductName_TextBox.Text);
                }
                int quantity = Int32.Parse(Ordre_Opdater_Antal_TextBox.Text);
                decimal subTotal = p.Price * quantity;
                int orderID = Int32.Parse(Ordre_Opdater_Find_Ordre_TextBox.Text);
                bool result = orderController.CreateOrderLineInDesktop(quantity, subTotal, p.ID, orderID);

                if (result) {
                    Ordre_Opdater_Label_Tilføjet.Content = "Ordrelinjen blev oprettet";
                    CreateOrderlineHandler();
                    ClearOrderLineFields();
                }
                else {
                    Ordre_Opdater_Label_Tilføjet.Content = "Der opstod en fejl. Prøv igen";
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
        }

        private void Ordre_Opdater_Afslut_Knap_Click(object sender, RoutedEventArgs e) {
            Ordre_Opdater_Find_Ordre_TextBox.IsEnabled = true;
            ClearOrderLineFields();
            UpdateOrderlineListBox();
        }

        private void Ordre_Opdater_Slet_Ordrelinje_Knap_Click(object sender, RoutedEventArgs e) {
            try {
                Orderline ol = orderController.FindOrderLine(selectedListItemOrderLineID);

                if (ol != null) {
                    bool result = orderController.DeleteOrderLineInDesktop(ol.Product.ID, ol.SubTotal, ol.Quantity, selectedListItemOrderLineID);

                    if (result) {
                        Ordre_Opdater_Label_Tilføjet.Content = "Ordrelinjen blev slettet";

                    }
                    else {
                        Ordre_Opdater_Label_Tilføjet.Content = "Der opstod en fejl. Prøv igen";
                    }

                    UpdateOrderlineListBox();
                    CreateOrderlineHandler();
                }
            }
            catch (NullReferenceException) {
                MessageBox.Show("Ordrelinjen findes ikke");
            }
        }

        private void Ordre_Opdater_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (Ordre_Opdater_ListBox.Items.Count > 0) {
                selectedListItem = Ordre_Opdater_ListBox.Items[Ordre_Opdater_ListBox.SelectedIndex].ToString();
                selectedListItemOrderLineID = Int32.Parse(Regex.Match(selectedListItem, @"\d+").Value);
            }
        }

        private void Product_Search_Slet_Click(object sender, RoutedEventArgs e) {

        }
    }
}


