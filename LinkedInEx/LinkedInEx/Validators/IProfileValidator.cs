using LinkedInEx.CommonClasses;

namespace LinkedInEx.Validators
{
    public interface IProfileValidator
    {
        AddNewProfileResponse ValidateProfile(InnerLinkedInProfile profile);
    }
}