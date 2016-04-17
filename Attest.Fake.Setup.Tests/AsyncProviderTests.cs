﻿using Attest.Fake.Builders;
using Attest.Fake.Moq;
using NUnit.Framework;

namespace Attest.Fake.Setup.Tests
{
    [TestFixture]    
    class AsyncProviderTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            FakeFactoryContext.Current = new FakeFactory();
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithResultAndNoParametersReturnsCorrectValue()
        {
            var items = new[]
            {
                new WarehouseItemDto
                {
                    Kind = "Top",
                    Price = 5,
                    Quantity = 1
                }
            };
            var builder = WarehouseProviderBuilder.CreateBuilder();
            builder.WithWarehouseItems(items);

            var provider = builder.GetService();
            var actualItems = await provider.GetWarehouseItems();

            CollectionAssert.AreEqual(items, actualItems);
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithResultAndOneParameterReturnsCorrectValue()
        {
            var items = new[]
            {
                new WarehouseItemDto
                {
                    Kind = "Top",
                    Price = 5,
                    Quantity = 1
                }
            };
            var builder = WarehouseProviderBuilder.CreateBuilder();
            builder.WithWarehouseItems(items);

            var provider = builder.GetService();
            var actualItems = await provider.GetWarehouseItemsWithOneParameter("firstParameter");

            CollectionAssert.AreEqual(items, actualItems);
        }

        [Test]        
        public async void AsyncProviderIsSetup_MethodCallWithResultAndTwoParametersReturnsCorrectValue()
        {
            var items = new[]
            {
                new WarehouseItemDto
                {
                    Kind = "Top",
                    Price = 5,
                    Quantity = 1
                }
            };
            var builder = WarehouseProviderBuilder.CreateBuilder();
            builder.WithWarehouseItems(items);

            var provider = builder.GetService();
            var actualItems = await provider.GetWarehouseItemsWithTwoParameters("firstParameter", "secondParameter");

            CollectionAssert.AreEqual(items, actualItems);
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithResultAndThreeParametersReturnsCorrectValue()
        {
            var items = new[]
            {
                new WarehouseItemDto
                {
                    Kind = "Top",
                    Price = 5,
                    Quantity = 1
                }
            };
            var builder = WarehouseProviderBuilder.CreateBuilder();
            builder.WithWarehouseItems(items);

            var provider = builder.GetService();
            var actualItems =
                await
                    provider.GetWarehouseItemsWithThreeParameters("firstParameter", "secondParameter", "thirdParameter");

            CollectionAssert.AreEqual(items, actualItems);
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithResultAndFourParametersReturnsCorrectValue()
        {
            var items = new[]
            {
                new WarehouseItemDto
                {
                    Kind = "Top",
                    Price = 5,
                    Quantity = 1
                }
            };
            var builder = WarehouseProviderBuilder.CreateBuilder();
            builder.WithWarehouseItems(items);

            var provider = builder.GetService();
            var actualItems =
                await
                    provider.GetWarehouseItemsWithFourParameters("firstParameter", "secondParameter", "thirdParameter", "fourthParameter");

            CollectionAssert.AreEqual(items, actualItems);
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithResultAndFiveParametersReturnsCorrectValue()
        {
            var items = new[]
            {
                new WarehouseItemDto
                {
                    Kind = "Top",
                    Price = 5,
                    Quantity = 1
                }
            };
            var builder = WarehouseProviderBuilder.CreateBuilder();
            builder.WithWarehouseItems(items);

            var provider = builder.GetService();
            var actualItems =
                await
                    provider.GetWarehouseItemsWithFiveParameters("firstParameter", "secondParameter", "thirdParameter",
                        "fourthParameter", "fifthParameter");

            CollectionAssert.AreEqual(items, actualItems);
        }

        [Test]        
        public async void AsyncProviderIsSetup_MethodCallWithoutResultAndNoParametersCompletesSuccessfully()
        {           
            var builder = LoginProviderBuilder.CreateBuilder();            

            var provider = builder.GetService();
            await provider.Login();

            var isLoggedIn = provider.IsLoggedIn;
            Assert.IsTrue(isLoggedIn);            
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithoutResultAndOneParameterCompletesSuccessfully()
        {
            var builder = LoginProviderBuilder.CreateBuilder();

            var provider = builder.GetService();
            await provider.LoginWithOneParameter("parameter");

            var isLoggedIn = provider.IsLoggedIn;
            Assert.IsTrue(isLoggedIn);
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithoutResultAndTwoParametersCompletesSuccessfully()
        {
            var builder = LoginProviderBuilder.CreateBuilder();

            var provider = builder.GetService();
            await provider.LoginWithTwoParameters("firstParameter", "secondParameter");

            var isLoggedIn = provider.IsLoggedIn;
            Assert.IsTrue(isLoggedIn);
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithoutResultAndThreeParametersCompletesSuccessfully()
        {
            var builder = LoginProviderBuilder.CreateBuilder();

            var provider = builder.GetService();
            await provider.LoginWithThreeParameters("firstParameter", "secondParameter", "thirdParameter");

            var isLoggedIn = provider.IsLoggedIn;
            Assert.IsTrue(isLoggedIn);
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithoutResultAndFourParametersCompletesSuccessfully()
        {
            var builder = LoginProviderBuilder.CreateBuilder();

            var provider = builder.GetService();
            await provider.LoginWithFourParameters("firstParameter", "secondParameter", "thirdParameter", "fourthParameter");

            var isLoggedIn = provider.IsLoggedIn;
            Assert.IsTrue(isLoggedIn);
        }

        [Test]
        public async void AsyncProviderIsSetup_MethodCallWithoutResultAndFiveParametersCompletesSuccessfully()
        {
            var builder = LoginProviderBuilder.CreateBuilder();

            var provider = builder.GetService();
            await
                provider.LoginWithFiveParameters("firstParameter", "secondParameter", "thirdParameter",
                    "fourthParameter", "fifthParameter");

            var isLoggedIn = provider.IsLoggedIn;
            Assert.IsTrue(isLoggedIn);
        }
    }
}