using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

class Customer
{
    public int CardNumber { get; set; }
    public decimal Money { get; set; }
    public int PurchasedItemsCount { get; set; } = 0;
    public decimal TotalSpent { get; set; } = 0;
}

class Program
{
    static void Main()
    {
        Random rnd = new Random();

        Console.Write("Введіть кількість покупців: ");
        if (!int.TryParse(Console.ReadLine(), out int customerCount) || customerCount <= 0)
        {
            Console.WriteLine("error");
            return;
        }

        List<Product> products = new List<Product>()
        {
            new Product { Name = "Хліб", Price = 30, Quantity = 15 },
            new Product { Name = "Молоко", Price = 45, Quantity = 10 },
            new Product { Name = "Сир", Price = 120, Quantity = 8 },
            new Product { Name = "Яблука", Price = 25, Quantity = 25 },
            new Product { Name = "Шоколад", Price = 50, Quantity = 12 }
        };

        decimal storeProfit = 0;

        List<Customer> customers = new List<Customer>();
        for (int i = 0; i < customerCount; i++)
        {
            customers.Add(new Customer
            {
                CardNumber = rnd.Next(1, 1001),
                Money = rnd.Next(1000, 10001)
            });
        }

        foreach (var customer in customers)
        {
            while (customer.Money > 0)
            {
                bool hasAnyProducts = false;
                foreach (var p in products)
                {
                    if (p.Quantity > 0) { hasAnyProducts = true; break; }
                }
                if (!hasAnyProducts) break;

                List<Product> availableProducts = new List<Product>();
                foreach (var p in products)
                {
                    if (p.Quantity > 0 && customer.Money >= p.Price)
                    {
                        availableProducts.Add(p);
                    }
                }

                if (availableProducts.Count == 0) break;

                Product chosenProduct = availableProducts[rnd.Next(availableProducts.Count)];

                chosenProduct.Quantity--;
                customer.Money -= chosenProduct.Price;
                customer.PurchasedItemsCount++;
                customer.TotalSpent += chosenProduct.Price;
                storeProfit += chosenProduct.Price;
            }
        }

        Console.WriteLine("РЕЗУЛЬТАТИ");
        Console.WriteLine($"Прибуток магазину: {storeProfit} грн");

        if (customers.Count > 0)
        {
            Customer maxItemsCustomer = customers[0];
            Customer maxSpentCustomer = customers[0];

            foreach (var c in customers)
            {
                if (c.PurchasedItemsCount > maxItemsCustomer.PurchasedItemsCount)
                {
                    maxItemsCustomer = c;
                }
                if (c.TotalSpent > maxSpentCustomer.TotalSpent)
                {
                    maxSpentCustomer = c;
                }
            }

            Console.WriteLine($"Покупець з максимальною кількістю товарів: картка №{maxItemsCustomer.CardNumber} (придбав {maxItemsCustomer.PurchasedItemsCount} шт.)");
            Console.WriteLine($"Покупець, що витратив найбільше грошей: картка №{maxSpentCustomer.CardNumber} (витратив {maxSpentCustomer.TotalSpent} грн)");
        }
    }
}