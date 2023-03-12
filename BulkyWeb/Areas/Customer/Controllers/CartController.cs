using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers {

	[Area("customer")]
	[Authorize]
	public class CartController : Controller {

		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCartVM ShoppingCartVM { get; set; }
		public CartController(IUnitOfWork unitOfWork) {
			_unitOfWork = unitOfWork;
		}


		public IActionResult Index() {

			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM = new() {
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
				includeProperties: "Product")
			};

			foreach(var cart in ShoppingCartVM.ShoppingCartList) {
				double price = GetPriceBasedOnQuantity(cart);
				ShoppingCartVM.OrderTotal += (price * cart.Count);
			}

			return View(ShoppingCartVM);
		}

		private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart) {
			if (shoppingCart.Count <= 50) {
				return shoppingCart.Product.Price;
			}
			else {
				if (shoppingCart.Count <= 100) {
					return shoppingCart.Product.Price50;
				}
				else {
					return shoppingCart.Product.Price100;
				}
			}
		}
	}
}
