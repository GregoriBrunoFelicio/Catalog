using AutoBogus;
using Catalog.API.Data;
using Catalog.API.Inputs;
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
    public abstract class ProductServiceTests
    {
        protected AutoFaker<Product> ProductFaker = new();
        protected AutoFaker<CreateProductInput> CreateProductFaker = new();
        protected AutoFaker<UpdateProductInput> UpdateProductFaker = new();
        protected Mock<IProductRepository> ProductRepositoryMock = new();
        protected Mock<ICategoryRepository> CategoryRepositoryMock = new();
        protected IProductService ProductService;

        [OneTimeSetUp]
        public void SetUp() =>
            ProductService = new ProductService(ProductRepositoryMock.Object, CategoryRepositoryMock.Object);
    }

    public class AddProductTests : ProductServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var product = CreateProductFaker.Generate();

            ProductRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(() => new List<Product>());
            CategoryRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(() => new Category());

            result = await ProductService.Add(product);
        }

        [Test]
        public void Should_Call_Method_Get() =>
            ProductRepositoryMock.Verify(x => x.Get(
                It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);

        [Test]
        public void Should_Call_Method_Add() =>
            ProductRepositoryMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Produto criado com sucesso", true);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class AddProductWhenNameIsAlreadyInUseTests : ProductServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var product = CreateProductFaker.Generate();
            var products = ProductFaker.Generate(2);

            ProductRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(() => products);

            result = await ProductService.Add(product);
        }

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Nome do produto já está em uso", false);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class AddProductWhenCategoryIsNotFoundTests : ProductServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var product = CreateProductFaker.Generate();

            ProductRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(() => new List<Product>());
            CategoryRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(() => null);

            result = await ProductService.Add(product);
        }

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Categoria informada não foi encontrada", false);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class UpdateProductTests : ProductServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var product = UpdateProductFaker.Generate();

            ProductRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(() => new List<Product>());
            CategoryRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(() => new Category());

            result = await ProductService.Update(product);
        }

        [Test]
        public void Should_Call_Method_Get() =>
            ProductRepositoryMock.Verify(x => x.Get(
                It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);

        [Test]
        public void Should_Call_Method_Update() =>
            ProductRepositoryMock.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Produto atualizado com sucesso", true);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class UpdateProductWhenNameIsAlreadyInUseTests : ProductServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var product = UpdateProductFaker.Generate();
            var products = ProductFaker.Generate(2);

            ProductRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(() => products);

            ProductRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>()))
                .ReturnsAsync(() => new Product { Name = $"{product.Name}-test" });

            result = await ProductService.Update(product);
        }

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Nome do produto já está em uso", false);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class UpdateProductWhenCategoryIsNotFoundTests : ProductServiceTests
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var product = UpdateProductFaker.Generate();

            ProductRepositoryMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(() => new List<Product>());
            CategoryRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(() => null);

            result = await ProductService.Update(product);
        }

        [Test]
        public void Should_Return_The_Expected_Result()
        {
            var expectedResult = new Result("Categoria informada não foi encontrada", false);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
