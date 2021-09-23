using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LogComexService.Repositorio;
using LogComexService.Model;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;


namespace LogComexService.Repositorio
{
    public class BlHouseRepositorio: IBlHouseRepositorio
    {
        private List<BlHouse> _blHouse = new List<BlHouse>();
        IConfiguration _configuration;

        public BlHouseRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Conexao()
        {
            var connection = _configuration.GetSection("SqlConnection").GetSection("SqlConnectionString").Value;
            return connection;
        }

        public List<BlHouse> GetAllDadosBlHouse()
        {
            try
            {                
                using (var con = new SqlConnection(Conexao()))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT  ");
                    sb.AppendLine(" b.NR_BL, ");
                    sb.AppendLine(" BL_TOKEN, ");
                    sb.AppendLine(" p.ID_ARMADOR_LOGCOMEX, ");
                    sb.AppendLine(" b.TRAKING_BL ");
                    sb.AppendLine(" FROM  ");
                    sb.AppendLine(" TB_BL b ");
                    sb.AppendLine(" LEFT JOIN TB_PARCEIRO p ON(b.ID_PARCEIRO_TRANSPORTADOR  = p.ID_PARCEIRO ) ");
                    sb.AppendLine(" WHERE b.GRAU = 'C' ");
                    //sb.AppendLine(" AND  ");
                    //sb.AppendLine(" NR_BL =  '" + bl + "' "); 

                    _blHouse = con.Query<BlHouse>(sb.ToString()).ToList();

                    return _blHouse;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void CadastrarTokenBLHouse(BlHouse bl)
        {
            try
            {
                using (var con = new SqlConnection(Conexao()))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("  UPDATE TB_BL SET BL_TOKEN = '" + bl.BL_TOKEN + "' WHERE NR_BL = '" + bl.NR_BL + "'  ");

                    con.Query<BlHouse>(sb.ToString()).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AtualizarBLTrackingHouse(BlHouse bl)
        {
            try
            {
                using (var con = new SqlConnection(Conexao()))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" UPDATE TB_BL SET TRAKING_BL = '" + bl.TRAKING_BL.Replace("'", "") + "' WHERE BL_TOKEN = '" + bl.BL_TOKEN + "' ");

                    con.Query<BlHouse>(sb.ToString()).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
