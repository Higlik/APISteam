
using APISteam.Core.Enums;
using APISteam.Domain.Entities;
using APISteam.Domain.Interface;
using APISteam.Infra.Data;
using APISteam.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Helpers;

namespace Tests.RepositoriesTests
{   
    [TestClass]
    public class GenreRepositoryTest
    {
        private DataContext context;
        private IGenreRepository repository;
        private FakeDataHelper fakeDataHelper;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "apisteam")
            .Options;

            context = new DataContext(options);
            repository = new GenreRepository(context);
            fakeDataHelper = new FakeDataHelper(context);
        }

        [TestMethod]
        public void ListAllGenreAsync_WhenCalled_ReturnSuccess()
        {
            //Arrange
            DropDataBase();
            
            for(int i=0; i<25; i++)
            {
                fakeDataHelper.SetupGenre();
            }

            //Action
            IEnumerable<Genre> actual = repository.ListAll();

            //Assert   
            Assert.AreEqual(25, actual.Count());
        }

        [TestMethod]
        public void ListAllGenreAsync_WhenCalled_ReturnEmpty()
        {
            //Arrange
            DropDataBase();

            //Action
            IEnumerable<Genre> actual = repository.ListAll();

            //Assert   
            Assert.AreEqual(0, actual.Count());
        }

        private void DropDataBase()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}