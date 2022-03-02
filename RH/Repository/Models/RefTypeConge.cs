using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("REF_TypeConge")]
    public partial class RefTypeConge
    {
        public RefTypeConge()
        {
            Conges = new HashSet<Conge>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Conge.RefTypeConge))]
        public virtual ICollection<Conge> Conges { get; set; }
    }
}
