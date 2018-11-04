using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using serviceToHost = Server.ServiceLayer;

namespace Server.Host {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("Console based host");

            using (ServiceHost serviceHost = new ServiceHost(typeof(serviceToHost.ProductService))) {
                // Open the host and start listening for oncoming calls
                serviceHost.Open();
                DisplayHostInfo(serviceHost);

                //Keep the service running until key pressed
                Console.WriteLine("The service is ready");
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
