using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Cockpit.Handler
{
    public static class ExceptionHandler
    {
        public static String SqlExceptionMessage(SqlException e)
        {
            if (e.Number == 547 && e.ErrorCode == -2146232060) //Erro de exclusão de elemento ainda associado
            {
                String foreignKeyString = e.Message.Substring(e.Message.IndexOf(@"FK_"));
                foreignKeyString = foreignKeyString.Substring(0, foreignKeyString.IndexOf("\""));

                String[] foreignKeyElements = foreignKeyString.Split('_');
                return String.Format("Não foi possível excluir o registro em {0}, pois existem registros em {1} que estão associados a esse registro", foreignKeyElements[2], foreignKeyElements[1]);
            }

            return "Ocorreu um erro durante a consulta.";
        }
    }
}