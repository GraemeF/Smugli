﻿namespace Smugli.UnitTests
{
    #region Using Directives

    using NSubstitute;

    using NUnit.Framework;

    using Nancy;
    using Nancy.Testing;

    using Should.Fluent;

    using Smugli.Domain;
    using Smugli.Dto;

    using StoryQ;

    #endregion

    [TestFixture]
    public class PreditionsModuleTests
    {
        private readonly Feature _feature = new Story("Make predictions")
            .InOrderTo("show off to my friends")
            .AsA("person")
            .IWant("to make predictions")
            .And("share them with my friends");

        private Browser _browser;

        private BrowserResponse _lastResponse;

        private IPredictionFactory _predictionFactory;

        private IPredictionRepository _predictionRepository;

        [Test]
        public void PostANewPrediction()
        {
            const string predictionText = "Hovercars blah blah blah";
            IPrediction prediction = A.Prediction.Fake;

            _feature.WithScenario("Post a new prediction")
                .Given(IAmAlreadyLoggedIn)
                .And(TheFactoryCreates_For_, prediction, predictionText)
                .When(IPostThePredictionThat_, predictionText)
                .Then(APrediction_IsAddedToTheRepository, prediction)
                .And(TheResponseStatusShouldBe_, HttpStatusCode.Created)
                .And(TheResponseShouldHaveALocation)
                .And(TheResponseShouldHaveAContentLocation)
                .Execute();
        }

        [SetUp]
        public void SetUp()
        {
            _predictionFactory = Substitute.For<IPredictionFactory>();
            _predictionRepository = Substitute.For<IPredictionRepository>();

            _browser =
                new Browser(new InjectedBootstrapper(_predictionRepository,
                                                     _predictionFactory));
        }

        private void APrediction_IsAddedToTheRepository(IPrediction prediction)
        {
            _predictionRepository.Received().Add(prediction);
        }

        private void IAmAlreadyLoggedIn()
        {
        }

        private void IPostThePredictionThat_(string prediction)
        {
            _lastResponse = _browser.Post("/Predictions",
                                          with =>
                                              {
                                                  with.Header("Content-Type", Schema.ContentTypes.GenericJson);
                                                  with.Body(new PredictionDto
                                                                {
                                                                    Prediction = prediction
                                                                }.ToJson());
                                              });
        }

        private void TheFactoryCreates_For_(IPrediction prediction, string predictionText)
        {
            _predictionFactory.CreatePrediction(predictionText)
                .Returns(prediction);
        }

        private void TheResponseShouldHaveAContentLocation()
        {
            _lastResponse.Headers.Should().ContainKey("Content-Location");
        }

        private void TheResponseShouldHaveALocation()
        {
            _lastResponse.Headers.Should().ContainKey("Location");
        }

        private void TheResponseStatusShouldBe_(HttpStatusCode expected)
        {
            _lastResponse.StatusCode.Should().Equal(expected);
        }
    }
}