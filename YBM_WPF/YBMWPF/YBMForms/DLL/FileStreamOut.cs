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
            : base(path,fileMode)
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
}
