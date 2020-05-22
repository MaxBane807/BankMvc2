using System;
using System.Collections.Generic;
using System.Linq;
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
        private BankAppDataContext _context;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new BankAppDataContext(options);
            
            if (_context.Accounts.FirstOrDefault(x => x.AccountId == 2) == null)
            {
                _context.Accounts.Add(new Accounts {AccountId = 2, Balance = 100});
                _context.SaveChanges();
            }
            
            _sut = new InsertService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void Insert_with_negative_values_should_be_impossible()
        {
            Assert.Throws<System.ArgumentException>(() => _sut.CreateAnInsert(2,"credit in cash",-5,"","",""));
        }

        [Test]
        public void Insert_should_create_transaction_correctly()
        {
            _sut.CreateAnInsert(2,"Credit in cash",50,"","","");
            
            var result = _context.Accounts.FirstOrDefault(x => x.AccountId == 2).Transactions.FirstOrDefault();
            
            Assert.AreEqual(2,result.AccountId);
            Assert.AreEqual("Credit in cash",result.Operation);
            Assert.AreEqual(50,result.Amount);
            Assert.AreEqual("",result.Symbol);
            Assert.AreEqual("",result.Bank);
            Assert.AreEqual("",result.Account);

            Assert.AreEqual(150,result.Balance);
            Assert.AreEqual(DateTime.Today,result.Date.Date);
            Assert.AreEqual("Credit",result.Type);
        }
    }
}
