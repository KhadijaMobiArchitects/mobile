using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("MAP_Traçabilite_Projet")]
    public partial class MapTraçabiliteProjet
    {
        [Key]
        public int Id { get; set; }
        public int TraçabiliteId { get; set; }
        public int ProjetId { get; set; }

        [ForeignKey(nameof(ProjetId))]
        [InverseProperty("MapTraçabiliteProjets")]
        public virtual Projet Projet { get; set; } = null!;
        [ForeignKey(nameof(TraçabiliteId))]
        [InverseProperty("MapTraçabiliteProjets")]
        public virtual Traçabilite Traçabilite { get; set; } = null!;
    }
}
