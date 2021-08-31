using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Domain.Service.Services;
using Moq;
using NUnit.Framework;
using Security;
using System.Linq;
using System.Threading.Tasks;

namespace ToroApplicationTests
{
    public class Tests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private IUserService userService;
        private string userName = "User Test";
        private string cpf = "906.510.230-25";
        private string password = "1234";
        private string passwordHash = string.Empty;

        [SetUp]
        public void Setup()
        {
            var passwordService = new PasswordService();
            passwordHash = passwordService.HashPassword(password);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(c => c.Users.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(() => null);

            unitOfWorkMock.Setup(c => c.Users.Create(It.IsAny<User>())).ReturnsAsync(new User(cpf, userName, passwordHash));

            userService = new UserService(unitOfWorkMock.Object, passwordService);
        }

        [Test]
        public async Task Should_Create_User()
        {
            //Arrange
            //Act
            var serviceResult = await userService.CreateUser(cpf, userName, password).ConfigureAwait(false);

            //Assert
            Assert.IsTrue(serviceResult.Success);
            Assert.IsEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(userName, serviceResult.Result.UserName);
            Assert.AreEqual(passwordHash, serviceResult.Result.PasswordHash);
        }

        [Test]
        public async Task Should_Validate_Existing_User()
        {
            //Arrange
            unitOfWorkMock.Setup(c => c.Users.GetUserByCpf(It.IsAny<string>())).ReturnsAsync(new User(cpf, userName, passwordHash));

            //Act
            var serviceResult = await userService.CreateUser(cpf, userName, password).ConfigureAwait(false);

            //Assert
            Assert.IsFalse(serviceResult.Success);
            Assert.IsNull(serviceResult.Result);
            Assert.IsNotEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(serviceResult.ValidationMessages.FirstOrDefault(), $"There's already a user with this CPF. 'CPF: {cpf}'");
        }

        [Test]
        public async Task Should_Validate_Incorrect_CPF_Format()
        {
            //Arrange
            var incorrectCpf = "12322.21134.1";

            //Act
            var serviceResult = await userService.CreateUser(incorrectCpf, userName, password).ConfigureAwait(false);

            //Assert
            Assert.IsFalse(serviceResult.Success);
            Assert.IsNull(serviceResult.Result);
            Assert.IsNotEmpty(serviceResult.ValidationMessages);
            Assert.AreEqual(serviceResult.ValidationMessages.FirstOrDefault(), $"The CPF's format is invalid. 'CPF: {incorrectCpf}'");
        }

    }
}