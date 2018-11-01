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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IProductService")]
    public interface IProductService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/CreateProduct", ReplyAction="http://tempuri.org/IProductService/CreateProductResponse")]
        void CreateProduct(string name, double price, int stock, int minStock, int maxStock, string description, int rating);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductService/CreateProduct", ReplyAction="http://tempuri.org/IProductService/CreateProductResponse")]
        System.Threading.Tasks.Task CreateProductAsync(string name, double price, int stock, int minStock, int maxStock, string description, int rating);
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
        
        public void CreateProduct(string name, double price, int stock, int minStock, int maxStock, string description, int rating) {
            base.Channel.CreateProduct(name, price, stock, minStock, maxStock, description, rating);
        }
        
        public System.Threading.Tasks.Task CreateProductAsync(string name, double price, int stock, int minStock, int maxStock, string description, int rating) {
            return base.Channel.CreateProductAsync(name, price, stock, minStock, maxStock, description, rating);
        }
    }
}
