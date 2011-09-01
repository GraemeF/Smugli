namespace Smugli.Domain
{
    public interface IPredictionRepository
    {
        void Add(IPrediction prediction);

        IPrediction Get(PredictionId id);
    }
}