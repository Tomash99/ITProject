using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapsmvc.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string User { get; set; } //ПІБ ПОКУПЦІ
        public string Address { get; set; } // АДРЕСА ПОКУПЦЯ
        public string ContactPhone { get; set; } //телефон покупця
        public int PhoneId { get; set; } // посилання на звязану модель телефону 
        public Phone Phone { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   OrderId == order.OrderId &&
                   User == order.User &&
                   Address == order.Address &&
                   ContactPhone == order.ContactPhone &&
                   PhoneId == order.PhoneId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, User, Address, ContactPhone, PhoneId);
        }
    }
}
