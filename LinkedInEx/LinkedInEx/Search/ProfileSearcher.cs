using System.Collections.Generic;
using LinkedInEx.CommonClasses;
using LinkedInEx.LinkedInCache;
using LinkedInEx.Validators;

namespace LinkedInEx.Search
{
    public class ProfileSearcher : IProfileSearcher
    {
        private readonly ILinkedInCache<InnerLinkedInProfile> _dataCache;

        public ProfileSearcher(ILinkedInCache<InnerLinkedInProfile> dataCache)
        {
            _dataCache = dataCache;
        }
        public SearchActionResponse Search(Dictionary<string, string> fieldsToValues)
        {
            var result =_dataCache.GetTByFields(fieldsToValues);
            return new SearchActionResponse(true, string.Empty, result);
        }
    }
}