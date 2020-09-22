using Common.Domain.Entities;
using Common.Domain.Infra.Queue;
using Common.Infra.Queue;
using Domain.EnvioRemessa;
using Repository;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Api
{
    public class SimpleInjectorContainer
    {
        private static readonly Container Container = new Container();

        public static Container Build()
        {
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            Container.Register<Parameters>(Lifestyle.Singleton);

            Container.Register<IRabbitMqClient, RabbitMqClient>();

            Container.Register<IDigitalizacaoDocumentosService, DigitalizacaoDocumentosService>();

            Container.Register<IDigitalizacaoDocumentosRepository, DigitalizacaoDocumentosRepository>();

            Container.Verify();

            return Container;
        }

    }
}
