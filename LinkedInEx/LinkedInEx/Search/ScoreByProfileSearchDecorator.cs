using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkedInEx.CommonClasses;
using LinkedInEx.LinkedInCache;

namespace LinkedInEx.Search
{
    public class SearchCountDecorator : IProfileSearcher
    {
        private readonly IProfileSearcher _decorated;
        private readonly ILinkedInCache<SearchCount> _searchCache;

        public SearchCountDecorator(IProfileSearcher decorated, ILinkedInCache<SearchCount> searchCache)
        {
            _decorated = decorated;
            _searchCache = searchCache;
        }
        public SearchActionResponse Search(Dictionary<string, string> fieldsToValues)
        {
            foreach (var fieldToValue in fieldsToValues)
            {
                _searchCache.Add(new SearchCount(fieldToValue.Key, fieldToValue.Value, 1));
            }
            return _decorated.Search(fieldsToValues);
        }
    }
}