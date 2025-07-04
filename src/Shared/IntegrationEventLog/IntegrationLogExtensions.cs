﻿namespace IntegrationEventLog;

public static class IntegrationLogExtensions
{
    public static void UseIntegrationEventLogs(this ModelBuilder builder)
    {
        builder.Entity<IntegrationEventLogEntry>(builder =>
        {
            builder.ToTable("IntegrationEventLog");

            builder.HasKey(e => e.EventId);
        });

        builder.Entity<EventLock>(builder =>
        {
            builder.ToTable("EventLock");

            builder.HasKey(e => e.EventId);
            builder.Property(e => e.LockedAt).IsRequired();
        });
    }
}
