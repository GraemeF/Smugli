namespace EndToEndTestEntities
{
    #region Using Directives

    using System;

    #endregion

    public class SmugliServer : IDisposable
    {
        private const string ServerFolder =
            SolutionFolderFromTestRunFolder + ServerProjectName;

        private const string ServerProjectName = @"Smugli";

        private const string SolutionFolderFromTestRunFolder = @"..\..\..\..\..\";

        private readonly WebHostServer _webHost = new WebHostServer();

        public SmugliServer()
        {
            _webHost = new WebHostServer();
            _webHost.StartServer(ServerFolder);
        }

        ~SmugliServer()
        {
            Dispose(false);
        }

        public Browser CreateBrowser()
        {
            return new Browser(_webHost.RootUrl);
        }

        #region IDisposable members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private string CreateUri(string relativeUri)
        {
            return _webHost.NormalizeUri(relativeUri).AbsoluteUri;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                _webHost.Dispose();
        }
    }
}