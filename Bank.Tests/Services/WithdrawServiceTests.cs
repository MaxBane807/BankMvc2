using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Data;
using Bank.Data.Models;
using Bank.Web.Services.Classes;
using Bank.Web.Services.Interfaces;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bank.Tests.Services
{
    public class WithdrawServiceTests
    {
        private WithdrawService _sut;
        private BankAppDataContext _context;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
                .UseInMemoryDatabase(databaseName: "BankAppData")
                .Options;

            _context = new BankAppDataContext(options);

            if (_context.Accounts.FirstOrDefault(x => x.AccountId == 1) == null)
            {
                _context.Accounts.Add(new Accounts {AccountId = 1, Balance = 100});
                _context.SaveChanges();
            }

            Mock<IAccountService> mockedAccountService = new Mock<IAccountService>();
            mockedAccountService.Setup(x => x.GetAccountBalanceByID(It.IsAny<int>())).Returns(100m);

            _sut = new WithdrawService(_context,mockedAccountService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
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

        [Test]
        public void Withdraw_should_create_transaction_correctly()
        {
            _sut.CreateWithdraw(1,"Withdraw in cash",50,"","","");

            var result = _context.Accounts.FirstOrDefault(x => x.AccountId == 1).Transactions.FirstOrDefault();

            Assert.AreEqual(1, result.AccountId);
            Assert.AreEqual("Withdraw in cash", result.Operation);
            Assert.AreEqual(-50m, result.Amount);
            Assert.AreEqual("", result.Symbol);
            Assert.AreEqual("", result.Bank);
            Assert.AreEqual("", result.Account);

            Assert.AreEqual(50, result.Balance);
            Assert.AreEqual(DateTime.Today, result.Date.Date);
            Assert.AreEqual("Debit", result.Type);
        }
    }
}
