namespace EndToEndTestEntities
{
    #region Using Directives

    using System;

    using EndToEndTestEntities.PageDrivers;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    #endregion

    public class Browser : IDisposable
    {
        private readonly IWebDriver _driver;

        private readonly string _homeUri;

        private object _browser;

        public Browser(string homeUri)
            : this(new FirefoxDriver())
        {
            _homeUri = homeUri;
        }

        private Browser(IWebDriver driver)
        {
            _driver = driver;
        }

        ~Browser()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        public HomePageDriver Home
        {
            get { return GetBrowser(() => new HomePageDriver(_driver, _homeUri)); }
        }

        public PredictionPageDriver Prediction
        {
            get
            {
                return
                    GetBrowser<PredictionPageDriver>(() =>
                        {
                            throw new InvalidOperationException(
                                "The browser must already be on a Prediction. Use NavigateToPrediction(uri) instead.");
                        });
            }
        }

        public PredictionPageDriver NavigateToPrediction(string predictionUri)
        {
            return (PredictionPageDriver)(_browser = new PredictionPageDriver(_driver, predictionUri));
        }

        #region IDisposable members

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        private void Dispose(bool disposing)
        {
            if (disposing)
                _driver.Dispose();
        }

        private TBrowser GetBrowser<TBrowser>(Func<TBrowser> factory) where TBrowser : class
        {
            return _browser as TBrowser
                   ?? (TBrowser)(_browser = factory());
        }
    }
}