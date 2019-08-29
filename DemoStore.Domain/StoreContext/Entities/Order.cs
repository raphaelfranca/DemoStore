
using System;
using System.Collections.Generic;
using System.Linq;
using DemoStore.Domain.StoreContext.Enums;
using FluentValidator;

namespace DemoStore.Domain.StoreContext.Entities
{
    public class Order : Notifiable
    {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;
        public Order(Customer customer)
        {
            Customer = customer;            
            CreateDate = DateTime.Now;
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

    
        public void AddItem(Product product, decimal quantity)
        {
            if(quantity > product.QuantityOnHand)
                AddNotification("OrderItem", $"Produto {product.Title} não tem {quantity} itens em estoque.");

            var item = new OrderItem(product, quantity);
           _items.Add(item);
        }


        public void AddDelivery(Delivery delivery)
        {
            _deliveries.Add(delivery);
        }

        // Criar um pedido
        public void Place()
        {
            // validar
            // gera numero do pedido
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

        }

        // Pagar um pedido
        public void Pay()
        {
            Status = EOrderStatus.Paid;                     
        }

        // enviar um pedido
        public void Ship()
        {
            var deliveries = new List<Delivery>();            
            var count = 1;
            // quebra as entregas
            foreach (var item in _items)
            {
                if(count == 5)
                {
                    count = 1;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }
                count++;                
            }
            // envia todas as entregas
            deliveries.ForEach(x => x.Ship());
            deliveries.ForEach(x => _deliveries.Add(x));
        }

        // cancelar um pedido
        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
            _deliveries.ToList().ForEach(x => x.Cancel());

        }

    }
}