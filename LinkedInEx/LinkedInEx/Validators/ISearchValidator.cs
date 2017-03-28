using System.Collections.Generic;
using LinkedInEx.CommonClasses;

namespace LinkedInEx.Validators
{
    public interface ISearchValidator
    {
        SearchActionResponse ValidateSearch(Dictionary<string, string> fieldToValue);
    }
}