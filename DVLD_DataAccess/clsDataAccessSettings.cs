using System;
using System.Configuration;

namespace DVLD_DataAccess
{
    internal static class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
    }
}
