using System.Collections.Generic;
using LinkedInEx.CommonClasses;

namespace LinkedInEx.Indexers
{
    public interface ILinkedInProfilesIndexer 
    {
        void Initialize();
        void Index(InnerLinkedInProfile profile);
        InnerLinkedInProfile GetProfileById(string id);
        IEnumerable<InnerLinkedInProfile> GetProfilesByIds(IEnumerable<string> ids);
        IEnumerable<InnerLinkedInProfile> GetAllProfiles();
    }
}