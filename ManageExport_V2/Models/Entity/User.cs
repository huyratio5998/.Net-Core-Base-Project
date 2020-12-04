using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ManageExport_V2.Models.Entity
{
    public class User : BaseEntity<int>
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Note { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public UserType UserType { get; set; }
        public int? SubsidiaryTotalProduct { get; set; }
        public string AgentName { get; set; }
        public string SupplyCode { get; set; }
        public string SupplyName { get; set; }
        public double? Salary { get; set; }
        public string Avatar { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ExportProductBill> ExportDocumentBills { get; set; }

        [NotMapped]        
        public IFormFile ImageFile { get; set; }
    }
    public enum Gender
    {
        FeMale=0,
        Male=1,
        Other=2
    }
    public enum UserType
    {
        Supply=0,
        SubsidiaryAgent=1,
        StockExportManager=2
    }
}
