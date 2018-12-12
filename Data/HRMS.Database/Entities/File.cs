using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    public class File:BaseEntity<int>
    {
        [MaxLength(200)]
        public string FileName { get; set; }

        [MaxLength(50)]
        public string FileExtension { get; set; }

        [MaxLength(500)]
        public string PhysicAddr { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
