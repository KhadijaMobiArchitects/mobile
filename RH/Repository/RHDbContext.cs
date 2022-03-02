using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RH.Repository.Models;

namespace RH.Repository
{
    public partial class RHDbContext : DbContext
    {
        public RHDbContext()
        {
        }

        public RHDbContext(DbContextOptions<RHDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conge> Conges { get; set; } = null!;
        public virtual DbSet<MapTraçabiliteConge> MapTraçabiliteConges { get; set; } = null!;
        public virtual DbSet<MapTraçabiliteProjet> MapTraçabiliteProjets { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Projet> Projets { get; set; } = null!;
        public virtual DbSet<RefDecision> RefDecisions { get; set; } = null!;
        public virtual DbSet<RefFonction> RefFonctions { get; set; } = null!;
        public virtual DbSet<RefRole> RefRoles { get; set; } = null!;
        public virtual DbSet<RefSituationProjet> RefSituationProjets { get; set; } = null!;
        public virtual DbSet<RefStatutConge> RefStatutConges { get; set; } = null!;
        public virtual DbSet<RefTypeConge> RefTypeConges { get; set; } = null!;
        public virtual DbSet<RefTypeTraçabilite> RefTypeTraçabilites { get; set; } = null!;
        public virtual DbSet<Squad> Squads { get; set; } = null!;
        public virtual DbSet<Traçabilite> Traçabilites { get; set; } = null!;
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RH;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conge>(entity =>
            {
                entity.HasOne(d => d.RefStatutConge)
                    .WithMany(p => p.Conges)
                    .HasForeignKey(d => d.RefStatutCongeId)
                    .HasConstraintName("FK_Conge_ToREF_StatutConge");

                entity.HasOne(d => d.RefTypeConge)
                    .WithMany(p => p.Conges)
                    .HasForeignKey(d => d.RefTypeCongeId)
                    .HasConstraintName("FK_Conge_ToTypeConge");

                entity.HasOne(d => d.Utilisateur)
                    .WithMany(p => p.Conges)
                    .HasForeignKey(d => d.UtilisateurId)
                    .HasConstraintName("FK_Conge_ToUtilisateur");
            });

            modelBuilder.Entity<MapTraçabiliteConge>(entity =>
            {
                entity.HasOne(d => d.Conge)
                    .WithMany(p => p.MapTraçabiliteConges)
                    .HasForeignKey(d => d.CongeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAP_Traçabilite_Conge_ToConge");

                entity.HasOne(d => d.Traçabilite)
                    .WithMany(p => p.MapTraçabiliteConges)
                    .HasForeignKey(d => d.TraçabiliteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAP_Traçabilite_Conge_ToTraçabilite");
            });

            modelBuilder.Entity<MapTraçabiliteProjet>(entity =>
            {
                entity.HasOne(d => d.Projet)
                    .WithMany(p => p.MapTraçabiliteProjets)
                    .HasForeignKey(d => d.ProjetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAP_Traçabilite_Projet_ToConge");

                entity.HasOne(d => d.Traçabilite)
                    .WithMany(p => p.MapTraçabiliteProjets)
                    .HasForeignKey(d => d.TraçabiliteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAP_Traçabilite_Projet_ToTraçabilite");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.Traçabilite)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TraçabiliteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_ToTraçabilite");
            });

            modelBuilder.Entity<Projet>(entity =>
            {
                entity.HasOne(d => d.RefSituationProjet)
                    .WithMany(p => p.Projets)
                    .HasForeignKey(d => d.RefSituationProjetId)
                    .HasConstraintName("FK_Projet_ToREF_SituationProjet");

                entity.HasOne(d => d.Squad)
                    .WithMany(p => p.Projets)
                    .HasForeignKey(d => d.SquadId)
                    .HasConstraintName("FK_Projet_ToSquad");
            });

            modelBuilder.Entity<Squad>(entity =>
            {
                entity.HasOne(d => d.Utilisateur)
                    .WithMany(p => p.Squads)
                    .HasForeignKey(d => d.UtilisateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Squad_ToUtilisateur");
            });

            modelBuilder.Entity<Traçabilite>(entity =>
            {
                entity.HasOne(d => d.RefDecision)
                    .WithMany(p => p.Traçabilites)
                    .HasForeignKey(d => d.RefDecisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Traçabilite_ToDecision");

                entity.HasOne(d => d.RefTypeTraçabilite)
                    .WithMany(p => p.Traçabilites)
                    .HasForeignKey(d => d.RefTypeTraçabiliteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Traçabilite_ToREF_TypeTraçabilite");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasOne(d => d.RefFonction)
                    .WithMany(p => p.Utilisateurs)
                    .HasForeignKey(d => d.RefFonctionId)
                    .HasConstraintName("FK_Utilisateur_ToREF_Fonction");

                entity.HasOne(d => d.RefRole)
                    .WithMany(p => p.Utilisateurs)
                    .HasForeignKey(d => d.RefRoleId)
                    .HasConstraintName("FK_Utilisateur_ToREF_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
