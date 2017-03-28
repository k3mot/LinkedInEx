using System.Collections.Generic;
using System.Linq;
using LinkedInEx.CommonClasses;

namespace LinkedInEx.Indexers
{
    public interface IHashSetLinkedInIndexer
    {
        void IndexToMultipleKeys(IEnumerable<string> keys, string id);
        void Index(string key, string id);
        HashSet<string> GetIdsForQuery(string key);
    }
}