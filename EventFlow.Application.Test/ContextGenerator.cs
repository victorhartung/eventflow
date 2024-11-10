using EventFlow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Tests
{
    public static class ContextGenerator
    {

        public static EventFlowContext Generate()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventFlowContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .EnableSensitiveDataLogging()
              .LogTo(Console.WriteLine);

            var context = new EventFlowContext(optionsBuilder.Options);
         

            return context;

        }

    }
}
