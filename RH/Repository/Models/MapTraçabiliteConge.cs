using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("MAP_Traçabilite_Conge")]
    public partial class MapTraçabiliteConge
    {
        [Key]
        public int Id { get; set; }
        public int TraçabiliteId { get; set; }
        public int CongeId { get; set; }

        [ForeignKey(nameof(CongeId))]
        [InverseProperty("MapTraçabiliteConges")]
        public virtual Conge Conge { get; set; } = null!;
        [ForeignKey(nameof(TraçabiliteId))]
        [InverseProperty("MapTraçabiliteConges")]
        public virtual Traçabilite Traçabilite { get; set; } = null!;
    }
}
