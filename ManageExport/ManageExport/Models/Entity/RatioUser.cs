using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport.Models.Entity
{
    public class RatioUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public UserType UserType { get; set; }
        public int TotalProduct { get; set; }
        public string AgentName { get; set; }
        public string SupplyCode { get; set; }
        public string SupplyName { get; set; }
        public double Salary { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ExportDocumentBill> ExportDocumentBills { get; set; }

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
