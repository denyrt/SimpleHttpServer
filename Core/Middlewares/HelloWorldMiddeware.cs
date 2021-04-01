using System.Net;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class HelloWorldMiddeware : Middleware
    {
        public HelloWorldMiddeware(string routing) : base(routing)
        {

        }

        public HelloWorldMiddeware(string routing, Middleware middleware) : base(routing, middleware)
        {

        }

        public override async Task HandleRequest(HttpListenerContext context)
        {
            if (context.Request.Url.AbsolutePath == _routing)
            {
                var response = new BaseResponse
                {
                    IsSuccess = true,
                    Message = "Hello, world!"
                };

                await ReturnJsonResponse(context, response);
            }

            else
            {
                await base.HandleRequest(context);
            }
        }
    }
}
