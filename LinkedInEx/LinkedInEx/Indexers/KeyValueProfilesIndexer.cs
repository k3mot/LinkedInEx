using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinkedInEx.CommonClasses;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace LinkedInEx.Indexers
{
    public class PersistentKeyValueProfilesIndexer : ILinkedInProfilesIndexer
    {
        private IDictionary<string, InnerLinkedInProfile> _idToProfileCache;
        private readonly ICacheClient _redisAccess;
        private const string RedisKeyPrefix = "id:";

        public PersistentKeyValueProfilesIndexer()
        {
            _idToProfileCache = new Dictionary<string, InnerLinkedInProfile>();

            //Obviously ip+port should be in config file.
            var localMultiplexer = ConnectionMultiplexer.Connect("localhost:6379");
            _redisAccess = new StackExchangeRedisCacheClient(localMultiplexer, new NewtonsoftSerializer());
        }

        public void Initialize()
        {
            var keys = _redisAccess.SearchKeys(RedisKeyPrefix + "*");
            _idToProfileCache = _redisAccess.GetAll<InnerLinkedInProfile>(keys);
        }

        public void Index(InnerLinkedInProfile profile)
        {
            //could lock and check if key already exits - but I assume Guid won't be repeated.
            _idToProfileCache.Add(profile.Id, profile);
            _redisAccess.Add(RedisKeyPrefix + profile.Id, profile);
        }

        public IEnumerable<InnerLinkedInProfile> GetProfilesByIds(IEnumerable<string> ids)
        {
            var result = new Collection<InnerLinkedInProfile>();
            foreach (var id in ids)
            {
                if (_idToProfileCache.ContainsKey(id))
                {
                    result.Add(_idToProfileCache[id]);
                }
            }

            return result;
        }

        public IEnumerable<InnerLinkedInProfile> GetAllProfiles()
        {
            return _idToProfileCache.Values;
        }

        public InnerLinkedInProfile GetProfileById(string id)
        {
            if (_idToProfileCache.ContainsKey(id))
            {
                return _idToProfileCache[id];
            }

            return null;
        }
    }
}