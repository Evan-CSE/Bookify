using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings
{
    public class PricingService
    {
        public PricingDetails CalculatePrice(Apartment apartment, DateRange duration)
        {
            var currency = apartment.Price.Currency;

            var priceForPeriod = new Money(
                apartment.Price.Amount * duration.LengthInDays,
                currency
            );

            decimal amenitiesCharge = 0;

            foreach (var amenity in apartment.Amenities)
            {
                amenitiesCharge += amenity switch
                {
                    Amenity.Spa => 0.05m,
                    Amenity.GardenView => 0.07m,
                    _ => 0
                };
            }
            Money amanitiesUpCharge = Money.Zero(currency);

            if (amenitiesCharge > 0)
            {
                amanitiesUpCharge = new Money(
                    priceForPeriod.Amount * amenitiesCharge,
                    currency
                );
            }
            var totalPrice = Money.Zero(currency);

            totalPrice += priceForPeriod;

            totalPrice += amanitiesUpCharge;

            if (!apartment.CleaningFee.IsZero())
            {
                totalPrice += apartment.CleaningFee;
            }

            return new PricingDetails(priceForPeriod, apartment.CleaningFee, amanitiesUpCharge, totalPrice);
        }
    }
}
