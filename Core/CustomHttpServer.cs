using Core.Middlewares;
using System.Net;
using System.Threading.Tasks;

namespace Core
{
    public class CustomHttpServer
    {
        private readonly HttpListener listener;          
        public Middleware MiddlewareChain { get; set; }

        public CustomHttpServer(string[] routings, Middleware middlewareChain)
        {            
            listener = new HttpListener();

            foreach (var routing in routings)
            {
                listener.Prefixes.Add(routing);
            }

            MiddlewareChain = middlewareChain;                        
        }

        public async Task Listen()
        {
            listener.Start();
            while (true)
            {
                var context = await listener.GetContextAsync();
                await MiddlewareChain.HandleRequest(context);
            }
        }
    }
}