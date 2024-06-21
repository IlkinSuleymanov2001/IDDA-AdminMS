using AdminApi.Commons.Attributes;
using Application.Futures.Category.Commands.Create;
using Application.Futures.Category.Commands.Delete;
using Application.Futures.Category.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ApiController
    {
        [AuthorizeRoles(Roles=Role.ADMIN)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest createCategoryCommandRequest) =>
             Ok(await Mediator.Send(createCategoryCommandRequest));


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DeleteCategoryCommandRequest deleteCategoryCommandRequest) =>
            Ok(await Mediator.Send(deleteCategoryCommandRequest));

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateCategoryCommandRequest updateCategoryCommandRequest) =>
            Ok(await Mediator.Send(updateCategoryCommandRequest));
    }
}
