namespace EndToEndTests
{
    #region Using Directives

    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Should.Fluent;

    using StoryQ;

    #endregion

    [TestClass]
    public class AddPredictionTests : SmugliServerTests
    {
        private readonly Feature _story =
            new Story("Add a prediction to Smugli")
                .InOrderTo("show off how clever I am")
                .AsA("user")
                .IWant("to make a prediction")
                .And("share it with my friends");

        [TestMethod]
        public void AddAPredictionWhenAlreadyLoggedIn()
        {
            _story.WithScenario("Already logged in")
                .Given(Nothing)
                .When(IPredictThat_By_, "Hovercars will be in general use", new DateTime(3000, 1, 1))
                .Then(IAmGivenALinkToMyPrediction)
                .Execute();
        }

        private void IAmGivenALinkToMyPrediction()
        {
            Browser.Home.LinkToLastPrediction.Should().Not.Be.NullOrEmpty();
        }

        private void IPredictThat_By_(string prediction, DateTime reveal)
        {
            Browser.Home.SubmitPrediction(prediction);
        }

        private void Nothing()
        {
        }
    }
}