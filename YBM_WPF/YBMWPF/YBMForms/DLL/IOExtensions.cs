using System;
using System.IO;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace YBMForms.DLL
{
    class FileStreamOut : FileStream
    {
        /// <summary>
        /// Constructor, used for initalizing the file stream base
        /// </summary>
        /// <param name="path">the location of the file</param>
        /// <param name="fileMode">how to access the file</param>
        public FileStreamOut(string path, FileMode fileMode)
            : base(path, fileMode)
        {

        }

        /// <summary>
        /// the constructor to help convert an existing filestream into a filestream out
        /// </summary>
        /// <param name="path">the location of the file</param>
        /// <param name="fileAccess">how to access the file</param>
        public FileStreamOut(SafeFileHandle path, FileAccess fileAccess)
            : base(path, fileAccess)
        {

        }

        /// <summary>
        /// a method to quickly write a string through a filestream
        /// </summary>
        /// <param name="s">the string you wish to write</param>
        public void WriteLine(string s)
        {
            //get the bytes of the string with the end line delimiter
            Write(UnicodeEncoding.Unicode.GetBytes(s + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(s + "\r\n"));
        }
    }

    internal class MemorystreamOut : MemoryStream
    {
        /// <summary>
        /// memory stream constructor
        /// </summary>
        internal MemorystreamOut()
        {

        }

        /// <summary>
        /// method to write a line to the memory stream
        /// </summary>
        /// <param name="s"></param>
        internal void WriteLine(string s)
        {
            //writes a line of code with the carrage return in unicode encoding
            Write(UnicodeEncoding.Unicode.GetBytes(s + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(s + "\r\n"));
        }
    }

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
