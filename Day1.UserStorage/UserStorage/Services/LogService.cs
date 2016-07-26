﻿using System;
using System.Diagnostics;
using UserStorage.Interfaces.Services;

namespace UserStorage.Services
{
    public sealed class LogService : MarshalByRefObject, ILogService
    {
        private readonly BooleanSwitch boolSwitch;
        private readonly TraceSource source;

        private class Nested
        {
            public static readonly LogService Service = new LogService();
        }
        
        private LogService()
        {
            boolSwitch = new BooleanSwitch("Switch", "");
            source = new TraceSource("Source");
        }

        public static LogService Instance => Nested.Service;

        public void Log(TraceEventType traceEventType, string message)
        {
            if (boolSwitch.Enabled)
            {
                source.TraceEvent(traceEventType, 0, message);
            }
        }

    }
}
