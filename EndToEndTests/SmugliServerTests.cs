namespace EndToEndTests
{
    #region Using Directives

    using EndToEndTestEntities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    #endregion

    [TestClass]
    public abstract class SmugliServerTests
    {
        protected Browser Browser { get; set; }

        protected SmugliServer SmugliServer { get; set; }

        [TestCleanup]
        public void Cleanup()
        {
            if (SmugliServer != null)
            {
                SmugliServer.Dispose();
                SmugliServer = null;
            }
            if (Browser != null)
            {
                Browser.Dispose();
                Browser = null;
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            SmugliServer = new SmugliServer();
            Browser = SmugliServer.CreateBrowser();
        }
    }
}