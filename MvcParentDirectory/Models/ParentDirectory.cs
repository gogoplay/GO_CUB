using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MvcParentDirectory.Models
{
    public class ParentDirectory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
    }

    public class ParentDirectoryDBContext : DbContext
    {
        public DbSet<ParentDirectory> ParentDirectories { get; set; }
    }
}