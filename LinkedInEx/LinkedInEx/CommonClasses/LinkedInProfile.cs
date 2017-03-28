using System.Collections.Generic;

namespace LinkedInEx.CommonClasses
{
    public class LinkedInProfile
    {
        public string FullName { get; private set; }
        public string CurrentTitle { get; private set; }
        public string CurrentPosition { get; private set; }
        public string Summary { get; private set; }
        public IEnumerable<string> Skills { get; private set; }
        public IEnumerable<string> Experience { get; private set; }
        public IEnumerable<string> Education { get; private set; }

        public LinkedInProfile(string fullName, string currentTitle, string currentPosition, string summary, IEnumerable<string> skills, IEnumerable<string> experience, IEnumerable<string> education)
        {
            FullName = fullName;
            CurrentTitle = currentTitle;
            CurrentPosition = currentPosition;
            Summary = summary;
            Skills = skills;
            Experience = experience;
            Education = education;
        }

        public LinkedInProfile(InnerLinkedInProfile inner)
        {
            FullName = inner.FullName;
            CurrentTitle = inner.CurrentTitle;
            CurrentPosition = inner.CurrentPosition;
            Summary = inner.Summary;
            Skills = inner.Skills;
            Experience = inner.Experience;
            Education = inner.Education;
        }
    }
}