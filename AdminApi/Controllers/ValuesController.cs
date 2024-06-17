using Application.Controllers;
using Application.Repositories;
using Application.Repositories.Cores;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiController

    {

     

        [HttpPost]
        public async  Task<IActionResult> POST(UserDto userDto)
        {
            await Mediator.Send(userDto);

            return Ok();
        }
    }
}
