using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("REF_StatutConge")]
    public partial class RefStatutConge
    {
        public RefStatutConge()
        {
            Conges = new HashSet<Conge>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Libelle { get; set; }

        [InverseProperty(nameof(Conge.RefStatutConge))]
        public virtual ICollection<Conge> Conges { get; set; }
    }
}
