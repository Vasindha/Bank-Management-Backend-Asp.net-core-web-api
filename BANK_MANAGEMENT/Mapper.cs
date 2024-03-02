
using AutoMapper;
using BANK_MANAGEMENT.DTO;
using BANK_MANAGEMENT.Models;

namespace BANK_MANAGEMENT
{
    public class Mapper:Profile
    {
        public Mapper() { 
        CreateMap<CustomerDTO,Customer>().ReverseMap();
            CreateMap<CustomerViewDTO,Customer>().ReverseMap();
            CreateMap<Account,AccountDto>().ReverseMap();
        }

    }
}
