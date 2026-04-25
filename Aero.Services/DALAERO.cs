using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data1;

namespace AERO.Services
{
    class DALAERO
    {
        public class DALEmailModel : Connection
        {
            public DataSet Return_DataSet(BOLAERO.BOLEmailModel ObjBOL)
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@msg", SqlDbType.VarChar, 50);        
                param[1] = new SqlParameter("@Operation", ObjBOL.Operation);
                param[2] = new SqlParameter("@Id", ObjBOL.Id);
                DataSet ds = SqlHelper1.ExecuteDataset(con, CommandType.StoredProcedure, "[dbo].[aero_EmailQueue]", param);
                return ds;               
            }
            public string Return_String(BOLAERO.BOLEmailModel ObjBOL)
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                param[1] = new SqlParameter("@Id", ObjBOL.Id);
                param[2] = new SqlParameter("@ToEmail", ObjBOL.ToEmail);
                param[3] = new SqlParameter("@CcEmail", ObjBOL.CCEmail);
                param[4] = new SqlParameter("@Subject", ObjBOL.Subject);
                param[5] = new SqlParameter("@Body", ObjBOL.Body);
                param[6] = new SqlParameter("@EmailType", ObjBOL.EmailType);
                param[7] = new SqlParameter("@Operation", ObjBOL.Operation);
                param[8] = new SqlParameter("@ErrorMessage", ObjBOL.ErrorMessage);
                param[9] = new SqlParameter("@ReferenceId", ObjBOL.ReferenceId);
                SqlHelper1.ExecuteNonQuery(con, CommandType.StoredProcedure, "[dbo].[aero_EmailQueue]", param);
                string msg = param[0].Value.ToString();
                return msg;
            }
        }

        public class DALException : Connection
        {
            public String SaveException(BOLAERO.BOLException ObjBOLException)
            {
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@msg", SqlDbType.Char, 50);
                param[0].Direction = ParameterDirection.Output;
                param[1] = new SqlParameter("@Action", ObjBOLException.Action);
                param[2] = new SqlParameter("@module_name", ObjBOLException.module_name);
                param[3] = new SqlParameter("@source", ObjBOLException.source);
                param[4] = new SqlParameter("@message", ObjBOLException.message);
                param[5] = new SqlParameter("@data", ObjBOLException.data);
                param[6] = new SqlParameter("@target_site", ObjBOLException.target_site);
                param[7] = new SqlParameter("@stack_trace", ObjBOLException.stack_trace);
                param[8] = new SqlParameter("@date", ObjBOLException.date);
                param[9] = new SqlParameter("@counts", ObjBOLException.counts);
                SqlHelper1.ExecuteNonQuery(con, CommandType.StoredProcedure, "aero_AddEditException", param);
                string msg = param[0].Value.ToString();
                return msg;
            }
        }
    }
}
