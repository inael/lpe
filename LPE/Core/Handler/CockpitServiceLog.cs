using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Cockpit.Handle
{
    public class CockpitServiceLog
    {
        public static void WriteErrorToEventLog(string message)
        {
            string pName = "Cockpit Service Source ";
            string pTitle = "Cockpit Service Log ";

            if (!EventLog.SourceExists(pName))
                EventLog.CreateEventSource(pName, pTitle);

            EventLog.WriteEntry(pName, message, EventLogEntryType.Error);
        }
    }
}