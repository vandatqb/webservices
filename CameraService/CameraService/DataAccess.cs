using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CameraService
{
    public class DataAccess
    {
        public DataAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        string connectionString = "workstation id=dncamera.mssql.somee.com;packet size=4096;user id=vandat03_SQLLogin_1;pwd=h4j5kx3m2w;data source=dncamera.mssql.somee.com;persist security info=False;initial catalog=dncamera";
        public SqlConnection KetNoi()
        {
            SqlConnection con = new SqlConnection(@connectionString);
            con.Open();
            return con;
        }
        public int capNhatTuTable(DataTable dt, string qr)
        {
            SqlConnection con = KetNoi();
            SqlCommand com = new SqlCommand(
                qr, con);
            SqlDataAdapter sda = new SqlDataAdapter(com);
            SqlCommandBuilder buider = new SqlCommandBuilder(sda);
            int kq = sda.Update(dt);
            con.Close();
            return kq;
        }
        public string docDLDuyNhat(string truyVan)
        {
            SqlConnection con = KetNoi();
            SqlCommand com = new SqlCommand(
                truyVan, con);
            string kq = com.ExecuteScalar() + "";
            con.Close();
            return kq;
        }

        public int capNhatDuLieu(
            string tenStore, string[] doiSo, string[] giaTri)
        {
            SqlConnection con = KetNoi();
            SqlCommand com = new SqlCommand(
                tenStore, con);
            com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < doiSo.Length; i++)
                com.Parameters.Add(
                    new SqlParameter("@" + doiSo[i], giaTri[i]));
            int kq = com.ExecuteNonQuery();
            con.Close();
            return kq;
        }

        public int capNhatDuLieu(string query)
        {
            SqlConnection con = KetNoi();
            SqlCommand com = new SqlCommand(
                query, con);
            int kq = com.ExecuteNonQuery();
            con.Close();
            return kq;
        }
        public DataTable docDuLieuDataTable(string query)
        {
            SqlConnection con = KetNoi();
            SqlCommand com = new SqlCommand(
                query, con);
            SqlDataAdapter sda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            return dt;
        }
    }
}