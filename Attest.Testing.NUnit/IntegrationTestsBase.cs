﻿using Attest.Testing.Core;
using NUnit.Framework;
using Solid.Practices.IoC;

namespace Attest.Testing.NUnit
{
    /// <summary>
    /// Base class for all integration-tests fixtures that involve real IoC container and test bootstrapper
    /// and use NUnit as test framework provider.
    /// </summary>
    /// <typeparam name="TContainer">Type of IoC container.</typeparam>
    /// <typeparam name="TRootObject">Type of root object, from whom the test's flow starts.</typeparam>
    /// <typeparam name="TBootstrapper">Type of bootstrapper.</typeparam>
    public abstract class IntegrationTestsBase<TContainer, TRootObject, TBootstrapper> : 
        IntegrationTestsBase<TContainer, TRootObject>,
        IRootObjectFactory       
        where TContainer : IIocContainer, new() where TRootObject : class
    {
        private readonly IInitializationParametersManager<TContainer> _initializationParametersManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestsBase{TContainer, TRootObject, TBootstrapper}"/> class.
        /// </summary>
        /// <param name="resolutionStyle">The resolution style.</param>
        protected IntegrationTestsBase(
            InitializationParametersResolutionStyle resolutionStyle = InitializationParametersResolutionStyle.PerRequest)
        {
            InitializationParametersManagerStore<TBootstrapper, TContainer>.BootstrapperInit =
                    OnCreateBootstrapperOverride;
            _initializationParametersManager =
                InitializationParametersManagerStore<TBootstrapper, TContainer>.GetInitializationParametersManager(
                    resolutionStyle);
            ScenarioContext.Current = new Scenario();
        }

        /// <summary>
        /// Override this method to implement custom test setup logic
        /// </summary>
        [SetUp]
        protected override void Setup()
        {
            SetupCore();
            SetupOverride();
        }

        /// <summary>
        /// Override this method to implement custom test teardown logic
        /// </summary>
        [TearDown]
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

        /// <summary>
        /// Override this method to inject custom logic upon bootstrapper creation.
        /// </summary>
        protected virtual void OnCreateBootstrapperOverride(TBootstrapper bootstrapper)
        {
            
        }

        private void TearDownCore()
        {
            ScenarioHelper.Clear();
            IocContainer.Dispose();
            //Dispose();
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
    }

    /// <summary>
    /// Base class for all integration-tests fixtures that involve real IoC container, its adapter, and test bootstrapper
    /// and use NUnit as test framework provider.
    /// </summary>
    /// <typeparam name="TContainer">Type of IoC container.</typeparam>
    /// <typeparam name="TContainerAdapter">Type of IoC container adapter.</typeparam>
    /// <typeparam name="TRootObject">Type of root object, from whom the test's flow starts.</typeparam>
    /// <typeparam name="TBootstrapper">Type of bootstrapper.</typeparam>
    public abstract class IntegrationTestsBase<TContainer, TContainerAdapter, TRootObject, TBootstrapper> :
        Attest.Testing.Core.IntegrationTestsBase<TContainer, TContainerAdapter, TRootObject>,
        IRootObjectFactory
        where TContainer : new()
        where TContainerAdapter : class, IIocContainer, IIocContainerAdapter<TContainer>, new()
        where TRootObject : class 
    {
        private readonly IInitializationParametersManager<TContainer> _initializationParametersManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestsBase{TContainer, TRootObject, TBootstrapper}"/> class.
        /// </summary>
        /// <param name="resolutionStyle">The resolution style.</param>
        protected IntegrationTestsBase(
            InitializationParametersResolutionStyle resolutionStyle = InitializationParametersResolutionStyle.PerRequest)
        {
            InitializationParametersManagerStore<TBootstrapper, TContainer>.BootstrapperInit =
                    OnCreateBootstrapperOverride;
            _initializationParametersManager =
                InitializationParametersManagerStore<TBootstrapper, TContainer>.GetInitializationParametersManager(
                    resolutionStyle);
            ScenarioContext.Current = new Scenario();
        }

        /// <summary>
        /// Override this method to implement custom test setup logic
        /// </summary>
        [SetUp]
        protected override void Setup()
        {
            SetupCore();
            SetupOverride();
        }

        /// <summary>
        /// Override this method to implement custom test teardown logic
        /// </summary>
        [TearDown]
        protected override void TearDown()
        {
            OnBeforeTeardown();
            TearDownCore();
            OnAfterTeardown();
        }

        private void SetupCore()
        {
            var initializationParameters = _initializationParametersManager.GetInitializationParameters();
            IocContainer = CreateAdapter(initializationParameters.IocContainer);
            //Then the scenario helper is initialized with the new instance of the IoC container and the root object factory
            ScenarioHelper.Initialize(IocContainer, this);
        }

        /// <summary>
        /// Provides additional opportunity to modify the test setup logic
        /// </summary>
        protected virtual void SetupOverride()
        {

        }

        /// <summary>
        /// Override this method to inject custom logic upon bootstrapper creation.
        /// </summary>
        protected virtual void OnCreateBootstrapperOverride(TBootstrapper bootstrapper)
        {

        }

        /// <summary>
        /// Override to provide adapter creation logic.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        protected abstract TContainerAdapter CreateAdapter(TContainer container);        

        private void TearDownCore()
        {
            ScenarioHelper.Clear();
            IocContainer.Dispose();
            //Dispose();
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
    }
}
