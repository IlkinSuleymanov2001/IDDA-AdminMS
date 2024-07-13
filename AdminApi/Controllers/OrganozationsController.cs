using Application.Futures.Organization.Commands.Create;
using Application.Futures.Organization.Commands.Delete;
using Application.Futures.Organization.Commands.Update;
using Application.Futures.Organization.Queries.Get;
using Application.Futures.Organization.Queries.GetList;
using Application.Futures.Organization.Queries.List;
using Application.Repositories.Cores.Paging;
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

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateOrganizationNameCommand updateStaffCommand)=>
            Ok(await Mediator.Send(updateStaffCommand));
            


        [HttpGet("getpaginationlist")]
        public async Task<IActionResult> GetPaginationList([FromQuery]PageRequest pageRequest) =>
            Ok(await Mediator.Send(new GetOrganizationPaginationListQuery(pageRequest)));
        
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList() =>
            Ok(await Mediator.Send(new GetOrganizationListQuery()));
        
        [HttpPost("get")]
        public async Task<IActionResult> Get(GetOrganizationQueryRequest getOrganizationQueryRequest ) =>
            Ok(await Mediator.Send(getOrganizationQueryRequest));
    }
}
