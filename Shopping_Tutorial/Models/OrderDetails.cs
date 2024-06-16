﻿namespace Shopping_Tutorial.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }
		public int UserName { get; set; }
		public string OrderCode { get; set; }
		public int ProductId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
