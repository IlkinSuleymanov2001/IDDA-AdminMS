using Application.Futures.Organization.Create;
using Application.Futures.Organization.Delete;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganozationsController : ApiController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOrganizationCommandRequest createOrganizationCommandRequest) =>
            Ok(await Mediator.Send(createOrganizationCommandRequest));


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(RemoveOrganizationCommandRequest removeOrganizationCommandRequest) =>
            Ok(await Mediator.Send(removeOrganizationCommandRequest));
    }
}
