using System.Collections.Generic;
using LinkedInEx.CommonClasses;
using LinkedInEx.Validators;

namespace LinkedInEx.Search
{
    public class ValidationProfileSearchProxy : IProfileSearcher
    {
        private readonly IProfileSearcher _profileSearcher;
        private readonly ISearchValidator _searchValidator;

        public ValidationProfileSearchProxy(IProfileSearcher profileSearcher, ISearchValidator searchValidator)
        {
            _profileSearcher = profileSearcher;
            _searchValidator = searchValidator;
        }
        public SearchActionResponse Search(Dictionary<string, string> fieldsToValues)
        {
            var validationResponse = _searchValidator.ValidateSearch(fieldsToValues);
            if (!validationResponse.Succeeded)
            {
                return validationResponse;
            }

            return _profileSearcher.Search(fieldsToValues);
        }
    }
}