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
        private int selectedListItemReviewID;
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
            bool res = false;
            try {
                string name = nameTextBox.Text;
                decimal price = decimal.Parse(priceTextBox.Text);
                int stock = Int32.Parse(stockTextBox.Text);
                int minStock = Int32.Parse(minStockTextBox.Text);
                int maxStock = Int32.Parse(maxStockTextBox.Text);
                string description = descriptionTextBox.Text;
                Product p = new Product();
                if(ImageName != null && ImageURL != null && maxStock >= minStock) {
                    p = productController.CreateProduct(name, price, stock, minStock, maxStock, description, ImageURL, ImageName);
                    res = true;
                }
                if(ImageName == null && ImageURL == null) {
                    addProductText.Content = "Du skal tilføje et billede";
                }
                if(p.ErrorMessage == "" && res) {
                    clearFields();
                    addProductText.Content = "Produktet blev oprettet";
                }
                if(maxStock < minStock) {
                    addProductText.Content = "Minimumslager skal være" + System.Environment.NewLine + "mindre end maximumslager";
                }
                if(maxStock < 0 || minStock < 0) {
                    addProductText.Content = "Lagerbeholdningen kan" + System.Environment.NewLine + "ikke være under 0";
                }
                if(p.ErrorMessage != "") {
                    addProductText.Content = p.ErrorMessage;
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
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
            
            CreateReviewHandler();
        }

        private void Produkt_Slet_SletProdukt_Clicked_Click(object sender, RoutedEventArgs e) {
            try {
                int value = Int32.Parse(deleteTextBox.Text);
                Product product = productController.DeleteProduct(value);
                if (product.ErrorMessage == "") {
                    deleteStatusLabel.Content = "Produktet blev deaktiveret";
                    deleteTextBox.Text = "";
                }
                else {
                    deleteStatusLabel.Content = product.ErrorMessage;
                    deleteTextBox.Text = "";
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }
        }

        private void Søgbutton_Click(object sender, RoutedEventArgs e) {
            try {
                Product p = new Product();
                if (Int32.TryParse(_inputIDtextBox.Text, out int i)) {
                    p = productController.Find(i);
                    if(p.ErrorMessage != "") {
                        updateProductText1.Content = p.ErrorMessage;
                    }
                }
                else {
                    p = productController.FindByName(_inputIDtextBox.Text);
                    if (p.ErrorMessage != "") {
                        updateProductText1.Content = p.ErrorMessage;
                    }
                }
                if(p.ErrorMessage == "") {
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
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
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
                Product product = new Product();
                if(minStock > 0 && maxStock > 0) {
                    product = productController.Update(id, name, price, stock, minStock, maxStock, description, isActive);
                }
                else if(minStock > maxStock) {
                    product.ErrorMessage = "Minimumslager skal være" + System.Environment.NewLine + "mindre";
                }
                else {
                    product.ErrorMessage = "";
                }
                if (product.ErrorMessage != "") {
                    updateProductText1.Content = product.ErrorMessage;
                    ProductClearUpdateFields();
                    _inputIDtextBox.IsEnabled = true;
                }
                else {
                    updateProductText1.Content = "Produktet blev opdateret";
                    ProductClearUpdateFields();
                    _inputIDtextBox.IsEnabled = true;
                }
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
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
                    if (p.ErrorMessage != "") {
                        Ordre_Opret_Tilføjet_Label.Content = p.ErrorMessage;
                    }
                }
                else {
                    p = productController.FindByName(Ordre_Opret_Find_Product_TextBox.Text);
                    if (p.ErrorMessage != "") {
                        Ordre_Opret_Tilføjet_Label.Content = p.ErrorMessage;
                    }
                }
                int quantity = Int32.Parse(Ordre_Opret_Antal_TextBox.Text);
                decimal subTotal = p.Price * quantity;
                Orderline ol = orderController.CreateOrderLine(quantity, subTotal, p.ID);

                if (ol.ErrorMessage == "") {
                    orderlines.Add(ol);
                    Ordre_Opret_Tilføjet_Label.Content = "Ordrelinjen blev oprettet";
                    OrdrelineClearFields();
                }
                else {
                    Ordre_Opret_Tilføjet_Label.Content = ol.ErrorMessage;
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e) {

            try {
                Customer c = userController.GetCustomerByMail(Ordre_Opret_Find_Kunde_TextBox.Text);

                if (c.ErrorMessage == "") {
                    Order order = orderController.CreateOrder(c.FirstName, c.LastName, c.Address, c.ZipCode, c.City, c.Email, c.Phone, orderlines);
                    Ordre_Opret_Tilføjet_Label.Content = "Ordren blev oprettet";
                    OrderClearFields();
                    orderlines = new List<Orderline>();
                }
                else {
                    Ordre_Opret_Tilføjet_Label.Content = c.ErrorMessage;
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            OrderClearFields();
        }

        private void Ordre_Søg_Ok_Knap_Click(object sender, RoutedEventArgs e) {

            try {

                int id = Int32.Parse(Ordre_Søg_Find_Ordre_TextBox.Text);
                Order o = orderController.FindOrder(id);
                if (o.ErrorMessage == "") {
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
                    MessageBox.Show(o.ErrorMessage);
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
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
                if (user.ErrorMessage == "") {
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
                    UserSøgLabel.Content = user.ErrorMessage;
                    userFindFirstNameLabel1.Content = "";
                    userFindLastNameLabel1.Content = "";
                    userFindAddressLabel1.Content = "";
                    userFindZipCodeLabel1.Content = "";
                    userFindCityLabel1.Content = "";
                    userFindPhoneLabel1.Content = "";
                    userOrderListWithID = new List<string>();
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }
        }

        private void Kunde_Opdater_OK_Click(object sender, RoutedEventArgs e) {
            bool res = false;
            try {
                User user = userController.GetUser(Kunde_Opdater_SøgEmail_TextBox.Text);
                user.FirstName = Kunde_Opdater_FirstName_TextBox.Text;
                user.LastName = Kunde_Opdater_LastName_TextBox.Text;
                user.Address = Kunde_Opdater_Address_TextBox.Text;
                user.ZipCode = Int32.Parse(Kunde_Opdater_ZipCode_TextBox.Text);
                user.City = Kunde_Opdater_City_TextBox.Text;
                user.Phone = Int32.Parse(Kunde_Opdater_Phone_TextBox.Text);
                if (user.Email != Kunde_Opdater_Email_TextBox.Text) {
                    User tempUser = userController.IsEmailAlreadyRegistered(Kunde_Opdater_Email_TextBox.Text);
                    if(tempUser.ErrorMessage != "Brugeren findes ikke") {
                        Kunde_Opdater_Result_Label.Content = "Venligt vælg en anden Email";
                    }
                    else {
                        res = true;
                        user.Email = Kunde_Opdater_Email_TextBox.Text;
                    }
                } 
                else {
                    user.Email = Kunde_Opdater_Email_TextBox.Text;
                    res = true;
                }
               if(res) {
                    Customer c = userController.UpdateCustomer(user.FirstName, user.LastName, user.Phone, user.Email, user.Address, user.ZipCode, user.City, Kunde_Opdater_Email_TextBox.Text);
                    if (c.ErrorMessage == "") {
                        Kunde_Opdater_Result_Label.Content = "Kunden blev opdateret!";
                    }
                    else {
                        Kunde_Opdater_Result_Label.Content = c.ErrorMessage;
                    }
                    ClearCustomerFields();
                }

            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
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
            if (user.ErrorMessage == "") {
                SetCustomerFields(user);
                Kunde_Opdater_FindKunde_Resultat_Label.Content = "";
            }
            else {
                Kunde_Opdater_FindKunde_Resultat_Label.Content = user.ErrorMessage;
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
                if (customer.ErrorMessage == "") {
                    userDeleteFirstNameLabel.Content = customer.FirstName;
                    userDeleteLastNameLabel.Content = customer.LastName;
                    userDeleteAddressLabel.Content = customer.Address;
                    userDeleteZipCodeLabel.Content = customer.ZipCode;
                    userDeleteCityLabel.Content = customer.City;
                    userDeletePhoneLabel.Content = customer.Phone;
                    userDeleteMailLabel.Content = customer.Email;
                }
                else {
                    userDeleteUserDontExLabel.Content = customer.ErrorMessage;
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }
        }

        private void Kunde_Slet_SletKunde_Clicked(object sender, RoutedEventArgs e) {
            User user = userController.DeleteUser(userDeleteMailLabel.Content.ToString());
            if (user.ErrorMessage == "") {
                userDeleteCompleteLabel.Content = "Brugeren blev slettet";
                Kunde_Slet_ClearFields();
            }
            else {
                userDeleteCompleteLabel.Content = user.ErrorMessage;
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

                if (o.ErrorMessage == "") {
                    foreach (Orderline ol in o.Orderlines) {
                        orderlineItems.Add("Orderlinje ID: " + ol.ID.ToString() + " " + "Antal: " + ol.Quantity.ToString() + " " + "Sub-total: " + ol.SubTotal.ToString() + " " + "Product ID: " + ol.Product.ID.ToString());
                    }
                    UpdateOrderlineListBox();
                }
                else {
                    MessageBox.Show(o.ErrorMessage);
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (NullReferenceException) {
                MessageBox.Show("Ordren findes ikke");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }
        }

        private void UpdateOrderlineListBox() {
            Ordre_Opdater_ListBox.ItemsSource = orderlineItems;
            orderlineItems = new List<string>();

        }

        private void Ordre_Slet_SletOrdre_Button_Click(object sender, RoutedEventArgs e) {
            try {
                int value = Int32.Parse(Ordre_Slet_FindOrdre_Input.Text);
                Order order = orderController.DeleteOrder(value);
                if (order.ErrorMessage == "") {
                    Ordre_Slet_SletStatus_Label.Content = "Ordren blev slettet";
                    Ordre_Slet_FindOrdre_Input.Text = "";
                }
                else {
                    Ordre_Slet_SletStatus_Label.Content = order.ErrorMessage;
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
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
                    if(p.ErrorMessage == "") {
                        Ordre_Opdater_Label_Tilføjet.Content = p.ErrorMessage;
                    }
                }
                else {
                    p = productController.FindByName(Ordre_Opdater_ProductName_TextBox.Text);
                    if(p.ErrorMessage == "") {
                        Ordre_Opdater_Label_Tilføjet.Content = p.ErrorMessage;
                    }
                }
                int quantity = Int32.Parse(Ordre_Opdater_Antal_TextBox.Text);
                decimal subTotal = p.Price * quantity;
                int orderID = Int32.Parse(Ordre_Opdater_Find_Ordre_TextBox.Text);
                Orderline orderline = orderController.CreateOrderLineInDesktop(quantity, subTotal, p.ID, orderID);

                if (orderline.ErrorMessage == "") {
                    Ordre_Opdater_Label_Tilføjet.Content = "Ordrelinjen blev oprettet";
                    CreateOrderlineHandler();
                    ClearOrderLineFields();
                }
                else {
                    Ordre_Opdater_Label_Tilføjet.Content = orderline.ErrorMessage;
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
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

                if (ol.ErrorMessage == "") {
                    Orderline orderline = orderController.DeleteOrderLineInDesktop(ol.Product.ID, ol.SubTotal, ol.Quantity, selectedListItemOrderLineID);

                    if (orderline.ErrorMessage == "") {
                        Ordre_Opdater_Label_Tilføjet.Content = "Ordrelinjen blev slettet";
                    }
                    else {
                        Ordre_Opdater_Label_Tilføjet.Content = orderline.ErrorMessage;
                    }
                    UpdateOrderlineListBox();
                    CreateOrderlineHandler();
                }
            }
            catch (NullReferenceException) {
                MessageBox.Show("Ordrelinjen findes ikke");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }
        }

        private void Ordre_Opdater_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if (Ordre_Opdater_ListBox.Items.Count > 0) {
                selectedListItem = Ordre_Opdater_ListBox.Items[Ordre_Opdater_ListBox.SelectedIndex].ToString();
                selectedListItemOrderLineID = Int32.Parse(Regex.Match(selectedListItem, @"\d+").Value);
            }
        }

        private void Product_Search_Slet_Click(object sender, RoutedEventArgs e) {

            try {
                Review r = productController.FindReview(selectedListItemReviewID);

                if (r.ErrorMessage == "") {
                    Review review = productController.DeleteReview(r.ID, r.User.ID);

                    if (review.ErrorMessage == "") {
                        foundDescriptionlabel.Content = "Anmeldelsen blev slettet";

                    }
                    else {
                        foundDescriptionlabel.Content = review.ErrorMessage;
                    }
                    UpdateReviewListBox();
                    CreateReviewHandler();
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }
        }

        private void CreateReviewHandler() {
            try {
                inputIDtextBox.IsEnabled = false;
                Product p = new Product();
                if (Int32.TryParse(inputIDtextBox.Text, out int i)) {
                    p = productController.Find(i);
                }
                else {
                    p = productController.FindByName(inputIDtextBox.Text);
                }
                if (p.ErrorMessage == "") {
                    productShowID.Content = p.ID;
                    foundNamelabel.Content = p.Name;
                    foundPricelabel.Content = p.Price;
                    foundStocklabel.Content = p.Stock;
                    foundDescriptionlabel.Content = p.Description;
                    foundRatinglabel.Content = p.Rating;
                    ProductSøgLabel.Content = "";
                    showReviewList = new List<string>();
                    foreach (Review review in p.Reviews) {
                        showReviewList.Add("Review ID: " + review.ID + " " + "Skrevet af " + review.User.FirstName + ": " + review.Text);
                        
                    }
                    showReviewList.Reverse();
                    UpdateReviewListBox();
                    
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
                    ProductSøgLabel.Content = p.ErrorMessage;
                    inputIDtextBox.IsEnabled = true;
                }
            }
            catch (FormatException) {
                MessageBox.Show("Ugyldig tekst indsat");
            }
            catch (OverflowException) {
                MessageBox.Show("Du har indtastet for store tal værdier");
            }
        }

        private void UpdateReviewListBox() {
            ProductSøgShowReviews.ItemsSource = showReviewList;
            showReviewList = new List<string>();
        }

        private void ProductSøgShowReviews_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (ProductSøgShowReviews.Items.Count > 0) {
                selectedListItem = ProductSøgShowReviews.Items[ProductSøgShowReviews.SelectedIndex].ToString();
                selectedListItemReviewID = Int32.Parse(Regex.Match(selectedListItem, @"\d+").Value);
            }
        }

        private void Product_Søg_Annuller_Click(object sender, RoutedEventArgs e) {
            inputIDtextBox.IsEnabled = true;
            ClearProductSøgFields();
            UpdateReviewListBox();                                                                                                                                                                                                             
        }

        public void ClearProductSøgFields() {
            inputIDtextBox.Text = "";
            productShowID.Content = "";
            foundNamelabel.Content = "";
            foundPricelabel.Content = "";
            foundStocklabel.Content = "";
            foundDescriptionlabel.Content = "";
            foundRatinglabel.Content = "";
        }
    }
}


