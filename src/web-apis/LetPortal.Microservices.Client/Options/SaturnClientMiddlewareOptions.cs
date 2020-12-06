﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LetPortal.Microservices.Client.Options
{
    public class SaturnClientMiddlewareOptions
    {
        public bool UseGenerateTraceId { get; set; } = true;

        public bool UseBuiltInCors { get; set; } = true;

        public bool AllowCheckUserSessionId { get; set; } = false;

        public bool AllowCheckTraceId { get; set; } = false;

        public bool AllowWrapException { get; set; } = false;

        public string[] SkipCheckUrls { get; set; } 
    }
}
