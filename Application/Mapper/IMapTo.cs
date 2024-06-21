using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public interface IMapTo<T>
    {
        virtual void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
