using System.Web;
using System.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Linq;
using System.Collections;
using AERO.Services;
using CrystalDecisions.CrystalReports.Engine;
namespace Aero.Services
{
    public class EmailSchedulerService
    {
        BOLAERO.BOLEmailModel ObjBOL = new BOLAERO.BOLEmailModel();
        BLLAERO.BLLEmailModel ObjBLL = new BLLAERO.BLLEmailModel();         
        commonclass1 clscon = new commonclass1();
        ReportDocument rprt = new ReportDocument();
        public void ProcessEmails()
        {
            var emails = GetPendingEmails();

            foreach (var email in emails)
            {
                try
                {
                    Send_Email(
                        email.Body,
                        email.Subject,
                        ConvertToMailList(email.ToEmail),
                        ConvertToMailList(email.CCEmail),
                        email.ReferenceId
                    );

                    UpdateSuccess(email.Id);
                }
                catch (Exception ex)
                {
                    UpdateFailure(email.Id, ex.Message);
                }
            }

        }

        

        private List<BOLAERO.BOLEmailModel> GetPendingEmails()
        {
            List<BOLAERO.BOLEmailModel> list = new List<BOLAERO.BOLEmailModel>();
            try
            {
                DataSet ds = new DataSet();
                ObjBOL.Operation = 2;
                ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BOLAERO.BOLEmailModel obj = new BOLAERO.BOLEmailModel();

                        obj.Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0;
                        obj.ToEmail = dr["ToEmail"].ToString();
                        obj.CCEmail = dr["CcEmail"].ToString();
                        obj.Subject = dr["Subject"].ToString();
                        obj.Body = dr["Body"].ToString();
                        obj.ReferenceId =Convert.ToInt32(dr["ReferenceId"].ToString());
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
            return list;
        }

        private void Send_Email(String Message, string Subject, List<MailAddress> sendToList, List<MailAddress> ccList, Int32 Referenceid)
        {
            try
            {
                if (sendToList.Count > 0)
                {
                    using (MemoryStream attachmentStream = new MemoryStream())
                    {
                        StreamWriter writer = new StreamWriter(attachmentStream);
                    }

                    MailMessage message = new MailMessage(new MailAddress(Utility.Email(), Utility.EmailDisplayName()), sendToList[0]);
                    string mailSubject = String.Empty;
                    //mailSubject =  + ".pdf";
                    string mailbody = Message;
                    message.Subject = Subject;
                    message.Body = mailbody;

                    Attachment file = new Attachment(GetPackingDetailsReportStream(Referenceid), mailSubject, "application/pdf");
                    message.Attachments.Add(file);
                    foreach (var sendto in sendToList)
                    {
                        if (!message.To.Contains(sendto))
                        {
                            message.To.Add(sendto);
                        }
                    }
                    foreach (var cc in ccList)
                    {
                        if (!message.CC.Contains(cc))
                        {
                            message.CC.Add(cc);
                        }

                    }
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["Host"], 587);
                    System.Net.NetworkCredential basicCredential1 = new
                    System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromMail"], ConfigurationManager.AppSettings["Password"]);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    client.Send(message);
                    file.Dispose();
                    Message = string.Empty;                    
                }

            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
        }

        private Stream GetPackingDetailsReportStream(Int32 Referenceid)
        {
            Stream reportStream = null;
            try
            {
                PrepareReport(Referenceid);
                reportStream = (Stream)rprt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
            finally
            {
                if (rprt != null)
                {
                    try
                    {
                        rprt.Close();
                        rprt.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Utility.AddEditException(ex);
                    }
                }
            }
            return reportStream;
        }


        private void PrepareReport(int Referenceid)
        {
            try
            {
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                dt2 = ReportDataZero(Referenceid);
                dt3 = ReportDataFirst(Referenceid);
                if (dt2.Rows.Count > 0 || dt3.Rows.Count > 0)
                {
                    string reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                    rprt.Load(Path.Combine(reportDir, "rptPackingDetails.rpt"));              
                    rprt.SetDataSource(dt2);
                    rprt.Subreports[0].SetDataSource(dt3);
                    Stream stream = rprt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);                
                }
                else
                {
                    //Utility.ShowMessage_Warning(Page, "No Matching Data Found !!");
                }
            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
        }

        private DataTable ReportDataFirst(int Referenceid)
        {
            DataTable dt = new DataTable();
            try
            {
                clscon.Return_DT(dt, "EXEC [IV].[Get_PackingDetails_Jobs] '" + Referenceid + "'");
            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
            return dt;
        }

        private DataTable ReportDataZero(int Referenceid)
        {
            DataTable dt = new DataTable();
            try
            {
                clscon.Return_DT(dt, "EXEC [IV].[Get_PackingDetails_V1] '" + Referenceid + "'");
            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
            return dt;
        }



        private void UpdateSuccess(int id)
        {
            try
            {
                ObjBOL.Id = id;
                ObjBOL.Operation = 3;
                ObjBOL.Id = id;
                ObjBLL.Return_DataSet(ObjBOL);
            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
        }

        private void UpdateFailure(int id, string error)
        {
            try
            {
                ObjBOL.Operation = 4;
                ObjBOL.Id = id;
                ObjBLL.Return_String(ObjBOL);
            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
        }

    }
}
