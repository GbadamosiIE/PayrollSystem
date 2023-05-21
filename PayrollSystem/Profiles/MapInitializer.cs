
using AutoMapper;
using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.Entities;
using System.Linq;


namespace PayRollSystem.Api.Profiles
{
    public class MapInitializer : Profile
    {
        public Mapper regMapper { get; set; }
        public MapInitializer()
        {
            // Authentication Maps

            var regConfig = new MapperConfiguration(conf => conf.CreateMap<RegisterDTO, Employee>());
            regMapper = new Mapper(regConfig);
            // Amenity Maps
            // Transaction Maps

           // CreateMap<Payment, PaymentDTO>().ReverseMap();

        }
    }
}
