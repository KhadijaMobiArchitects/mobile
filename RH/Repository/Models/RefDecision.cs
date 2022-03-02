using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("REF_Decision")]
    public partial class RefDecision
    {
        public RefDecision()
        {
            Traçabilites = new HashSet<Traçabilite>();
        }

        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Traçabilite.RefDecision))]
        public virtual ICollection<Traçabilite> Traçabilites { get; set; }
    }
}
