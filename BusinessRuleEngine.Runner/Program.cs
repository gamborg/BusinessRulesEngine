using BusinessRulesEngine;
using BusinessRulesEngine.Models;
using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BusinessRulesEngine.Data;
using BusinessRulesEngine.Interfaces;

namespace BusinessRuleEngine.Runner
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<BusinessRulesEngineDbContext>(options => {
                    options.UseInMemoryDatabase(databaseName: "BusinessRulesEngineDb");
                })
                .AddMediatR(typeof(PaymentHandler))
                .AddSingleton<IDbContext, BusinessRulesEngineDbContext>()
                .AddSingleton<IDateTime, ConcreteDateTime>()
                .BuildServiceProvider();

            var handler = new PaymentHandler(serviceProvider);

            var input = "";
            while (input != "q")
            {
                Console.Clear();
                PrintOptions();
                input = Console.ReadLine();
                var payment = new Payment()
                {
                    Agent = Guid.NewGuid(),
                    Customer = new Customer
                    {
                        Name = "Hans Hansen",
                        Email = "hans@hansen.dk"
                    },
                    OrderId = Guid.NewGuid(),
                    Products = new List<Product>()
                };

                switch(input) 
                {
                    case "1":
                        payment.Products.Add(new Product() { Name = "Physical product", ProductType = "PhysicalProduct" });
                        handler.Handle("physical", payment);
                        break;
                    case "2":
                        payment.Products.Add(new Product() { Name = "Test book", ProductType = "Book" });
                        handler.Handle("book", payment);
                        break;
                    case "3":
                        handler.Handle("membership", payment);
                        break;
                    case "4":
                        handler.Handle("membershipupgrade", payment);
                        break;
                    case "5":
                        payment.Products.Add(new Product() { Name = "Test video", ProductType = "Video" });
                        handler.Handle("video", payment);
                        break;
                    case "6":
                        payment.Products.Add(new Product() { Name = "Learning to Ski", ProductType = "Video" });
                        handler.Handle("video", payment);
                        break;
                    case "q":
                    case "Q":
                        return;
                }

                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }

        static void PrintOptions()
        {
            Console.WriteLine("What do you wan't to test?");
            Console.WriteLine("1 - Physical product");
            Console.WriteLine("2 - Book product");
            Console.WriteLine("3 - Membership, activate");
            Console.WriteLine("4 - Membership, upgrade");
            Console.WriteLine("5 - Video, random title");
            Console.WriteLine("6 - Video, Learning to Ski");
            Console.WriteLine("q - To quit the program");
        }
    }
}
