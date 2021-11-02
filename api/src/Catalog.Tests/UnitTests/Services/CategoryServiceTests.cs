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
        protected AutoFaker<Category> CategoryFaker = new();
        protected Mock<ICategoryRepository> CategoryRepositoryMock = new();
        protected ICategoryService CategoryService;

        [OneTimeSetUp]
        public void SetUp() =>
            CategoryService = new CategoryService(CategoryRepositoryMock.Object);
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
            var expectedResult = new Result("Categoria criada com sucesso", true);
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
            var expectedResult = new Result("Nome da categoria já está em uso", false);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class UpdateCategoryTests : CategoryServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var category = CategoryFaker.Generate();

            CategoryRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(() => new List<Category>());

            result = await CategoryService.Update(category);
        }

        [Test]
        public void Should_Call_Method_Get() =>
            CategoryRepositoryMock.Verify(x => x.Get(
                It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);

        [Test]
        public void Should_Call_Method_Update() =>
            CategoryRepositoryMock.Verify(x => x.Update(It.IsAny<Category>()), Times.Once);

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Categoria atualizada com sucesso", true);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class UpdateCategoryWhenNameIsAlreadyInUseTests : CategoryServiceTests
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

            result = await CategoryService.Update(category);
        }

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Nome da categoria já está em uso", false);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
