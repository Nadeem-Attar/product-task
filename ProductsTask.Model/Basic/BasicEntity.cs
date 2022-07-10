using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsTask.Model.Basic
{
    public class BasicEntity
    {
        public BasicEntity()
        {
            IsValid = true;
            CreateDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public bool IsValid { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int LastUpdateBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
