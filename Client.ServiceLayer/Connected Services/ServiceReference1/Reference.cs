﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceLayer.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
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
        private Client.ServiceLayer.ServiceReference1.Image[] ImagesField;
        
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
        public Client.ServiceLayer.ServiceReference1.Image[] Images {
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IProductService")]
    public interface IProductService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/CreateProduct", ReplyAction="http://tempuri.org/IProductService/CreateProductResponse")]
        bool CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/CreateProduct", ReplyAction="http://tempuri.org/IProductService/CreateProductResponse")]
        System.Threading.Tasks.Task<bool> CreateProductAsync(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/FindProduct", ReplyAction="http://tempuri.org/IProductService/FindProductResponse")]
        Client.ServiceLayer.ServiceReference1.Product FindProduct(int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/FindProduct", ReplyAction="http://tempuri.org/IProductService/FindProductResponse")]
        System.Threading.Tasks.Task<Client.ServiceLayer.ServiceReference1.Product> FindProductAsync(int ID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/FindProductByName", ReplyAction="http://tempuri.org/IProductService/FindProductByNameResponse")]
        Client.ServiceLayer.ServiceReference1.Product FindProductByName(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/FindProductByName", ReplyAction="http://tempuri.org/IProductService/FindProductByNameResponse")]
        System.Threading.Tasks.Task<Client.ServiceLayer.ServiceReference1.Product> FindProductByNameAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/DeleteProduct", ReplyAction="http://tempuri.org/IProductService/DeleteProductResponse")]
        bool DeleteProduct(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/DeleteProduct", ReplyAction="http://tempuri.org/IProductService/DeleteProductResponse")]
        System.Threading.Tasks.Task<bool> DeleteProductAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/Update", ReplyAction="http://tempuri.org/IProductService/UpdateResponse")]
        bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/Update", ReplyAction="http://tempuri.org/IProductService/UpdateResponse")]
        System.Threading.Tasks.Task<bool> UpdateAsync(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/GetAllProducts", ReplyAction="http://tempuri.org/IProductService/GetAllProductsResponse")]
        Client.ServiceLayer.ServiceReference1.Product[] GetAllProducts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/GetAllProducts", ReplyAction="http://tempuri.org/IProductService/GetAllProductsResponse")]
        System.Threading.Tasks.Task<Client.ServiceLayer.ServiceReference1.Product[]> GetAllProductsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProductServiceChannel : Client.ServiceLayer.ServiceReference1.IProductService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductServiceClient : System.ServiceModel.ClientBase<Client.ServiceLayer.ServiceReference1.IProductService>, Client.ServiceLayer.ServiceReference1.IProductService {
        
        public ProductServiceClient() {
        }
        
        public ProductServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CreateProduct(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName) {
            return base.Channel.CreateProduct(name, price, stock, minStock, maxStock, description, ImageURL, ImageName);
        }
        
        public System.Threading.Tasks.Task<bool> CreateProductAsync(string name, decimal price, int stock, int minStock, int maxStock, string description, string ImageURL, string ImageName) {
            return base.Channel.CreateProductAsync(name, price, stock, minStock, maxStock, description, ImageURL, ImageName);
        }
        
        public Client.ServiceLayer.ServiceReference1.Product FindProduct(int ID) {
            return base.Channel.FindProduct(ID);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceLayer.ServiceReference1.Product> FindProductAsync(int ID) {
            return base.Channel.FindProductAsync(ID);
        }
        
        public Client.ServiceLayer.ServiceReference1.Product FindProductByName(string name) {
            return base.Channel.FindProductByName(name);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceLayer.ServiceReference1.Product> FindProductByNameAsync(string name) {
            return base.Channel.FindProductByNameAsync(name);
        }
        
        public bool DeleteProduct(int id) {
            return base.Channel.DeleteProduct(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteProductAsync(int id) {
            return base.Channel.DeleteProductAsync(id);
        }
        
        public bool Update(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive) {
            return base.Channel.Update(ID, name, price, stock, minStock, maxStock, description, isActive);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateAsync(int ID, string name, decimal price, int stock, int minStock, int maxStock, string description, bool isActive) {
            return base.Channel.UpdateAsync(ID, name, price, stock, minStock, maxStock, description, isActive);
        }
        
        public Client.ServiceLayer.ServiceReference1.Product[] GetAllProducts() {
            return base.Channel.GetAllProducts();
        }
        
        public System.Threading.Tasks.Task<Client.ServiceLayer.ServiceReference1.Product[]> GetAllProductsAsync() {
            return base.Channel.GetAllProductsAsync();
        }
    }
}
