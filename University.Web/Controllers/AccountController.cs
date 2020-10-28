using System.Web.Http;
using University.BL.DTOs;
namespace University.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Metodo encargado de resalizar la autenticación
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>

        [HttpPost]
        public IHttpActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //demo
            bool isCredencialValid = (loginDTO.Password == "123456");
            if (isCredencialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(loginDTO.UserName);
                return Ok(token);
            }
            else
                return Unauthorized();//ssatus code 401

            
        }
    }
}
