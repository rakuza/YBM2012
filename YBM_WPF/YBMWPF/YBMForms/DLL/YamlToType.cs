using System;

namespace YBMForms.DLL
{
    /// <summary>
    /// Has all the method relating to parsing yaml
    /// </summary>
    internal class YamlToType
    {
        /// <summary>
        /// gets a double from the yaml
        /// </summary>
        /// <param name="s">yaml string</param>
        /// <returns>double from the yaml</returns>
        static internal double GetDouble(string s)
        {
            string number = s.Substring(s.IndexOf(':') + 1);
            return double.Parse(number);
        }

        /// <summary>
        /// Gets a integer from a yaml string
        /// </summary>
        /// <param name="s">the yaml string</param>
        /// <returns>The integer from the yaml string</returns>
        static internal int Getint(string s)
        {
            string number = s.Substring(s.IndexOf(':') + 1);
            return int.Parse(number);
        }

        /// <summary>
        /// Gets a string from the yaml string
        /// </summary>
        /// <param name="s">the yaml string</param>
        /// <returns>the data string from the yaml</returns>
        static internal string GetString(string s)
        {
            string line = s.Substring(s.IndexOf(':') + 1);
            return line;
        }

        /// <summary>
        /// gets the key/parameter from the yaml
        /// </summary>
        /// <param name="s">the yaml string</param>
        /// <returns>the key/parameter from the yaml</returns>
        static internal string GetParam(string s)
        {

            string param = s.Remove(s.IndexOf(':'));
            param = param.Replace(":", "");
            param = param.TrimStart();
            return param;
        }

        /// <summary>
        /// gets the date time from the yaml
        /// </summary>
        /// <param name="s">the yaml string</param>
        /// <returns>the date time from the yaml</returns>
        static internal DateTime GetDate(string s)
        {
            DateTime temp = DateTime.Parse(s.Substring(s.IndexOf(':') + 1));
            return temp;
        }
    }
}
