using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

[WebService(Namespace = "http://panelrfid.sonraisystems.com/",
    Description = "A simple RFID WS for the panel",
    Name = "SendPanelReads")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    private string strDatabase = "Data Source=vcnsql89.webhost4life.com,1433;Initial Catalog=trashtracker;User ID=ttadmin;Password=$onrai0313";

    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SendPanelReads(string[] records)
    {
        string message = string.Empty;

        SqlConnection conn = new SqlConnection(strDatabase);
        int recordCount = records.Length;

        try
        {
            for (int i = 0; i < recordCount - 1; i++)
            {
                string[] read = new string[4];
                read = records[i].Split(',');

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Panel_RFID (store_id, readDateTime, tagID, reader) VALUES('" + read[0] + "','" + read[3] + "','" + read[2] + "','" + read[1] + "')";
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            message = "Success";
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        return message;
    }
    
}