﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceLayer.OrderReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OrderLine", Namespace="http://schemas.datacontract.org/2004/07/Server.Domain")]
    [System.SerializableAttribute()]
    public partial class OrderLine : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.ServiceLayer.OrderReference.Product ProductField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int QuantityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal SubTotalField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Client.ServiceLayer.OrderReference.Product Product {
            get {
                return this.ProductField;
            }
            set {
                if ((object.ReferenceEquals(this.ProductField, value) != true)) {
                    this.ProductField = value;
                    this.RaisePropertyChanged("Product");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Quantity {
            get {
                return this.QuantityField;
            }
            set {
                if ((this.QuantityField.Equals(value) != true)) {
                    this.QuantityField = value;
                    this.RaisePropertyChanged("Quantity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal SubTotal {
            get {
                return this.SubTotalField;
            }
            set {
                if ((this.SubTotalField.Equals(value) != true)) {
                    this.SubTotalField = value;
                    this.RaisePropertyChanged("SubTotal");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Product", Namespace="http://schemas.datacontract.org/2004/07/Server.Domain")]
    [System.SerializableAttribute()]
    public partial class Product : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.ServiceLayer.OrderReference.Image[] ImagesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsActiveField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MaxStockField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MinStockField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RatingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SalesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StockField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Client.ServiceLayer.OrderReference.Image[] Images {
            get {
                return this.ImagesField;
            }
            set {
                if ((object.ReferenceEquals(this.ImagesField, value) != true)) {
                    this.ImagesField = value;
                    this.RaisePropertyChanged("Images");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsActive {
            get {
                return this.IsActiveField;
            }
            set {
                if ((this.IsActiveField.Equals(value) != true)) {
                    this.IsActiveField = value;
                    this.RaisePropertyChanged("IsActive");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MaxStock {
            get {
                return this.MaxStockField;
            }
            set {
                if ((this.MaxStockField.Equals(value) != true)) {
                    this.MaxStockField = value;
                    this.RaisePropertyChanged("MaxStock");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MinStock {
            get {
                return this.MinStockField;
            }
            set {
                if ((this.MinStockField.Equals(value) != true)) {
                    this.MinStockField = value;
                    this.RaisePropertyChanged("MinStock");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Rating {
            get {
                return this.RatingField;
            }
            set {
                if ((this.RatingField.Equals(value) != true)) {
                    this.RatingField = value;
                    this.RaisePropertyChanged("Rating");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Sales {
            get {
                return this.SalesField;
            }
            set {
                if ((this.SalesField.Equals(value) != true)) {
                    this.SalesField = value;
                    this.RaisePropertyChanged("Sales");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Stock {
            get {
                return this.StockField;
            }
            set {
                if ((this.StockField.Equals(value) != true)) {
                    this.StockField = value;
                    this.RaisePropertyChanged("Stock");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Image", Namespace="http://schemas.datacontract.org/2004/07/Server.Domain")]
    [System.SerializableAttribute()]
    public partial class Image : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ImageSourceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ImageSource {
            get {
                return this.ImageSourceField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageSourceField, value) != true)) {
                    this.ImageSourceField = value;
                    this.RaisePropertyChanged("ImageSource");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Order", Namespace="http://schemas.datacontract.org/2004/07/Server.Domain")]
    [System.SerializableAttribute()]
    public partial class Order : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.ServiceLayer.OrderReference.Customer CustomerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateCreatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Client.ServiceLayer.OrderReference.OrderLine[] OrderlinesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal TotalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ValidationField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Client.ServiceLayer.OrderReference.Customer Customer {
            get {
                return this.CustomerField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerField, value) != true)) {
                    this.CustomerField = value;
                    this.RaisePropertyChanged("Customer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateCreated {
            get {
                return this.DateCreatedField;
            }
            set {
                if ((this.DateCreatedField.Equals(value) != true)) {
                    this.DateCreatedField = value;
                    this.RaisePropertyChanged("DateCreated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Client.ServiceLayer.OrderReference.OrderLine[] Orderlines {
            get {
                return this.OrderlinesField;
            }
            set {
                if ((object.ReferenceEquals(this.OrderlinesField, value) != true)) {
                    this.OrderlinesField = value;
                    this.RaisePropertyChanged("Orderlines");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Total {
            get {
                return this.TotalField;
            }
            set {
                if ((this.TotalField.Equals(value) != true)) {
                    this.TotalField = value;
                    this.RaisePropertyChanged("Total");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Validation {
            get {
                return this.ValidationField;
            }
            set {
                if ((this.ValidationField.Equals(value) != true)) {
                    this.ValidationField = value;
                    this.RaisePropertyChanged("Validation");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Customer", Namespace="http://schemas.datacontract.org/2004/07/Server.Domain")]
    [System.SerializableAttribute()]
    public partial class Customer : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PhoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ZipCodeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City {
            get {
                return this.CityField;
            }
            set {
                if ((object.ReferenceEquals(this.CityField, value) != true)) {
                    this.CityField = value;
                    this.RaisePropertyChanged("City");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Phone {
            get {
                return this.PhoneField;
            }
            set {
                if ((this.PhoneField.Equals(value) != true)) {
                    this.PhoneField = value;
                    this.RaisePropertyChanged("Phone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ZipCode {
            get {
                return this.ZipCodeField;
            }
            set {
                if ((this.ZipCodeField.Equals(value) != true)) {
                    this.ZipCodeField = value;
                    this.RaisePropertyChanged("ZipCode");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OrderReference.IOrderService")]
    public interface IOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CreateOrderLine", ReplyAction="http://tempuri.org/IOrderService/CreateOrderLineResponse")]
        bool CreateOrderLine(int quantity, decimal subTotal, int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CreateOrderLine", ReplyAction="http://tempuri.org/IOrderService/CreateOrderLineResponse")]
        System.Threading.Tasks.Task<bool> CreateOrderLineAsync(int quantity, decimal subTotal, int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CreateOrder", ReplyAction="http://tempuri.org/IOrderService/CreateOrderResponse")]
        Client.ServiceLayer.OrderReference.Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email, int number, Client.ServiceLayer.OrderReference.OrderLine[] ol);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CreateOrder", ReplyAction="http://tempuri.org/IOrderService/CreateOrderResponse")]
        System.Threading.Tasks.Task<Client.ServiceLayer.OrderReference.Order> CreateOrderAsync(string firstName, string lastName, string street, int zip, string city, string email, int number, Client.ServiceLayer.OrderReference.OrderLine[] ol);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/FindOrder", ReplyAction="http://tempuri.org/IOrderService/FindOrderResponse")]
        Client.ServiceLayer.OrderReference.Order FindOrder(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/FindOrder", ReplyAction="http://tempuri.org/IOrderService/FindOrderResponse")]
        System.Threading.Tasks.Task<Client.ServiceLayer.OrderReference.Order> FindOrderAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/UpdateOrderLine", ReplyAction="http://tempuri.org/IOrderService/UpdateOrderLineResponse")]
        bool UpdateOrderLine(int ID, decimal subTotal, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/UpdateOrderLine", ReplyAction="http://tempuri.org/IOrderService/UpdateOrderLineResponse")]
        System.Threading.Tasks.Task<bool> UpdateOrderLineAsync(int ID, decimal subTotal, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/DeleteOrderLine", ReplyAction="http://tempuri.org/IOrderService/DeleteOrderLineResponse")]
        bool DeleteOrderLine(int ID, decimal subTotal, int quantity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/DeleteOrderLine", ReplyAction="http://tempuri.org/IOrderService/DeleteOrderLineResponse")]
        System.Threading.Tasks.Task<bool> DeleteOrderLineAsync(int ID, decimal subTotal, int quantity);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderServiceChannel : Client.ServiceLayer.OrderReference.IOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderServiceClient : System.ServiceModel.ClientBase<Client.ServiceLayer.OrderReference.IOrderService>, Client.ServiceLayer.OrderReference.IOrderService {
        
        public OrderServiceClient() {
        }
        
        public OrderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CreateOrderLine(int quantity, decimal subTotal, int ID) {
            return base.Channel.CreateOrderLine(quantity, subTotal, ID);
        }
        
        public System.Threading.Tasks.Task<bool> CreateOrderLineAsync(int quantity, decimal subTotal, int ID) {
            return base.Channel.CreateOrderLineAsync(quantity, subTotal, ID);
        }
        
        public Client.ServiceLayer.OrderReference.Order CreateOrder(string firstName, string lastName, string street, int zip, string city, string email, int number, Client.ServiceLayer.OrderReference.OrderLine[] ol) {
            return base.Channel.CreateOrder(firstName, lastName, street, zip, city, email, number, ol);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceLayer.OrderReference.Order> CreateOrderAsync(string firstName, string lastName, string street, int zip, string city, string email, int number, Client.ServiceLayer.OrderReference.OrderLine[] ol) {
            return base.Channel.CreateOrderAsync(firstName, lastName, street, zip, city, email, number, ol);
        }
        
        public Client.ServiceLayer.OrderReference.Order FindOrder(int id) {
            return base.Channel.FindOrder(id);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceLayer.OrderReference.Order> FindOrderAsync(int id) {
            return base.Channel.FindOrderAsync(id);
        }
        
        public bool UpdateOrderLine(int ID, decimal subTotal, int quantity) {
            return base.Channel.UpdateOrderLine(ID, subTotal, quantity);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateOrderLineAsync(int ID, decimal subTotal, int quantity) {
            return base.Channel.UpdateOrderLineAsync(ID, subTotal, quantity);
        }
        
        public bool DeleteOrderLine(int ID, decimal subTotal, int quantity) {
            return base.Channel.DeleteOrderLine(ID, subTotal, quantity);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteOrderLineAsync(int ID, decimal subTotal, int quantity) {
            return base.Channel.DeleteOrderLineAsync(ID, subTotal, quantity);
        }
    }
}
