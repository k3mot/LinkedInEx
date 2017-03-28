using LinkedInEx.CommonClasses;

namespace LinkedInEx.Validators
{
    public class DummyProfileValidator : IProfileValidator
    {
        public AddNewProfileResponse ValidateProfile(InnerLinkedInProfile profile)
        {
            return new AddNewProfileResponse(true, string.Empty);
        }
    }
}