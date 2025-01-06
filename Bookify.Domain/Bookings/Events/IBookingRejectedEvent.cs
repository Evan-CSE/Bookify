﻿using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events
{
    public record IBookingRejectedEvent(DateTime utcNow) : IDomainEvent
    {
    }
}