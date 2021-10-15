using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Entities
{
    public class EntityBase
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }
        public bool Status { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid().ToString();
            Status = true;
        }
    }
}
