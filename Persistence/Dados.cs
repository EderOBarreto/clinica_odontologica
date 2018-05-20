using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    class Dados
    {
        public static string strConexao
        {
            get
            {
                //As configurações devem ser alteradas de acordo com as configuracoes do mysql.
                return "server = localhost; DataBase = clinica_odontologica; Uid = root; Pwd =; Connect Timeout = 30";
            }
        }
    }
}
