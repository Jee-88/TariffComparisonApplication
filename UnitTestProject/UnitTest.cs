using Microsoft.EntityFrameworkCore;
using Moq;
using TariffComparisonApplication.HandlerService;
using TariffComparisonApplication.Models;
using TariffComparisonApplication.Models.Entity;
using TariffComparisonApplication.Models.Enum;

namespace UnitTestProject
{
    public class UnitTest
    {

        [Fact]
        public void Calculate_AnnualCost_ForProductA()
        {
            // arrange
            var handlerMock = new Mock<IProductDataHandler>();

            var options = new DbContextOptionsBuilder<ProductDataBaseContext>()
                            .UseInMemoryDatabase(databaseName: "ProductDataBase")
                            .Options;
            using (var context = new ProductDataBaseContext(options))
            {
                var pID = Guid.NewGuid();
                context.ProductCosts.Add(new  ProductCostEntity()
                {
                    ProductEntityId = pID,
                    ProductCostEntityId = Guid.NewGuid(),
                    AdditionalCost = 0.22m,
                    BaseCost = 5,
                    BaseLimit = 12
                });

                context.Products.Add(new ProductEntity()
                {
                    ProductName = "Basic Electricity Tariff",
                productType = ProductType.BasicElectricityTariff,
                    ProductEntityId = pID,
                ProductDescription = "Basic plan"
                });
                context.SaveChanges();
            }

            // act
            using(var context = new ProductDataBaseContext(options))
            {
                ProductCostComparisonHandler productCostComparisonHandler = new ProductCostComparisonHandler(context, handlerMock.Object);
                var productCost = productCostComparisonHandler.CalculateProductAnnualCost(context.Products.First().productType, context.ProductCosts.First(), 3500);

                // assert
                Assert.Equal(830, productCost);
            }
        }

        [Fact]
        public void Calculate_AnnualCost_ForProductB()
        {
            // arrange
            var handlerMock = new Mock<IProductDataHandler>();

            var options = new DbContextOptionsBuilder<ProductDataBaseContext>()
                            .UseInMemoryDatabase(databaseName: "ProductDataBase")
                            .Options;
            using (var context = new ProductDataBaseContext(options))
            {
                var pID = Guid.NewGuid();
                context.ProductCosts.Add(new ProductCostEntity()
                {
                    ProductEntityId = pID,
                    ProductCostEntityId = Guid.NewGuid(),
                    PackageFixedCost = 800,
                    PackageLimit = 4000,
                    AdditionalCost = 0.30m,
                    BaseCost = 10,
                    BaseLimit = 10
                });

                context.Products.Add(new ProductEntity()
                {
                    ProductName = "Packaged Tariff",
                    productType = ProductType.PackagedTariff,
                    ProductEntityId = pID,
                    ProductDescription = "Package plan"
                });
                context.SaveChanges();
            }


            // act
            using (var context = new ProductDataBaseContext(options))
            {
                ProductCostComparisonHandler productCostComparisonHandler = new ProductCostComparisonHandler(context, handlerMock.Object);
                var productCost = productCostComparisonHandler.CalculateProductAnnualCost(context.Products.First().productType, context.ProductCosts.First(), 4500);

                // assert
                Assert.Equal(950, productCost);
            }
        }
    }
}