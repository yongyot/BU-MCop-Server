using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StatsUploadServer.Models
{
    public class FileDBManager
    {
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        SqlDataAdapter da;
        SqlConnection con;

        public FileDBManager()
        {
            //
            // TODO: Add constructor logic here
            //
            con = new SqlConnection(connectionString);
            da = new SqlDataAdapter();
        }

        public bool insertFile(string device_mac, String fileName, string os_version, string device_model, string address)
        {
            string query = @"insert into Files(device_mac,file_name, is_processed,os_version,device_model,address) values(@device_mac,@file_name, @is_processed,@os_version,@device_model,@address)";
            try
            {
                da.InsertCommand = new SqlCommand(query, con);
                da.InsertCommand.Parameters.AddWithValue("@device_mac", device_mac);
                da.InsertCommand.Parameters.AddWithValue("@file_name", fileName);
                da.InsertCommand.Parameters.AddWithValue("@is_processed", false);
                da.InsertCommand.Parameters.AddWithValue("@os_version", os_version);
                da.InsertCommand.Parameters.AddWithValue("@device_model", device_model);
                da.InsertCommand.Parameters.AddWithValue("@address", address);
                con.Open();
                da.InsertCommand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

                Logger.writeLog("Error inserting name of file. Values: file name:- " + fileName + " Details: \n " + e.ToString());
                con.Close();
                return false;
            }
            return true;

        } 
    }
}