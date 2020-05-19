using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Bank.Web.Services.Classes;

namespace Bank.Tests.Services
{
    public class WithdrawServiceTests
    {
        [SetUp]
        public void Setup()
        {
            var sut = new WithdrawService();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
