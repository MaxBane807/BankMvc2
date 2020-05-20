using System;
using System.Collections.Generic;
using System.Text;
using Bank.Web.Data;
using Bank.Web.Models;
using NUnit.Framework;
using Bank.Web.Services.Classes;
using Bank.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bank.Tests.Services
{
    public class WithdrawServiceTests
    {
        private WithdrawService _sut;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
                .UseInMemoryDatabase(databaseName: "BankAppData")
                .Options;

            using (var context = new BankAppDataContext(options))
            {
                context.Accounts.Add(new Accounts {AccountId = 1, Balance = 100});
                context.SaveChanges();
            }

            Mock<IAccountService> mockedAccountService = new Mock<IAccountService>();

            using (var context = new BankAppDataContext(options))
            {
                _sut = new WithdrawService(context,mockedAccountService.Object);
            }
        }

        [Test]
        public void Withdraw_more_money_then_exists_should_be_impossible()
        {
            Assert.Throws<System.ArgumentException>(() => _sut.CreateWithdraw(1, "debit", 110, "", "", ""));
        }

        [Test]
        public void No_withdraw_with_negative_values_should_be_allowed()
        {
            Assert.Throws<System.ArgumentException>(() => _sut.CreateWithdraw(1, "debit", -5, "", "", ""));
        }
    }
}
