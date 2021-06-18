using LogComex.Repositorio;
using LogComex.Servicos;
using Ninject;

namespace LogComex.App_Start
{
    public class NinjectWebCommon
    {
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBlMasterRepositorio>().To<BlMasterRepositorio>();
            kernel.Bind<ILogComexService>().To<LogComexService>();
        }
    }
}