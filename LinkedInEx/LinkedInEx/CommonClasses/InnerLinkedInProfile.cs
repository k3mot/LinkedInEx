using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedInEx.CommonClasses
{
    public class InnerLinkedInProfile
    {
        public string Id { get; private set; }
        public string FullName { get; private set; }
        public string CurrentTitle { get; private set; }
        public string CurrentPosition { get; private set; }
        public string Summary { get; private set; }
        public IEnumerable<string> Skills { get; private set; }
        public IEnumerable<string> Experience { get; private set; }
        public IEnumerable<string> Education { get; private set; }

        public InnerLinkedInProfile(string id, string fullName, string currentTitle, string currentPosition, string summary, IEnumerable<string> skills, IEnumerable<string> experience, IEnumerable<string> education)
        {
            Id = id;
            FullName = fullName;
            CurrentTitle = currentTitle;
            CurrentPosition = currentPosition;
            Summary = summary;
            Skills = skills;
            Experience = experience;
            Education = education;
        }

        public InnerLinkedInProfile(LinkedInProfile profile, string id)
        {
            Id = id;
            FullName = profile.FullName;
            CurrentTitle = profile.CurrentTitle;
            CurrentPosition = profile.CurrentPosition;
            Summary = profile.Summary;
            Skills = profile.Skills;
            Experience = profile.Experience;
            Education = profile.Education;
        }
    }
}