﻿using LetPortal.Core.Persistences;
using LetPortal.Core.Utils;
using LetPortal.Identity.Entities;
using LetPortal.Identity.Repositories.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LetPortal.Tests.ITs.Identity.Repositories
{
    public class IssuedTokenRepositoryTests : IClassFixture<IntegrationTestsContext>
    {
        private readonly IntegrationTestsContext _context;

        public IssuedTokenRepositoryTests(IntegrationTestsContext context)
        {
            _context = context;
        }

        [Fact]
        public async Task Get_By_Refresh_Token_Mongo_Test()
        {
            // Arrange
            var databaseOptions = _context.MongoDatabaseOptions;
            var databaseOptionsMock = Mock.Of<IOptionsMonitor<DatabaseOptions>>(_ => _.CurrentValue == databaseOptions);
            var issuedTokenRepository = new IssuedTokenMongoRepository(new MongoConnection(databaseOptionsMock.CurrentValue));

            // Act
            var issuedToken = new IssuedToken
            {
                Id = DataUtil.GenerateUniqueId(),
                ExpiredJwtToken = DateTime.UtcNow.AddDays(30),
                ExpiredRefreshToken = DateTime.UtcNow.AddDays(30),
                JwtToken = "abc",
                RefreshToken = "xyz",
                UserId = "5c06a15e4cc9a850bca44488",
                UserSessionId = DataUtil.GenerateUniqueId()
            };
            await issuedTokenRepository.AddAsync(issuedToken);
            var foundToken = await issuedTokenRepository.GetByRefreshToken("xyz");
            // Assert
            Assert.NotNull(foundToken);
        }

        [Fact]
        public async Task Deactive_Refresh_Token_Mongo_Test()
        {
            // Arrange
            var databaseOptions = _context.MongoDatabaseOptions;
            var databaseOptionsMock = Mock.Of<IOptionsMonitor<DatabaseOptions>>(_ => _.CurrentValue == databaseOptions);
            var issuedTokenRepository = new IssuedTokenMongoRepository(new MongoConnection(databaseOptionsMock.CurrentValue));

            // Act
            var issuedToken = new IssuedToken
            {
                Id = DataUtil.GenerateUniqueId(),
                ExpiredJwtToken = DateTime.UtcNow.AddDays(30),
                ExpiredRefreshToken = DateTime.UtcNow.AddDays(30),
                JwtToken = "abcy",
                RefreshToken = "xyzy",
                UserId = "5c06a15e4cc9a850bca44488",
                UserSessionId = DataUtil.GenerateUniqueId()
            };
            await issuedTokenRepository.AddAsync(issuedToken);
            var isDeactived = await issuedTokenRepository.DeactiveRefreshToken("xyzy");
            // Assert
            Assert.True(isDeactived);
        }

        [Fact]
        public async Task Get_Token_By_Refresh_Token_Mongo_Test()
        {
            // Arrange
            var databaseOptions = _context.MongoDatabaseOptions;
            var databaseOptionsMock = Mock.Of<IOptionsMonitor<DatabaseOptions>>(_ => _.CurrentValue == databaseOptions);
            var issuedTokenRepository = new IssuedTokenMongoRepository(new MongoConnection(databaseOptionsMock.CurrentValue));

            // Act
            var issuedToken = new IssuedToken
            {
                Id = DataUtil.GenerateUniqueId(),
                ExpiredJwtToken = DateTime.UtcNow.AddDays(30),
                ExpiredRefreshToken = DateTime.UtcNow.AddDays(30),
                JwtToken = "abcy2",
                RefreshToken = "xyzy1",
                UserId = "5c06a15e4cc9a850bca44488",
                UserSessionId = DataUtil.GenerateUniqueId()
            };
            await issuedTokenRepository.AddAsync(issuedToken);
            var token = await issuedTokenRepository.GetTokenByRefreshToken("xyzy1");
            // Assert
            Assert.True(token == "abcy2");
        }
    }
}
