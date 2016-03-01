using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Attest.Fake.Setup.Contracts;

namespace Attest.Fake.Setup
{
    /// <summary>
    /// Base class for method calls.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TCallback">The type of the callback.</typeparam>
    /// <seealso cref="Attest.Fake.Setup.MethodCallbacksContainerBase{TCallback}" />
    /// <seealso cref="Attest.Fake.Setup.Contracts.IMethodCall{TService, TCallback}" />    
    /// <seealso cref="Attest.Fake.Setup.Contracts.IHaveNoCallbacks{TCallback}" />
    public abstract class MethodCallBaseAsync<TService, TCallback> :
        MethodCallbacksContainerBase<TCallback>,
        IMethodCallAsync<TService, TCallback>,
        IAddCallbackShared<TCallback>
        where TService : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCallBase{TService, TCallback}"/> class.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        protected MethodCallBaseAsync(Expression<Func<TService, Task>> runMethod)
        {
            RunMethod = runMethod;
            RunMethodDescription = RunMethod.ToString();
        }

        /// <summary>
        /// Gets the run method.
        /// </summary>
        /// <value>
        /// The run method.
        /// </value>
        public Expression<Func<TService, Task>> RunMethod { get; private set; }

        /// <summary>
        /// Gets the run method description.
        /// </summary>
        /// <value>
        /// The run method description.
        /// </value>
        public override sealed string RunMethodDescription { get; protected set; }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public abstract void Accept(IMethodCallVisitorAsync<TService> visitor);
    }

    /// <summary>
    /// Represents async method call without return value.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>    
    public class MethodCallAsync<TService> : 
        MethodCallBaseAsync<TService, IMethodCallback>,
        IMethodCallInitialTemplateAsync<TService, IMethodCallback>,
        IHaveNoCallbacks<IMethodCallback> where TService : class
    {
        private MethodCallAsync(Expression<Func<TService, Task>> runMethod) : base(runMethod)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="MethodCall{TService}" /> using the specified run method.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <returns></returns>
        public static IMethodCallInitialTemplateAsync<TService, IMethodCallback> CreateMethodCall(
            Expression<Func<TService, Task>> runMethod)
        {
            return new MethodCallAsync<TService>(runMethod);
        }

        /// <summary>
        /// Adds custom callback to the callbacks container
        /// </summary>
        /// <param name="methodCallback">Custom callback</param>
        /// <returns>Callbacks container</returns>
        public IAddCallback<IMethodCallback> AddCallback(IMethodCallback methodCallback)
        {
            AddCallbackInternal(methodCallback);
            return this;
        }

        /// <summary>
        /// Adds successful completion callback to the callbacks container
        /// </summary>
        /// <returns>Callbacks container</returns>
        public IAddCallback<IMethodCallback> Complete()
        {
            Callbacks.Add(
                CallbackBuilder<ActionWrapper, IMethodCallbackTemplate<IActionWrapper>, IMethodCallback>.CreateCallbackBuilder()
                    .WithDefaultAction());
            return this;
        }

        /// <summary>
        /// Adds exception throwing callback to the callbacks container
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Callbacks container</returns>
        public IAddCallback<IMethodCallback> Throw(Exception exception)
        {
            Callbacks.Add(new OnErrorCallback(exception));
            return this;
        }

        /// <summary>
        /// Adds never-ending callback to the callbacks container
        /// </summary>
        /// <returns>Callbacks container</returns>
        public IAddCallback<IMethodCallback> WithoutCallback()
        {
            Callbacks.Add(ProgressCallback.Create().WithoutCallback().AsMethodCallback());
            return this;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(IMethodCallVisitorAsync<TService> visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Builds the method call with specified callbacks.
        /// </summary>
        /// <param name="buildCallbacks">The build callbacks.</param>
        /// <returns></returns>
        public IMethodCallAsync<TService, IMethodCallback> BuildCallbacks(Func<IHaveNoCallbacks<IMethodCallback>, IHaveCallbacks<IMethodCallback>> buildCallbacks)
        {
            buildCallbacks(this);
            return this;
        }

        IAddCallback<IMethodCallback> IAddCallback<IMethodCallback>.Complete(Action callback)
        {
            Callbacks.Add(
                CallbackBuilder<ActionWrapper, IMethodCallbackTemplate<IActionWrapper>, IMethodCallback>
                    .CreateCallbackBuilder()
                    .WithAction(new ActionWrapper(callback)).AsComplete().Build());
            return this;
        }
    }

    /// <summary>
    /// Represents method call without return value and one parameter.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="T">The type of the parameter</typeparam>    
    public class MethodCallAsync<TService, T> :
        MethodCallBaseAsync<TService, IMethodCallback<T>>,
        IMethodCallInitialTemplateAsync<TService, IMethodCallback<T>, T>,
        IHaveNoCallbacks<IMethodCallback<T>, T>,
        IGenerateMethodCallback<T> where TService : class
    {
        private Func<IHaveNoCallbacks<IMethodCallback<T>, T>, T, IHaveCallbacks<IMethodCallback<T>>> _callbacksProducer;

        private MethodCallAsync(Expression<Func<TService, Task>> runMethod)
            : base(runMethod)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="MethodCall{TService, T}" /> using the specified run method.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <returns></returns>
        public static IMethodCallInitialTemplateAsync<TService, IMethodCallback<T>, T> CreateMethodCall(Expression<Func<TService, Task>> runMethod)
        {
            return new MethodCallAsync<TService, T>(runMethod);
        }

        /// <summary>
        /// Adds custom callback to the callbacks container
        /// </summary>
        /// <param name="methodCallback">Custom callback</param>
        /// <returns>Callbacks container</returns>
        public IAddCallback<IMethodCallback<T>, T> AddCallback(IMethodCallback<T> methodCallback)
        {
            AddCallbackInternal(methodCallback);
            return this;
        }

        /// <summary>
        /// Adds successful completion callback to the callbacks container
        /// </summary>
        /// <returns>Callbacks container</returns>
        public IAddCallback<IMethodCallback<T>, T> Complete()
        {
            Callbacks.Add(
                CallbackBuilder<ActionWrapper<T>, IMethodCallbackTemplate<IActionWrapper<T>, T>, IMethodCallback<T>>.CreateCallbackBuilder()
                    .WithDefaultAction());
            return this;
        }

        /// <summary>
        /// Adds exception throwing callback to the callbacks container
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Callbacks container</returns>
        public IAddCallback<IMethodCallback<T>, T> Throw(Exception exception)
        {
            Callbacks.Add(new OnErrorCallback<T>(exception));
            return this;
        }

        /// <summary>
        /// Adds never-ending callback to the callbacks container
        /// </summary>
        /// <returns>Callbacks container</returns>
        public IAddCallback<IMethodCallback<T>, T> WithoutCallback()
        {
            Callbacks.Add(ProgressCallback<T>.Create().WithoutCallback().AsMethodCallback());
            return this;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        public override void Accept(IMethodCallVisitorAsync<TService> visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Builds the method call with specified callbacks.
        /// </summary>
        /// <param name="buildCallbacks">The build callbacks.</param>
        /// <returns></returns>
        public IMethodCallAsync<TService, IMethodCallback<T>> BuildCallbacks(
            Func<IHaveNoCallbacks<IMethodCallback<T>, T>, IHaveCallbacks<IMethodCallback<T>>> buildCallbacks)
        {
            buildCallbacks(this);
            return this;
        }

        IMethodCallAsync<TService, IMethodCallback<T>> IMethodCallInitialTemplateAsync<TService, IMethodCallback<T>, T>
            .BuildCallbacks(
            Func<IHaveNoCallbacks<IMethodCallback<T>, T>, T, IHaveCallbacks<IMethodCallback<T>>> callbacksProducer)
        {
            _callbacksProducer = callbacksProducer;
            return this;
        }

        /// <summary>
        /// Adds successful completion callback to the callbacks container
        /// </summary>
        /// <param name="callback">Successful completion callback</param>
        public IAddCallback<IMethodCallback<T>, T> Complete(Action<T> callback)
        {
            Callbacks.Add(new OnCompleteCallback<T>(callback));
            return this;
        }

        bool IGenerateMethodCallbackConditionChecker.CanGenerateCallback
        {
            get { return _callbacksProducer != null; }
        }

        void IGenerateMethodCallback<T>.GenerateCallback(T arg)
        {
            _callbacksProducer(this, arg);
        }
    }
}