using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RH.Repository.Models
{
    [Table("Traçabilite")]
    public partial class Traçabilite
    {
        public Traçabilite()
        {
            MapTraçabiliteConges = new HashSet<MapTraçabiliteConge>();
            MapTraçabiliteProjets = new HashSet<MapTraçabiliteProjet>();
            Notifications = new HashSet<Notification>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateCreation { get; set; }
        [Column("DateMiseAJour", TypeName = "date")]
        public DateTime DateMiseAjour { get; set; }
        [StringLength(50)]
        public string CreePar { get; set; } = null!;
        [StringLength(50)]
        public string SupprimePar { get; set; } = null!;
        [StringLength(50)]
        public string Descriptif { get; set; } = null!;
        [Column("REF_TypeTraçabiliteId")]
        public int RefTypeTraçabiliteId { get; set; }
        [Column("REF_DecisionId")]
        public int RefDecisionId { get; set; }

        [ForeignKey(nameof(RefDecisionId))]
        [InverseProperty("Traçabilites")]
        public virtual RefDecision RefDecision { get; set; } = null!;
        [ForeignKey(nameof(RefTypeTraçabiliteId))]
        [InverseProperty("Traçabilites")]
        public virtual RefTypeTraçabilite RefTypeTraçabilite { get; set; } = null!;
        [InverseProperty(nameof(MapTraçabiliteConge.Traçabilite))]
        public virtual ICollection<MapTraçabiliteConge> MapTraçabiliteConges { get; set; }
        [InverseProperty(nameof(MapTraçabiliteProjet.Traçabilite))]
        public virtual ICollection<MapTraçabiliteProjet> MapTraçabiliteProjets { get; set; }
        [InverseProperty(nameof(Notification.Traçabilite))]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
