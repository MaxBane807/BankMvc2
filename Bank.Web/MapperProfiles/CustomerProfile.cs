using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Web.Models;
using Bank.Web.ServiceModels.AdminServiceModels;
using Bank.Web.ViewModels.AdminViewModels;

namespace Bank.Web.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerViewModel, CreateCustomerServiceModel>()
                .ForMember(x => x.CustomerId, opt => opt.Ignore());
            CreateMap<CreateCustomerServiceModel, Customers>();
        }
    }
}
