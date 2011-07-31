namespace Smugli.Domain
{
    public interface IPredictionFactory
    {
        IPrediction CreatePrediction(string predictionText);
    }
}