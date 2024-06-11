namespace Shopping_Tutorial.Models
{
	public class CartItemModel
	{
		public long ProductId { get; set; }
		public string ProductName { get; set; } 
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		public decimal Total {  
			get { return Quantity * Price; }
		}

		public string Image {  get; set; }

		//gio ko co sp
		public CartItemModel() { }

		//gio hang co sp
		public CartItemModel(ProductModel product) {
			ProductId = product.Id;
			ProductName = product.Name;
			Price = product.Price;
			Quantity = 1;
			Image = product.Image;
		}

	}
}
