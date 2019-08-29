using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess;

namespace CleanArchitecture.Infrastructure.Tests
{
    public abstract class TestBase
    {
        public TestBase()
        {
            var builder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase("TesteDbServer");
            this.Context = new Context(builder.Options, null);
        }

        public Context Context { get; }
    }
}
