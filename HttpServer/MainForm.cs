using Core;
using Core.Middlewares;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpServer
{
    public partial class MainForm : Form
    {
        private readonly CustomHttpServer httpServer;
        private readonly Task task;

        public MainForm()
        {
            InitializeComponent();

            var domain = "http://localhost:8888/";
            var routings = new string[]
            {
                domain
            };

            var helloWorldMiddleware = new HelloWorldMiddeware("/helloworld");
            var weatherMiddeware = new WeatherMiddleware("/weather");

            helloWorldMiddleware.LinkWith(weatherMiddeware);
            httpServer = new CustomHttpServer(routings, helloWorldMiddleware);
            task = new Task(async () => await httpServer.Listen());
            task.Start();

            LogMsg("HttpServer was stated for listening ...");
            LogMsg(string.Format("Domain: {0}", domain));
            LogMsg("Endpoints:");
            LogMsg("1) helloworld");
            LogMsg("2) weather");
            LogMsg("Try in browser:");
            LogMsg(string.Format("1) {0}helloworld", domain));
            LogMsg(string.Format("2) {0}weather", domain));

        }

        private void LogMsg(string message)
        {
            var log = new Action<string>(richTextBoxLogs.AppendText);

            if (InvokeRequired)
            {
                Invoke(log, message + Environment.NewLine);
            }
            else
            {
                log(message + Environment.NewLine);
            }
        }
    }
}