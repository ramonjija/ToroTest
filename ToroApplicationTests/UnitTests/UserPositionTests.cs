using Domain.Model.Aggregate;
using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Domain.Service.Services;
using Moq;
using NUnit.Framework;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
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
            unitOfWorkMock.Setup(c => c.UserPositions.GetPositionByCpf(It.IsAny<string>())).ReturnsAsync(new UserPosition(positions, 9, 10, user));
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
            Assert.IsEmpty(serviceResult.Validator.ValidationMessages);
            Assert.AreEqual(0, serviceResult.Result.UserPositionId);
            Assert.AreEqual(10, serviceResult.Result.Consolidated);
            Assert.AreEqual(9, serviceResult.Result.CheckingAccountAmount);
            Assert.AreEqual(user, serviceResult.Result.User);
            Assert.AreEqual(positions, serviceResult.Result.Positions);
        }

        //[Test]
        //public async Task Should_Get_Nullable_Position_By_Cpf()
        //{
        //    //Arrange
        //    unitOfWorkMock.Setup(c => c.UserPositions.GetPositionByCpf(It.IsAny<string>())).ReturnsAsync(() => null);

        //    //Act
        //    var serviceResult = await userPositionService.GetUserPosition(cpf).ConfigureAwait(false);

        //    //Assert
        //    Assert.IsTrue(serviceResult.Success);
        //    Assert.IsEmpty(serviceResult.Validator.ValidationMessages);
        //    Assert.IsNull(serviceResult.Result);
        //}

        [Test]
        public async Task Should_Get_Position_Not_Found_By_Cpf()
        {
            //Arrange
            unitOfWorkMock.Setup(c => c.UserPositions.GetPositionByCpf(It.IsAny<string>())).ReturnsAsync(() => null);

            //Act
            var serviceResult = await userPositionService.GetUserPosition(cpf).ConfigureAwait(false);

            //Assert
            Assert.IsFalse(serviceResult.Success);
            Assert.IsNull(serviceResult.Result);
            Assert.IsNotEmpty(serviceResult.Validator.ValidationMessages);
            Assert.AreEqual(serviceResult.Validator.ValidationMessages.FirstOrDefault(), $"User Position not found");
        }

        [Test]
        public async Task Should_Get_Position_With_No_Shares_By_Cpf()
        {
            //Arrange
            unitOfWorkMock.Setup(c => c.UserPositions.GetPositionByCpf(It.IsAny<string>())).ReturnsAsync(new UserPosition(null, 10, user));

            //Act
            var serviceResult = await userPositionService.GetUserPosition(cpf).ConfigureAwait(false);

            //Assert
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.Validator.ValidationMessages);
            Assert.AreEqual(0, serviceResult.Result.UserPositionId);
            Assert.AreEqual(10, serviceResult.Result.Consolidated);
            Assert.AreEqual(10, serviceResult.Result.CheckingAccountAmount);
            Assert.AreEqual(user, serviceResult.Result.User);
            Assert.IsEmpty(serviceResult.Result.Positions);
        }

        [Test]
        public void Should_Add_Position_To_User()
        {
            //Arrange
            var positions = new List<Position>()
            {
                new Position()
                {
                    PositionId = 1,
                    Amout = 1,
                    Share = new Share()
                    {
                        ShareId = 1,
                        CurrentPrice = 10,
                        Symbol = "TEST1"
                    }
                },
                 new Position()
                {
                    PositionId = 2,
                    Amout = 3,
                    Share = new Share()
                    {
                        ShareId = 1,
                        CurrentPrice = 30,
                        Symbol = "TEST2"
                    }
                }
            };
            var userPosition = new UserPosition(null, 10, user);

            //Act
            userPosition.AddPositionsToUser(positions);

            //Assert
            Assert.AreEqual(0, userPosition.UserPositionId);
            Assert.AreEqual(110, userPosition.Consolidated);
            Assert.AreEqual(10, userPosition.CheckingAccountAmount);
            Assert.AreEqual(user, userPosition.User);
            Assert.IsNotEmpty(userPosition.Positions);
        }

        [Test]
        public void Should_Increase_Amount_Of_Existing_Position()
        {
            //Arrange
            var positions = new List<Position>()
            {
                new Position()
                {
                    PositionId = 1,
                    Amout = 1,
                    Share = new Share()
                    {
                        ShareId = 1,
                        CurrentPrice = 10,
                        Symbol = "TEST1"
                    }
                }
            };

            var share = new Share()
            {
                ShareId = 1,
                CurrentPrice = 2,
                Symbol = "TEST1"
            };

            var userPosition = new UserPosition(positions, 10, user);

            //Act
            userPosition.AddPositionToUser(share, 2);

            //Assert
            Assert.AreEqual(0, userPosition.UserPositionId);
            Assert.AreEqual(36, userPosition.Consolidated);
            Assert.AreEqual(6, userPosition.CheckingAccountAmount);
            Assert.AreEqual(user, userPosition.User);
            Assert.IsNotEmpty(userPosition.Positions);
            Assert.AreEqual(3, userPosition.Positions.Select(c => c.Amout).FirstOrDefault());
        }

        [Test]
        public void Should_Remove_Position_Of_User()
        {
            //Arrange
            var positions = new List<Position>()
            {
                new Position()
                {
                    PositionId = 1,
                    Amout = 1,
                    Share = new Share()
                    {
                        ShareId = 1,
                        CurrentPrice = 10,
                        Symbol = "TEST1"
                    }
                },
                 new Position()
                {
                    PositionId = 2,
                    Amout = 3,
                    Share = new Share()
                    {
                        ShareId = 1,
                        CurrentPrice = 30,
                        Symbol = "TEST2"
                    }
                }
            };

            var positionsToRemove = new List<Position>()
            {
                new Position()
                {
                    PositionId = 1,
                    Amout = 1,
                    Share = new Share()
                    {
                        ShareId = 1,
                        CurrentPrice = 10,
                        Symbol = "TEST1"
                    }
                }
            };

            var userPosition = new UserPosition(positions, 10, user);

            //Act
            userPosition.RemovePositionsOfUser(positionsToRemove);

            //Assert
            //Assert
            Assert.AreEqual(0, userPosition.UserPositionId);
            Assert.AreEqual(100, userPosition.Consolidated);
            Assert.AreEqual(10, userPosition.CheckingAccountAmount);
            Assert.AreEqual(user, userPosition.User);
            Assert.IsNotEmpty(userPosition.Positions);
        }

        [Test]
        public void Should_Add_Balance_To_User()
        {
            var userPosition = new UserPosition(null, 10, user);
            //Act
            userPosition.AddBalance(10);
            //Assert
            Assert.AreEqual(0, userPosition.UserPositionId);
            Assert.AreEqual(20, userPosition.Consolidated);
            Assert.AreEqual(20, userPosition.CheckingAccountAmount);
            Assert.AreEqual(user, userPosition.User);
            Assert.IsEmpty(userPosition.Positions);
        }
    }
}
