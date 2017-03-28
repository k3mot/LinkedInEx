using System.Collections.Generic;
using LinkedInEx.CommonClasses;

namespace LinkedInEx.Search
{
    public interface IProfileSearcher
    {
        SearchActionResponse Search(Dictionary<string, string> fieldsToValues);
    }
}