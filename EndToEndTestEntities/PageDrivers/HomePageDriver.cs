namespace EndToEndTestEntities.PageDrivers
{
    #region Using Directives

    using System.Threading;

    using OpenQA.Selenium;

    #endregion

    public class HomePageDriver
    {
        private readonly IWebDriver _driver;

        public HomePageDriver(IWebDriver driver, string homeUri)
        {
            _driver = driver;

            if (_driver.Title != "Smugli")
                _driver.Navigate().GoToUrl(homeUri);
        }

        public string LinkToLastPrediction
        {
            get { return _driver.FindElement(By.Id("predictionLink")).Text; }
        }

        public void SubmitPrediction(string prediction)
        {
            _driver.FindElement(By.Id("prediction")).SendKeys(prediction);
            _driver.FindElement(By.Id("predictButton")).Click();
        }

        private void ClickLinkWithText(string linkText)
        {
            _driver.FindElement(By.LinkText(linkText)).Click();
            Thread.Sleep(1000);
        }
    }
}