using Praise.Commands;
using Praise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Praise.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthenticateController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CommandResult>> Post(
            [FromBody] AuthenticateCommand command
            )
        {
            try
            {

                var id = "5198dd11-a00c-4a33-b5e0-efdf268992a5";
                var usuario = "louvor";
                var senha = "";

                if (command.Username != usuario)
                    return BadRequest("Usuário inválido.");

                if (command.Password != senha)
                    return BadRequest("Senha inválida.");


                var token = TokenService.GetToken(id, usuario);

                return Ok(
                       new
                       {
                           token,
                           user = new
                           {
                               id,
                               username = usuario,
                               name = "Ajuntamento das Tribos"
                           }
                       }
                   );
            }
            catch (Exception)
            {
                return BadRequest("Ops! Algo errado aconteceu.");
            }
        }
    }
}
