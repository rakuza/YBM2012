using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace YBMForms.DLL
{
    class FileStreamOut : FileStream 
    {
        public FileStreamOut(string path, FileMode fileMode)
            : base(path,fileMode)
        {

        }

        public FileStreamOut(SafeFileHandle path, FileAccess fileAccess)
            : base(path, fileAccess)
        {

        }

        public void WriteLine(string s)
        {
            Write(UnicodeEncoding.Unicode.GetBytes(s + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(s + "\r\n"));
        }
    }
}
