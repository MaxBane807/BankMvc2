
using Bank.Web.Services.Classes;
using Bank.Web.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Bank.Tests.Services
{
    class TransferServiceTests
    {
        private TransferService _sut;
        private Mock<IInsertService> _mockedInsertService;
        private Mock<IWithdrawService> _mockedWithdrawService;
        private Mock<IAccountService> _mockedAccountService;

        [SetUp]
        public void Setup()
        {
            _mockedInsertService = new Mock<IInsertService>();
            _mockedWithdrawService = new Mock<IWithdrawService>();
            _mockedAccountService = new Mock<IAccountService>();

            _sut = new TransferService(_mockedInsertService.Object,_mockedWithdrawService.Object,_mockedAccountService.Object);
        }

        [Test]
        public void Transfer_more_money_then_exist_should_be_impossible()
        {
            _mockedAccountService.Setup(x => x.GetAccountBalanceByID(It.IsAny<int>())).Returns(100);

            _sut.CreateTransfer(1, false, "Transfer", 110, "", "1234");

            _mockedInsertService.Verify(x => x.CreateAnInsert(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);

            _mockedWithdrawService.Verify(x => x.CreateWithdraw(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }
    }
}
