﻿using Application.Common.Exceptions;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers
{
    public class UserDtoHandler : IRequestHandler<UserDto>
    {
        private readonly IMapper mapper;
        IStaffRepository staff;

      
        public UserDtoHandler(IMapper mapper, IStaffRepository staff)
        {
            this.mapper = mapper;
            this.staff = staff;

        }

        public async Task Handle(UserDto request, CancellationToken cancellationToken)
        {
           
            await staff.CreateAsync(new Staff("Azerbaijna2","salam dunya2 ",2));
            await staff.SaveChangeAsync();
            //throw new ArgumentNullException("argumant excption ");

        }
    }
}
