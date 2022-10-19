using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
       

        public BasketController(IBasketRepository basketRepository
                                )
        {
            this._basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //foreach (var item in basket.Items)
            //{
            //    var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            //    item.Price -= coupon.Amount;
            //}

            return Ok(await _basketRepository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.Accepted)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        //{
        //    // get existing basket with total price
        //    // create basketCheckoutEvent  --  set totalPrice on basketCheckout eventMessage
        //    // sent checkout event to rabbitmq
        //    // remove the basket

        //    // get existing basket with total price
        //    var basket = await _basketRepository.GetBasket(basketCheckout.UserName);
        //    if (basket == null)
        //    {
        //        return BadRequest();
        //    }

        //    // sent checkout event to rabbitmq
        //    var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        //    eventMessage.TotalPrice = basket.TotalPrice;
        //    await _publishEndpoint.Publish(eventMessage);


        //    // remove the basket
        //    await _basketRepository.DeleteBasket(basket.UserName);

        //    return Accepted();
        //}
    }
}
