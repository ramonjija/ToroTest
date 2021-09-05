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
    public class OrderServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IShareService> shareServiceMock;
        private Mock<IUserPositionService> userPositionServiceMock;
        private Mock<IUserService> userServiceMock;
        private IOrderService orderService;
        private string userName = "User Test";
        private string cpf = "906.510.230-25";
        private string passwordHash = new PasswordService().HashPassword("1234");
        private User user;
        private IEnumerable<Position> positions;
        private UserPosition userPosition;
        private IServiceResult<Share> shareServiceResult;
        private IServiceResult<UserPosition> userPositionServiceResult;
        private IServiceResult<User> userServiceResult;

        [SetUp]
        public void Setup()
        {
            user = new User(cpf, userName, passwordHash);
            var share = new Share()
            {

                CurrentPrice = 1,
                ShareId = 1,
                Symbol = "TEST1"
            };
            var boughtShare = new Share()
            {

                CurrentPrice = 1,
                ShareId = 3,
                Symbol = "TEST3"
            };
            positions = new List<Position>()
            {
                new Position()
                {
                    Amout = 1,
                    PositionId = 1,
                    Share = share
                }
            };
            userPosition = new UserPosition(positions, 9, 10, user);

            shareServiceResult = new ServiceResult<Share>();
            shareServiceResult.SetResult(boughtShare);

            userPositionServiceResult =  new ServiceResult<UserPosition>();
            userPositionServiceResult.SetResult(userPosition);

            userServiceResult = new ServiceResult<User>();
            userServiceResult.SetResult(user);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            shareServiceMock = new Mock<IShareService>();
            userPositionServiceMock = new Mock<IUserPositionService>();
            userServiceMock = new Mock<IUserService>();

            unitOfWorkMock.Setup(c => c.UserPositions.Update(It.IsAny<UserPosition>())).Returns(userPosition);
            shareServiceMock.Setup(c => c.GetShare(It.IsAny<string>())).ReturnsAsync(shareServiceResult);
            userPositionServiceMock.Setup(c => c.GetUserPosition(It.IsAny<string>())).ReturnsAsync(userPositionServiceResult);
            userServiceMock.Setup(c => c.GetUser(It.IsAny<string>())).ReturnsAsync(userServiceResult);


            orderService = new OrderService(unitOfWorkMock.Object, shareServiceMock.Object, userPositionServiceMock.Object, userServiceMock.Object);
        }

        [Test]
        public async Task Should_Add_Share_To_Position()
        {
            //Arrange
            //Act
            var serviceResult = await orderService.BuyShare("TEST3", 1, cpf).ConfigureAwait(false);
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(0, serviceResult.Result.UserPositionId);
            Assert.AreEqual(10, serviceResult.Result.Consolidated);
            Assert.AreEqual(8, serviceResult.Result.CheckingAccountAmount);
            Assert.AreEqual(user, serviceResult.Result.User);
            Assert.AreEqual(userPosition.Positions, serviceResult.Result.Positions);
            Assert.IsTrue(serviceResult.Result.Positions.Any(c => c.Share.Symbol.Equals("TEST3")));
        }

        [Test]
        public async Task Should_Not_Add_Share_To_Position_User_Not_Found()
        {
            //Arrange
            var userServiceResult = new ServiceResult<User>();
            userServiceResult.AddMessage("User not found");
            userServiceMock.Setup(c => c.GetUser(It.IsAny<string>())).ReturnsAsync(userServiceResult);

            //Act
            var serviceResult = await orderService.BuyShare("TEST3", 1, cpf).ConfigureAwait(false);
            Assert.IsFalse(serviceResult.Success);
            Assert.IsNull(serviceResult.Result);
            Assert.IsNotEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(serviceResult.ValidationMessages.FirstOrDefault(), "User not found");
        }
        [Test]
        public async Task Should_Not_Add_Share_To_Position_Share_Not_Found()
        {
            //Arrange
            shareServiceResult = new ServiceResult<Share>();
            shareServiceResult.AddMessage("Share not found");
            shareServiceMock.Setup(c => c.GetShare(It.IsAny<string>())).ReturnsAsync(shareServiceResult);

            //Act
            var serviceResult = await orderService.BuyShare("TEST3", 1, cpf).ConfigureAwait(false);
            Assert.IsFalse(serviceResult.Success);
            Assert.IsNull(serviceResult.Result);
            Assert.IsNotEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(serviceResult.ValidationMessages.FirstOrDefault(), $"Share not found");
        }

        [Test]
        public async Task Should_Not_Add_Share_To_Position_UserPosition_Not_Found()
        {
            //Arrange
            userPositionServiceResult = new ServiceResult<UserPosition>();
            userPositionServiceResult.AddMessage("User Position not found");
            userPositionServiceMock.Setup(c => c.GetUserPosition(It.IsAny<string>())).ReturnsAsync(userPositionServiceResult);

            //Act
            var serviceResult = await orderService.BuyShare("TEST3", 1, cpf).ConfigureAwait(false);
            Assert.IsFalse(serviceResult.Success);
            Assert.IsNull(serviceResult.Result);
            Assert.IsNotEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(serviceResult.ValidationMessages.FirstOrDefault(), $"User Position not found");
        }

        [Test]
        public async Task Should_Not_Add_Share_To_Position_Not_Enought_Balance()
        {
            //Arrange
            //Act
            var serviceResult = await orderService.BuyShare("TEST3", 1000, cpf).ConfigureAwait(false);
            Assert.IsFalse(serviceResult.Success);
            Assert.IsNull(serviceResult.Result);
            Assert.IsNotEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(serviceResult.ValidationMessages.FirstOrDefault(), $"Share could not be bought. Check Account Balance");
        }

        [Test]
        public async Task Should_Add_Balance_To_New_User()
        {
            //Arrange
            var newUserPositionServiceResult = new ServiceResult<UserPosition>();
            newUserPositionServiceResult.AddMessage("User position not found");
            userPositionServiceMock.Setup(c => c.GetUserPosition(It.IsAny<string>())).ReturnsAsync(newUserPositionServiceResult);

            //Act
            var serviceResult = await orderService.AddBalance(10, cpf).ConfigureAwait(false);
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(0, serviceResult.Result.UserPositionId);
            Assert.AreEqual(10, serviceResult.Result.Consolidated);
            Assert.AreEqual(10, serviceResult.Result.CheckingAccountAmount);
            Assert.AreEqual(user, serviceResult.Result.User);
            Assert.IsEmpty(serviceResult.Result.Positions);
        }

        [Test]
        public async Task Should_Add_Balance_To_Existing_User()
        {
            //Arrange
            //Act
            var serviceResult = await orderService.AddBalance(10, cpf).ConfigureAwait(false);
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(0, serviceResult.Result.UserPositionId);
            Assert.AreEqual(20, serviceResult.Result.Consolidated);
            Assert.AreEqual(19, serviceResult.Result.CheckingAccountAmount);
            Assert.AreEqual(user, serviceResult.Result.User);
            Assert.AreEqual(userPosition.Positions, serviceResult.Result.Positions);
            Assert.IsTrue(serviceResult.Result.Positions.Any(c => c.Share.Symbol.Equals("TEST1")));
        }

        [Test]
        public async Task Should_Not_Add_Balance_To_Unexisting_User()
        {
            //Arrange
            var userServiceResult = new ServiceResult<User>();
            userServiceResult.AddMessage("User not found");
            userServiceMock.Setup(c => c.GetUser(It.IsAny<string>())).ReturnsAsync(userServiceResult);

            //Act
            var serviceResult = await orderService.AddBalance(10, cpf).ConfigureAwait(false);
            Assert.IsFalse(serviceResult.Success);
            Assert.IsNull(serviceResult.Result);
            Assert.IsNotEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(serviceResult.ValidationMessages.FirstOrDefault(), "User not found");
        }
    }
}
