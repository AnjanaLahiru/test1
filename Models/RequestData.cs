using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ID_Request.Models
{
    public class RequestData
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Requested By is required")]
        [Display(Name = "Requested By")]
        public string RequestedBy { get; set; }

        [Required(ErrorMessage = "Section is required")]
        public string Section { get; set; }

        [Required(ErrorMessage = "Request Date is required")]
        [Display(Name = "Request Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RequestedDate { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        [Display(Name = "Employee Number")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }

    // Class to manage RequestData operations
    public class RequestDataManager
    {
        private List<RequestData> _requestDataList;

        // Constructor that initializes the list or loads it from a data source
        public RequestDataManager()
        {
            _requestDataList = new List<RequestData>();
            // Initialize with sample data or load from database
        }

        // Get all requests
        public List<RequestData> GetAllRequests()
        {
            return _requestDataList;
        }

        // Get a specific request by ID
        public RequestData GetRequestById(int id)
        {
            return _requestDataList.FirstOrDefault(r => r.Id == id);
        }

        // Add or update a request
        public bool SaveRequest(RequestData request)
        {
            try
            {
                if (request.Id == 0)
                {
                    // New request - assign ID and add to list
                    request.Id = _requestDataList.Count > 0 ? _requestDataList.Max(r => r.Id) + 1 : 1;
                    _requestDataList.Add(request);
                }
                else
                {
                    // Update existing request
                    var existingRequest = _requestDataList.FirstOrDefault(r => r.Id == request.Id);
                    if (existingRequest != null)
                    {
                        existingRequest.RequestedBy = request.RequestedBy;
                        existingRequest.Section = request.Section;
                        existingRequest.RequestedDate = request.RequestedDate;
                        existingRequest.EmployeeId = request.EmployeeId;
                        existingRequest.EmployeeName = request.EmployeeName;
                        existingRequest.Reason = request.Reason;
                        existingRequest.Status = request.Status;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete a request
        public bool DeleteRequest(int id)
        {
            try
            {
                var request = _requestDataList.FirstOrDefault(r => r.Id == id);
                if (request != null)
                {
                    _requestDataList.Remove(request);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

}