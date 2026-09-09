/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Create a queue with an invalid maximum size.
        // Expected Result: The maximum size should default to 10.
        Console.WriteLine("Test 1");
        var cs = new CustomerService(0);
        Console.WriteLine(cs);
        // Defect(s) Found: No defect found. The invalid maximum size correctly defaults to 10.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Create a queue with a valid maximum size.
        // Expected Result: The queue should use the size provided.
        Console.WriteLine("Test 2");
        cs = new CustomerService(3);
        Console.WriteLine(cs);
        // Defect(s) Found: No defect found. The valid maximum size is stored correctly.

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Try to serve a customer when the queue is empty.
        // Expected Result: An error message should be displayed.
        Console.WriteLine("Test 3");
        cs = new CustomerService(3);
        cs.ServeCustomer();
        // Defect(s) Found: ServeCustomer did not check if the queue was empty before accessing index 0.

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Add customers until the queue reaches the maximum size.
        // Expected Result: The queue should stop accepting customers when it is full.
        Console.WriteLine("Test 4");
        cs = new CustomerService(2);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        Console.WriteLine(cs);
        cs.AddNewCustomer();
        // Defect(s) Found: The full queue condition used > instead of >=, allowing one extra customer.

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Add two customers and serve one of them.
        // Expected Result: The first customer added should be the first customer served.
        Console.WriteLine("Test 5");
        cs = new CustomerService(2);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.ServeCustomer();
        Console.WriteLine(cs);
        // Defect(s) Found: ServeCustomer removed the first customer before saving and displaying it,
        // causing the next customer in the queue to be shown instead.
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        // The queue is full when the current count reaches the maximum size.
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // Check if there is a customer available before trying to access the queue.
        if (_queue.Count == 0) {
            Console.WriteLine("No Customers in Queue.");
            return;
        }

        // A queue follows FIFO, so the customer at index 0 should be served first.
        var customer = _queue[0];

        // Remove the customer after saving the information that will be displayed.
        _queue.RemoveAt(0);

        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}