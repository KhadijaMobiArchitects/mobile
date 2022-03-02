using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("Utilisateur")]
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Conges = new HashSet<Conge>();
            Squads = new HashSet<Squad>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Nom { get; set; }
        [StringLength(50)]
        public string? Prenom { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public string? Tel { get; set; }
        [StringLength(100)]
        public string? NomUtilisateur { get; set; }
        [StringLength(100)]
        public string? MotDePasse { get; set; }
        [Column("REF_FonctionId")]
        public int? RefFonctionId { get; set; }
        [Column("REF_RoleId")]
        public int? RefRoleId { get; set; }

        [ForeignKey(nameof(RefFonctionId))]
        [InverseProperty("Utilisateurs")]
        public virtual RefFonction? RefFonction { get; set; }
        [ForeignKey(nameof(RefRoleId))]
        [InverseProperty("Utilisateurs")]
        public virtual RefRole? RefRole { get; set; }
        [InverseProperty(nameof(Conge.Utilisateur))]
        public virtual ICollection<Conge> Conges { get; set; }
        [InverseProperty(nameof(Squad.Utilisateur))]
        public virtual ICollection<Squad> Squads { get; set; }
    }
}
