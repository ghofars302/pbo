using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekPBO {
    internal class Payment {
        public PaymentType Type;
        public string Email;
        public string Info;
    }

    internal enum PaymentType {
        None,
        CreditCard,
        PayPal
    }
}
