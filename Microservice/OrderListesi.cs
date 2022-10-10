using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Core.Domain;
using Microservice;

namespace Microservice
{
    public class Orderlistesi
    {
         
        
        public List<Order> Doldur()
        {

            

            IEnumerable<Order> siparisler = new List<Order>()
            {
                    new Order {Id = 1,BrandId= 0,Price = 19,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
                    new Order {Id = 2,BrandId= 5,Price = 22,StoreName = "kutlu",CustomerName = "Malik",CreatedOn = new DateTime(2021,11,11),Status = (OrderStatus)20},
                    new Order {Id = 3,BrandId= 5,Price =45,StoreName = "kemal",CustomerName = "eray",CreatedOn = new DateTime(2021,10,11),Status = (OrderStatus)30},
                    new Order {Id = 4,BrandId= 5,Price = 19,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,09,11),Status = (OrderStatus)40},
                    new Order {Id = 5,BrandId= 0,Price = 2,StoreName = "selcuk",CustomerName = "Malik",CreatedOn = new DateTime(2021,08,11),Status = (OrderStatus)50},
                    new Order {Id = 6,BrandId= 5,Price = 87,StoreName = "kemal",CustomerName = "eray",CreatedOn = new DateTime(2021,07,11),Status = (OrderStatus)60},
                    new Order {Id = 7,BrandId= 5,Price = 96,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)10},
                    new Order {Id = 8,BrandId= 0,Price = 4,StoreName = "kutlu",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)20},
                    new Order {Id = 9,BrandId= 5,Price = 97,StoreName = "kemal",CustomerName = "eray",CreatedOn = new DateTime(2022,12,11),Status = (OrderStatus)30},
                    new Order {Id = 10,BrandId= 5,Price = 21,StoreName = "selcuk",CustomerName = "Malik",CreatedOn = new DateTime(2019,12,11),Status = (OrderStatus)40},
                    new Order {Id = 11,BrandId= 5,Price = 23,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)50},
                    new Order {Id = 12,BrandId= 5,Price = 25,StoreName = "selcuk",CustomerName = "ahmet",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)60},
                    new Order {Id = 13,BrandId= 5,Price = 9,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2015,12,11),Status = (OrderStatus)10},
                    new Order {Id = 14,BrandId= 5,Price = 47,StoreName = "kutlu",CustomerName = "ahmet",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)20},
                    new Order {Id = 15,BrandId= 5,Price = 98,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2012,12,11),Status = (OrderStatus)30},
                    new Order {Id = 16,BrandId= 5,Price = 10,StoreName = "kemal",CustomerName = "Malik",CreatedOn = new DateTime(2021,12,11),Status = (OrderStatus)40}
            };

            foreach(var x in siparisler)
            {
                if(x.BrandId == 0)
                {
                    siparisler = siparisler.Where(o => (o.BrandId != 0));
                }
            }
            return siparisler.ToList();
        }
        
        
    }
}
