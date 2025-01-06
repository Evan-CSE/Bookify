﻿using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events
{
    public record IBookingCancelledEvent(DateTime utcNow) : IDomainEvent
    {
    }
}