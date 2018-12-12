using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Database.Interfaces;

namespace HRMS.Database.Entities
{
    public abstract class BaseEntity<TIdentifier> : IEntity<TIdentifier>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TIdentifier Id { get; set; }

        public System.DateTime? ModifyTime { get; set; }
        [MaxLength(150)]
        public string Modifier { get; set; }

        public System.DateTime CreateTime { get; set; }
        [MaxLength(150)]
        public string Creator { get; set; }
    }
}
