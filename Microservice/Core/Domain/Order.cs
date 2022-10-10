using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microservice.Core.Application.ViewModels;
using Microservice.Core.Domain;
using Microservice.Infrastructure;
using Microsoft.Extensions.Options;
using System.Collections;

namespace Microservice.Core.Domain
{
 
    public class Order
    {
        public int Id { get; set; } 
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public string StoreName { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public OrderStatus Status { get; set; }

        

        
    }
    public enum OrderStatus
    {
        Created = 10,
        InProgress = 20,
        Failed = 30,
        Completed = 40,
        Canceled = 50,
        Returned = 60
    }

     
}
