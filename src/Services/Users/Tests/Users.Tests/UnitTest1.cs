using AutoMapper;
using Moq;
using Users.Application.Features.Users.Commands.RegisterUser;
using Users.Application.Utils.PasswordEncrypter;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Domain.Entities;
using Users.Models.Exceptions;
using Users.Models.Users;

namespace Users.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Скурат","Денис","feeze","12345678", true)]
        [TestCase("Скурат", "Денис", "testNickname", "12345678", true)]
        [TestCase("Скурат", "Денис", "maksim","12345678", true)]
        [TestCase("Скурат", "Денис", "tasher", "12345678", false)]
        [TestCase("Иванов", "Иван", "ivanich", "12345678", false)]
        public async Task RegistrationTest(string lastName, string firstName, string nickname, string password, bool isException)
        {
            var nicknameIsExists = new List<string>
            {
                "feeze",
                "maksim",
                "testNickname"
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            var userFinderMock = new Mock<IUserFinder>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();
            var passwordEncrypter = new PasswordEncryptor();
            var roleFinderMock = new Mock<IRoleFinder>();
            var roleRepositoryMock = new Mock<IRoleRepository>();

            userFinderMock.Setup(t => t.HasAnyByNicknameAsync(nickname, default))
                .ReturnsAsync(nicknameIsExists.Any(t => t.ToLower() == nickname.ToLower()));

            mapperMock.Setup(t => t.Map<UserEntity>(It.IsAny<RegisterUserCommand>())).Returns(new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Id = Guid.NewGuid(),
                Nickname = nickname,
                Password = password,
                ProfileImageUrl = null
            });

            mapperMock.Setup(t => t.Map<GetUserShortDto>(It.IsAny<UserEntity>())).Returns(new GetUserShortDto
            {
                FirstName = firstName,
                LastName = lastName,
                Id = Guid.NewGuid(),
                Nickname = nickname,
                ProfileImageUrl = null
            });


            var registerUserCommandHandler = new RegisterUserCommandHandler(
                userRepositoryMock.Object, 
                userFinderMock.Object,
                unitOfWorkMock.Object, 
                mapperMock.Object,
                passwordEncrypter,
                roleFinderMock.Object,
                roleRepositoryMock.Object
            );

            var registerUser = new RegisterUserCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Nickname = nickname,
                Password = password,
                ProfileImageUrl = null
            };
            try
            {
                var result = await registerUserCommandHandler.Handle(registerUser, default);
                Assert.That(result.FirstName, Is.EqualTo(firstName));
                Assert.That(result.LastName, Is.EqualTo(lastName));
                Assert.That(result.Nickname, Is.EqualTo(nickname));
            }
            catch (EntityAlreadyExistException e)
            {
                if (isException)
                    Assert.Pass();
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}