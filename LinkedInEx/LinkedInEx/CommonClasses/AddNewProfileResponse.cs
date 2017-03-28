namespace LinkedInEx.CommonClasses
{
    public class AddNewProfileResponse
    {
        public bool Succeeded { get; private set; }
        public string Reason { get; private set; }

        public AddNewProfileResponse(bool succeeded, string reason)
        {
            Succeeded = succeeded;
            Reason = reason;
        }

    }
}