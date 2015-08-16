﻿using System;
using System.Linq.Expressions;
using Attest.Fake.Core;
using FakeItEasy;

namespace Attest.Fake.FakeItEasy
{
    public class Fake<TFaked> : IFake<TFaked> where TFaked : class
    {
        private readonly TFaked _fake = A.Fake<TFaked>();

        public void VerifyCall(Expression<Action<TFaked>> expression)
        {
            A.CallTo(expression).MustHaveHappened();
        }

        public void VerifyNoCall(Expression<Action<TFaked>> expression)
        {
            A.CallTo(expression).MustNotHaveHappened();
        }

        public void VerifySingleCall(Expression<Action<TFaked>> expression)
        {
            A.CallTo(expression).MustHaveHappened(Repeated.Exactly.Once);
        }

        public IFake<TFaked> SetupWithCallback(Expression<Action<TFaked>> expression, Action action)
        {
            A.CallTo(() => expression.Compile().Invoke(_fake)).Invokes(action);
            return this;
        }

        public IFake<TFaked> SetupWithResult<TResult>(Expression<Func<TFaked, TResult>> expression, TResult result)
        {
            A.CallTo(() =>expression.Compile().Invoke(_fake)).Returns(result);
            return this;
        }

        public IFake<TFaked> SetupWithException<TResult>(Expression<Func<TFaked, TResult>> expression, Exception exception)
        {
            A.CallTo(() => expression.Compile().Invoke(_fake)).Throws(exception);
            return this;
        }

        public TFaked Object
        {
            get { return _fake; }
        }
    }
}
