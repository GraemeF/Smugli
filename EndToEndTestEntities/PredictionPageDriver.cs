namespace EndToEndTestEntities
{
    #region Using Directives

    using OpenQA.Selenium;

    #endregion

    public class PredictionPageDriver
    {
        private readonly IWebDriver _driver;

        public PredictionPageDriver(IWebDriver driver, string predictionUri)
        {
            _driver = driver;

            _driver.Navigate().GoToUrl(predictionUri);
        }

        public bool IsRevealed
        {
            get { return _driver.FindElement(By.Id("hiddenPrediction")).Displayed; }
        }
    }
}