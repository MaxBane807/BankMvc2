using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Bank.Web.Controllers;
using Moq;
using Bank.Web.Services.Interfaces;
using AutoMapper;
using Bank.Data.Models;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using Bank.Web.ViewModels;
using System.Linq;
using Bank.Search;

namespace Bank.Tests.Controllers
{
    class CustomerControllerTests
    {
        private CustomerController sut;
        private Mock<ICustomerService> customerService;
        private Mock<IAccountService> accountService;
        private Mock<IMapper> mapper;
        private Mock<ISearchCustomers> search;
        private Fixture fixture;
        
        
        [SetUp]
        public void Setup()
        {
            customerService = new Mock<ICustomerService>();
            mapper = new Mock<IMapper>();
            accountService = new Mock<IAccountService>();
            search = new Mock<ISearchCustomers>(); 
            fixture = new Fixture();
            sut = new CustomerController(customerService.Object, accountService.Object, mapper.Object, search.Object);
        }

        [Test]
        public void ViewCustomer_shall_return_correct_view_in_case_customer_is_found()
        {
            customerService.Setup(x => x.getCustomerByUniqueID(It.IsAny<string>())).Returns(new Customers());

            var result = sut.viewCustomer("CustomerName") as ViewResult;
            Assert.IsNull(result.ViewName);
        }
        [Test]
        public void ViewCustomer_shall_return_correct_view_if_customer_is_not_found()
        {
            var result = sut.viewCustomer("CustomerName") as ViewResult;
            Assert.AreEqual(result.ViewName, "CustomerNotFound");
        }
        [Test]
        public void ViewCustomer_shall_use_correct_viewmodel()
        {
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var fixedCustomer = fixture.Create<Customers>();
            
            customerService.Setup(x => x.getCustomerByUniqueID(It.IsAny<string>()))
                .Returns(fixedCustomer);

            var result = sut.viewCustomer(fixture.Create<string>()) as ViewResult;

            Assert.IsInstanceOf(typeof(CustomerOverviewViewModel), result.Model);            
        }
        [Test]
        public void ViewCustomer_shall_return_correct_data_in_viewmodel()
        {
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var fixedCustomer = fixture.Create<Customers>();
            var fixedAccounts = fixture.CreateMany<int>().ToList();

            customerService.Setup(x => x.getCustomerByUniqueID(It.IsAny<string>()))
                .Returns(fixedCustomer);

            customerService.Setup(x => x.getTotalAmountByID(It.IsAny<int>()))
                .Returns(100);

            accountService.Setup(x => x.getAccountsByCustomerID(It.IsAny<int>()))
                .Returns(fixedAccounts);

            var result = sut.viewCustomer(fixture.Create<string>()) as ViewResult;

            var model = result.Model as CustomerOverviewViewModel;
            
            Assert.AreEqual(fixedCustomer.Birthday, model.Birthday);
            Assert.AreEqual(fixedCustomer.City, model.City);
            Assert.AreEqual(fixedCustomer.Country, model.Country);
            Assert.AreEqual(fixedCustomer.CountryCode, model.CountryCode);
            Assert.AreEqual(fixedCustomer.CustomerId, model.CustomerId);
            Assert.AreEqual(fixedCustomer.Emailaddress, model.Emailaddress);
            Assert.AreEqual(fixedCustomer.Gender, model.Gender);
            Assert.AreEqual(fixedCustomer.Givenname, model.Givenname);
            Assert.AreEqual(fixedCustomer.NationalId, model.NationalId);
            Assert.AreEqual(fixedCustomer.Streetaddress, model.Streetaddress);
            Assert.AreEqual(fixedCustomer.Surname, model.Surname);
            Assert.AreEqual(fixedCustomer.Telephonecountrycode, model.Telephonecountrycode);
            Assert.AreEqual(fixedCustomer.Telephonenumber, model.Telephonenumber);
            Assert.AreEqual(fixedCustomer.UniqueId, model.UniqueId);
            Assert.AreEqual(fixedCustomer.Zipcode, model.Zipcode);

            Assert.AreEqual(100, model.TotalAmount);
            Assert.AreEqual(fixedAccounts, model.Accounts);
        }
        [Test]
        public void ViewCustomers_shall_call_dependencies_correctly_if_customer_is_found()
        {
            customerService.Setup(x => x.getCustomerByUniqueID(It.IsAny<string>())).Returns(new Customers());

            sut.viewCustomer(fixture.Create<string>());

            customerService.Verify(x => x.getCustomerByUniqueID(It.IsAny<string>()), Times.Once);
            customerService.Verify(x => x.getTotalAmountByID(It.IsAny<int>()), Times.Once);
            accountService.Verify(x => x.getAccountsByCustomerID(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ViewCustomers_shall_call_dependencies_correctly_if_customer_is_not_found()
        {
            sut.viewCustomer(fixture.Create<string>());

            customerService.Verify(x => x.getCustomerByUniqueID(It.IsAny<string>()), Times.Once);
            customerService.Verify(x => x.getTotalAmountByID(It.IsAny<int>()), Times.Never);
            accountService.Verify(x => x.getAccountsByCustomerID(It.IsAny<int>()), Times.Never);
        }
    }
}
