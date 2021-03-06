using System;
using Ninject;
using IContainer = Xam.Zero.Ioc.IContainer;

namespace Xam.Zero.Ninject
{
    public class NinjectZeroContainer: IContainer
    {
        private readonly StandardKernel _kernel;
        
        public NinjectZeroContainer(StandardKernel kernel)
        {
            this._kernel = kernel;
        }
       
        public void Register<T>(bool transient) where T : class
        {
            if (transient)
            {
                this._kernel.Bind<T>().ToSelf().InTransientScope();
                return;
            }

            this._kernel.Bind<T>().ToSelf().InSingletonScope();
        }

        public void Register<T, TImpl>(bool transient) where TImpl : T
        {
            if (transient)
            {
                this._kernel.Bind<T>().To<TImpl>().InTransientScope();
                return;
            }

            this._kernel.Bind<T>().To<TImpl>().InSingletonScope();
        }

        public void Register(Type type, bool transient)
        {
            if (transient)
            {
                this._kernel.Bind(type).ToSelf().InTransientScope();
                return;
            }

            this._kernel.Bind(type).ToSelf().InSingletonScope();
        }

        public void RegisterInstance<T>(T instance)
        {
            this._kernel.Bind<T>().ToConstant(instance);
        }

        public T Resolve<T>()
        {
            return this._kernel.Get<T>();
        }

        public object Resolve(Type type)
        {
            return this._kernel.Get(type);
        }

        public static NinjectZeroContainer Build(StandardKernel kernel)
        {
            return new NinjectZeroContainer(kernel);
        }
    }

}