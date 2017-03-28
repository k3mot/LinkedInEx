using System.Collections.Generic;
using LinkedInEx.CommonClasses;

namespace LinkedInEx.Service
{
    public class BulkedLinkedInServiceDecorator : ILinkedInService
    {
        private readonly ILinkedInService _decorated;
        private readonly int _bulkSize;
        private readonly IList<InnerLinkedInProfile> _bulk;
        private readonly object _bulkFlushingLocker = new object();

        public BulkedLinkedInServiceDecorator(ILinkedInService decorated)
        {
            _decorated = decorated;
            _bulkSize = 100; //Should be from configuration.
            _bulk = new List<InnerLinkedInProfile>();
        }
        public AddNewProfileResponse AddNewProfilePage(InnerLinkedInProfile newProfile)
        {
            _bulk.Add(newProfile);

            if (_bulk.Count >= _bulkSize)
            {
                lock (_bulkFlushingLocker)
                {
                    if (_bulk.Count >= _bulkSize)
                    {
                        foreach (var innerLinkedInProfile in _bulk)
                        {
                            _decorated.AddNewProfilePage(innerLinkedInProfile);
                        }

                        _bulk.Clear();

                        return new AddNewProfileResponse(true, string.Empty);
                    }
                }
            }

            return new AddNewProfileResponse(true, string.Empty);
        }

        public SearchActionResponse Search(Dictionary<string, string> fieldsToValues)
        {
            return _decorated.Search(fieldsToValues);
        }
    }
}