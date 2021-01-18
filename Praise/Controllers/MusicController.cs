using Praise.Contexts;
using Praise.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praise.Const;
using Praise.Repositories;
using Praise.Commands;
using MySql.Data.MySqlClient;

namespace Praise.Controllers
{
    [ApiController]
    [Route("v1/musics")]
    public class MusicController : ControllerBase
    {
        [HttpGet]        
        //[Authorize]
        [AllowAnonymous]
        public async Task<ActionResult> Get([FromServices]
            MusicRepository repository)
        {
            try
            {
                return Ok(await repository.GetMusicsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("search/{value}")]
        [Authorize]
        public async Task<ActionResult<List<Music>>> Search(
            [FromServices] MusicRepository repository, string value)
        {
            try
            {
                return Ok(await repository.GetSearchMusicsAsync(value));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Music>> GetById([FromServices] MusicRepository repository, int id)
        {
            try
            {
                return Ok(await repository.GetByIdAsync(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Music>> Post(
            [FromServices] MusicRepository repository,
            [FromServices] DataContext context,
            [FromBody] AddMusicCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var music = new Music(
                    command.Title.Trim(),
                    command.Reminder.Trim(),
                    command.Singer.Trim(),
                    command.Lirycs,
                    command.Notation,
                    command.Video,
                    command.Play);

                repository.Add(music);

                await context.SaveChangesAsync();
                return Ok(new { music });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Music>> Put(
            [FromServices] DataContext context, 
            [FromServices] MusicRepository repository,
            [FromBody] UpdateMusicCommand command,
            int id)
        {
            try
            {                
                var music = await repository.GetByIdAsync(id);
                if (music == null)
                    return NotFound(new { message = "A música não foi localizada."});

                music.Singer = command.Singer;
                music.Title = command.Title;
                music.Reminder = command.Reminder;
                music.Lirycs = command.Lirycs;
                music.Notation = command.Notation;
                music.Video = command.Video;
                music.Play = command.Play;

                music.LastModifiedDate = DateTime.Now;

                repository.Update(music);
                await context.SaveChangesAsync();

                return Ok(new { music });

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Music>> Delete(
            [FromServices] DataContext context,
            [FromServices] MusicRepository repository,
            int id)
        {
            try
            {
                var music = await repository.GetByIdAsync(id);
                if (music == null)
                    return NotFound(new { message = "A música não foi localizada." });

                repository.Delete(music);                
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ErrorMessage.Internal });
            }
        }

        [HttpGet]
        [Route("import")]
        [AllowAnonymous]
        public async Task<ActionResult> Import(
            [FromServices] DataContext context,
            [FromServices] MusicRepository repository
            )
        {
            try
            {
                using(var con = new MySqlConnection("Server= mysql.saidata.net.br; Database= saidata01; Uid= saidata01; Pwd= Pttsq6adj;"))
                {
                    con.Open();

                    var cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = @"SELECT `Musicas`.`Id`,
                                        `Musicas`.`Titulo`,
                                        `Musicas`.`Lembrete`,
                                        `Musicas`.`Interprete`,
                                        `Musicas`.`Letra`,
                                        `Musicas`.`Cifra`,
                                        `Musicas`.`Video`,
                                        `Musicas`.`Tocamos`
                                    FROM `saidata01`.`Musicas`";

                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var music = new Music(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6), rdr.GetBoolean(7));
                        music.Id = rdr.GetInt32(0);
                        repository.Add(music);
                        await context.SaveChangesAsync();
                    }
                    cmd.Dispose();
                }
                
                return Ok(await repository.GetMusicsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = ex.Message });
            }
        }
    }
}