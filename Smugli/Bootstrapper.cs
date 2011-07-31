namespace Smugli
{
    #region Using Directives

    using Nancy;
    using Nancy.SassAndCoffee;

    using SassAndCoffee.Core.Caching;

    using TinyIoC;

    #endregion

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void InitialiseInternal(TinyIoCContainer container)
        {
            base.InitialiseInternal(container);

            Hooks.Enable(this, new InMemoryCache(), container.Resolve<IRootPathProvider>());
        }
    }
}