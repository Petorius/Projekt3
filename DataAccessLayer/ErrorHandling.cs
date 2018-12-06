using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccessLayer {
    public static class ErrorHandling {
        public static string Exception(SqlException e) {
            string errorMessage = "";
            switch(e.Number) {
                case 53:
                    errorMessage = "Der er ikke forbindelse til databasen";
                    break;
                case 8187:
                    errorMessage = "Ressourcen du prøver at tilgå findes ikke";
                    break;

                default:
                    errorMessage = "Der er sket en ukendt fejl";
                    break;
            }
            return errorMessage;
        }
    }
}
