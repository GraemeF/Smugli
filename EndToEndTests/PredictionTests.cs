namespace EndToEndTests
{
    #region Using Directives

    using System;

    using Should.Fluent;

    #endregion

    public class PredictionTests : SmugliServerTests
    {
        private string _linkToLastPrediction;

        protected void IAmGivenALinkToMyPrediction()
        {
            _linkToLastPrediction = Browser.Home.LinkToLastPrediction;
            _linkToLastPrediction.Should().Not.Be.NullOrEmpty();
        }

        protected void IPredictThat_By_(string prediction, DateTime reveal)
        {
            Browser.Home.SubmitPrediction(prediction);
        }

        protected void IVisitTheLinkIWasGiven()
        {
            Browser.NavigateToPrediction(_linkToLastPrediction);
        }

        protected void Nothing()
        {
        }

        protected void ThePredictionWillNotBeRevealed()
        {
            Browser.Prediction.IsRevealed.Should().Be.False();
        }
    }
}