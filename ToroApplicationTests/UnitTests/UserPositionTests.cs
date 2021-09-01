using Domain.Model.Aggregate;
using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Domain.Service.Services;
using Moq;
using NUnit.Framework;
using Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToroApplicationTests.UnitTests
{
    [TestFixture]
    public class UserPositionTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private IUserPositionService userPositionService;
        private string userName = "User Test";
        private string cpf = "906.510.230-25";
        private string password = "1234";
        private string passwordHash = new PasswordService().HashPassword("1234");
        private User user;
        private IEnumerable<Position> positions;

        [SetUp]
        public void SetUp() 
        {
            user = new User(cpf, userName, passwordHash);
            positions = new List<Position>()
            {
                new Position()
                {
                    Amout = 1,
                    PositionId = 1,
                    Share = new Share()
                    {
                        CurrentPrice = 1,
                        ShareId = 1,
                        Symbol = "TEST3"
                    }
                }
            };
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(c => c.UserPositions.GetPositionByCpf(It.IsAny<string>())).ReturnsAsync(new UserPosition(1, positions, 9, 10, user));
            userPositionService = new UserPositionService(unitOfWorkMock.Object);
        }

        [Test]
        public async Task Should_Get_Position_By_Cpf()
        {
            //Arrange
            //Act
            var serviceResult = await userPositionService.GetUserPosition(cpf).ConfigureAwait(false);

            //Assert
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(1, serviceResult.Result.UserPositionId);
            Assert.AreEqual(10, serviceResult.Result.Consolidated);
            Assert.AreEqual(9, serviceResult.Result.CheckingAccountAmount);
            Assert.AreEqual(user, serviceResult.Result.User);
            Assert.AreEqual(positions, serviceResult.Result.Positions);
        }

        [Test]
        public async Task Should_Get_Nullable_Position_By_Cpf()
        {
            //Arrange
            unitOfWorkMock.Setup(c => c.UserPositions.GetPositionByCpf(It.IsAny<string>())).ReturnsAsync(() => null);

            //Act
            var serviceResult = await userPositionService.GetUserPosition(cpf).ConfigureAwait(false);

            //Assert
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.ValidationMessages);
            Assert.IsNull(serviceResult.Result);
        }

        [Test]
        public async Task Should_Get_Position_With_No_Shares_By_Cpf()
        {
            //Arrange
            unitOfWorkMock.Setup(c => c.UserPositions.GetPositionByCpf(It.IsAny<string>())).ReturnsAsync(new UserPosition(1, null, 10, 10, user));

            //Act
            var serviceResult = await userPositionService.GetUserPosition(cpf).ConfigureAwait(false);

            //Assert
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(1, serviceResult.Result.UserPositionId);
            Assert.AreEqual(10, serviceResult.Result.Consolidated);
            Assert.AreEqual(10, serviceResult.Result.CheckingAccountAmount);
            Assert.AreEqual(user, serviceResult.Result.User);
            Assert.IsNull(serviceResult.Result.Positions);
        }
    }
}
