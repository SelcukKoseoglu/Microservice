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
using Microservice;




namespace Microservice.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _orderContext;

        public OrderController(OrderContext context)
        {
            _orderContext = context ?? throw new ArgumentNullException(nameof(context));
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(OrderFilterModel<Order>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1,string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = await GetItemsByIdsAsync(ids);

                if (!items.Any())
                {
                    return BadRequest("ids value invalid");
                }
                return Ok(items);
            }
            var totalItems = await _orderContext.Orders.LongCountAsync();

            var itemsOnPage = await _orderContext.Orders.OrderBy(o => o.Id)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync();

         // itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            var model = new OrderFilterModel<Order>(pageNumber, pageSize,totalItems,itemsOnPage);
            return Ok(model);
        }

        private async Task<List<Order>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<Order>();
            }
            var idsToSelect = numIds.Select(id => id.Value);
            var items = await _orderContext.Orders.Where(oi => idsToSelect.Contains(oi.Id)).ToListAsync();
         // items = ChangeUriPlaceholder(items);
            return items;
        }


        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Order),(int)HttpStatusCode.OK)]

        public async Task<ActionResult<Order>> ItemByIdAsync(int id)
        {
            if(id<= 0)
            {
                return BadRequest();
            }
            var item = await _orderContext.Orders.SingleOrDefaultAsync(oi => oi.Id == id);
            if(item != null)
            {
                return item;
            }
            return NotFound();
        }

        [HttpGet]
        [Route("items/withstorename/{StoreName}")]
        [ProducesResponseType(typeof(OrderFilterModel<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderFilterModel<Order>>> ItemsWithNameAsync(string StoreName ,[FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var totalItems = await _orderContext.Orders
                .Where(o => o.StoreName.StartsWith(StoreName))
                .LongCountAsync();
            var itemsOnPage = await _orderContext.Orders
                .Where(o => o.StoreName.StartsWith(StoreName))
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync();

            return new OrderFilterModel<Order>(pageNumber,pageSize,totalItems,itemsOnPage);
        }

        [HttpGet]
        [Route("items/withcustomername/{SearchText}")]
        [ProducesResponseType(typeof(OrderFilterModel<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderFilterModel<Order>>> ItemsWithNameAsync_2(string SearchText, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var totalItems = await _orderContext.Orders
                .Where(o => o.CustomerName.StartsWith(SearchText))
                .LongCountAsync();
            var itemsOnPage = await _orderContext.Orders
                .Where(o => o.CustomerName.StartsWith(SearchText))
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync();

            return new OrderFilterModel<Order>(pageNumber, pageSize, totalItems, itemsOnPage);
        }

        [HttpGet]
        [Route("items/withmindate/{StartDate}")]
        [ProducesResponseType(typeof(OrderFilterModel<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderFilterModel<Order>>> ItemsWithDateAsync(DateTime StartDate, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var totalItems = await _orderContext.Orders
                .Where(o => o.CreatedOn>=(StartDate))
                .LongCountAsync();
            var itemsOnPage = await _orderContext.Orders
                .Where(o => o.CreatedOn>=(StartDate))
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync();

            return new OrderFilterModel<Order>(pageNumber, pageSize, totalItems, itemsOnPage);
        }

        [HttpGet]
        [Route("items/withmaxdate/{EndDate}")]
        [ProducesResponseType(typeof(OrderFilterModel<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderFilterModel<Order>>> ItemsWithDateAsync_2(DateTime EndDate, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 0)
        {
            var totalItems = await _orderContext.Orders
                .Where(o => o.CreatedOn < (EndDate))
                .LongCountAsync();
            var itemsOnPage = await _orderContext.Orders
                .Where(o => o.CreatedOn < (EndDate))
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync();

            return new OrderFilterModel<Order>(pageNumber, pageSize, totalItems, itemsOnPage);
        }

        

        

        [HttpGet]
        [Route("items/statuses/{statusler}")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> ItemsWithNameAsync__1(string statusler)
        {
            int a, b, c, d, e, f;
            string[] state = statusler.Split(',');
            if(state.Length == 1)
            {
                a = Convert.ToInt32(state[0]);
                return await _orderContext.Orders.Where(o => (o.Status == (OrderStatus)a)).ToListAsync();

            }
            if (state.Length == 2)
            {
                a = Convert.ToInt32(state[0]);
                b = Convert.ToInt32(state[1]);
                return await _orderContext.Orders.Where(o => (o.Status == (OrderStatus)a) || (o.Status == (OrderStatus)b)).ToListAsync();

            }
            if (state.Length == 3)
            {
                a = Convert.ToInt32(state[0]);
                b = Convert.ToInt32(state[1]);
                c = Convert.ToInt32(state[2]);
                return await _orderContext.Orders.Where(o => (o.Status == (OrderStatus)a) || (o.Status == (OrderStatus)b) || (o.Status == (OrderStatus)c)).ToListAsync();

            }
            if (state.Length == 4)
            {
                a = Convert.ToInt32(state[0]);
                b = Convert.ToInt32(state[1]);
                c = Convert.ToInt32(state[2]);
                d = Convert.ToInt32(state[3]);
                return await _orderContext.Orders.Where(o => (o.Status == (OrderStatus)a) || (o.Status == (OrderStatus)b) || (o.Status == (OrderStatus)c) || (o.Status == (OrderStatus)d)).ToListAsync();

            }

            if (state.Length == 5)
            {
                a = Convert.ToInt32(state[0]);
                b = Convert.ToInt32(state[1]);
                c = Convert.ToInt32(state[2]);
                d = Convert.ToInt32(state[3]);
                e = Convert.ToInt32(state[4]);
                return await _orderContext.Orders.Where(o => (o.Status == (OrderStatus)a)|| (o.Status == (OrderStatus)b)|| (o.Status == (OrderStatus)c) || (o.Status == (OrderStatus)d)|| (o.Status == (OrderStatus)e)).ToListAsync();

            }
            if (state.Length == 6)
            {
                a = Convert.ToInt32(state[0]);
                b = Convert.ToInt32(state[1]);
                c = Convert.ToInt32(state[2]);
                d = Convert.ToInt32(state[3]);
                e = Convert.ToInt32(state[4]);
                f = Convert.ToInt32(state[5]);
                return await _orderContext.Orders.Where(o => (o.Status == (OrderStatus)a) || (o.Status == (OrderStatus)b) || (o.Status == (OrderStatus)c) || (o.Status == (OrderStatus)d) || (o.Status == (OrderStatus)e) || (o.Status == (OrderStatus)f)).ToListAsync();

            }



            return BadRequest();            
            
            
        }


        [HttpGet]
        [Route("items/orderby/storename")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> ItemsWithNameAsync_3()
        {
            return await _orderContext.Orders.OrderBy(oi=>oi.StoreName).ToListAsync();
        }

        [HttpGet]
        [Route("items/orderby/customername")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> ItemsWithNameAsync_2()
        {
            return await _orderContext.Orders.OrderBy(oi => oi.CustomerName).ToListAsync();
        }

        [HttpGet]
        [Route("items/orderby/price")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> ItemsWithNameAsync_1()
        {
            return await _orderContext.Orders.OrderBy(oi => oi.Price).ToListAsync();
        }

        [HttpGet]
        [Route("items/orderby/createdon")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> ItemsWithNameAsync_4()
        {
            return await _orderContext.Orders.OrderBy(oi => oi.CreatedOn).ToListAsync();
        }

        [HttpGet]
        [Route("items/orderby/brandid")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> ItemsWithNameAsync_5()
        {
            return await _orderContext.Orders.OrderBy(oi => oi.BrandId).ToListAsync();
        }

        [HttpGet]
        [Route("items/orderby/id")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> ItemsWithNameAsync_6()
        {
            return await _orderContext.Orders.OrderBy(oi => oi.Id).ToListAsync();
        }

        [HttpGet]
        [Route("items/orderby/status")]
        [ProducesResponseType(typeof(List<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Order>>> ItemsWithNameAsync_7()
        {
            return await _orderContext.Orders.OrderBy(oi => oi.Status).ToListAsync();
        }
        public Order siparis1 = new Order();
        
        




    }
}
