using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basketService;

        public CartModel(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public BasketModel Cart { get; set; } = new BasketModel();        

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketService.GetBasket("sohit");            

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            string userName = "sohit";
            var basket=await _basketService.GetBasket(userName);
            var item = basket.Items.SingleOrDefault(a => a.ProductId == productId);
            basket.Items.Remove(item);
            var basketUpdated=await _basketService.UpdateBasket(basket);
            return RedirectToPage();
        }
    }
}