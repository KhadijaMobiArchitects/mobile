using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("Squad")]
    public partial class Squad
    {
        public Squad()
        {
            Projets = new HashSet<Projet>();
        }

        [Key]
        public int Id { get; set; }
        public int UtilisateurId { get; set; }

        [ForeignKey(nameof(UtilisateurId))]
        [InverseProperty("Squads")]
        public virtual Utilisateur Utilisateur { get; set; } = null!;
        [InverseProperty(nameof(Projet.Squad))]
        public virtual ICollection<Projet> Projets { get; set; }
    }
}
