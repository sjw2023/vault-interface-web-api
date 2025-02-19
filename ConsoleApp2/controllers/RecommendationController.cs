using System;
using System.Web.Http;
using ConsoleApp2.Services;

namespace ConsoleApp2.Controllers
{
    [RoutePrefix("api/recommendation")]
    public class RecommendationController : ApiController
    {
        private VaultRecommendationService _recommendationService = new VaultRecommendationService();

        [HttpGet]
        public IHttpActionResult GetItemRecommendations([FromUri] string searchQuery, [FromUri] uint searchPropertyType)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return BadRequest("Search query cannot be empty.");
            }

            // Connect to Autodesk Vault
            _recommendationService.ConnectToVault("VaultServerName", "VaultName", "Username", "Password");

            // Get recommendations
            Console.WriteLine(searchQuery);

            var recommendations = _recommendationService.GetRecommendations(searchQuery, searchPropertyType);

            // Return recommendations in structured JSON format
            return Ok(recommendations);
        }
    }
}

