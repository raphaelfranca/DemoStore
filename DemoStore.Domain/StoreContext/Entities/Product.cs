
using System;

namespace Demostore.Domain.StoreContext.Entities
{
    public class Product
    {
        public Product(
            string title,
            string description,
            string image,
            decimal price,
            decimal quantity)
        {
            Title = title;
            Description = description;
            Image = image;
            Price = price;
            QuamtityOnHand = quantity;
        }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public decimal QuamtityOnHand { get; private set; }

        public override string ToString()
        {
            return Title;
        }

    }
}