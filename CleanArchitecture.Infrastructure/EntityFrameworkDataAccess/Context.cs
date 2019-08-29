using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Entities;

namespace CleanArchitecture.Infrastructure.EntityFrameworkDataAccess
{
    public class Context : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ContaCorrente> Contas { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<Debito> Debitos { get; set; }


        public Context(DbContextOptions options, ContextInitializer contextInitializer) : base(options)
        {
            contextInitializer?.InicializarComDadosFake(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Cliente>();

            modelBuilder.Entity<Entities.ContaCorrente>();

            modelBuilder.Entity<Entities.Credito>();

            modelBuilder.Entity<Entities.Debito>();

            base.OnModelCreating(modelBuilder);
        }


    }
}
