using Application.Futures.Organization.Dtos;
using Application.Futures.Staff.Commands.Create;
using Application.Futures.Staff.Commands.Update;
using Application.Futures.Staff.Dtos;
using AutoMapper;
using Core.Repository.Paging;
using Domain.Entities;

namespace Application.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Staff, StaffDto>().
                ForMember(c=>c.IsActiveStaff,y=>y.MapFrom(c=>c.Active)).
                ForMember(c=>c.IsActiveOrganization,y=>y.MapFrom(c=>c.Organization!.Active)).ReverseMap();
            CreateMap<Staff, CreateStaffCommandRequest>().ReverseMap();
            CreateMap<Staff, UpdateStaffCommandRequest>().ReverseMap();
            CreateMap<Organization, OrganizationDto>().ReverseMap();
            CreateMap<IPaginate<Organization>, PaginateOrganizationModel>().ReverseMap();
            CreateMap<IPaginate<Staff>, PaginateStaffModel>().ReverseMap();

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
