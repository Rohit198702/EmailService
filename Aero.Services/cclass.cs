using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for commonclass
/// </summary>
public class commonclass1
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
    protected SqlCommand cmd = new SqlCommand();
    protected SqlDataAdapter adp = new SqlDataAdapter();
    protected SqlDataReader dr;

    public SqlConnection convisual = new SqlConnection(ConfigurationManager.ConnectionStrings["convisual"].ConnectionString);
    //  private HttpContext Context( get (return HttpContext.Current)); 
    // private Hashtable RequestParams(get ( return (Hashtable ) contex ))
    protected DataSet ds = new DataSet();

    public void conopen()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
            con.Open();
        }
    }


    public void conclose()
    {

        if (con.State == ConnectionState.Open)
        {
            con.Close();

        }
    }


    public void conopenvisual()
    {
        if (convisual.State == ConnectionState.Closed)
        {
            convisual.ConnectionString = ConfigurationManager.ConnectionStrings["convisual"].ConnectionString;
            convisual.Open();
        }
    }


    public void conclosevisual()
    {

        if (convisual.State == ConnectionState.Open)
        {
            convisual.Close();

        }
    }

    public bool ProcessSP(string spName, Hashtable DataValues)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmdSP = new SqlCommand(spName, con);

        cmdSP.CommandType = CommandType.StoredProcedure;

        SqlCommandBuilder.DeriveParameters(cmdSP);

        foreach (SqlParameter sqlParam in cmdSP.Parameters)
        {
            if ((sqlParam.Direction == ParameterDirection.Input) || (sqlParam.Direction == ParameterDirection.InputOutput))
            {
                //if (DataValues[sqlParam.ParameterName.Substring(1)] != null)
                //  {

                sqlParam.Value = DataValues[sqlParam.ParameterName];
                //  }
            }

        }
        cmdSP.ExecuteNonQuery();




        return false;
    }
    public DataSet ProcessSelectSP(string spName, Hashtable DataValues)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            SqlCommand cmdSP = new SqlCommand(spName, con);

            cmdSP.CommandType = CommandType.StoredProcedure;

            SqlCommandBuilder.DeriveParameters(cmdSP);

            foreach (SqlParameter sqlParam in cmdSP.Parameters)
            {
                if ((sqlParam.Direction == ParameterDirection.Input) || (sqlParam.Direction == ParameterDirection.InputOutput))
                {
                    sqlParam.Value = DataValues[sqlParam.ParameterName.ToString()].ToString();
                }

            }

            DataSet dsData = new DataSet();



            SqlDataAdapter adpData = new SqlDataAdapter(cmdSP);
            adpData.Fill(dsData);

            return dsData;



        }

        catch (Exception ex)
        {
            return null;
        }

    }
   
    public void GetDays()
    {

    }
    public bool Check(string st)
    {

        bool code = false;
        if (con.State == ConnectionState.Closed)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
            con.Open();

        }

        SqlCommand cmd = new SqlCommand(st, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows == true)
        {
            code = true;
        }
        else
        {
            code = false;
        }
        dr.Close();
        con.Close();
        return (code);
    }
    public void ProcessSPSelectStatementDS(string spName, DataSet ds)
    {

        if (con.State == ConnectionState.Closed)
        {
            conopen();

        }

        SqlDataAdapter adp = new SqlDataAdapter(spName, con);


        //        DataSet ds = new DataSet();
        adp.Fill(ds);

        if (con.State == ConnectionState.Open)
        {
            conclose();

        }

        // return ds;


    }


    public DataSet ProcessSPSelectStatement(string spName)
    {

        if (con.State == ConnectionState.Closed)
        {
            conopen();

        }

        SqlDataAdapter adp = new SqlDataAdapter(spName, con);


        DataSet ds = new DataSet();
        adp.Fill(ds);

        if (con.State == ConnectionState.Open)
        {
            conclose();

        }

        return ds;


    }


    public string Return_string(string st)
    {

        string code = "";

        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd = new SqlCommand(st, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows == true)
        {
            dr.Read();
            code = dr[0].ToString();

        }
        dr.Close();
        con.Close();
        return (code);
    }
    public System.DateTime Return_Date(string st)
    {

        System.DateTime code = System.DateTime.Now;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd = new SqlCommand(st, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows == true)
        {
            dr.Read();
            if (dr[0] is DBNull)
            {

            }
            else
            {
                code = Convert.ToDateTime(dr[0]);
            }

        }
        dr.Close();
        con.Close();
        return code;
    }



    public int Return_Int(string st)
    {

        int code = 0;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd = new SqlCommand(st, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows == true)
        {


            dr.Read();
            if (dr[0] is DBNull)
            {
                code = 0;
            }
            else
            {
                code = Convert.ToInt32(dr[0]);

            }


        }
        dr.Close();
        con.Close();
        return (code);
    }

    public decimal Return_Desc(string st)
    {

        decimal code = 0;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd = new SqlCommand(st, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows == true)
        {


            dr.Read();
            if (dr[0] is DBNull)
            {
                code = 0;
            }
            else
            {
                code = Convert.ToDecimal(dr[0]);

            }


        }
        dr.Close();
        con.Close();
        return (code);
    }  
   
    public void Execqry(string qry)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        if ((con.State == ConnectionState.Closed) || (con.State == ConnectionState.Broken))
            con.Open();
        SqlCommand cmd = new SqlCommand(qry);

        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        con.Close();
    }
    public object Execscalr(string qry)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        if ((con.State == ConnectionState.Closed) || (con.State == ConnectionState.Broken))
            con.Open();
        SqlCommand cmd = new SqlCommand(qry);
        cmd.Connection = con;
        object obj=cmd.ExecuteScalar();
        cmd.Dispose();
        con.Close();
        return obj;
    }
    public string Converter(string ddate)
    {
        DateTime dt;        
        if (ddate != "")
        {
            ddate = Convert.ToDateTime(ddate).ToString("MM/dd/yyyy");
            char[] separator = new char[] { '/','-' };
            string[] strSplitArr = ddate.Split(separator);
            string dd, mm, yy;
            mm = strSplitArr[0].ToString();
            dd = strSplitArr[1].ToString();
            yy = strSplitArr[2].ToString();
            string db;
            db = mm + "/" + dd + "/" + yy;
            dt = Convert.ToDateTime(db);

            return dt.ToString("MM/dd/yyyy");
        }
        else
        {
            //return ddate.ToString();
            return null;
        }
    }
    public string Converter2(string ddate)
    {
        DateTime dt;


        char[] separator = new char[] { '/' };
        string[] strSplitArr = ddate.Split(separator);
        string dd, mm, yy;

        mm = strSplitArr[0].ToString();
        dd = strSplitArr[1].ToString();
        yy = strSplitArr[2].ToString();
        string db;
        db = mm + "/" + dd + "/" + yy;
        dt = Convert.ToDateTime(db);

        return dt.ToString("dd/MM/yyyy");

    }


    
    public void Return_DT(DataTable dt, string str)
    {
        conopen();
        cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = str;
        cmd.CommandTimeout = 14000;
        SqlDataAdapter adp = new SqlDataAdapter();
        adp.SelectCommand=cmd;
        adp.Fill(dt);
    }


    public void Return_DT_Visaul(DataTable dt, string str)
    {
        conopenvisual();
        cmd = new SqlCommand();
        cmd.Connection = convisual;
        cmd.CommandText = str;
        cmd.CommandTimeout = 14000;
        SqlDataAdapter adp = new SqlDataAdapter();
        adp.SelectCommand = cmd;
        adp.Fill(dt);
    }

    public void Return_DS(DataSet ds, string str)
    {
        conopen();
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        adp.Fill(ds);
    }
    public DataSet Get_DS(string str)
    {
        if ((con.State == ConnectionState.Closed) || (con.State == ConnectionState.Broken))
            con.Open();
        ds.Tables.Clear();
        SqlDataAdapter adp = new SqlDataAdapter(str, con);
        adp.SelectCommand.CommandTimeout = 12000;
        adp.Fill(ds);
        return ds;
    }
    public Int32 autocode(string str)
    {
        if ((con.State == ConnectionState.Closed) || (con.State == ConnectionState.Broken))
            con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = str;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        Int32 i;
        dr.Read();
        if (dr[0] is DBNull)
        {
            i = 0;
        }
        else
        {
            i = Convert.ToInt32(dr[0]);
        }
        dr.Close();
        cmd.Dispose();
        con.Close();
        return i + 1;
    }
    public Boolean check(string str)
    {
        if ((con.State == ConnectionState.Closed) || (con.State == ConnectionState.Broken))
            con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = str;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        Int32 i;
        dr.Read();
        if (dr.HasRows == true)
        {
            if (dr[0] is DBNull)
            {
                i = 0;
            }
            else
            {
                i = Convert.ToInt32(dr[0]);
            }
            cmd.Dispose();
            con.Close();
            if (i == 0)
            {
                cmd.Dispose();
                con.Close();
                return false;
            }
            else
            {
                cmd.Dispose();
                con.Close();
                return true;
            }
        }
        else
        {
            cmd.Dispose();
            con.Close();
            return false;
        }
    }
    public SqlDataReader return_DR(string qry)
    {
        if ((con.State == ConnectionState.Closed) || (con.State == ConnectionState.Broken))
            con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = qry;
        cmd.Connection = con;
        dr = cmd.ExecuteReader();
        return dr;
    }
    public IDataReader ReturnReader(string cmdtxt)
    {

        conopen();
        SqlCommand cmd = new SqlCommand(cmdtxt, con);
        SqlDataReader dr = cmd.ExecuteReader();
        dr.Close();

        return dr;
    }

}
