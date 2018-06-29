using RestSharp;

namespace RevisoSamples.Rest
{
    public static class RevisoRestClient
    {
        private const string RootUrl = "https://rest.reviso.com/";
        private const string SecretToken = "jjV9CqUnkqG5lWjM9gzIKNtNMM1u6qdrMboFCy8wkA81";
        private const string GrantToken = "vhN7izGMYDeuYTlgKbIZbMWxlzHJ9kyHlSS8s1Xpayw1";

        public static RestClient CreateClient()
        {
            RestClient client = new RestClient(RootUrl);
            client.AddDefaultHeader("X-AppSecretToken", SecretToken);
            client.AddDefaultHeader("X-AgreementGrantToken", GrantToken);
            return client;
        }
    }
}