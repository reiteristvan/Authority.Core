﻿using System.Data.Entity.ModelConfiguration;
using AuthorityIdentity.DomainModel;

namespace AuthorityIdentity.EntityFramework.Configurations
{
    public sealed class AuthorityClaimConfiguration : EntityTypeConfiguration<AuthorityClaim>
    {
        public AuthorityClaimConfiguration()
        {
            ToTable(AuthorityClaim.TableName);

            HasKey(e => e.Id);

            Property(c => c.FriendlyName).IsRequired().HasMaxLength(128);
            Property(c => c.Issuer).IsRequired().HasMaxLength(256);
            Property(c => c.Type).IsRequired().HasMaxLength(256);
            Property(c => c.Value).IsRequired().HasMaxLength(512);

            HasMany(c => c.Policies);
        }
    }
}
