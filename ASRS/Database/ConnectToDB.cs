﻿using Microsoft.EntityFrameworkCore;

namespace ASRS.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<InputConcentration> InputConcentrations { get; set; } = null!;
        public DbSet<Phase> Phases { get; set; } = null!;
        public DbSet<BaseForm> BaseForms { get; set; } = null!;
        public DbSet<FormingForm> FormingForms { get; set; } = null!;
        public DbSet<ConcentrationConstant> ConcentrationConstants { get; set; } = null!;
        public DbSet<ConstantsSeries> ConstantsSeries { get; set; } = null!;
        public DbSet<Mechanisms> Mechanisms { get; set; } = null!;
        public DbSet<ReactionMechanism> ReactionMechanism { get; set; } = null!;
        public DbSet<Reaction> Reactions { get; set; } = null!;
        public DbSet<ExperimentalPoints> ExperimentalPoints { get; set; } = null!;
        public DbSet<Points> Points { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=compmodeling.db");
        }
    }
}