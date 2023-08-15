using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Eudmarco
{

    public class DBE
    {
        //CONEXÃO COM BASE DE DADOS ORACLE
        public static string _stringConexao { get; set; }


        public static string ExecuteProcedureWithOutParameter(int lote, string motivo, string motivoLib, string acao, string usuario)
        {
            string erroCode = null;

            using (OracleConnection Con = new OracleConnection(ConnectionString()))
            {
                using (OracleCommand Cmd = new OracleCommand("PROC_CHRONOS_BLOQUEIO", Con))
                {
                    try
                    {
                        Cmd.CommandType = CommandType.StoredProcedure;

                        Cmd.Parameters.Add("l_ID_LOTE", OracleDbType.Int32).Value = lote;
                        Cmd.Parameters.Add("l_V_MOTIVO", OracleDbType.Varchar2).Value = motivo;
                        Cmd.Parameters.Add("l_V_MOTIVO_LIB", OracleDbType.Varchar2).Value = motivoLib;
                        Cmd.Parameters.Add("l_ACAO", OracleDbType.Varchar2).Value = acao;
                        Cmd.Parameters.Add("l_V_USUARIO", OracleDbType.Varchar2).Value = usuario;
                        Cmd.Parameters.Add("l_ERROCODE", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output;

                        Con.Open();
                        Cmd.ExecuteNonQuery();

                        // Obtém o valor retornado pelo parâmetro OUT l_ERROCODE
                        erroCode = Cmd.Parameters["l_ERROCODE"].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return erroCode;
        }
        public static string ExecuteScalar(string SQL)
        {

            object Result = null;

            using (OracleConnection Con = new OracleConnection(ConnectionString()))
            {
                using (OracleCommand Cmd = new OracleCommand(SQL, Con))
                {
                    try
                    {
                        Con.Open();
                        Result = Cmd.ExecuteScalar();
                        Con.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        return null;
                    }
                }
            }

            return Result == null ?
                null : Result.ToString();

        }

        public static bool BeginTransaction(string SQL)
        {

            bool Success = false;


            using (OracleConnection Con = new OracleConnection(ConnectionString()))
            {
                using (OracleCommand Cmd = new OracleCommand(SQL, Con))
                {

                    OracleTransaction Transaction;

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

        public static DataTable List()
        {

            DataSet Ds = new DataSet();

            using (OracleConnection Con = new OracleConnection(ConnectionString()))
            {
                using (OracleCommand Cmd = new OracleCommand("FN_SAIDA_CARGA", Con))
                {

                    Cmd.CommandTimeout = 120;
                    Cmd.Connection = Con;
                    Cmd.CommandType = CommandType.StoredProcedure;
                    /*Cmd.CommandType = CommandType.Text;*/

                    Cmd.Parameters.Add(new OracleParameter
                    {
                        OracleDbType = OracleDbType.Varchar2,
                        Direction = ParameterDirection.Input,
                        Value = "19000101"
                    });

                    Cmd.Parameters.Add(new OracleParameter
                    {
                        OracleDbType = OracleDbType.Varchar2,
                        Direction = ParameterDirection.Input,
                        Value = "29000401"
                    });
                    Cmd.Parameters.Add(new OracleParameter
                    {
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    });

                    Con.Open();
                    OracleDataReader dr = Cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        Ds.Tables.Add();

                        Ds.Tables[0].Load(dr);
                    }

                    dr.Close();
                    Con.Close();
                }
            }
            return Ds.Tables[0];
        }

        public static string[] Reader(string SQL)
        {

            List<string> lista = new List<string>();

            using (OracleConnection Con = new OracleConnection(ConnectionString()))
            {
                using (OracleCommand Cmd = new OracleCommand(SQL, Con))
                {

                    OracleDataReader dr;
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
            return ConfigurationManager.ConnectionStrings["StringConexaoOracle"].ConnectionString;
        }

    }

}