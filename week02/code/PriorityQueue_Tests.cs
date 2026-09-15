using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities, with the highest priority added last.
    // Expected Result: The item with the highest priority should be dequeued first.
    // Defect(s) Found: The loop does not check the last item in the queue, so the highest priority item can be skipped.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Add two items with the same highest priority.
    // Expected Result: The first item added with the highest priority should be dequeued first.
    // Defect(s) Found: Defect(s) Found: When two items have the same priority, the later item is selected instead of keeping FIFO order.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 10);
        priorityQueue.Enqueue("Second", 10);
        priorityQueue.Enqueue("Low", 1);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("First", result);
    }

    [TestMethod]
    // Scenario: Dequeue the highest priority item and then dequeue again.
    // Expected Result: The first call should return the highest priority item,
    // and the second call should return the next highest item.
    // Defect(s) Found: Dequeue returns the item but does not remove it from the queue.
    public void TestPriorityQueue_RemovesItem()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 10);
        priorityQueue.Enqueue("Second", 5);

        var firstResult = priorityQueue.Dequeue();
        var secondResult = priorityQueue.Dequeue();

        Assert.AreEqual("First", firstResult);
        Assert.AreEqual("Second", secondResult);
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty priority queue.
    // Expected Result: InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: No defect found. The correct exception and message are returned when the queue is empty.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}