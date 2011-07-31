namespace Smugli.UnitTests
{
    #region Using Directives

    using NSubstitute;

    using Smugli.Domain;

    #endregion

    public class PredictionBuilder
    {
        private readonly PredictionId _id = new PredictionId("Unspecified");

        public IPrediction Fake
        {
            get { return Build(); }
        }

        private IPrediction Build()
        {
            var fake = Substitute.For<IPrediction>();
            fake.Id.Returns(_id);

            return fake;
        }
    }
}