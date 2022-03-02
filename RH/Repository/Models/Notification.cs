using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Titre { get; set; } = null!;
        public string Message { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public bool EstLis { get; set; }
        public int TraçabiliteId { get; set; }

        [ForeignKey(nameof(TraçabiliteId))]
        [InverseProperty("Notifications")]
        public virtual Traçabilite Traçabilite { get; set; } = null!;
    }
}
