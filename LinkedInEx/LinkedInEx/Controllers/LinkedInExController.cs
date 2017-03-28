using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LinkedInEx.CommonClasses;
using LinkedInEx.ProfileIdProvider;
using LinkedInEx.ScoreService;
using LinkedInEx.Service;

namespace LinkedInEx.Controllers
{
    public class LinkedInExController : ApiController
    {
        private readonly ILinkedInService _linkedInService;
        private readonly IScoreService _scoreService;
        private readonly IProfileIdProvider _idProvider;

        public LinkedInExController(ILinkedInService linkedInService, IScoreService scoreService, IProfileIdProvider idProvider)
        {
            _linkedInService = linkedInService;
            _scoreService = scoreService;
            _idProvider = idProvider;
        }

        [HttpPost]
        public IHttpActionResult AddProfilePage(LinkedInProfile newProfile)
        {
            var id = _idProvider.ProvideId();
            var newInnerProfile = new InnerLinkedInProfile(newProfile, id);

            try
            {
                var response = _linkedInService.AddNewProfilePage(newInnerProfile);
                if (!response.Succeeded)
                {
                    return BadRequest(response.Reason);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult SearchByAnyField(Dictionary<string, string> fieldsToValues)
        {
            try
            {
                var response = _linkedInService.Search(fieldsToValues);
                if (!response.Succeeded)
                {
                    return BadRequest(response.Reason);
                }

                return Ok(response.Result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public IHttpActionResult GetScoreById(string userId)
        {
            //Score Service should be a completely different micro-service.
            try
            {
                var score = _scoreService.GetScoreById(userId);

                return Ok(score);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
