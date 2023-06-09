using Application.Logging;
using Bugsnag;
using System.Collections.Generic;

namespace API.ErrorLogging
{
    public class BugSnagErrorLogger : IErrorLogger
    {
        private readonly IClient _bugsnag;

        public BugSnagErrorLogger(IClient bugsnag)
        {
            _bugsnag = bugsnag;
        }

        public void Log(AppError error)
        {

            _bugsnag.Notify(error.Exception, (report) => {
                report.Event.Metadata.Add("Error", new Dictionary<string, string> {
                    { "user", error.Username },
                    { "erroCode", error.ErrorId.ToString() },
                });
            });
        }
    }
}
