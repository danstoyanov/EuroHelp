using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Xunit;

using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Services.Damages;

namespace EuroHelp.Tests.Services
{
    public class DamageServiceTests
    {
        private DbContextOptionsBuilder<EuroHelpDbContext> optionsBuilder;
        private EuroHelpDbContext db;
        private DamageService damageService;

        public DamageServiceTests()
        {
            this.optionsBuilder = new DbContextOptionsBuilder<EuroHelpDbContext>()
                .UseInMemoryDatabase("TestDataBasse");
            this.db = new EuroHelpDbContext(optionsBuilder.Options);
            this.damageService = new DamageService(this.db);
        }

        [Fact]
        public void TheMethodGetAllReturnCorrectCountByDefaultAndWhenYouAddNewDamageInDb()
        {
            ClearData();
            FillData();

            var damagesTest = this.damageService.GetAll();

            Assert.Equal(4, damagesTest.Count);
            Assert.NotEqual(3, damagesTest.Count);

            var testDamage = new Damage
            {
                Id = "32a",
                DamageType = "Broken door!",
                CompanyName = "EuroKarma LTD",
                EventDate = DateTime.Parse("19.11.2021"),
                CompanyId = "1",
                Comment = "Not good !",
                EventPlace = "Sofia, Bulgaria",
                IdentityNumber = 123321,
                PersonFirstName = "Ivani",
                PersonSecondName = "Ivanovil",
                ConsumerId = "1c"
            };
            this.db.Damages.Add(testDamage);
            this.db.SaveChanges();

            damagesTest = this.damageService.GetAll();

            Assert.NotEqual(4, damagesTest.Count);
            Assert.Equal(5, damagesTest.Count);
        }

        [Fact]
        public void TheMethodGetByConsumerReturnsCorrectDamageByConsumerId()
        {
            ClearData();
            FillData();

            var firstTestConsumerId = "1c";
            var secondTestConsumerId = "test";

            var firstTestDamagesList = this.damageService.DamagesByConsumer(firstTestConsumerId);
            var secondTestDamageList = this.damageService.DamagesByConsumer(secondTestConsumerId);

            Assert.Equal(4, firstTestDamagesList.Count);
            Assert.NotNull(firstTestDamagesList);
            Assert.Equal(0, secondTestDamageList.Count);
            Assert.NotNull(secondTestDamageList);
        }

        [Fact]
        public void TheMethodCreateDamageReturnsCorrectDamage()
        {
            ClearData();
            FillData();

            var returnedId = this.damageService.Create(
                 "Broken Schooter",
                 DateTime.Parse("10.12.2022"),
                 123321,
                 "Grisha",
                 "Ganchev",
                 "Lovech, Bulgaria",
                 "I broke my schooter today !",
                 "13",
                 "1c",
                 "TestCompany"
                );

            var currDamage = this.db.Damages
                .Where(d => d.EventPlace == "Lovech, Bulgaria")
                .FirstOrDefault();

            Assert.Equal(returnedId, currDamage.Id);
            Assert.Equal("Lovech, Bulgaria", currDamage.EventPlace);
            Assert.NotNull(currDamage);
        }

        [Fact]
        public void TheDeleteMethodWorkCorrectly()
        {
            ClearData();
            FillData();

            var deleteDamage = this.db.Damages
                .Where(d => d.Id == "1a")
                .FirstOrDefault();

            this.damageService.Delete("1a");

            Assert.Equal(3, this.db.Damages.Count());
            Assert.False(this.db.Damages.Contains(deleteDamage));
        }

        [Fact]
        public void TheValidMethodWorksCorrectly()
        {
            ClearData();
            FillData();

            this.damageService.Delete("1a");

            var testResult1 = this.damageService.IsValid("1a");
            var testResult2 = this.damageService.IsValid("2a");
            var testResult3 = this.damageService.IsValid("test101");

            Assert.False(testResult1);
            Assert.True(testResult2);
            Assert.False(testResult3);
        }

        [Fact]
        public void TheChangeStatusMethodsWorksCorrectly()
        {
            ClearData();
            FillData();

            var firstTestDamage = this.db.Damages
                .Where(d => d.Id == "1a")
                .FirstOrDefault();
            var firstTestStatus = this.damageService
                .ChangeStatus("1a");

            var secondTestDamage = this.db.Damages
                .Where(d => d.Id == "4a")
                .FirstOrDefault();
            var secondTestStatus = this.damageService.ChangeStatus("4a");

            //var thirdTestDamage = this.db.Damages
            //    .Where(d => d.Id == "test")
            //    .FirstOrDefault();
            //var thirdTestStatus = this.damageService.ChangeStatus("test");

            Assert.True(firstTestDamage.IsApproved == firstTestStatus);
            Assert.True(secondTestDamage.IsApproved == secondTestStatus);
        }

        [Fact]
        public void ReturnAllDamageTypes()
        {
            ClearData();
            FillData();

            var firstTestType = "Broken door!";
            var secondTestType = "Broken car!";
            var thirdTestType = "Test damage";

            var types = this.damageService.AllDamageTypes();

            Assert.True(types.Contains(firstTestType));
            Assert.True(types.Contains(secondTestType));
            Assert.False(types.Contains(thirdTestType));
        }

        [Fact]
        public void GetDamageMethodWorksCorrectly()
        {
            ClearData();
            FillData();

            var firstId = "1a";
            var secondId = "2a";
            var thirdId = "a4";

            var firstDamage = this.damageService.GetDamage(firstId);
            var secondDamage = this.damageService.GetDamage(secondId);
            //var thirdDamage = this.damageService.GetDamage(thirdId);

            Assert.Equal(firstId, firstDamage.Id);
            Assert.Equal(secondId, secondDamage.Id);
            //Assert.NotEqual(thirdId, thirdDamage.Id);
        }

        [Fact]
        public void EditDamageCorrectly()
        {
            ClearData();
            FillData();

            var testId = "1a";
            var testDamageType = "TestDamage";
            var testEventDate = "10.11.2021";
            var testPersonFirstName = "Test";
            var testPersonSecondName = "Testov";
            var testIdentityNumber = 321123;

            this.damageService.Edit(
                testId,
                testDamageType,
                testEventDate,
                testPersonFirstName,
                testPersonSecondName,
                testIdentityNumber);

            var changedDamage = this.damageService.GetDamage(testId);

            Assert.Equal(testId, changedDamage.Id);
            Assert.Equal(testDamageType, changedDamage.DamageType);
            Assert.Equal(DateTime.Parse(testEventDate), changedDamage.EventDate);
            Assert.Equal(testPersonFirstName, changedDamage.PersonFirstName);
            Assert.Equal(testPersonSecondName, changedDamage.PersonSecondName);
            Assert.Equal(testIdentityNumber, changedDamage.IdentityNumber);
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

        public void FillData()
        {
            AddInsuranceCompanies();
            AddDamages();
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
                Id = "11",
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
                Id = "12",
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
                Id = "13",
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
