//using AutoFixture;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using WebApi.Controllers;
//using WebApi.Entities;
//using WebApi.Enums;
//using WebApi.Models.Category;
//using WebApi.Models.Common;
//using WebApi.Services;

//namespace WebApi.UnitTest
//{
//    public class CategoryControllerTest
//    {
//        [Fact]
//        public async Task GetApprovedAsync_WithExistingApprovedItem_ReturnsListApprovedItems()
//        {
//            // Arrange
//            const string keyword = "";
//            const int page = 0;
//            const int pageSize = 10;
//            const int numberOfExpectedItems = 5;

//            var categoryService = new Mock<ICategoryService>();

//            var listApprovedItems = this.CreateRandomListCategory(numberOfExpectedItems);

//            categoryService
//                .Setup(service => service.GetAll(StatusEnum.APPROVED, keyword, page, pageSize))
//                .ReturnsAsync(listApprovedItems);

//            categoryService
//                .Setup(service => service.CountAll(StatusEnum.APPROVED, keyword))
//                .ReturnsAsync(listApprovedItems.Count);

//            var categoriesController = new CategoriesController(categoryService.Object);
//            // Act
//            var response = await categoriesController.GetApprovedAsync(keyword, page, pageSize);

//            // Assert
//            Assert.IsType<ApiResponseDto>(response.Value);
//            var dto = (response as ActionResult<ApiResponseDto>).Value;
//            Assert.NotNull(dto);
//            Assert.IsType<ApiResponseDto>(dto);

//            var results = dto!.Results as PaginationResponseDto;
//            Assert.NotNull(results);
//            Assert.Equal(numberOfExpectedItems, results!.TotalCount);

//            Assert.IsType<List<CategoryResponseDto>>(results.Items);
//            var items = results!.Items as List<CategoryResponseDto>;
//            items.Should().BeEquivalentTo(listApprovedItems);
//        }

//        [Fact]
//        public async Task GetAsync_WithExistingItem_ReturnExpectedItem()
//        {
//            // Arrange
//            const int Id = 1;
//            var categoryMock = CreateRandomCategory(Id);

//            var categoryService = new Mock<ICategoryService>();
//            categoryService
//                .Setup(service => service.GetById(Id))
//                .ReturnsAsync(categoryMock);

//            var categoriesController = new CategoriesController(categoryService.Object);

//            // Act
//            var response = await categoriesController.GetAsync(Id);

//            // Fact
//            Assert.IsType<ApiResponseDto>(response.Value);
//            var dto = (response as ActionResult<ApiResponseDto>).Value;
//            Assert.NotNull(dto);
//            Assert.IsType<ApiResponseDto>(dto);

//            var results = dto!.Results as SingleItemResponseDto;
//            Assert.NotNull(results);
//            results!.Item.Should().BeEquivalentTo(categoryMock);
//        }

//        [Fact]
//        public async Task GetAsync_WithNotExistingItem_ReturnNullAnd404()
//        {
//            // Arrange
//            const int Id = 1;

//            var categoryService = new Mock<ICategoryService>();

//            var categoriesController = new CategoriesController(categoryService.Object);

//            // Act
//            var response = await categoriesController.GetAsync(Id);

//            // Fact
//            Assert.IsType<ApiResponseDto>(response.Value);
//            var dto = (response as ActionResult<ApiResponseDto>).Value;
//            Assert.NotNull(dto);
//            Assert.IsType<ApiResponseDto>(dto);

//            var results = dto!.Results as SingleItemResponseDto;
//            Assert.NotNull(results);
//            Assert.Null(results!.Item);
//            Assert.Equal(404, dto.StatusCode);
//        }
//        [Fact]
//        public async Task AddAsync_WithInvalidItem_ReturnModelStateError()
//        {
//            // Arrange
//            var categoryService = new Mock<ICategoryService>();

//            var categoryRequestDto = new CategoryRequestDto()
//            {
//                Name = "Category Name"
//            };
//            var categoryResponseDto = new CategoryResponseDto("Category Name", "category-name", StatusEnum.APPROVED, null);
//            var account = new Account();

//            //categoryService
//            //    .Setup(service => service.addAsync(categoryRequestDto, account))
//            //    .ReturnsAsync(categoryResponseDto);

//            var categoryController = new CategoriesController(categoryService.Object);
//            var HttpContext = new DefaultHttpContext();
//            HttpContext.Items["Account"] = account;
//            var ctx = new ControllerContext() { HttpContext = HttpContext };
//            categoryController.ControllerContext = ctx;

//            // Act
//            var response = await categoryController.AddAsync(categoryRequestDto);

//            // Assert
//            Assert.IsType<ApiResponseDto>(response.Value);
//            var dto = (response as ActionResult<ApiResponseDto>).Value;
//            Assert.NotNull(dto);
//            Assert.IsType<ApiResponseDto>(dto);

//            var results = dto!.Results as SingleItemResponseDto;
//            Assert.NotNull(results);
//            Assert.Equal(200, dto.StatusCode);
//            results!.Item.Should().BeEquivalentTo(categoryResponseDto);
//        }

//        private CategoryResponseDto CreateRandomCategory(int id)
//        {
//            Fixture fixture = new Fixture();
//            return new CategoryResponseDto(fixture.Create<string>(), fixture.Create<string>(), StatusEnum.APPROVED, null);
//        }

//        private List<CategoryResponseDto> CreateRandomListCategory(int numberOfItem)
//        {
//            var listApprovedItems = new List<CategoryResponseDto>();
//            for (int i = 0; i < numberOfItem; i++)
//            {
//                listApprovedItems.Add(CreateRandomCategory(i));
//            }
//            return listApprovedItems;
//        }
//    }
//}