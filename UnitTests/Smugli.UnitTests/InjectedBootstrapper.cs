namespace Smugli.UnitTests
{
    #region Using Directives

    using Nancy;

    using Smugli.Domain;
    using Smugli.Dto;

    using TinyIoC;

    #endregion

    public class InjectedBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IPredictionFactory _predictionFactory;

        private readonly IPredictionRepository _predictionRepository;

        public InjectedBootstrapper(IPredictionRepository predictionRepository, IPredictionFactory predictionFactory, IPredictionDtoFactory predictionDtoFactory)
        {
            _predictionRepository = predictionRepository;
            _predictionFactory = predictionFactory;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register(_predictionFactory);
            container.Register(_predictionRepository);
        }
    }
}