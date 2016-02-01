﻿using System;
using System.Linq.Expressions;
using Attest.Fake.Core;
using Moq;

namespace Attest.Fake.Moq
{
    /// <summary>
    /// Implementation of fake using Moq framework
    /// </summary>
    /// <typeparam name="TFaked">Type of faked service</typeparam>
    public class Fake<TFaked> : IFake<TFaked> where TFaked: class
    {
        private readonly Mock<TFaked> _fake;

        internal Fake(Mock<TFaked> fake)
        {
            _fake = fake;
        }

        /// <summary>
        /// Sets up the fake according to the provided setup expression of fake call
        /// </summary>
        /// <param name="expression">Setup expression</param>
        /// <returns>Fake callback after the setup</returns>
        public IFakeCallback Setup(Expression<Action<TFaked>> expression)
        {
            return new MoqFakeCallback<TFaked>(_fake.Setup(expression));
        }

        /// <summary>
        /// Sets up the fake according to the provided setup expression of fake call with return value
        /// </summary>
        /// <typeparam name="TResult">Type of return value</typeparam>
        /// <param name="expression">Setup expression</param>
        /// <returns>Fake callback after the setup</returns>
        public IFakeCallbackWithResult<TResult> Setup<TResult>(Expression<Func<TFaked, TResult>> expression)
        {
            return new MoqFakeCallbackWithResult<TFaked, TResult>(_fake.Setup(expression));
        }

        /// <summary>
        /// Faked service
        /// </summary>
        public TFaked Object
        {
            get { return _fake.Object; }
        }

        /// <summary>
        /// Verifies that a specific method was called on the fake
        /// </summary>
        /// <param name="expression">Verified method's call definition</param>
        public void VerifyCall(Expression<Action<TFaked>> expression)
        {
            _fake.Verify(expression);
        }

        /// <summary>
        /// Verifies that a specific method was not called on the fake
        /// </summary>
        /// <param name="expression">Verified method's call definition</param>
        public void VerifyNoCall(Expression<Action<TFaked>> expression)
        {
            _fake.Verify(expression, Times.Never);
        }

        /// <summary>
        /// Verifies that a specific method was called exactly once on the fake
        /// </summary>
        /// <param name="expression">Verified method's call definition</param>
        public void VerifySingleCall(Expression<Action<TFaked>> expression)
        {
            _fake.Verify(expression, Times.Once);
        }
    }
}
