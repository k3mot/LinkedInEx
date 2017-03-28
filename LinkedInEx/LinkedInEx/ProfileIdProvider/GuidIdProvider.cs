using System;

namespace LinkedInEx.ProfileIdProvider
{
    class GuidIdProvider : IProfileIdProvider
    {
        public string ProvideId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}