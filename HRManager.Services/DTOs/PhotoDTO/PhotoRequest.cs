using HRManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.PhotoDTO
{
    public class PhotoRequest
    {
        public string Filename { get; set; }
        public string Uri { get; set; }
        public int EmployeeID { get; set; }
    }
}
