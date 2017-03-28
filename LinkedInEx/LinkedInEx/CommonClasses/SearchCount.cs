namespace LinkedInEx.CommonClasses
{
    public class SearchCount
    {
        public string Field { get; }
        public string Value { get; }
        public int Count { get; set; }

        public SearchCount(string field, string value, int count)
        {
            Field = field;
            Value = value;
            Count = count;
        }
    }
}