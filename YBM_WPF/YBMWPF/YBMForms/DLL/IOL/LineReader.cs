using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YBMForms.DLL.IOL
{
    public class LineReader : IDisposable
    {
        private Stream stream;
        private BinaryReader reader;

        public LineReader(Stream stream) { reader = new BinaryReader(stream); }

        public string ReadLine()
        {
            StringBuilder result = new StringBuilder();
            char lastChar = reader.ReadChar();
            // an EndOfStreamException here would propogate to the caller

            try
            {
                char newChar = reader.ReadChar();
                if (lastChar == '\r' && newChar == '\n')
                    return result.ToString();

                result.Append(lastChar);
                lastChar = newChar;
                throw new EndOfStreamException();
            }
            catch (EndOfStreamException)
            {
                result.Append(lastChar);
                return result.ToString();
            }
        }

        public char Peek()
        {
            return (char)reader.PeekChar();
        }

        public void Dispose()
        {
            reader.Close();
        }
    }
}
