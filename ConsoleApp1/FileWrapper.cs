using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IFile {
        void Close();
    }

    public class FileWrapper : IDisposable
    {
        private IFile _file;

        public FileWrapper(IFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            this._file = file;
        }
        

        public void UpdateFile()
        {
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {                    
                }

                this._file.Close();
                disposed = true;
            }
        }
        
        ~FileWrapper()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
