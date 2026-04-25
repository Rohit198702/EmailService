using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERO.Services
{
    class Utility
    {
        enum Oper
        {
            Select = 'S',
            Update = 'U',
            Insert = 'I',
            Exception = 'E'
        }
        public static string Email()
        {
            return "aerowerksmohali@gmail.com";
        }
        public static string EmailDisplayName()
        {
            return "Liezl Sezirahiga";
        }
        public static void AddEditException(Exception e)
        {
            AddEditException(e, null);
        }

        public static void AddEditException(Exception e, string identifier)
        {
            if (e.Message != "Thread was being aborted.")
            {
                BOLAERO.BOLException objBOLEx = new BOLAERO.BOLException();
                BLLAERO.BLLException objBLLEx = new BLLAERO.BLLException();
                string msg = string.Empty;
                var trace = new StackTrace(e);
                var frame = trace.GetFrame(0);
                var method = frame.GetMethod();
                StackFrame[] frames = trace.GetFrames();
                DateTime date = DateTime.Now;
                if (e != null)
                {
                    objBOLEx.Action = (Char)(Oper.Exception);
                    objBOLEx.module_name = method.Name;
                    //objBolEx.hresult = HResult;
                    objBOLEx.source = Convert.ToString(e.Source);
                    if (identifier != null)
                    {
                        objBOLEx.message = "[" + identifier + "] " + Convert.ToString(e.Message);
                    }
                    else
                    {
                        objBOLEx.message = Convert.ToString(e.Message);
                    }
                    objBOLEx.data = Convert.ToString(e.Data);
                    objBOLEx.target_site = Convert.ToString(e.TargetSite);
                    objBOLEx.stack_trace = Convert.ToString(e.StackTrace + ':' + frame.GetFileLineNumber() + ':' + frame.GetFileColumnNumber());
                    objBOLEx.date = date;
                    msg = objBLLEx.SaveException(objBOLEx);
                }
            }
        }


    }
}
