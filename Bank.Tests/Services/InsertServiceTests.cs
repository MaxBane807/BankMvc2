using System;
using System.Collections.Generic;
using System.Text;
using Bank.Web.Data;
using Bank.Web.Models;
using Bank.Web.Services.Classes;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Bank.Tests.Services
{
    class InsertServiceTests
    {
        private InsertService _sut;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
                .UseInMemoryDatabase(databaseName: "BankAppData")
                .Options;

            using (var context = new BankAppDataContext(options))
            {
                context.Accounts.Add(new Accounts { AccountId = 2, Balance = 100 });
                context.SaveChanges();
            }

            using (var context = new BankAppDataContext(options))
            {
                _sut = new InsertService(context);
            }
            
        }

        [Test]
        public void Insert_with_negative_values_should_be_impossible()
        {
            Assert.Throws<System.ArgumentException>(() => _sut.CreateAnInsert(2,"credit in cash",-5,"","",""));
        }
    }
}
