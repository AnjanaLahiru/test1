using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID_Request.Models;
using System.Data.SqlClient;


namespace ID_Request.Repository
{
    interface IData
    {
        void SaveRequestData(RequestData details);
    }
}
