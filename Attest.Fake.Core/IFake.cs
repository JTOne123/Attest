﻿using System;
using System.Linq.Expressions;

namespace Attest.Fake.Core
{
    public interface IFake<TFaked> : IMock<TFaked> where TFaked: class
    {
        [Obsolete("Use fake callback returning Setup... methods")]
        IFake<TFaked> SetupWithCallback(Expression<Action<TFaked>> expression, Action action);
        [Obsolete("Use fake callback returning Setup... methods")]
        IFake<TFaked> SetupWithResult<TResult>(Expression<Func<TFaked, TResult>> expression, TResult result);
        [Obsolete("Use fake callback returning Setup... methods")]
        IFake<TFaked> SetupWithException<TResult>(Expression<Func<TFaked, TResult>> expression, Exception exception);
        IFakeCallback Setup(Expression<Action<TFaked>> expression);
        IFakeCallbackWithResult<TResult> Setup<TResult>(Expression<Func<TFaked, TResult>> expression);
    }

    /// <summary>
    /// Represents an object that fakes another type.
    /// </summary>
    /// <typeparam name="TFaked">Type of fake</typeparam>
    public interface IHaveFake<out TFaked> where TFaked : class
    {
        TFaked Object { get; }
    }
}
