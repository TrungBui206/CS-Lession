
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace StudentSubjectManagement.DAL
{
    public class DataHelper
    {
        private readonly string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;
              Database=QLSV;
              Integrated Security=True;
              Encrypt=True;
              TrustServerCertificate=True;";

        // Kiểm tra kết nối SQL Server
        public void TestConnection()
        {
            using SqlConnection con =
                new SqlConnection(connectionString);

            con.Open();
        }

        // Lấy dữ liệu từ database
        public DataTable GetDataTable(
            string sql,
            params SqlParameter[] parameters)
        {
            using SqlConnection con =
                new SqlConnection(connectionString);

            using SqlCommand cmd =
                new SqlCommand(sql, con);

            cmd.Parameters.AddRange(parameters);

            using SqlDataAdapter adapter =
                new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        // Thêm, sửa, xóa dữ liệu
        public int ExecuteNonQuery(
            string sql,
            params SqlParameter[] parameters)
        {
            using SqlConnection con =
                new SqlConnection(connectionString);

            using SqlCommand cmd =
                new SqlCommand(sql, con);

            cmd.Parameters.AddRange(parameters);

            con.Open();

            return cmd.ExecuteNonQuery();
        }

        // Lấy một giá trị đơn
        public object? ExecuteScalar(
            string sql,
            params SqlParameter[] parameters)
        {
            using SqlConnection con =
                new SqlConnection(connectionString);

            using SqlCommand cmd =
                new SqlCommand(sql, con);

            cmd.Parameters.AddRange(parameters);

            con.Open();

            return cmd.ExecuteScalar();
        }
    }
}
