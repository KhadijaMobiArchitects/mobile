using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("Projet")]
    public partial class Projet
    {
        public Projet()
        {
            MapTraçabiliteProjets = new HashSet<MapTraçabiliteProjet>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Nom ")]
        [StringLength(100)]
        public string Nom { get; set; } = null!;
        public float Pourcentage { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateCreation { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateFin { get; set; }
        [Column("SquadID")]
        public int? SquadId { get; set; }
        [Column("REF_SituationProjetID")]
        public int? RefSituationProjetId { get; set; }

        [ForeignKey(nameof(RefSituationProjetId))]
        [InverseProperty("Projets")]
        public virtual RefSituationProjet? RefSituationProjet { get; set; }
        [ForeignKey(nameof(SquadId))]
        [InverseProperty("Projets")]
        public virtual Squad? Squad { get; set; }
        [InverseProperty(nameof(MapTraçabiliteProjet.Projet))]
        public virtual ICollection<MapTraçabiliteProjet> MapTraçabiliteProjets { get; set; }
    }
}
