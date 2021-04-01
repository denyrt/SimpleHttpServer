using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public abstract class Middleware
    {
        protected readonly string _routing;
        private Middleware _next;

        /// <summary>
        /// Create middleware with specific path.
        /// </summary>
        /// <param name="routing"> Absolute url path (Without hostname). </param>
        public Middleware(string routing) : this(routing, null)
        {
            
        }

        /// <summary>
        /// Create middleware with specific path.
        /// </summary>
        /// <param name="routing"> Absolute url path (Without hostname). </param>
        /// <param name="next"> Next middleware in chain. </param>
        public Middleware(string routing, Middleware next)
        {
            if (string.IsNullOrEmpty(routing)) throw new ArgumentNullException(nameof(routing));                

            _routing = routing;
            LinkWith(next);
        }

        /// <summary>
        /// Link current Middleware with next Middleware.
        /// </summary>
        /// <param name="middleware"> Middleware that will be linked to current Middleware. </param>
        /// <returns> Linked middleware. </returns>
        public Middleware LinkWith(Middleware middleware)
        {
            return _next = middleware;
        }
        

        public virtual async Task HandleRequest(HttpListenerContext context)
        {
            if (_next != null)
            {
                await _next.HandleRequest(context);
            }

            else
            {
                var response = context.Response;
                response.StatusCode = 404;
                response.Close();
            }
        }       

        protected static async Task ReturnJsonResponse(HttpListenerContext context, object data)
        {
            var responseStringContent = JsonConvert.SerializeObject(data);
            var buffer = Encoding.UTF8.GetBytes(responseStringContent);

            var httpListenerResponse = context.Response;
            httpListenerResponse.ContentLength64 = buffer.Length;
            httpListenerResponse.ContentType = "application/json";

            await httpListenerResponse.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            httpListenerResponse.OutputStream.Close();
        }
    }
}