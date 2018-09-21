using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SharpReverseProxy {
    public class ProxyRule {
        // SendAsync removes chunking from the response. 
        // This removes the header so it doesn't expect a chunked response.
        public List<string> HeadersToRemove { get;  }
            = (new string[] { "transfer-encoding" }).ToList();

        public Func<Uri, bool> Matcher { get; set; } = uri => false;
        public Action<HttpRequestMessage, ClaimsPrincipal> Modifier { get; set; } = (msg, user) => { };
        public Func<HttpRequest, ClaimsPrincipal, HttpClient> GetClient { get; set; } = null;
        public Func<HttpResponseMessage, HttpContext, Task> ResponseModifier { get; set; } = null;

        public Action<HttpRequestException> ErrorHandler { get; set; }
        public bool PreProcessResponse { get; set; } = true;
        public bool RequiresAuthentication { get; set; }
    }
}
