﻿using Application.Futures.Staff.Commands.Create;
using Application.Futures.Staff.Commands.Update;
using Application.Futures.Staff.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Staff, CreateStaffCommandRequest>().ReverseMap();
            CreateMap<Staff, UpdateStaffCommandRequest>().ReverseMap();
            CreateMap<Staff, StaffListDto>().ForMember(c => c.OrganizationName, c => c.MapFrom(c => c.Organization.Name)).ReverseMap();
            //ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /*        private void ApplyMappingsFromAssembly(Assembly assembly)
                {
                    var types = assembly.GetExportedTypes()
                        .Where(t => t.GetInterfaces().Any(i =>
                            i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapFrom<>) || i.GetGenericTypeDefinition() == typeof(IMapTo<>))))
                        .ToList();

                    foreach (var type in types)
                    {
                        var instance = Activator.CreateInstance(type);



                        var methodInfo = type.GetMethod("Mapping", BindingFlags.Instance | BindingFlags.NonPublic)
                                         ?? (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                                             ? type.GetInterface("IMapFrom`1").GetMethod("Mapping")
                                             : type.GetInterface("IMapTo`1").GetMethod("Mapping"));

                    }
                }*/
    }
}