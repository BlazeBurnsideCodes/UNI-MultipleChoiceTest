using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Database
{
    class DatabaseCommunicator
    {
        protected SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-2PBBTUH;Initial Catalog=MC_Database;Integrated Security=True");    //Connection string for accessing the database

        
    }
}
