using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using AdventureWorksApp.Models;
using AdventureWorksApp.Controllers;


namespace AdventureWorksApp.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<AdventureWorks2016Context> _dbContextMock;

        public ProductControllerTests()
        {
            _dbContextMock = new Mock<AdventureWorks2016Context>();
            _productController = new ProductController(_dbContextMock.Object);
        }

        [Fact]
        public async void GetProduct_ShouldReturnProductById()
        {
            int productId = 1;
            Product product = new Product
            {
                ProductId = productId
            };

            _dbContextMock.Setup(context => context.Product.FindAsync(productId)).ReturnsAsync(product);
            var response = await _productController.GetProduct(productId);

            Assert.Equal(productId, response.Value.ProductId);
        }

        //[Fact]
        //public async void GetProduct_ShouldReturnPNotFound()
        //{
        //    int productId = 100;
        //    _dbContextMock.Setup(context => context.Product.FindAsync(productId)).ReturnsAsync((Product)null);
        //    var response = await _productController.GetProduct(productId);

        //    Assert.IsType<NotFoundResult>(response.Result);
        //}
    }
}
