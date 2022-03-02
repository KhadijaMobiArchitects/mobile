using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("Conge")]
    public partial class Conge
    {
        public Conge()
        {
            MapTraçabiliteConges = new HashSet<MapTraçabiliteConge>();
        }

        [Key]
        public int Id { get; set; }
        [Column("Date_debut", TypeName = "date")]
        public DateTime DateDebut { get; set; }
        [Column("Date_fin", TypeName = "date")]
        public DateTime DateFin { get; set; }
        public bool ConfirmeParSquad { get; set; }
        [Column("REF_TypeCongeID")]
        public int? RefTypeCongeId { get; set; }
        [Column("REF_StatutCongeID")]
        public int? RefStatutCongeId { get; set; }
        [Column("UtilisateurID")]
        public int? UtilisateurId { get; set; }

        [ForeignKey(nameof(RefStatutCongeId))]
        [InverseProperty("Conges")]
        public virtual RefStatutConge? RefStatutConge { get; set; }
        [ForeignKey(nameof(RefTypeCongeId))]
        [InverseProperty("Conges")]
        public virtual RefTypeConge? RefTypeConge { get; set; }
        [ForeignKey(nameof(UtilisateurId))]
        [InverseProperty("Conges")]
        public virtual Utilisateur? Utilisateur { get; set; }
        [InverseProperty(nameof(MapTraçabiliteConge.Conge))]
        public virtual ICollection<MapTraçabiliteConge> MapTraçabiliteConges { get; set; }
    }
}
