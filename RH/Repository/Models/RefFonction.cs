using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("REF_Fonction")]
    public partial class RefFonction
    {
        public RefFonction()
        {
            Utilisateurs = new HashSet<Utilisateur>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Utilisateur.RefFonction))]
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
