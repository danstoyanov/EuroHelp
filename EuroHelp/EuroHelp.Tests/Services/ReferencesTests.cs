using System;
using Microsoft.EntityFrameworkCore;

using Xunit;

using EuroHelp.Data;
using EuroHelp.Services.References;
using System.Collections.Generic;
using EuroHelp.Data.Models;
using System.Linq;

namespace EuroHelp.Tests.Services
{
    public class ReferencesTests
    {
        private DbContextOptionsBuilder<EuroHelpDbContext> optionsBuilder;
        private EuroHelpDbContext db;
        private ReferenceService referenceService;

        public ReferencesTests()
        {
            this.optionsBuilder = new DbContextOptionsBuilder<EuroHelpDbContext>()
                .UseInMemoryDatabase("TestDataBase");
            this.db = new EuroHelpDbContext(optionsBuilder.Options);
            this.referenceService = new ReferenceService(this.db);
        }

        [Fact]
        public void TheMethodGenerateAndReturnReferenceExportCorrectFileNameAndContentType()
        {
            var startTestDate = "10.02.2020";
            var endTestDate = "15.04.2021";

            var testOutputFile = this.referenceService.GenerateFile(startTestDate, endTestDate);

            var testOutputFileName = "damages_list.csv";
            var testContentType = "text/csv";

            Assert.Equal(testOutputFileName, testOutputFile.FileName);
            Assert.Equal(testContentType, testOutputFile.ContentType);
        }

        [Fact]
        public void CheckTheExportFileContestsIsNotNull()
        {
            ClearData();
            FillData();

            var testDate = DateTime.Parse("09.11.2021");

            var damage = this.db.Damages
                .Where(d => d.EventDate == testDate)
                .FirstOrDefault();

            var testOutputFile = this.referenceService
                .GenerateFile(damage.EventDate.ToString(), damage.EventDate.ToString());

            Assert.NotNull(testOutputFile);
        }
        private void FillData()
        {
            AddInsuranceCompanies();
            AddDamages();
        }

        public void ClearData()
        {
            foreach (var damage in this.db.Damages)
            {
                this.db.Damages.Remove(damage);
            }

            foreach (var company in this.db.InsuranceCompanies)
            {
                this.db.InsuranceCompanies.Remove(company);
            }
        }

        private void AddDamages()
        {
            var damages = new List<Damage>();

            damages.Add(new Damage
            {
                Id = "1a",
                DamageType = "Broken door!",
                CompanyName = "EuroKarma LTD",
                EventDate = DateTime.Parse("09.11.2021"),
                CompanyId = "1",
                Comment = "Not good !",
                EventPlace = "Sofia, Bulgaria",
                IdentityNumber = 123321,
                PersonFirstName = "Ivan",
                PersonSecondName = "Ivanov",
                ConsumerId = "1c"
            });

            damages.Add(new Damage
            {
                Id = "2a",
                DamageType = "Broken car!",
                CompanyName = "EuroKarma LTD",
                EventDate = DateTime.Parse("10.11.2021"),
                CompanyId = "1",
                Comment = "Not good !",
                EventPlace = "Plovdiv, Bulgaria",
                IdentityNumber = 123321,
                PersonFirstName = "Ivan",
                PersonSecondName = "Ivanov",
                ConsumerId = "1c"
            });

            damages.Add(new Damage
            {
                Id = "3a",
                DamageType = "Broken bike!",
                CompanyName = "EuroKarma LTD",
                EventDate = DateTime.Parse("11.11.2021"),
                CompanyId = "1",
                Comment = "Not good !",
                EventPlace = "Varna, Bulgaria",
                IdentityNumber = 123321,
                PersonFirstName = "Ivan",
                PersonSecondName = "Ivanov",
                ConsumerId = "1c"
            });

            damages.Add(new Damage
            {
                Id = "4a",
                DamageType = "Broken arm!",
                CompanyName = "Hospital Insurance",
                EventDate = DateTime.Parse("12.11.2021"),
                CompanyId = "2",
                Comment = "Not good !",
                EventPlace = "Berlin, Germany",
                IdentityNumber = 123321,
                PersonFirstName = "Ivan",
                PersonSecondName = "Ivan",
                ConsumerId = "1c"
            });


            this.db.Damages.AddRange(damages);
            this.db.SaveChanges();
        }

        private void AddInsuranceCompanies()
        {
            var insuranceCompanies = new List<InsuranceCompany>();

            insuranceCompanies.Add(new InsuranceCompany
            {
                Id = "1",
                Name = "EuroKarma LTD",
                Bulstat = 123,
                Address = "Sofia, 1251 Street",
                PhoneNumber = "0299888",
                MobilePhoneNumber = "0888333333",
                Email = "eurocarma@mail.com",
                FAX = 3123,
                Notes = "This is the best insurance company !"
            });

            insuranceCompanies.Add(new InsuranceCompany
            {
                Id = "2",
                Name = "Hospital Insurance",
                Bulstat = 321,
                Address = "Burgas, 1251 Street",
                PhoneNumber = "0599888",
                MobilePhoneNumber = "08883322133",
                Email = "hospitalinsurance@mail.com",
                FAX = 3123,
                Notes = "This is the best hospital insurance company !"
            });

            insuranceCompanies.Add(new InsuranceCompany
            {
                Id = "3",
                Name = "Velingrad Insurance",
                Bulstat = 312,
                Address = "Velingrad, 1251 Street",
                PhoneNumber = "029981231",
                MobilePhoneNumber = "08555333333",
                Email = "velingradinsurance@mail.com",
                FAX = 4312,
                Notes = "This is the best insurance Velingrad company !"
            });

            this.db.InsuranceCompanies.AddRange(insuranceCompanies);
            this.db.SaveChanges();
        }
    }
}
