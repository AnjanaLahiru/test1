using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ID_Request.Models;

namespace ID_Request.Models
{
    public class RequestData
    {
        public int Id { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string Reason { get; set; }
        public string Section { get; set; }
        public string Status { get; set; }

        public RequestData()
        {
            RequestedDate = DateTime.Today; // Sets to current date with time at midnight
        }
    }
}