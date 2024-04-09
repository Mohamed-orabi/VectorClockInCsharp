namespace VectorClocks
{
    internal class VectorClock
    {
        private Dictionary<int, int> clock;

        public VectorClock(int nodeId, int totalNodes)
        {
            clock = new Dictionary<int, int>();
            for (int i = 0; i < totalNodes; i++)
            {
                clock[i] = 0;
            }

            // Initialize the node's own clock
            clock[nodeId] = 1;
        }

        public void Tick(int nodeId)
        {
            if (clock.ContainsKey(nodeId))
            {
                clock[nodeId]++;
            }
        }

        public void Update(Dictionary<int, int> incomingClock)
        {
            foreach (var entry in incomingClock)
            {
                if (clock.ContainsKey(entry.Key))
                {
                    clock[entry.Key] = Math.Max(clock[entry.Key], entry.Value);
                }
                else
                {
                    clock[entry.Key] = entry.Value;
                }
            }

            // Simulate the event of receiving a message
            Tick(incomingClock.First().Key);
        }

        public Dictionary<int, int> GetClock()
        {
            return new Dictionary<int, int>(clock);
        }

        public static bool IsConcurrent(VectorClock clockA, VectorClock clockB)
        {
            bool aLessThanB = false;
            bool bLessThanA = false;

            foreach (var key in clockA.GetClock().Keys.Union(clockB.GetClock().Keys))
            {
                int aValue = clockA.GetClock().ContainsKey(key) ? clockA.GetClock()[key] : 0;
                int bValue = clockB.GetClock().ContainsKey(key) ? clockB.GetClock()[key] : 0;

                if (aValue < bValue) aLessThanB = true;
                else if (aValue > bValue) bLessThanA = true;

                if (aLessThanB && bLessThanA) return true; // They are concurrent
            }

            return false; // One precedes the other
        }
    }
}
