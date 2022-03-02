using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("REF_SituationProjet")]
    public partial class RefSituationProjet
    {
        public RefSituationProjet()
        {
            Projets = new HashSet<Projet>();
        }

        [Key]
        public int Id { get; set; }
        public string? Libelle { get; set; }

        [InverseProperty(nameof(Projet.RefSituationProjet))]
        public virtual ICollection<Projet> Projets { get; set; }
    }
}
