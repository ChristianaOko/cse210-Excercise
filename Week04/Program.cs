using System;
using System.Collections.Generic;

// Address class
class Address
{
    private string _streetAddress;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        return _country.ToUpper() == "USA";
    }

    public string GetFullAddress()
    {
        return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
    }
}

// Customer class
class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string GetName()
    {
        return _name;
    }

    public bool IsInUSA()
    {
        return _address.IsInUSA();
    }

    public string GetAddress()
    {
        return _address.GetFullAddress();
    }
}

// Product class
class Product
{
    private string _name;
    private string _productId;
    private decimal _price;
    private int _quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public decimal CalculateTotalCost()
    {
        return _price * _quantity;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetProductId()
    {
        return _productId;
    }
}

// Order class
class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal CalculateTotalPrice()
    {
        decimal totalProductsCost = 0;
        foreach (Product product in _products)
        {
            totalProductsCost += product.CalculateTotalCost();
        }

        // Add shipping cost based on customer location
        decimal shippingCost = _customer.IsInUSA() ? 5 : 35;
        
        return totalProductsCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string packingLabel = "PACKING LABEL:\n";
        foreach (Product product in _products)
        {
            packingLabel += $"Product: {product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return packingLabel;
    }

    public string GetShippingLabel()
    {
        return $"SHIPPING LABEL:\nCustomer: {_customer.GetName()}\n{_customer.GetAddress()}";
    }
}

// Program class
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Online Ordering Program\n");

        // Create first order
        Address address1 = new Address("123 Main St", "Seattle", "WA", "USA");
        Customer customer1 = new Customer("John Smith", address1);
        
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "TECH001", 999.99m, 1));
        order1.AddProduct(new Product("Mouse", "TECH002", 24.95m, 2));
        order1.AddProduct(new Product("Keyboard", "TECH003", 59.99m, 1));

        // Display results for first order
        Console.WriteLine("Order 1:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.CalculateTotalPrice()}\n");

        // Create second order
        Address address2 = new Address("456 Queen St", "Toronto", "Ontario", "Canada");
        Customer customer2 = new Customer("Jane Doe", address2);
        
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Headphones", "AUDIO001", 149.99m, 1));
        order2.AddProduct(new Product("Phone Case", "ACC001", 19.99m, 3));

        // Display results for second order
        Console.WriteLine("Order 2:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.CalculateTotalPrice()}");
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}