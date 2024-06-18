﻿using Application.Futures.Staff.Commands.Create;
using Application.Futures.Staff.Commands.Remove;
using Application.Futures.Staff.Commands.Update;
using Application.Futures.Staff.Queries.Get;
using Application.Futures.Staff.Queries.GetList;
using Application.Repositories.Cores.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ApiController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateStaffCommandRequest createStaffCommandRequest)=>
             Ok(await Mediator.Send(createStaffCommandRequest));


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DeleteStaffCommandRequest deleteStaffCommandRequest) =>
            Ok(await Mediator.Send(deleteStaffCommandRequest));

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateStaffCommandRequest updateStaffCommandRequest) =>
            Ok(await Mediator.Send(updateStaffCommandRequest));

        [HttpGet("get")]
        public async Task<IActionResult> Get() =>
            Ok(await Mediator.Send(new GetStaffQueryRequest()));

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery]PageRequest pageRequest) =>
            Ok(await Mediator.Send(new GetStaffListQueryRequest(pageRequest)));


    }
}