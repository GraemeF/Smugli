namespace Smugli.Domain
{
    public class Prediction : IPrediction
    {
        public Prediction(PredictionId id, string predictionText)
        {
            Id = id;
            PredictionText = predictionText;
        }

        public PredictionId Id { get; private set; }

        public string PredictionText { get; private set; }
    }
}