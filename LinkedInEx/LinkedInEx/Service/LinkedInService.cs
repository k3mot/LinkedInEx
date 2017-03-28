using System.Collections.Generic;
using System.Threading.Tasks;
using LinkedInEx.CommonClasses;
using LinkedInEx.LinkedInCache;
using LinkedInEx.Search;
using LinkedInEx.Validators;

namespace LinkedInEx.Service
{
    public class LinkedInService : ILinkedInService
    {
        private readonly IProfileValidator _validator;
        private readonly ILinkedInCache<InnerLinkedInProfile> _cache;
        private readonly IProfileSearcher _profileSearcher;

        public LinkedInService(IProfileValidator validator, ILinkedInCache<InnerLinkedInProfile> cache, IProfileSearcher profileSearcher)
        {
            _validator = validator;
            _cache = cache;
            _profileSearcher = profileSearcher;
        }

        public AddNewProfileResponse AddNewProfilePage(InnerLinkedInProfile newProfile)
        {
            var validateResponse = _validator.ValidateProfile(newProfile);
            if (!validateResponse.Succeeded)
            {
                return validateResponse;
            }

            Task.Run(() => { InnerAddNewProfilePage(newProfile); });
            return validateResponse;
        }

        private void InnerAddNewProfilePage(InnerLinkedInProfile newProfile)
        {
            _cache.Add(newProfile);
        }

        public SearchActionResponse Search(Dictionary<string, string> fieldsToValues)
        {
            return _profileSearcher.Search(fieldsToValues);
        }
    }
}