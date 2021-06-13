using BusinessRulesEngine.Data;
using BusinessRulesEngine.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Tests.Utils
{
    internal class MockContext
    {
        public IDbContext GetDbContext(string dataBaseName)
        {
            var options = new DbContextOptionsBuilder<BusinessRulesEngineDbContext>()
                .UseInMemoryDatabase(databaseName: dataBaseName)
                .Options;

            var dataBaseContext = new BusinessRulesEngineDbContext(options);

            return dataBaseContext;
        }
    }
}
