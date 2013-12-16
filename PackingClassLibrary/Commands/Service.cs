using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingClassLibrary.Commands
{
    public static class Service
    {

        /// <summary>
        /// Get Service Call
        /// </summary>
        public static GetService.GetClient Get = new GetService.GetClient();

        /// <summary>
        /// Set service call;
        /// </summary>
        public static SetService.SaveClient Set = new SetService.SaveClient();
    }
}
