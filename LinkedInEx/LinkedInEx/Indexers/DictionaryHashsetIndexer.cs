using System.Collections.Generic;
using LinkedInEx.CommonClasses;

namespace LinkedInEx.Indexers
{
    public class DictionaryHashsetIndexer : IHashSetLinkedInIndexer
    {
        private readonly IDictionary<string, HashSet<string>> _keyToHashsetCache;
        private readonly object _indexLocker = new object();

        public DictionaryHashsetIndexer()
        {
            _keyToHashsetCache = new Dictionary<string, HashSet<string>>();
        }

        public void IndexToMultipleKeys(IEnumerable<string> keys, string id)
        {
            foreach (var key in keys)
            {
                Index(key, id);
            }
        }

        public void Index(string key, string id)
        {
            if (!_keyToHashsetCache.ContainsKey(key))
            {
                lock (_indexLocker)
                {
                    if (!_keyToHashsetCache.ContainsKey(key))
                    {
                        _keyToHashsetCache.Add(key, new HashSet<string>(new List<string>() { id }));
                    }
                    else
                    {
                        _keyToHashsetCache[key].Add(id);
                    }
                }
            }
            else
            {
                _keyToHashsetCache[key].Add(id);
            }
        }

        public HashSet<string> GetIdsForQuery(string key)
        {
            if (_keyToHashsetCache.ContainsKey(key))
            {
                return _keyToHashsetCache[key];
            }

            return null;
        }
    }
}