namespace Smugli.Domain
{
    #region Using Directives

    using System.Collections.Concurrent;

    #endregion

    public class InMemoryPredictionRepository : IPredictionRepository
    {
        private readonly ConcurrentDictionary<PredictionId, IPrediction> _predictions =
            new ConcurrentDictionary<PredictionId, IPrediction>();

        #region IPredictionRepository members

        public void Add(IPrediction prediction)
        {
            _predictions.TryAdd(prediction.Id, prediction);
        }

        #endregion
    }
}