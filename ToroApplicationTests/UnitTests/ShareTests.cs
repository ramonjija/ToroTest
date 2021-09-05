using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Domain.Service.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToroApplicationTests.UnitTests
{
    [TestFixture]
    public class ShareTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private IShareService shareService;
        private IEnumerable<Share> shrares;

        [SetUp]
        public void Setup()
        {
            shrares = new List<Share>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(c => c.ShareRepository.Get()).ReturnsAsync(shrares);
            shareService = new ShareService(unitOfWorkMock.Object);

        }

        [Test]
        public async Task Should_Get_Empty_Shares()
        {
            //Arrange
            //Act
            var serviceResult = await shareService.GetAllShares().ConfigureAwait(false);

            //Assert
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.Result);
        }
        [Test]
        public async Task Should_Get_List_Of_Shares()
        {
            //Arrange
            var shares = new List<Share>()
            {
                new Share(1, "TEST4", 10),
                new Share(2, "TEST5", 20),
            };

            unitOfWorkMock.Setup(c => c.ShareRepository.Get()).ReturnsAsync(shares);
            //Act
            var serviceResult = await shareService.GetAllShares().ConfigureAwait(false);

            //Assert
            Assert.IsTrue(serviceResult.Success);
            Assert.IsTrue(serviceResult.Result.Any());
            Assert.IsTrue(serviceResult.Result.Select(c => shares.Contains(c)).Any());
        }
    }
}
