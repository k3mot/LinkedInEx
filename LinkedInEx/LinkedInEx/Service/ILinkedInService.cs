using System.Collections.Generic;
using LinkedInEx.CommonClasses;

namespace LinkedInEx.Service
{
    public interface ILinkedInService
    {
        AddNewProfileResponse AddNewProfilePage(InnerLinkedInProfile newProfile);
        SearchActionResponse Search(Dictionary<string, string> fieldsToValues);
    }
}