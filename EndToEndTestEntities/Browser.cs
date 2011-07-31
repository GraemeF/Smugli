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
            get { return new HomePageDriver(_driver, _homeUri); }
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
    }
}