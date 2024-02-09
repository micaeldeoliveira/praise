using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Praise.Commands;
using Praise.Const;
using Praise.Contexts;
using Praise.Models;
using Praise.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praise.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get(
            [FromServices] UserRepository repository)
        {
            try
            {
                return Ok(await repository.GetAllAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     new { message = ErrorMessage.Internal });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> Get(
            [FromServices] UserRepository repository,
            int id)
        {
            try
            {
                var user = await repository.GetByIdAsync(id);
                if (user == null)
                    return NotFound(new { message = "Usuário não foi localizado." });

                user.HidePassword();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     new { message = ErrorMessage.Internal });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(
            [FromServices] DataContext context,
            [FromBody] AddUserCommand command)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return BadRequest(ModelState);

                var user = new User(
                    command.Name.Trim(),
                    command.Username.Trim().ToLower(),
                    command.Email.Trim().ToLower(),
                    command.Password.Trim(),
                    command.Phone.Trim(),
                    command.Birthday,
                    command.Photo.ToLower());

                context.Users.Add(user);
                await context.SaveChangesAsync();

                return Ok(new { user.Id });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = ErrorMessage.Internal });
            }


        }
    }
}
