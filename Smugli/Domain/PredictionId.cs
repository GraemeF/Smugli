namespace Smugli.Domain
{
    public class PredictionId : SimpleStringBase
    {
        public PredictionId()
        {
        }

        public PredictionId(string id)
            : base(id)
        {
        }

        public PredictionId(int id)
            : base(id)
        {
        }
    }
}