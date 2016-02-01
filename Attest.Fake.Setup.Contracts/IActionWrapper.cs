﻿using System;
using Solid.Patterns.Visitor;

namespace Attest.Fake.Setup.Contracts
{
    /// <summary>
    /// Wraps an action that has 0 parameters
    /// </summary>
    public interface IActionWrapper : IAcceptor<IActionWrapperVisitor, MethodCallbackTemplate>
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        Action Action { get; }
    }

    /// <summary>
    /// Wraps an action that has 1 parameter
    /// </summary>
    public interface IActionWrapper<T> : IAcceptor<IActionWrapperVisitor, MethodCallbackTemplate<T>>
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        Action<T> Action { get; }
    }

    /// <summary>
    /// Wraps an action that has 2 parameters
    /// </summary>
    public interface IActionWrapper<T1, T2> : IAcceptor<IActionWrapperVisitor, MethodCallbackTemplate<T1, T2>>
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        Action<T1, T2> Action { get; }
    }

    /// <summary>
    /// Wraps an action that has 3 parameters
    /// </summary>
    public interface IActionWrapper<T1, T2, T3> : IAcceptor<IActionWrapperVisitor, MethodCallbackTemplate<T1, T2, T3>>
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        Action<T1, T2, T3> Action { get; }
    }

    /// <summary>
    /// Wraps an action that has 4 parameters
    /// </summary>
    public interface IActionWrapper<T1, T2, T3, T4> : IAcceptor<IActionWrapperVisitor, MethodCallbackTemplate<T1, T2, T3, T4>>
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        Action<T1, T2, T3, T4> Action { get; }
    }

    /// <summary>
    /// Wraps an action that has 5 parameters
    /// </summary>
    public interface IActionWrapper<T1, T2, T3, T4, T5> : IAcceptor<IActionWrapperVisitor, MethodCallbackTemplate<T1, T2, T3, T4, T5>>
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        Action<T1, T2, T3, T4, T5> Action { get; }
    } 

    /// <summary>
    /// Visitor for all action wrappers
    /// </summary>
    public interface IActionWrapperVisitor
    {
        /// <summary>
        /// Visits the specified action wrapper resulting in method callback template.
        /// </summary>
        /// <param name="actionWrapper">The action wrapper.</param>
        /// <returns></returns>
        MethodCallbackTemplate Visit(IActionWrapper actionWrapper);
        /// <summary>
        /// Visits the specified action wrapper resulting in method callback template.
        /// </summary>
        /// <typeparam name="T">The type of action's parameter.</typeparam>
        /// <param name="actionWrapper">The action wrapper.</param>
        /// <returns></returns>
        MethodCallbackTemplate<T> Visit<T>(IActionWrapper<T> actionWrapper);
        /// <summary>
        /// Visits the specified action wrapper resulting in method callback template.
        /// </summary>
        /// <typeparam name="T1">The type of the action's first parameter.</typeparam>
        /// <typeparam name="T2">The type of the action's second parameter.</typeparam>
        /// <param name="actionWrapper">The action wrapper.</param>
        /// <returns></returns>
        MethodCallbackTemplate<T1, T2> Visit<T1, T2>(IActionWrapper<T1, T2> actionWrapper);
        /// <summary>
        /// Visits the specified action wrapper resulting in method callback template.
        /// </summary>
        /// <typeparam name="T1">The type of the action's first parameter.</typeparam>
        /// <typeparam name="T2">The type of the action's second parameter.</typeparam>
        /// <typeparam name="T3">The type of the action's third parameter.</typeparam>
        /// <param name="actionWrapper">The action wrapper.</param>
        /// <returns></returns>
        MethodCallbackTemplate<T1, T2, T3> Visit<T1, T2, T3>(IActionWrapper<T1, T2, T3> actionWrapper);
        /// <summary>
        /// Visits the specified action wrapper resulting in method callback template.
        /// </summary>
        /// <typeparam name="T1">The type of the action's first parameter.</typeparam>
        /// <typeparam name="T2">The type of the action's second parameter.</typeparam>
        /// <typeparam name="T3">The type of the action's third parameter.</typeparam>
        /// <typeparam name="T4">The type of the action's fourth parameter.</typeparam>
        /// <param name="actionWrapper">The action wrapper.</param>
        /// <returns></returns>
        MethodCallbackTemplate<T1, T2, T3, T4> Visit<T1, T2, T3, T4>(IActionWrapper<T1, T2, T3, T4> actionWrapper);
        /// <summary>
        /// Visits the specified action wrapper resulting in method callback template.
        /// </summary>
        /// <typeparam name="T1">The type of the action's first parameter.</typeparam>
        /// <typeparam name="T2">The type of the action's second parameter.</typeparam>
        /// <typeparam name="T3">The type of the action's third parameter.</typeparam>
        /// <typeparam name="T4">The type of the action's fourth parameter.</typeparam>
        /// <typeparam name="T5">The type of the action's fifth parameter.</typeparam>
        /// <param name="actionWrapper">The action wrapper.</param>
        /// <returns></returns>
        MethodCallbackTemplate<T1, T2, T3, T4, T5> Visit<T1, T2, T3, T4, T5>(IActionWrapper<T1, T2, T3, T4, T5> actionWrapper);
    }

    /// <summary>
    /// Wraps a return value
    /// </summary>
    public interface IResultWrapper<TResult> : IAcceptor<IResultWrapperVisitor, MethodCallbackWithResultTemplate<TResult>>        
    {
       
    }

    /// <summary>
    /// Wraps a return value with 1 parameter.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    /// <typeparam name="TResult">The type of the return value.</typeparam>    
    public interface IResultWrapper<T, TResult> : IAcceptor<IResultWrapperVisitor, MethodCallbackWithResultTemplate<T, TResult>>       
    {
    }

    /// <summary>
    /// Wraps a return value with 2 parameters
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>    
    public interface IResultWrapper<T1, T2, TResult> : IAcceptor<IResultWrapperVisitor, MethodCallbackWithResultTemplate<T1, T2, TResult>>        
    {
    }

    /// <summary>
    /// Visitor for all result wrappers
    /// </summary>
    public interface IResultWrapperVisitor
    {
        /// <summary>
        /// Visits the specified result wrapper resulting in method callback with return value template.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="resultWrapper">The result wrapper.</param>
        /// <returns></returns>
        MethodCallbackWithResultTemplate<TResult> Visit<TResult>(IResultWrapper<TResult> resultWrapper);
        /// <summary>
        /// Visits the specified result wrapper resulting in method callback with return value template.
        /// </summary>
        /// <typeparam name="T">The type of the parameter</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="resultWrapper">The result wrapper.</param>
        /// <returns></returns>
        MethodCallbackWithResultTemplate<T, TResult> Visit<T, TResult>(IResultWrapper<T, TResult> resultWrapper);
        /// <summary>
        /// Visits the specified result wrapper resulting in method callback with return value template.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="resultWrapper">The result wrapper.</param>
        /// <returns></returns>
        MethodCallbackWithResultTemplate<T1, T2, TResult> Visit<T1, T2, TResult>(IResultWrapper<T1, T2, TResult> resultWrapper);
    }   
}
