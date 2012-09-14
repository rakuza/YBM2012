using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YBMServer
{
    public class LogEntry
    {
        
        public LogEntry(string Event)
        {
            timeStamp = DateTime.Now;
            colour = Color.White;
            logText = Event;
        }

        public LogEntry(string Event, Color color)
        {
            timeStamp = DateTime.Now;
            colour = color;
            logText = Event;
        }

        public LogEntry(string Event, Color color, DateTime eventTime)
        {
            timeStamp = eventTime;
            colour = color;
            logText = Event;
        }

        private DateTime timeStamp;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
        private Color colour;

        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        private string logText;

        public string LogText
        {
            get { return logText; }
            set { logText = value; }
        }

        public override string ToString()
        {
            return this.timeStamp.ToString()+" "+this.logText;
        }
    }
}
