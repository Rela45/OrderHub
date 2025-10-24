namespace ProgettoOrderHub.MethodFactory
{
    using Domain;
    public interface FactoryMethod
    {
        public enum PaymentType
        {
            Carta,
            Paypal,
            Googlepay
        }

        public enum NotifierType
        {
            Email,
            Sms
        }
        public interface IPayment
        {
            void ProcessPayment(Order order);
        }
        public interface INotifier
        {
            void Notify(Order order, string message);
        }

        public class CartaPayment : IPayment
        {
            public void ProcessPayment(Order order)
            {
                Console.WriteLine($"Order {order.Id} pagato con carta di credito: totale {order.Total}");
            }
        }
        public class PayPal : IPayment
        {
            public void ProcessPayment(Order order)
            {
                Console.WriteLine($"Order {order.Id} pagato con  PayPal: totale {order.Total}");
            }
        }
        public class GooglePay : IPayment
        {
            public void ProcessPayment(Order order)
            {
                Console.WriteLine($"Ordine {order.Id} pagato con GooglePay: totale {order.Total}");
            }
        }

        public static class PaymentFactory
        {
            public static IPayment CreatePayment(PaymentType type)
            {
                return type switch
                {
                    PaymentType.Carta => new CartaPayment(),
                    PaymentType.Paypal => new PayPal(),
                    PaymentType.Googlepay => new GooglePay(),
                    _ => throw new NotImplementedException("Tipo di pagamento non supportato")
                };
            }
        }
        public class EmailNotifier : INotifier
        {
            public void Notify(Order order, string message)
            {
                Console.WriteLine($"[Email] Oridine {order.Id}: {message}");
            }
        }

        public class SmsNotifier : INotifier
        {
            public void Notify(Order order, string message)
            {
                Console.WriteLine($"[Sms] Ordine {order.Id}: {message}");
            }
        }

        public static class NotifierFactory
        {
            public static INotifier CreateNotifier(NotifierType type)
            {
                return type switch
                {
                    NotifierType.Email => new EmailNotifier(),
                    NotifierType.Sms => new SmsNotifier(),
                    _ => throw new NotImplementedException("Notifier non supportato")
                };
            }
        }

    }
}