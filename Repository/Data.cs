using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ID_Request.Models;
using ID_Request.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ID_Request.Repository
{
    public class Data : IData
    {
        public string ConnectionString { get; set; }

        public Data()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        }

        public bool SaveRequestData(RequestData details)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertOrUpdateRequestData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestedBy", details.RequestedBy);
                    cmd.Parameters.AddWithValue("@EmployeeName", details.EmployeeName);
                    cmd.Parameters.AddWithValue("@EmployeeId", details.EmployeeId);
                    cmd.Parameters.AddWithValue("@Reason", details.Reason);
                    cmd.Parameters.AddWithValue("@Section", details.Section);
                    cmd.Parameters.AddWithValue("@Status", details.Status);
                    cmd.Parameters.AddWithValue("@RequestDate", details.RequestedDate);
                    // If you're updating an existing record, include the ID
                    if (details.Id > 0)
                    {
                        cmd.Parameters.AddWithValue("@Id", details.Id);
                    }
                    SqlParameter outputParam = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Consider logging this exception
                return false;
            }
        }

        public List<RequestData> GetAllRequestData()
        {
            List<RequestData> requestList = new List<RequestData>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAllRequestData", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        RequestData request = new RequestData
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RequestedBy = reader["RequestedBy"].ToString(),
                            EmployeeName = reader["EmployeeName"].ToString(),
                            EmployeeId = reader["EmployeeId"].ToString(),
                            Reason = reader["Reason"].ToString(),
                            Section = reader["Section"].ToString(),
                            Status = reader["Status"].ToString(),
                            RequestedDate = Convert.ToDateTime(reader["RequestDate"])
                        };

                        requestList.Add(request);
                    }

                    reader.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                // You might want to throw or handle the exception differently
            }

            return requestList;
        }

        public RequestData GetRequestDataById(int id)
        {
            RequestData request = null;

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetRequestDataById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        request = new RequestData
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RequestedBy = reader["RequestedBy"].ToString(),
                            EmployeeName = reader["EmployeeName"].ToString(),
                            EmployeeId = reader["EmployeeId"].ToString(),
                            Reason = reader["Reason"].ToString(),
                            Section = reader["Section"].ToString(),
                            Status = reader["Status"].ToString(),
                            RequestedDate = Convert.ToDateTime(reader["RequestDate"])
                        };
                    }

                    reader.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception
            }

            return request;
        }

        public bool DeleteRequest(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteRequestData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Consider logging this exception
                return false;
            }
        }

        // Implement the IData interface method - just call the implemented method
        void IData.SaveRequestData(RequestData details)
        {
            SaveRequestData(details);
        }
    }
}