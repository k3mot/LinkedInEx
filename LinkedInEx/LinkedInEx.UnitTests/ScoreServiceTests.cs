using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using LinkedInEx.CommonClasses;
using LinkedInEx.LinkedInCache;
using LinkedInEx.ScoreService;
using NUnit.Framework;

namespace LinkedInEx.UnitTests
{
    [TestFixture]
    public class ScoreServiceTests
    {
        private ILinkedInCache<InnerLinkedInProfile> _fakeDataCache;
        private ILinkedInCache<SearchCount> _fakeSearchCache;
        private ILinkedInCache<InnerLinkedInProfile> _fakeEmptyDataCache;

        [SetUp]
        public void Setup()
        {
            var dummyProfileWithThreeSkills = new InnerLinkedInProfile("a", "b", "c", "d", "f", new List<string>() {"SkillA", "SkillB", "SkillC"}, new List<string>(), new List<string>());
            _fakeDataCache = A.Fake<ILinkedInCache<InnerLinkedInProfile>>();
            _fakeSearchCache = A.Fake<ILinkedInCache<SearchCount>>();
            A.CallTo(() => _fakeDataCache.GetTByKey(null)).WithAnyArguments().Returns(dummyProfileWithThreeSkills);
            A.CallTo(() => _fakeSearchCache.GetTByKey(null)).WithAnyArguments().Returns(new SearchCount("a", "b", 1));

            _fakeEmptyDataCache = A.Fake<ILinkedInCache<InnerLinkedInProfile>>();
            A.CallTo(() => _fakeEmptyDataCache.GetTByKey(null)).WithAnyArguments().Returns(null);
        }

        [Test]
        public void ProfileScoreShouldBeTheNumberOfSkills()
        {
            var scoreService = new BySearchCacheScoreService(_fakeDataCache, _fakeSearchCache);
            var score = scoreService.GetScoreById("dummy");

            Assert.AreEqual(score, 3);
        }

        [Test]
        public void UnkownProfileShouldReturnZeroScore()
        {
            var scoreService = new BySearchCacheScoreService(_fakeEmptyDataCache, _fakeSearchCache);
            var score = scoreService.GetScoreById("unknown");

            Assert.AreEqual(score, 0);
        }
    }
}
