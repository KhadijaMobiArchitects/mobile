using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("REF_TypeTraçabilite")]
    public partial class RefTypeTraçabilite
    {
        public RefTypeTraçabilite()
        {
            Traçabilites = new HashSet<Traçabilite>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Traçabilite.RefTypeTraçabilite))]
        public virtual ICollection<Traçabilite> Traçabilites { get; set; }
    }
}
