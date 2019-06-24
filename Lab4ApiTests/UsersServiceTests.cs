using LabII.Models;
using LabII.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class UsersServiceTests
    {
        private IOptions<AppSettings> config;

        [SetUp]
        public void Setup()
        {
            config = Options.Create(new AppSettings
            {
                Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING"
            });
        }

        [Test]
        public void ValidRegisterShouldCreateANewUser()
        {
            var options = new DbContextOptionsBuilder<LabII.Models.ExpensesDbContext>()
              .UseInMemoryDatabase(databaseName:nameof(ValidRegisterShouldCreateANewUser))// "ValidRegisterShouldCreateANewUser")
              .Options;

            using (var context = new ExpensesDbContext(options))
            {
                var usersService = new UsersService(context, config);
                var added = new LabII.DTOs.RegisterPostModel

                {
                    Email = "petre@aol.com",
                    FirstName = "Petre",
                    LastName = "Popescu",
                    Password = "123456",
                    Username = "ppetre",
                };
                var result = usersService.Register(added);

                Assert.IsNotNull(result);
                Assert.AreEqual(added.Username, result.Username);

            }
        }

        [Test]
        public void AuthenticateShouldLoginSuccessfullyTheUser()
        {
            
            var options = new DbContextOptionsBuilder<ExpensesDbContext>()
              .UseInMemoryDatabase(databaseName:nameof(AuthenticateShouldLoginSuccessfullyTheUser))// "ValidUsernameAndPasswordShouldLoginSuccessfully")
              .Options;

            using (var context = new ExpensesDbContext(options))
            {
                var usersService = new UsersService(context, config);

                var added = new LabII.DTOs.RegisterPostModel

                {
                    Email = "petre@aol.com",
                    FirstName = "Petre",
                    LastName = "Popica",
                    Password = "123456",
                    Username = "ppetre",
                };
                usersService.Register(added);
                var loggedIn = new LabII.DTOs.LoginPostModel
                {
                    Username = "ppetre",
                    Password = "123456"

                };
                var authoresult = usersService.Authenticate(added.Username, added.Password);

                Assert.IsNotNull(authoresult);
                Assert.AreEqual(1, authoresult.Id);
                Assert.AreEqual(loggedIn.Username, authoresult.Username);
                //Assert.AreEqual(loggedIn.Password, UsersService.);
            }


        }

        

        [Test]
        public void ValidGetAllShouldDisplayAllUsers()
        {
            var options = new DbContextOptionsBuilder<ExpensesDbContext>()
              .UseInMemoryDatabase(databaseName:nameof(AuthenticateShouldLoginSuccessfullyTheUser))// "ValidGetAllShouldDisplayAllUsers")
              .Options;

            using (var context = new ExpensesDbContext(options))
            {
                var usersService = new UsersService(context, config);

                var added = new LabII.DTOs.RegisterPostModel

                {
                    Email = "petre@aol.com",
                    FirstName = "Petre",
                    LastName = "Popescu",
                    Password = "123456",
                    Username = "ppetre",
                };
                usersService.Register(added);

                // Act
                var result = usersService.GetAll();

                // Assert
                Assert.IsNotEmpty(result);
                Assert.AreEqual(1, result.Count());

            }
        }
    }   
}