﻿using System;
using Attest.Tests.Core;
using Solid.Practices.IoC;

namespace Attest.Tests.XUnit
{
    /// <summary>
    /// Base class for all integration-tests fixtures that involve real IoC container and test bootstrapper
    /// and use xUnit.net as test framework provider
    /// </summary>
    /// <typeparam name="TContainer">Type of IoC container</typeparam>
    /// <typeparam name="TRootObject">Type of root object, from whom the test's flow starts</typeparam>
    /// <typeparam name="TBootstrapper">Type of bootstrapper</typeparam>
    public abstract class IntegrationTestsBase<TContainer, TRootObject, TBootstrapper> :
        IntegrationTestsBase<TContainer, TRootObject>,
        IRootObjectFactory,
        IDisposable
        where TContainer : IIocContainer, new() where TRootObject : class
    {
        private readonly IInitializationParametersManager<TContainer> _initializationParametersManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestsBase{TContainer, TRootObject, TBootstrapper}"/> class.
        /// </summary>
        /// <param name="resolutionStyle">The resolution style.</param>
        protected IntegrationTestsBase(InitializationParametersResolutionStyle resolutionStyle = InitializationParametersResolutionStyle.PerRequest)
        {
            _initializationParametersManager =
                InitializationParametersManagerStore<TBootstrapper, TContainer>.GetInitializationParametersManager(
                    resolutionStyle);
            ScenarioContext.Current = new Scenario();

            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            // xUnit.net does not have dedicated attributes for Setup methods; 
            // therefore the logic is put inside the constructor instead.
            Setup();
        }

        /// <summary>
        /// Override this method to implement custom test setup logic
        /// </summary>        
        protected override void Setup()
        {
            SetupCore();
            SetupOverride();
        }

        /// <summary>
        /// Override this method to implement custom test teardown logic
        /// </summary>        
        protected override void TearDown()
        {
            OnBeforeTeardown();
            TearDownCore();
            OnAfterTeardown();
        }

        private void SetupCore()
        {
            var initializationParameters = _initializationParametersManager.GetInitializationParameters();
            IocContainer = initializationParameters.IocContainer;
            //Then the scenario helper is initialized with the new instance of the IoC container and the root object factory
            ScenarioHelper.Initialize(IocContainer, this);
        }

        /// <summary>
        /// Provides additional opportunity to modify the test setup logic
        /// </summary>
        protected virtual void SetupOverride()
        {

        }

        private void TearDownCore()
        {
            ScenarioHelper.Clear();
            IocContainer.Dispose();            
        }

        /// <summary>
        /// Called when the teardown starts
        /// </summary>
        protected virtual void OnBeforeTeardown()
        {

        }

        /// <summary>
        /// Called when the teardown finishes
        /// </summary>
        protected virtual void OnAfterTeardown()
        {

        }

        private TRootObject CreateRootObjectTyped()
        {
            return CreateRootObject();
        }

        object IRootObjectFactory.CreateRootObject()
        {
            return CreateRootObjectTyped();
        }

        void IDisposable.Dispose()
        {
            TearDown();
        }
    }
}
