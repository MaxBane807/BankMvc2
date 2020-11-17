using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Data.Models;
using Bank.Search;
using Bank.Web.ServiceModels.CustomerServiceModels;
using Bank.Web.ViewModels;


namespace Bank.Web.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerViewModel, CreateCustomerServiceModel>()
                .ForMember(x => x.CustomerId, opt => opt.Ignore());
            CreateMap<CreateCustomerServiceModel, Customers>()
                .ForMember(x => x.CustomerId, opt => opt.Ignore());

            CreateMap<CustomerOverviewViewModel, ChangeCustomerServiceModel>();

            CreateMap<ChangeCustomerServiceModel, Customers>()
                .ForMember(x => x.Dispositions, opt => opt.Ignore());

            CreateMap<Customers, ListCustomersViewModel.CustomerViewModel>();
            
            CreateMap<Customers, CustomerIndex>();
        }
    }
}
