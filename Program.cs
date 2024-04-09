using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using static Module7Homework.Program;

namespace Module7Homework
{
    //Мне не понравилось задание, где нет конкретного условия. Вот у тебя есть что нужно использовать - сделай что-то. Надеюсь в дальнейшем подобного не будет


    public class Program
    {
        static void Main(string[] args)
        {
            Product memorySlot = new Product("Модуль памяти", "Та самая штучка", 120);
            Product goldenApple = new Product("Золотое яблоко", "Может оно и не такое вкусное", 999);
            goldenApple.WriteProductInfo();
            HomeDelivery homeDelivery = new HomeDelivery("Г. Город, ул.Безымянная, д.28");
            var order = MakeHomeOrder(homeDelivery);
            order.AddProductToOrder(goldenApple);
            order.AddProductToOrder(goldenApple);
            order.AddProductToOrder(memorySlot);

        }

        public static Order<HomeDelivery> MakeHomeOrder(HomeDelivery delivery)
        {
            Order<HomeDelivery> order = new Order<HomeDelivery>();

            return order;
        } //Для заказа в пункт выдачи свой метод сделать, можно так же унаследовать от общего

        public abstract class Delivery
        {
            public string Adress { get; private set; }

            public Delivery(string adress)
            {
                Adress = adress;
            }

            public abstract void WriteDeliveryPlace();
        }

        public class HomeDelivery : Delivery
        {
            public HomeDelivery(string adress) : base(adress) { }
            public override void WriteDeliveryPlace()
            {
                Console.WriteLine("Выбранный тип доставки: На дом. Адресс: " + Adress);
            }
        }

        public class PickPointDelivery : Delivery
        {
            public PickPointDelivery(string adress) : base(adress) { }
            public override void WriteDeliveryPlace()
            {
                Console.WriteLine("Выбранный тип доставки: В пункт выдачи. Адресс: " + Adress);
            }
        }




    }

    public static class ProductExtension
    {
        public static void WriteProductInfo(this Product product)
        {
            Console.WriteLine("Товар номер: " + product.Id);
            Console.WriteLine("Название товара: " + product.Name);
            Console.WriteLine("Описание товара: " + product.Description);
            Console.WriteLine("Цена: " + product.Price);
        }
    }

    

    public class Order<TDelivery> where TDelivery : Delivery
    {
        public Guid Id { get; private set; }
        public List<Product> Products = new List<Product>();
        
        Product[] products; //далее представлен индексатор - на мой взгляд штука почти во всем проигрывает коллекции, но как вариант напишу.
        public Product this[int index]
        {
            get => products[index];
            set => products[index] = value;
        }


        public TDelivery Delivery;

        public Order()
        {
            Id = Guid.NewGuid();
        }

        public void WriteDelivery(TDelivery delivery)
        {
            delivery.WriteDeliveryPlace();
        }

        public void AddProductToOrder(Product product)
        {
            Products.Add(product);
        }
    }

    public class DistantOrder<TDelivery>:Order<TDelivery> where TDelivery : Delivery
    {
        ///альт логика для заказов из-за рубежа
    }


    public class Product
    {
        public string Name;
        public string Description;
        public uint Price
        {
            get
            {
                return Price;
            }
            set
            {
                if (Price < 10)
                {
                    Console.WriteLine("Минимальная цена товара - 10 рублей");
                }
                else
                {
                    Price = value;
                }
            }
        }
        public Guid Id { get; private set; }

        public Product(string name, string description, uint price)
        {
            Name = name;
            Description = description;
            Price = price;
            Id = Guid.NewGuid();
        }

        public static Product operator +(Product product, uint cost)
        {
            product.Price += cost;
            return product;
        }
    }
}