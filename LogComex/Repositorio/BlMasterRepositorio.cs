using LogComex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Configuration;

namespace LogComex.Repositorio
{
    public class BlMasterRepositorio : IBlMasterRepositorio
    {
        private List<BlMaster> _blmasters = new List<BlMaster>();
        public BlMasterRepositorio()
        {

        }
        public IEnumerable<BlMaster> ListarTodas()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NVOCC"].ConnectionString;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT b.NR_BL, BL_TOKEN, p.ID_ARMADOR_LOGCOMEX from TB_BL b " +
                                "LEFT JOIN TB_PARCEIRO p ON b.ID_PARCEIRO_TRANSPORTADOR = p.ID_PARCEIRO " +
                                "WHERE b.GRAU = 'M' AND p.ID_ARMADOR_LOGCOMEX !=''";
                    _blmasters = con.Query<BlMaster>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }finally
                {
                    con.Close();
                }
                return _blmasters;
            }
        }
        public BlMaster CadastrarTokenBl(BlMaster bl)
        {
            throw new NotImplementedException();
        }
    }
}