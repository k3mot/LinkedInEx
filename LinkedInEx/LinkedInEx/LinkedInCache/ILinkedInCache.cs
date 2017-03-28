using System.Collections.Generic;
using LinkedInEx.CommonClasses;
using LinkedInEx.Controllers;

namespace LinkedInEx.LinkedInCache
{
    public interface ILinkedInCache<T> 
        where T : class
    {
        void Add(T newT);
        T GetTByKey(string key);
        IEnumerable<T> GetTByFields(Dictionary<string, string> fieldsToValues);
        
    }
}