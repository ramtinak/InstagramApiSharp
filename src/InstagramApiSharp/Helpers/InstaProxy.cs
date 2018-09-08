using System;
using System.Net;

namespace InstagramApiSharp.Helpers
{
    public class InstaProxy : IWebProxy
    {
        private readonly string ipaddress;
        private readonly string port;

        public InstaProxy(string ipaddress, string port)
        {
            this.ipaddress = ipaddress;
            this.port = port;
        }

        public Uri GetProxy(Uri destination)
        {
            return new Uri($"http://{ipaddress}:{port}");
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }

        public ICredentials Credentials { get; set; }
    }
}