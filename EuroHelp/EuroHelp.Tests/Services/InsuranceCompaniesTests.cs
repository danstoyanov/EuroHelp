using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Xunit;

using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Services.InsuranceCompanies;

namespace EuroHelp.Tests.Services
{
    public class InsuranceCompaniesTests
    {
        private DbContextOptionsBuilder<EuroHelpDbContext> optionsBuilder;
        private EuroHelpDbContext db;
        private CompanyService insuranceCompanyService;

        public InsuranceCompaniesTests()
        {
            this.optionsBuilder = new DbContextOptionsBuilder<EuroHelpDbContext>()
                .UseInMemoryDatabase("TestDataBasse");
            this.db = new EuroHelpDbContext(optionsBuilder.Options);
            this.insuranceCompanyService = new CompanyService(this.db);
        }

        [Fact]
        public void GetAllInsuranceCompaniesCount()
        {
            ClearData();
            FillData();

            var companies = this.insuranceCompanyService.GetAll();
            var result = companies.Count;

            Assert.Equal(6, result);
            Assert.IsType<List<AllInsuranceCompaniesServiceModel>>(companies);
        }

        [Fact]
        public void CheckIfTheCompanyIdContainsInDatabase()
        {
            ClearData();
            FillData();

            var testIdFirst = "1";
            var testIdSecond = "OK";
            var testIdThird = "";

            var firstResult = this.insuranceCompanyService.IsCompanyContains(testIdFirst);
            var secondResult = this.insuranceCompanyService.IsCompanyContains(testIdSecond);
            var thirdResult = this.insuranceCompanyService.IsCompanyContains(testIdThird);

            Assert.Equal(true, firstResult);
            Assert.Equal(false, secondResult);
            Assert.Equal(false, thirdResult);
            Assert.NotNull(secondResult);
        }

        [Fact]
        public void GetCompanyReturnsCorrectCompany()
        {
            ClearData();
            FillData();

            var firstTestCompany = this.insuranceCompanyService.GetCompany("1");
            var secondTestCompany = this.insuranceCompanyService.GetCompany("2");
            var thirdTestCompany = this.insuranceCompanyService.GetCompany("1312");

            var firstTestId = "1";
            var secondTestId = "2";

            Assert.Equal(firstTestId, firstTestCompany.Id);
            Assert.Equal(secondTestId, secondTestCompany.Id);
            Assert.Null(thirdTestCompany);
        }

        [Fact]
        public void WhenYouTakeGetInsuranceCompaniesMethodYouWillTakeOnlyTheIdAndComapnyName()
        {
            ClearData();
            FillData();

            var insuranceCompanies = this.insuranceCompanyService.GetInsuranceCompanies();

            Assert.NotNull(insuranceCompanies);
        }

        [Fact]
        public void CreateNewCompanyAndCheckTheIdIfTheSameAndCompaniesCout()
        {
            ClearData();
            FillData();

            var companyResultId = this.insuranceCompanyService.Create(
            "Test Company LTD",
            1234,
            "5412 Street, Sofia",
            "09331144",
            "0883333333",
            "testcompany@mail.com",
            124312,
            "The first insurance company in Sofia since 1990",
            "b123");

            var count = this.insuranceCompanyService.GetAll().Count;

            Assert.Equal(4, count);
            Assert.NotNull(companyResultId);
        }

        [Fact]
        public void TryToChangeFewTimesTheInsuranceCompanyStatus()
        {
            ClearData();
            FillData();

            var status = this.insuranceCompanyService.ChangeStatus("1");

            Assert.Equal("Active", status);
            Assert.NotEqual("Non active", status);

            status = this.insuranceCompanyService.ChangeStatus("1");
            Assert.Equal("Non active", status);
            Assert.NotEqual("Active", status);
        }

        private void ClearData()
        {
            foreach (var company in this.db.InsuranceCompanies)
            {
                this.db.InsuranceCompanies.Remove(company);
            }
        }

        private void FillData()
        {
            AddInsuranceCompanies();
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