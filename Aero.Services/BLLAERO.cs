using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERO.Services;
using System.Data;

namespace AERO.Services
{
    class BLLAERO
    {
        public class BLLEmailModel
        {
            private DALAERO.DALEmailModel ObjDAL = new DALAERO.DALEmailModel();
            public DataSet Return_DataSet(BOLAERO.BOLEmailModel ObjBOL)
            {
                DataSet ds = new DataSet();
                ds = ObjDAL.Return_DataSet(ObjBOL);
                return ds;
            }
            public String Return_String(BOLAERO.BOLEmailModel ObjBOL)
            {
                String Status = "";
                Status = ObjDAL.Return_String(ObjBOL);
                return Status;
            }

        }
        public class BLLException
        {
            private DALAERO.DALException ObjException = new DALAERO.DALException();
            public String SaveException(BOLAERO.BOLException ObjBOLException)
            {
                string Status = "";
                Status = ObjException.SaveException(ObjBOLException);
                return Status;
            }
        }
    }
}
