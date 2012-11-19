using System;
using System.IO;
using System.Text;

namespace YBMForms.DLL
{
    sealed class LineReader : IDisposable
    {
        private BinaryReader br;

        /// <summary>
        /// LineReader constructor
        /// 
        /// opens a new binaryreader from the stream with it in unicode reading mode
        /// </summary>
        /// <param name="stream">the stream to read</param>
        public LineReader(Stream stream) { br = new BinaryReader(stream, Encoding.Unicode); }

        /// <summary>
        /// reads from a file till it finds a \n\r end of line character
        /// </summary>
        /// <returns>a line of text from a file</returns>
        public string ReadLine()
        {

            StringBuilder sb = new StringBuilder();
            
            char last = ' ';
            //if no character read return an empty string
            if (br.PeekChar() < 0)
                return string.Empty;
            //read one character into the buffer
            char buffer = br.ReadChar();
            //add the char to the bring builder
            sb.Append(buffer);
            //peeks at the next char
            int i = br.PeekChar();
            //file no empty data is being returned keep reading
            while (!(i < 0))
            {
                last = buffer;

                buffer = (char)br.ReadChar();
                sb.Append(buffer);
                //if end of line reached stop reading
                if (buffer == '\n' && last == '\r')
                {
                    break;
                }

            }
            //remove end of line charas
            sb.Replace("\r\n", "");

            return sb.ToString();

        }

        /// <summary>
        /// Allows for a character to be seen from the stream
        /// </summary>
        /// <returns>a character from the stream</returns>
        public char Peek()
        {
            return (char)br.PeekChar();
        }

        /// <summary>
        /// deconstructor for the stream
        /// </summary>
        public void Dispose()
        {
            br.Dispose();
        }

    }
}
