using System.Collections.Generic;
using LinkedInEx.CommonClasses;

namespace LinkedInEx.Validators
{
    public class DummySearchValidator : ISearchValidator
    {
        public SearchActionResponse ValidateSearch(Dictionary<string, string> fieldToValue)
        {
            return new SearchActionResponse(true, string.Empty, null);
        }
    }
}