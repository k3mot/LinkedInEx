using LinkedInEx.CommonClasses;
using LinkedInEx.Controllers;
using LinkedInEx.LinkedInCache;

namespace LinkedInEx.ScoreService
{
    public class BySearchCacheScoreService : IScoreService
    {
        private readonly ILinkedInCache<InnerLinkedInProfile> _dataCache;
        private readonly ILinkedInCache<SearchCount> _searchCache;

        public BySearchCacheScoreService(ILinkedInCache<InnerLinkedInProfile> dataCache, ILinkedInCache<SearchCount>  searchCache)
        {
            _dataCache = dataCache;
            _searchCache = searchCache;
        }
        public int GetScoreById(string userId)
        {
            var profile = _dataCache.GetTByKey(userId);
            if (profile == null)
            {
                return 0;
            }

            var totalScore = 0;
            foreach (var skill in profile.Skills)
            {
                var skillSearchCount = _searchCache.GetTByKey(skill);
                if (skillSearchCount != null)
                    totalScore += skillSearchCount.Count;
            }

            return totalScore;
        }
    }
}