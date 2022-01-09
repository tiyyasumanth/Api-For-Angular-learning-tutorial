using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Api.RoomRentService.Controllers
{
    public class UserController : Controller
    {
        private IRentService _rentService;

        #region Constructor
        public UserController(IRentService rentService)
        {
            this._rentService = rentService;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// To  write asset details
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        [HttpPost("api/user/register")]
        [Authorize]
        [SwaggerResponse(statusCode: 200, type: typeof(Token), description: "Sucessful Response")]
        public async Task<IActionResult> WriteUserDetails([FromBody]RegistrationEntity userRequest)
        {
            return Ok("Success");
        }

        /// <summary>
        /// To  write asset details
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        [HttpPost("api/generate/token/jwt")]
        [AllowAnonymous]
        [SwaggerResponse(statusCode: 200, type: typeof(Token), description: "Sucessful Response")]
        public async Task<IActionResult> GenerateJwt([FromBody]UserRequest userRequest)
        {
            try
            {
                return Ok(await this._rentService.GetTokenInfo(userRequest));
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}