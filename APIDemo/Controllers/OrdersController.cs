using APIDemo.Dtos;
using APIDemo.Extensions;
using APIDemo.ResponseModule;
using AutoMapper;
using Core.Entities.OrderAggreagate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<ShippingAddress>(orderDto.Address);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order is null)
                return BadRequest(new ApiResponse(400,"Problem when creating your order!!"));

            return Ok(order);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order is null)
                return NotFound(new ApiResponse(404, "Order does not exist"));

            return Ok(_mapper.Map<OrderDetailsDto>(order));
        }

        [HttpGet("GetAllOrderForUser")]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetOrderForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);

            if (orders.Count == 0)
                return NotFound(new ApiResponse(404, "No Orders for this user yet"));

            return Ok(_mapper.Map<IReadOnlyList<OrderDetailsDto>>(orders));
        }

        [HttpGet("GetDeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
            => Ok(await _orderService.GetDeliveryMethodsAsync());
    }
}
