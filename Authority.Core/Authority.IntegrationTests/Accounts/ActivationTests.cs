﻿using System;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.IntegrationTests.Common;
using Authority.IntegrationTests.Fixtures;
using Authority.Operations.Account;
using Xunit;

namespace Authority.IntegrationTests.Accounts
{
    public sealed class ActivationTests : IClassFixture<AccountTestFixture>
    {
        private readonly AccountTestFixture _fixture;

        public ActivationTests(AccountTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ActivationShouldSucceed()
        {
            User user = await TestOperations.RegisterUser(_fixture.Context, _fixture.Product.Id);

            UserActivation activation = new UserActivation(_fixture.Context, _fixture.Product.Id, user.PendingRegistrationId);
            await activation.Do();
            await activation.CommitAsync();

            User activated = _fixture.Context.ReloadEntity<User>(user.Id);

            Assert.True(activated.IsPending == false && activated.PendingRegistrationId == Guid.Empty);
        }
    }
}