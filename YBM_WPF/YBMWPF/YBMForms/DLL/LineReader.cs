using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YBMForms.DLL
{
    sealed class LineReader : IDisposable
    {
        private BinaryReader br;

      

        public LineReader(Stream stream) { br = new BinaryReader(stream, Encoding.Unicode); }


        public string ReadLine()
        {

                StringBuilder sb = new StringBuilder();
                char last = ' ';
                if (br.PeekChar() < 0)
                    return string.Empty;
                char buffer = br.ReadChar();
                sb.Append(buffer);
                int i = br.PeekChar();
                while (!(i < 0))
                {
                    last = buffer;

                    buffer = (char)br.ReadChar();
                    sb.Append(buffer);
                    if (buffer == '\n' && last == '\r')
                    {
                        break;
                    }

                }
                sb.Replace("\r\n", "");

                return sb.ToString();

        }

        public char Peek()
        {
            return (char)br.PeekChar();
        }

        public void Dispose()
        {
            br.Dispose();
        }

    }
}
