namespace Smugli.Domain
{
    public class PredictionFactory : IPredictionFactory
    {
        private static int _nextId = 1;

        #region IPredictionFactory members

        public IPrediction CreatePrediction(string predictionText)
        {
            return new Prediction(new PredictionId(_nextId++.ToString()), predictionText);
        }

        #endregion
    }
}