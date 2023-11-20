namespace Flock.Client.Params
{
    public class SearchParams
    {
        public string Keywords { get; private set; }

        public Order Order { get; private set; }

        public SearchParams(string keywords, Order order)
        {
            this.Keywords = keywords;
            this.Order = order;
        }
    }
}