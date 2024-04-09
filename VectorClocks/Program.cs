namespace VectorClocks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage
            VectorClock clock1 = new VectorClock(0, 3);
            VectorClock clock2 = new VectorClock(1, 3);

            // Simulate events and message passing
            clock1.Tick(0);
            clock2.Update(clock1.GetClock());

            // Check causality
            bool concurrent = VectorClock.IsConcurrent(clock1, clock2);
            Console.WriteLine($"Are clock1 and clock2 concurrent? {concurrent}");
        }
    }
}
