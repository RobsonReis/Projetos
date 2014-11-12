using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DellMare.Addon
{
    public class Log
    {
        private string fileName;
        private string folder;

        public Log()
        {
            fileName = string.Format("Log_DellMare_{0}.log", DateTime.Now.ToString("ddMMyyyyhhmmss"));
        }

        public void WriteLog(string Mensagem)
        {
            try
            {
                TextWriter oFile = new StreamWriter(string.Format(@"{0}\{1}",folder, fileName), true);
                oFile.WriteLine(DateTime.Now.ToString("dd/MM/yyyy") + " as " + DateTime.Now.ToShortTimeString() + " - " + Mensagem);
                oFile.Close();
            }
            catch 
            {
            }
        }

        public string GetFileName()
        {
            return string.Format(@"{0}\{1}", folder, fileName);
        }

        public void SetFolder(string strFolder)
        {
            folder = strFolder;
        }

        

       
    }
}
