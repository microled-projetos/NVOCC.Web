using Dapper;
using LogComexService.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LogComexService.Repositorio
{
    public class BlMasterRepositorio : IBlMasterRepositorio
    {
        private List<BlMaster> _blmasters = new List<BlMaster>();
        IConfiguration _configuration;
        public BlMasterRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnection()
        {
            var connection = _configuration.GetSection("SqlConnection").GetSection("SqlConnectionString").Value;
            return connection;
        }
        public List<BlMaster> ListarTodas()
        {
            var connectionString = this.GetConnection();
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
                }
                finally
                {
                    con.Close();
                }
                return _blmasters;
            }
        }
        public void CadastrarTokenBl(BlMaster bl)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE TB_BL SET BL_TOKEN = '" + bl.BL_TOKEN + "' WHERE NR_BL = '" + bl.NR_BL +"'";
                    count = con.Execute(query, bl);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void AtualizarBlTraking(BlMaster bl)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE TB_BL SET TRAKING_BL = '" + bl.TRAKING_BL.Replace("'","") + "' WHERE BL_TOKEN = '" + bl.BL_TOKEN + "'";
                    count = con.Execute(query, bl);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
