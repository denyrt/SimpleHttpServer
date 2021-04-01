using Core.Models;
using System.Net;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class WeatherMiddleware : Middleware
    {
        public WeatherMiddleware(string routing) : base(routing)
        {

        }

        public WeatherMiddleware(string routing, Middleware next) : base(routing, next)
        {
            
        }

        public async override Task HandleRequest(HttpListenerContext context)
        {
            if (context.Request.Url.AbsolutePath == _routing)
            {
                var response = new BaseResponse<WeatherInfo[]>
                {
                    IsSuccess = true,
                    Message = "Weathers",
                    Data = WeatherInfo.CreateRandWeathers()
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