using Dapper;
using LogComexService.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using LogComexService.Helpers;

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
                    StringBuilder query = new StringBuilder();

                    con.Open();

                    query.AppendLine(" ( ");
                    query.AppendLine(" SELECT  ");
                    query.AppendLine(" b.NR_BL, ");
                    query.AppendLine(" BL_TOKEN, ");
                    query.AppendLine(" p.ID_ARMADOR_LOGCOMEX, ");
                    query.AppendLine(" b.TRAKING_BL ");
                    query.AppendLine(" FROM  ");
                    query.AppendLine(" TB_BL b ");
                    query.AppendLine(" LEFT JOIN TB_PARCEIRO p ON(b.ID_PARCEIRO_TRANSPORTADOR  = p.ID_PARCEIRO ) ");
                    query.AppendLine(" WHERE b.GRAU = 'M' ");
                    query.AppendLine(" ) ");
                    query.AppendLine(" UNION ");
                    query.AppendLine(" ( ");
                    query.AppendLine(" SELECT  ");
                    query.AppendLine(" b.NR_BL, ");
                    query.AppendLine(" BL_TOKEN, ");
                    query.AppendLine(" p.ID_ARMADOR_LOGCOMEX, ");
                    query.AppendLine(" b.TRAKING_BL ");
                    query.AppendLine(" FROM  ");
                    query.AppendLine(" TB_BL b ");
                    query.AppendLine(" LEFT JOIN TB_PARCEIRO p ON(b.ID_PARCEIRO_TRANSPORTADOR  = p.ID_PARCEIRO ) ");
                    query.AppendLine(" WHERE b.GRAU = 'C' ");
                    query.AppendLine(" ) ");

                    _blmasters = con.Query<BlMaster>(query.ToString()).ToList();

                    string dados = null;
                    

                    Uteis ut = new Uteis();

                    foreach (var item in _blmasters)
                    {
                        if (item.BL_TOKEN == null)
                            item.BL_TOKEN = "-";

                        if (item.ID_ARMADOR_LOGCOMEX == 0 || item.ID_ARMADOR_LOGCOMEX == null)
                            item.ID_ARMADOR_LOGCOMEX = 0;

                        if (item.NR_BL == null)
                            item.NR_BL = "-";

                        if (item.TRAKING_BL == null)
                            item.TRAKING_BL = "-";

                        dados += "NR_BL :" + item.NR_BL + ", " +  "BL_TOKEN: " + item.BL_TOKEN + ", ID_ARMADOR_COMEX = " + item.ID_ARMADOR_LOGCOMEX +  "\n"; 
                    }
                    
                    ut.salvaLog(dados, query.ToString());

                    
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
        public List<BlMaster> GravaLog()
        {
            var connectionString = this.GetConnection();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    StringBuilder query = new StringBuilder();

                    con.Open();

                    query.AppendLine(" ( ");
                    query.AppendLine(" SELECT  ");
                    query.AppendLine(" b.NR_BL, ");
                    query.AppendLine(" BL_TOKEN, ");
                    query.AppendLine(" p.ID_ARMADOR_LOGCOMEX, ");
                    query.AppendLine(" b.TRAKING_BL ");
                    query.AppendLine(" FROM  ");
                    query.AppendLine(" TB_BL b ");
                    query.AppendLine(" LEFT JOIN TB_PARCEIRO p ON(b.ID_PARCEIRO_TRANSPORTADOR  = p.ID_PARCEIRO ) ");
                    query.AppendLine(" WHERE b.GRAU = 'M' ");
                    query.AppendLine(" ) ");
                    query.AppendLine(" UNION ");
                    query.AppendLine(" ( ");
                    query.AppendLine(" SELECT  ");
                    query.AppendLine(" b.NR_BL, ");
                    query.AppendLine(" BL_TOKEN, ");
                    query.AppendLine(" p.ID_ARMADOR_LOGCOMEX, ");
                    query.AppendLine(" b.TRAKING_BL ");
                    query.AppendLine(" FROM  ");
                    query.AppendLine(" TB_BL b ");
                    query.AppendLine(" LEFT JOIN TB_PARCEIRO p ON(b.ID_PARCEIRO_TRANSPORTADOR  = p.ID_PARCEIRO ) ");
                    query.AppendLine(" WHERE b.GRAU = 'C' ");
                    query.AppendLine(" ) ");

                    _blmasters = con.Query<BlMaster>(query.ToString()).ToList();

                    string dados = null;


                    Uteis ut = new Uteis();

                    foreach (var item in _blmasters)
                    {
                        if (item.BL_TOKEN == null)
                            item.BL_TOKEN = "-";

                        if (item.ID_ARMADOR_LOGCOMEX == 0 || item.ID_ARMADOR_LOGCOMEX == null)
                            item.ID_ARMADOR_LOGCOMEX = 0;

                        if (item.NR_BL == null)
                            item.NR_BL = "-";

                        if (item.TRAKING_BL == null)
                            item.TRAKING_BL = "-";

                        dados += "NR_BL :" + item.NR_BL + ", " + "BL_TOKEN: " + item.BL_TOKEN + ", ID_ARMADOR_COMEX = " + item.ID_ARMADOR_LOGCOMEX + ", " + "TRACKING_BL:" + item.TRAKING_BL + "\n";
                    }

                    ut.salvaLog(dados, query.ToString());


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
