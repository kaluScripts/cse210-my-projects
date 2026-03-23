class Program
{
    static void Main(string[] args)
    {
        // --- Order 1: Domestic (USA) customer ---
        Address address1 = new Address("742 Evergreen Terrace", "Springfield", "IL", "USA");
        Customer customer1 = new Customer("Homer Simpson", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Wireless Mouse", "WM-101", 29.99, 2));
        order1.AddProduct(new Product("USB-C Hub", "UCH-305", 49.99, 1));
        order1.AddProduct(new Product("Laptop Stand", "LS-210", 24.99, 1));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Order Total (incl. ${5} domestic shipping): ${order1.GetTotalCost():F2}");
        Console.WriteLine(new string('-', 40));

        // --- Order 2: International customer ---
        Address address2 = new Address("10 Downing Street", "London", "England", "UK");
        Customer customer2 = new Customer("James Moriarty", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Mechanical Keyboard", "MK-450", 89.99, 1));
        order2.AddProduct(new Product("Monitor Light Bar", "MLB-88", 39.99, 2));

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Order Total (incl. ${35} international shipping): ${order2.GetTotalCost():F2}");
        Console.WriteLine(new string('-', 40));
    }
}
