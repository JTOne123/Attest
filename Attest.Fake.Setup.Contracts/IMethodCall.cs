using System;
using System.Linq.Expressions;
using Solid.Patterns.Visitor;

namespace Attest.Fake.Setup.Contracts
{
    /// <summary>
    /// Represents an object that allows creating new method call using callbacks mapping function
    /// </summary>
    /// <typeparam name="TService">Type of service</typeparam>
    /// <typeparam name="TCallback">Type of callback</typeparam>
    public interface IMethodCallInitialTemplate<TService, TCallback> where TService : class
    {
        IMethodCall<TService, TCallback> BuildCallbacks(
            Func<IHaveNoCallbacks<TCallback>, IHaveCallbacks<TCallback>> buildCallbacks);        
    }

    /// <summary>
    /// Represents a service's method call
    /// </summary>
    /// <typeparam name="TService">Type of service</typeparam>
    public interface IMethodCall<TService> : IAcceptor<IMethodCallVisitor<TService>> where TService : class
    {
        /// <summary>
        /// Method to be called
        /// </summary>
        Expression<Action<TService>> RunMethod { get; }     
    }

    /// <summary>
    /// Represents a service's method call with provided callbacks
    /// </summary>
    /// <typeparam name="TService">Type of service</typeparam>
    /// <typeparam name="TCallback">Type of callback</typeparam>
    public interface IMethodCall<TService, TCallback> : 
        IMethodCallbacksContainer<TCallback>, 
        IMethodCall<TService> where TService : class
    {        
    }
}