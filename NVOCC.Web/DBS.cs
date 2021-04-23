using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;

namespace ABAINFRA.Web
{
    public class DBS
    {
        //CONEXÃO COM BASE DE DADOS SQL SERVER

        public static string _stringConexao { get; set; }

        public static string ExecuteScalar(string SQL)
        {

            object Result = null;

            using (SqlConnection Con = new SqlConnection(ConnectionString()))
            {
                using (SqlCommand Cmd = new SqlCommand(SQL, Con))
                {
                    //try
                    //{
                    Con.Open();
                    Result = Cmd.ExecuteScalar();
                    Con.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //return null;
                    //}
                }
            }

            return Result == null ?
                null : Result.ToString();

        }

        public static bool BeginTransaction(string SQL)
        {

            bool Success = false;


            using (SqlConnection Con = new SqlConnection(ConnectionString()))
            {
                using (SqlCommand Cmd = new SqlCommand(SQL, Con))
                {

                    SqlTransaction Transaction;

                    Con.Open();
                    Transaction = Con.BeginTransaction();
                    Cmd.Transaction = Transaction;

                    //try
                    //{
                    Cmd.ExecuteNonQuery();
                    Transaction.Commit();
                    Success = true;
                    //}
                    //catch (Exception ex)
                    //{
                    //Transaction.Rollback();
                    //}

                    return Success;

                }
            }

        }

        public static DataTable List(string SQL)
        {

            DataSet Ds = new DataSet();

            using (SqlConnection Con = new SqlConnection(ConnectionString()))
            {
                using (SqlCommand Cmd = new SqlCommand())
                {

                    Cmd.CommandTimeout = 120000;
                    Cmd.Connection = Con;
                    Cmd.CommandType = CommandType.Text;
                    Cmd.CommandText = SQL;

                    using (SqlDataAdapter Adp = new SqlDataAdapter(Cmd))
                    {

                        //try
                        //{
                        
                        Adp.Fill(Ds);
                        //}
                        //catch (Exception ex)
                        //{
                        //return null;
                        //}
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            return Ds.Tables[0];
                        }
                        else
                        {
                            return null;
                        }



                    }
                }


            }
        }

        public DataTable ListN(string SQL)
        {
            DataSet Ds = new DataSet();

            using (SqlConnection Con = new SqlConnection(ConnectionString()))
            {
                using (SqlCommand Cmd = new SqlCommand())
                {
                    Cmd.Connection = Con;
                    Cmd.CommandType = CommandType.Text;
                    Cmd.CommandText = SQL;

                    using (SqlDataAdapter Adp = new SqlDataAdapter(Cmd))
                    {
                        Adp.Fill(Ds);
                        return Ds.Tables[0];
                    }
                }
            }
        }



        public static string[] Reader(string SQL)
        {

            List<string> lista = new List<string>();

            using (SqlConnection Con = new SqlConnection(ConnectionString()))
            {
                using (SqlCommand Cmd = new SqlCommand(SQL, Con))
                {

                    SqlDataReader dr;
                    Con.Open();
                    dr = Cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lista.Add(dr[0].ToString());
                        }
                    }

                    dr.Close();
                    Con.Close();

                }

            }

            return lista.ToArray();

        }

        public static string ConnectionString()
        {

            return ConfigurationManager.ConnectionStrings["NVOCC"].ConnectionString;

        }
    }
}