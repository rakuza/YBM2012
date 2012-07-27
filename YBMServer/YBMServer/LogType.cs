using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YBMServer
{
    static class LogType
    {
        

        public static Color Application
        {
            get { return Color.LightGreen; }
        }

        public static Color Minor
        {
            get { return Color.LightYellow; }
        }

        public static Color Moderate
        {
            get { return Color.PeachPuff; }
        }

        public static Color Severe
        {
            get { return Color.LightCoral; }
        }

        public static Color Debug
        {
            get { return Color.LightBlue; }

        }
    }
}
