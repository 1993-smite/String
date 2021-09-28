namespace Elastic
{
    public class ESModel
    {
        protected string index;
        public string GetIndex => index;

        public int Id;

        public ESModel(string index)
        {
            this.index = index;
        }
    }
}
