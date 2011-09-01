namespace EndToEndTests
{
    #region Using Directives

    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using StoryQ;

    #endregion

    [TestClass]
    public class AddPredictionTests : PredictionTests
    {
        private readonly Feature _story =
            new Story("Add a prediction to Smugli")
                .InOrderTo("show off how clever I am")
                .AsA("prophet")
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
    }
}