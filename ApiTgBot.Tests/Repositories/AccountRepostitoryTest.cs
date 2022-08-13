using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiTgBot.Models.EF;
using ApiTgBot.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiTgBot.Tests.Repositories
{
    //TODO make tests
    internal class AccountRepostitoryTest
    {
        IRepository<Account> repository;
        [SetUp]
        public void Setup()
        {
            DbContextOptions<ApplicationContext> options = new DbContextOptions<ApplicationContext>();
            
            
            //ApplicationContext db = new ApplicationContext();
        }
    }
}
