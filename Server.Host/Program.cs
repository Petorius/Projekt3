using System;
using System.ServiceModel;
using serviceToHost = Server.ServiceLayer;

namespace Server.Host {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Console based host");

            // Services to host
            using (ServiceHost productHost = new ServiceHost(typeof(serviceToHost.ProductService)))
            using (ServiceHost orderHost = new ServiceHost(typeof(serviceToHost.OrderService)))
            using (ServiceHost userHost = new ServiceHost(typeof(serviceToHost.UserService)))
            using (ServiceHost tagHost = new ServiceHost(typeof(serviceToHost.TagService)))
            using (ServiceHost loginHost = new ServiceHost(typeof(serviceToHost.LoginService))) {

                // Open the product host and start listening for oncoming calls
                productHost.Open();
                DisplayHostInfo(productHost);

                // Display status.
                Console.WriteLine("The service is ready");


                // Open the user host and start listening for oncoming calls
                userHost.Open();
                DisplayHostInfo(userHost);

                // Display status.
                Console.WriteLine("The service is ready");


                // Open the order host and start listening for oncoming calls.
                orderHost.Open();
                DisplayHostInfo(orderHost);
                // Display status.
                Console.WriteLine("The service is ready");


                // Open the tag host and start listening for oncoming calls.
                tagHost.Open();
                DisplayHostInfo(tagHost);
                // Display status.
                Console.WriteLine("The service is ready");


                // Open the log in host and start listening for oncoming calls.
                loginHost.Open();
                DisplayHostInfo(loginHost);
                // Display status.
                Console.WriteLine("The service is ready");


                // Keep the service running until key pressed.
                Console.WriteLine("Press key to terminate");
                Console.ReadLine();
            }
        }

        static void DisplayHostInfo(ServiceHost host) {
            Console.WriteLine();
            Console.WriteLine("*-- Host Info --*");

            foreach (System.ServiceModel.Description.ServiceEndpoint se in host.Description.Endpoints) {
                Console.WriteLine($"Address: {se.Address}");
                Console.WriteLine($"Binding: {se.Binding.Name}");
                Console.WriteLine($"Contract: {se.Contract.Name}");
            }
            Console.WriteLine("*---------------*");
        }
    }
}
