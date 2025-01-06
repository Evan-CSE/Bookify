using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings
{
    public static class BookingErrors
    {
        public static Error NotConfirmed = new(
            "BookingError.NotConfirmed",
            "Booking not confirmed yet"
        );

        public static Error NotPending = new("BookingError.NotPending", "Booking is not pending");

        public static Error AlreadyStarted = new("BookingError.AlreadyStarted", "Booking already started");
    }
}
