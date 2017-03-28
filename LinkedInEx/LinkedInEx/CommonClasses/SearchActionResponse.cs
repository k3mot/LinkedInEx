using System.Collections.Generic;
using System.Linq;

namespace LinkedInEx.CommonClasses
{
    public class SearchActionResponse
    {
        public bool Succeeded { get; private set; }
        public string Reason { get; private set; }
        public IEnumerable<LinkedInProfile> Result { get; private set; }

        public SearchActionResponse(bool succeeded, string reason, IEnumerable<InnerLinkedInProfile> result)
        {
            Succeeded = succeeded;
            Reason = reason;
            Result = result.Select(innerProfile => new LinkedInProfile(innerProfile));
        }
    }
}