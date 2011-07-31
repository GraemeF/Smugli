namespace Smugli
{
    #region Using Directives

    using Nancy;

    using Smugli.Domain;
    using Smugli.Dto;

    #endregion

    public class PredictionsModule : NancyModule
    {
        private readonly IPredictionFactory _predictionFactory;

        private readonly IPredictionRepository _predictionRepository;

        public PredictionsModule(IPredictionRepository predictionRepository,
                                 IPredictionFactory predictionFactory)
        {
            _predictionRepository = predictionRepository;
            _predictionFactory = predictionFactory;

            Get["/"] = _ => View["Index"];

            Get["/Styles/style.css"] = _ => Response.AsCss("Styles/style.css");

            Post["/Predictions"] = _ => AddNewPrediction();
        }

        private Response AddNewPrediction()
        {
            IPrediction prediction = _predictionFactory.CreatePrediction(Request.BodyAsDto<PredictionDto>().Prediction);
            _predictionRepository.Add(prediction);

            var response = new Response
                               {
                                   StatusCode = HttpStatusCode.Created,
                               };
            response.Headers["Location"] = Request.Url.Path + "/" + prediction.Id.Value;
            response.Headers["Content-Location"] = Request.Url.Path + "/" + prediction.Id.Value;

            return response;
        }
    }
}