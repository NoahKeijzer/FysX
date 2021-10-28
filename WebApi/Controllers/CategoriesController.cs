using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IDiagnosisRepository diagnosisRepository;

        public CategoriesController(IDiagnosisRepository diagnosisRepository)
        {
            this.diagnosisRepository = diagnosisRepository;
        }

        /// <summary>
        /// gets all categories.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /categories
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <returns>All categories in list</returns>

        [HttpGet]
        public IEnumerable<string> GetCategories()
        {
            return diagnosisRepository.GetCategories();
        }
    }
}
