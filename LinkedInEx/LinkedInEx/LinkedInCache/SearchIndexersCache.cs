using System.Collections.Generic;
using System.Threading;
using LinkedInEx.CommonClasses;
using StackExchange.Redis.Extensions.Core;

namespace LinkedInEx.LinkedInCache
{
    public class OnlySkillSearchCache : ILinkedInCache<SearchCount>
    {
        private readonly IDictionary<string, SearchCount> _skillToSearchCount;
        //Score cache is not persistent, could add redis support...
        private readonly object _addSkillLocker = new object();

        public OnlySkillSearchCache()
        {
            _skillToSearchCount = new Dictionary<string, SearchCount>();
        }

        public void Add(SearchCount newT)
        {
            //For the simplicity of the exercise I've added support for skill search cache only.
            //Changing it would be easy - implement indexers for different fields 
            //like DataIndexersCache does.
            if (newT.Field != "Skill")
            {
                return;
            }

            if (!_skillToSearchCount.ContainsKey(newT.Value))
            {
                lock (_addSkillLocker)
                {
                    if (!_skillToSearchCount.ContainsKey(newT.Value))
                    {
                        _skillToSearchCount.Add(newT.Value, new SearchCount("Skill", newT.Value, 1));
                    }
                }
            }
            else
            {
                var currentCount = _skillToSearchCount[newT.Value].Count;
                Interlocked.Add(ref currentCount, newT.Count);
            }
        }

        public IEnumerable<SearchCount> GetTByFields(Dictionary<string, string> fieldToValue)
        {
            if (fieldToValue.ContainsKey(DefinedProfilePropsKeys.Skill))
            {
                var skill = fieldToValue[DefinedProfilePropsKeys.Skill];
                if (_skillToSearchCount.ContainsKey(skill))
                {
                    return new List<SearchCount>() { _skillToSearchCount[skill]};
                }
            }

            return null;
        }

        public SearchCount GetTByKey(string key)
        {
            if (_skillToSearchCount.ContainsKey(key))
            {
                return _skillToSearchCount[key] ;
            }

            return null;
        }
    }
}