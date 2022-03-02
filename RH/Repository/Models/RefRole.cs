using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("REF_Role")]
    public partial class RefRole
    {
        public RefRole()
        {
            Utilisateurs = new HashSet<Utilisateur>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Nom { get; set; } = null!;

        [InverseProperty(nameof(Utilisateur.RefRole))]
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
