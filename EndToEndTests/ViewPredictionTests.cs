namespace EndToEndTests
{
    #region Using Directives

    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using StoryQ;

    #endregion

    [TestClass]
    public class ViewPredictionTests : PredictionTests
    {
        private readonly Feature _story =
            new Story("View a prediction")
                .InOrderTo("see how clever someone is")
                .AsA("viewer")
                .IWant("to view a prediction");

        [TestMethod]
        public void ViewAPredictionThatHasNotBeenRevealedYet()
        {
            _story.WithScenario("Prediction has not been revealed yet")
                .Given(IPredictThat_By_, "Hovercars will be in general use", new DateTime(3000, 1, 1))
                .And(IAmGivenALinkToMyPrediction)
                .When(IVisitTheLinkIWasGiven)
                .Then(ThePredictionWillNotBeRevealed)
                .Execute();
        }
    }
}