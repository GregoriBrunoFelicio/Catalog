using AutoBogus;
using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Services;
using Catalog.API.Services.Results;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.Tests.UnitTests.Services
{
    public abstract class CategoryServiceTests
    {
        protected AutoFaker<Category> CategoryFaker;
        protected Mock<ICategoryRepository> CategoryRepositoryMock;
        protected CategoryService CategoryService;

        [OneTimeSetUp]
        public void SetUp()
        {
            CategoryFaker = new AutoFaker<Category>();
            CategoryRepositoryMock = new Mock<ICategoryRepository>();
            CategoryService = new CategoryService(CategoryRepositoryMock.Object);
        }
    }

    public class AddCategoryTests : CategoryServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var category = CategoryFaker.Generate();

            CategoryRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(() => new List<Category>());

            result = await CategoryService.Add(category);
        }

        [Test]
        public void Should_Call_Method_Get() =>
            CategoryRepositoryMock.Verify(x => x.Get(
                It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);

        [Test]
        public void Should_Call_Method_Add() =>
            CategoryRepositoryMock.Verify(x => x.Add(It.IsAny<Category>()), Times.Once);

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Category created", true);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }


    public class AddCategoryWhenNameIsAlreadyInUseTests : CategoryServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var category = CategoryFaker.Generate();
            var categories = CategoryFaker.Generate(2);

            CategoryRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(() => categories);

            result = await CategoryService.Add(category);
        }

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("The category name is already in use", false);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
