using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Praise.Commands;
using Praise.Const;
using Praise.Contexts;
using Praise.Models;
using Praise.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Praise.Controllers
{
    [ApiController]
    [Route("v1/events")]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get(
            [FromServices] EventRepository repository)
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
                [FromServices] EventRepository repository,
                int id)
        {
            try
            {
                var result = await repository.GetByIdViewDetailsAsync(id);

                if (result == null)
                    return NotFound(new { message = "O Evento não foi localizado." });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpGet]
        [Route("members/{id:int}")]
        public async Task<ActionResult> GetMembers(
                [FromServices] EventRepository repository,
                int id)
        {
            try
            {
                var result = await repository.GetEventUserIdByIdAsync(id);

                if (result == null)
                    return NotFound(new { message = "O Evento não foi localizado." });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpGet]
        [Route("musics/{id:int}")]
        public async Task<ActionResult> GetMusics(
                [FromServices] EventRepository repository,
                int id)
        {
            try
            {
                var result = await repository.GetEventMusicIdByIdAsync(id);

                if (result == null)
                    return NotFound(new { message = "O Evento não foi localizado." });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(
            [FromServices] EventRepository repository,
            [FromServices] DataContext context,
            [FromBody] AddEventCommand command)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return BadRequest(ModelState);

                var date = new DateTime(
                    Convert.ToInt32(command.Date.Substring(6, 4)),
                    Convert.ToInt32(command.Date.Substring(3, 2)),
                    Convert.ToInt32(command.Date.Substring(0, 2)),
                    0,0,0);

                DateTime? startH = null;
                DateTime? endH = null;

                if (command.StartHour != "") 
                    startH = new DateTime(01, 01, 01, Convert.ToInt32(command.StartHour.Substring(0, 2)), Convert.ToInt32(command.StartHour.Substring(3, 2)), 0);
                
                if (command.EndHour != "")
                    endH = new DateTime(01, 01, 01, Convert.ToInt32(command.EndHour.Substring(0, 2)), Convert.ToInt32(command.EndHour.Substring(3, 2)), 0);

                var @event = new Event(
                    command.Name.Trim(),
                    date,
                    startH, 
                    endH,
                    command.Local.Trim(),
                    command.Note.Trim());

                repository.Add(@event);

                await context.SaveChangesAsync();
                return Ok(new { @event });

            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message =ex.Message });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(
            [FromServices] DataContext context,
            [FromServices] EventRepository repository,
            [FromBody] UpdateEventCommand command,
            int id)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return BadRequest(ModelState);

                var @event = await repository.GetByIdAsync(id);
                if (@event == null)
                    return NotFound(new { message = "O Evento não foi localizado." });

                var date = new DateTime(
                    Convert.ToInt32(command.Date.Substring(6, 4)),
                    Convert.ToInt32(command.Date.Substring(3, 2)),
                    Convert.ToInt32(command.Date.Substring(0, 2)),
                    0, 0, 0);

                DateTime? startH = null;
                DateTime? endH = null;

                if (command.StartHour != "")
                    startH = new DateTime(01, 01, 01, Convert.ToInt32(command.StartHour.Substring(0, 2)), Convert.ToInt32(command.StartHour.Substring(3, 2)), 0);

                if (command.EndHour != "")
                    endH = new DateTime(01, 01, 01, Convert.ToInt32(command.EndHour.Substring(0, 2)), Convert.ToInt32(command.EndHour.Substring(3, 2)), 0);

                @event.UpdateEvent(command.Name.Trim(),
                    date,
                    startH,
                    endH,
                    command.Local,
                    command.Note,
                    command.Status);

                @event.LastModifiedDate = DateTime.Now;

                repository.Update(@event);
                await context.SaveChangesAsync();

                return Ok(new { @event });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ex.Message });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("members/{eventId:int}")]
        public async Task<ActionResult> PostEventUsers(
            [FromServices] DataContext context,
            [FromServices] EventRepository eventRepository,
            [FromServices] UserRepository userRepository,
            [FromBody] AddEventUserCommand command,            
            int eventId)
        {
            try
            {
                var eventExists = await eventRepository.GetByIdAsync(eventId);
                if (eventExists == null)
                    return NotFound(new { message = "O Evento não foi localizado." });

                var userExists = userRepository.CheckExistsById(command.UserId);
                if (!userExists)
                    return NotFound(new { message = "O Integrante não foi localizado." });

                var userAlreadyAdded = eventRepository.CheckUserAlreadyAdded(eventId, command.UserId);
                if (userAlreadyAdded)
                    return Ok();



                var eventUser = new EventUser
                {
                    EventId = eventId,
                    UserId = command.UserId
                };
                eventRepository.AddEventUser(eventUser);


                await context.SaveChangesAsync();

                var list = await eventRepository.GetEventUsersAsync(eventId);

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("members/{eventId:int}/{userId:int}")]
        public async Task<ActionResult> DeleteEventUsers(
            [FromServices] DataContext context,
            [FromServices] EventRepository eventRepository,
            [FromServices] UserRepository userRepository,
            int eventId, int userId)
        {
            try
            {

                var eventExists = eventRepository.CheckExistsById(eventId);
                if (!eventExists)
                    return NotFound(new { message = "O Evento não foi localizado." });

                var userExists = userRepository.CheckExistsById(userId);
                if (!userExists)
                    return NotFound(new { message = "O integrante não foi localizado." });

                eventRepository.DeleteUser(eventId, userId);

                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }


        [HttpPut]        
        [Route("musics/{eventId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> PostEventMusics(
            [FromServices] DataContext context,
            [FromServices] EventRepository eventRepository,
            [FromServices] MusicRepository musicRepository,
            [FromBody] AddEventMusicCommand command,
            int eventId)
        {
            try
            {
                var eventExists = await eventRepository.GetByIdAsync(eventId);
                if (eventExists == null)
                    return NotFound(new { message = "O Evento não foi localizado." });


                var musicExists = musicRepository.CheckExistsById(command.MusicId);
                if (!musicExists)
                    return NotFound(new { message = "A música não foi localizada." });

                var musicAlreadyAdded = eventRepository.CheckMusicAlreadyAdded(eventId, command.MusicId);
                if (musicAlreadyAdded)
                    return Ok();

                var eventMusic = new EventMusic
                {
                    EventId = eventId,
                    MusicId = command.MusicId
                };
                eventRepository.AddEventMusic(eventMusic);

                await context.SaveChangesAsync();

                //                var list = await eventRepository.GetEventUsersAsync(eventId);

                return Ok();
                

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("musics/{eventId:int}/{musicId:int}")]
        public async Task<ActionResult> DeleteEventMusics(
            [FromServices] DataContext context,
            [FromServices] EventRepository eventRepository,
            [FromServices] MusicRepository musicRepository,
            int eventId, int musicId)
        {
            try
            {

                var eventExists = eventRepository.CheckExistsById(eventId);
                if (!eventExists)
                    return NotFound(new { message = "O Evento não foi localizado." });

                var musicExists = musicRepository.CheckExistsById(musicId);
                if (!musicExists)
                    return NotFound(new { message = "A música não foi localizada." });

                eventRepository.DeleteMusic(eventId, musicId);

                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("musics/orders/{id:int}")]
        public async Task<ActionResult> PutChangeOrderMusic(
            [FromServices] DataContext context,
            [FromServices] EventRepository repository,
            [FromBody] ChangeOrderEventMusicCommand[] command,            
            int id)
        {
            try
            {                                   
                foreach(var music in command)
                {
                    var result = await repository.GetEventMusicById(id, music.MusicId);
                    if (result == null)
                        continue;
                    result.Order = music.Order;
                    repository.UpdateEventMusic(result);
                }

                await context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }
    }
}
