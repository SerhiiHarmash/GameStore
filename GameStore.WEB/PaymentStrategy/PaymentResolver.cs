using System.Collections.Generic;
using GameStore.Models.Enums;

namespace GameStore.WEB.PaymentStrategy
{
    internal class PaymentResolver
    {
        private readonly Dictionary<PaymentType, IPayment> _payments;

        public PaymentResolver()
        {
            _payments = new Dictionary<PaymentType, IPayment>()
            {
                {PaymentType.PayThroughBank, new Bank() },
                {PaymentType.PayThroughTerminal, new Terminal()},
                {PaymentType.PayThroughVisa, new Visa() }
            };
        }

        internal IPayment CreatePaymentInstance(PaymentType paymentType)
        {
            return _payments[paymentType];
        }
    }
}