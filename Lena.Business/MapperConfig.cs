using Lena.Data;
using Lena.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lena.Business
{
    public class MapperConfig
    {
        public void Map()
        {
            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<User, UserDTO>().ReverseMap();
                mapper.CreateMap<Form, FormDTO>().ReverseMap();
                mapper.CreateMap<Field, FieldDTO>().ReverseMap();
            });
        }
    }
}
