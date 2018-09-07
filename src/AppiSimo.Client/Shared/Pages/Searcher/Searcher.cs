namespace AppiSimo.Client.Shared.Pages.Searcher
{
    public class Searcher
    {
        public string Filter { get; }

        public Searcher()
        {
            Filter = string.Empty;
        }

        public Searcher(string filter)
        {
            Filter = filter;
        }
    }
}