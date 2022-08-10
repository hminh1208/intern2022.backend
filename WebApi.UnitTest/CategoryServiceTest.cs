using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Enums;
using WebApi.Helpers;
using WebApi.Models.Category;
using WebApi.Services;

namespace WebApi.UnitTest
{
    public class CategoryServiceTest : IDisposable
    {
        private DataContext _dbContext;
        private IMapper _mapper;

        public CategoryServiceTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            _dbContext = new DataContext(options, "Testing");
            _dbContext.Database.EnsureCreated();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile()); //your automapperprofile 
            });
            _mapper = mockMapper.CreateMapper();

            _dbContext.Accounts.Add(new Account());
            _dbContext.SaveChanges();
            var account = _dbContext.Accounts.FirstOrDefault();
            _dbContext.Categories.AddRange(CategoryMockData.GenerateCategoriesMockData(account));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Fact]
        public async Task GetAll_WithApprovedCategory_ReturnsApprovedCategory()
        {
            // Arrange
            var categoryService = new CategoryService(_dbContext, _mapper);

            // Act
            var result = await categoryService.GetAll(StatusEnum.APPROVED, "", 0, 10);

            // Assert
            result.Should().HaveCount(CategoryMockData.NUMBER_OF_APPROVED);
        }

        [Fact]
        public async Task CountAll_WithApprovedCategory_ReturnsNumberOfApprovedCategory()
        {
            // Arrange
            //_dbContext.Accounts.Add(new Account());
            //_dbContext.SaveChanges();
            //var account = await _dbContext.Accounts.FirstOrDefaultAsync();
            //_dbContext.Categories.AddRange(CategoryMockData.GenerateCategoriesMockData(account));
            //_dbContext.SaveChanges();

            var categoryService = new CategoryService(_dbContext, _mapper);

            // Act
            var result = await categoryService.CountAll(StatusEnum.APPROVED, "");

            // Assert
            Assert.Equal(result, CategoryMockData.NUMBER_OF_APPROVED);
        }

        [Fact]
        public async Task GetAll_WithDeletedCategory_ReturnsDeltedCategory()
        {
            // Arrange
            var categoryService = new CategoryService(_dbContext, _mapper);

            // Act
            var result = await categoryService.GetAll(StatusEnum.DELETED, "", 0, 10);

            // Assert
            result.Should().HaveCount(CategoryMockData.NUMBER_OF_DELETED);
        }

        [Fact]
        public async Task CountAll_WithDeletedCategory_ReturnsNumberOfDeletedCategory()
        {
            // Arrange
            var categoryService = new CategoryService(_dbContext, _mapper);

            // Act
            var result = await categoryService.CountAll(StatusEnum.DELETED, "");

            // Assert
            Assert.Equal(result, CategoryMockData.NUMBER_OF_DELETED);
        }

        //[Fact]
        //public async Task GetAll_WithFirst5ApprovedCategory_ReturnsDeltedCategory()
        //{
        //    // Arrange
        //    _dbContext.Accounts.Add(new Account());
        //    _dbContext.SaveChanges();
        //    var account = await _dbContext.Accounts.FirstOrDefaultAsync();
        //    _dbContext.Categories.AddRange(CategoryMockData.GenerateCategoriesMockData(account));
        //    _dbContext.SaveChanges();

        //    var categoryService = new CategoryService(_dbContext, _mapper);

        //    // Act
        //    var result = await categoryService.GetAll(StatusEnum.DELETED, "", 0, 10);

        //    // Assert
        //    result.Should().HaveCount(numnb);
        //}

        [Fact]
        public async Task GetById_WithExistingCategory_ReturnsExistingCategory()
        {
            // Arrange
            const int SELECTED_ID = 1;

            var categoryService = new CategoryService(_dbContext, _mapper);

            // Act
            var result = await categoryService.GetById(SELECTED_ID);

            // Assert
            Assert.Equal(SELECTED_ID, result.Id);
        }

        [Fact]
        public async Task GetById_WithNotExistingCategory_ReturnsNull()
        {
            // Arrange
            const int SELECTED_ID = 999;

            var categoryService = new CategoryService(_dbContext, _mapper);

            // Act
            var result = await categoryService.GetById(SELECTED_ID);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_WithNotExistingCategory_ReturnsNull()
        {
            // Arrange
            _dbContext.Accounts.Add(new Account());
            _dbContext.SaveChanges();
            var account = await _dbContext.Accounts.FirstOrDefaultAsync();

            var categoryRequestDto = new CategoryRequestDto()
            {
                Name = "Category 11"
            };

            var categoryService = new CategoryService(_dbContext, _mapper);

            // Act
            var result = await categoryService.AddAsync(categoryRequestDto, account);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusEnum.APPROVED, result.Status);
        }
    }
}
