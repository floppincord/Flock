namespace Flock.Client
{
    public class Order
    {
        public string Name { get; private set; }

        private Order(string name)
        {
            this.Name = name;
        }

        public static Order OrderScore = new Order("score");
        public static Order OrderNew = new Order("new");
    }
}