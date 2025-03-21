using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        User user = new User("Іван");
        Cart cart = new Cart();

        
        List<Product> products = new List<Product>
        {
            new Product("Ноутбук", 1500),
            new Product("Мишка", 30),
            new Product("Клавіатура", 70),
            new Product("Монітор", 300)
        };

        while (true)
        {
            Console.WriteLine("\n🔹 Виберіть дію:");
            Console.WriteLine("1️⃣ Показати список товарів");
            Console.WriteLine("2️⃣ Додати товар у кошик");
            Console.WriteLine("3️⃣ Переглянути кошик");
            Console.WriteLine("4️⃣ Оформити замовлення");
            Console.WriteLine("5️⃣ Вийти");

            Console.Write(" Ваш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowProducts(products);
                    break;
                case "2":
                    AddProductToCart(cart, products);
                    break;
                case "3":
                    Console.WriteLine(cart);
                    break;
                case "4":
                    user.PlaceOrder(cart);
                    break;
                case "5":
                    Console.WriteLine(" Дякуємо за використання нашої системи!");
                    return;
                default:
                    Console.WriteLine(" Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void ShowProducts(List<Product> products)
    {
        Console.WriteLine("\n Список доступних товарів:");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i]}");
        }
    }

    static void AddProductToCart(Cart cart, List<Product> products)
    {
        ShowProducts(products);
        Console.Write(" Введіть номер товару для додавання в кошик: ");

        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= products.Count)
        {
            cart.AddProduct(products[index - 1]);
            Console.WriteLine($" {products[index - 1].Name} додано в кошик!");
        }
        else
        {
            Console.WriteLine(" Невірний номер товару.");
        }
    }
}

class User
{
    public string Name { get; set; }
    public List<Order> Orders { get; set; }

    public User(string name)
    {
        Name = name;
        Orders = new List<Order>();
    }

    
    public void PlaceOrder(Cart cart)
    {
        if (cart.Products.Count == 0)
        {
            Console.WriteLine("Кошик порожній! Додайте товари перед оформленням замовлення.");
            return;
        }

        Order newOrder = new Order();
        newOrder.Products.AddRange(cart.Products);  
        Orders.Add(newOrder);  
        cart.Clear();  

        Console.WriteLine($"Замовлення оформлене! Загальна сума: {newOrder.GetTotalPrice():C}");
    }

    public override string ToString()
    {
        return $"{Name} - {Orders.Count} замовлень";
    }
}


class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price:C}";
    }
}


class Order
{
    public List<Product> Products { get; set; }
    public string Status { get; set; }  

    public Order()
    {
        Products = new List<Product>();
        Status = "Нове";
    }

    public decimal GetTotalPrice()
    {
        return Products.Count > 0 ? Products.Sum(p => p.Price) : 0;
    }

    public override string ToString()
    {
        return $"Замовлення ({Status}) - {GetTotalPrice():C}";
    }
}


class Cart
{
    public List<Product> Products { get; set; }

    public Cart()
    {
        Products = new List<Product>();
    }

    public void AddProduct(Product product) => Products.Add(product);
    public void RemoveProduct(Product product) => Products.Remove(product);
    public decimal GetTotalPrice() => Products.Sum(p => p.Price);

    
    public void Clear() => Products.Clear();

    public override string ToString()
    {
        return $"Кошик містить {Products.Count} товар(ів), сума: {GetTotalPrice():C}";
    }
}
