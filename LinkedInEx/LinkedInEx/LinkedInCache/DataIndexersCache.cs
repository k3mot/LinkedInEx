using System;
using System.Collections;
using LinkedInEx.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkedInEx.CommonClasses;
using LinkedInEx.Indexers;

namespace LinkedInEx.LinkedInCache
{
    public class DataIndexersCache : ILinkedInCache<InnerLinkedInProfile>
    {
        private readonly ILinkedInProfilesIndexer _idToFullProfileIndexer;
        private readonly IDictionary<string, IHashSetLinkedInIndexer> _typeToindexer;

        public DataIndexersCache(ILinkedInProfilesIndexer idToFullProfileIndexer)
        {
            _idToFullProfileIndexer = idToFullProfileIndexer;
            _typeToindexer = new Dictionary<string, IHashSetLinkedInIndexer>() //Should be injected.
            {
                { DefinedProfilePropsKeys.FullName, new DictionaryHashsetIndexer()},
                { DefinedProfilePropsKeys.CurrentTitle, new DictionaryHashsetIndexer()},
                { DefinedProfilePropsKeys.CurrentPosition, new DictionaryHashsetIndexer()},
                { DefinedProfilePropsKeys.Summary, new DictionaryHashsetIndexer()},
                { DefinedProfilePropsKeys.Skill, new DictionaryHashsetIndexer()}
            };
            bool indexersShouldInit = true; //Should be from configuration.

            if (indexersShouldInit)
            {
                InitializeIndexers();
            }
        }

        private void InitializeIndexers()
        {
            _idToFullProfileIndexer.Initialize();

            var profiles = _idToFullProfileIndexer.GetAllProfiles();
            Parallel.ForEach(profiles, ExtractAndIndex);
        }

    public void Add(InnerLinkedInProfile newProfile)
    {
        _idToFullProfileIndexer.Index(newProfile);
        //TODO: do it paralelly for complex indexers.
        ExtractAndIndex(newProfile);
    }

    public InnerLinkedInProfile GetTByKey(string key)
    {
        return _idToFullProfileIndexer.GetProfileById(key);
    }

    public IEnumerable<InnerLinkedInProfile> GetTByFields(Dictionary<string, string> fieldsToValues)
    {
        var intersectedIds = new HashSet<string>();
        foreach (var fieldToValue in fieldsToValues)
        {
            if (_typeToindexer.ContainsKey(fieldToValue.Key))
            {
                var ids = _typeToindexer[fieldToValue.Key].GetIdsForQuery(fieldToValue.Value);

                if (intersectedIds.Count == 0)
                {
                    intersectedIds.UnionWith(ids);
                }
                else
                {
                    intersectedIds.IntersectWith(ids);
                    if (intersectedIds.Count == 0)
                    {
                        return null;
                    }
                }
            }
        }

        return _idToFullProfileIndexer.GetProfilesByIds(intersectedIds);
    }

    private void ExtractAndIndex(InnerLinkedInProfile newProfile)
    {
        //Could be written better with properties reflection... with its cost.
        if (_typeToindexer.ContainsKey(DefinedProfilePropsKeys.FullName))
        {
            _typeToindexer[DefinedProfilePropsKeys.FullName].Index(newProfile.FullName, newProfile.Id);
        }
        if (_typeToindexer.ContainsKey(DefinedProfilePropsKeys.CurrentTitle))
        {
            _typeToindexer[DefinedProfilePropsKeys.CurrentTitle].Index(newProfile.CurrentTitle, newProfile.Id);
        }
        if (_typeToindexer.ContainsKey(DefinedProfilePropsKeys.CurrentPosition))
        {
            _typeToindexer[DefinedProfilePropsKeys.CurrentPosition].Index(newProfile.CurrentPosition, newProfile.Id);
        }
        if (_typeToindexer.ContainsKey(DefinedProfilePropsKeys.Summary))
        {
            var keywords = newProfile.Summary.Split(' ');
            _typeToindexer[DefinedProfilePropsKeys.Summary].IndexToMultipleKeys(keywords, newProfile.Id);
        }
        if (_typeToindexer.ContainsKey(DefinedProfilePropsKeys.Skill))
        {
            _typeToindexer[DefinedProfilePropsKeys.Skill].IndexToMultipleKeys(newProfile.Skills, newProfile.Id);
        }
    }
}
}