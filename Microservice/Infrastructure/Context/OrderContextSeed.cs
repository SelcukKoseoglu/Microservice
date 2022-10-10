using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Core.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Collections;
using Microservice;



namespace Microservice.Infrastructure.Context
{
    public class OrderContextSeed
    {


        public async Task SeedAsync(OrderContext context, IWebHostEnvironment env, ILogger<OrderContextSeed> logger)
        {
            var policy = Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(exception, "....");
                });
            var setupDirPath = Path.Combine(env.ContentRootPath, "Infrastructure", "Setup", "SeedFiles");
            await policy.ExecuteAsync(() => ProcessSeeding(context, setupDirPath, logger));
        }
        private async Task ProcessSeeding(OrderContext context, string setupDirPath, ILogger logger)
        {
            Orderlistesi liste = new Orderlistesi();
            await context.Orders.AddRangeAsync(liste.Doldur());
            //await context.Orders.AddRangeAsync(GetOrderItemsFromFile(setupDirPath, context));
            await context.SaveChangesAsync();

        }
        //IEnumerable<Order> GetOrderItemsFromFile(string contentPath, OrderContext context)
        //{
        //    IEnumerable<Order> GetPreconfiguredItems()
        //    {

        //        return new List<Order>()
        //        {
        //            new Order {Id = 1,BrandId= 0,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 2,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 3,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 4,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 5,BrandId= 0,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 6,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 7,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 8,BrandId= 0,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 9,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 10,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 11,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 12,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 13,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 14,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 15,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
        //            new Order {Id = 16,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10}
        //        };
        //    }





            //return GetPreconfiguredItems();




            //string fileName = Path.Combine(contentPath, "OrderItem.txt");


            //var fileContent = File.ReadAllLines(fileName).Select(i => i.Split(',')).Select(i => new Order()
            //{
                


            //    Id = Convert.ToInt32(i[0]),
            //    BrandId = Convert.ToInt32(i[1]),
            //    Price = Convert.ToDecimal(i[2]),
            //    StoreName = Convert.ToString(i[3]),
            //    CustomerName = Convert.ToString(i[4]),
            //    CreatedOn = Convert.ToDateTime(i[5]),


            //}) ;
            //return fileContent;
        //}



        






    }

}

















