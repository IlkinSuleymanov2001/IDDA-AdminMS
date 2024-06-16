using Application.Controllers;
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
        public readonly IUnitOfWork unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async  Task<IActionResult> GET(UserDto user)
        {

            var response = await Mediator.Send(user);

            return Ok(response);
        
        }
    }
}
