using System;
using System.Linq;
using CQRS_Demo.Domain;
using CQRS_Demo.Domain.Commands;
using CQRS_Demo.Domain.Events;
using CQRS_Demo.Domain.Queries;

namespace CQRS_Demo.Tests
{
    public partial class Test_base
    {
        // Factory methods for commands, queries and alike. 
        // Easier to read (since the "new" keyword is omitted, and also decouple tests from the actual implementation.
        protected Reserve_seat Reserve_seat(Guid screening, Guid seat, Guid customer) => new Reserve_seat(screening, seat, customer);
        protected Seat_has_been_reserved Seat_has_been_reserved(Guid screening, Guid seat, Guid customer) => new Seat_has_been_reserved(screening, seat, customer);
        protected Screening_has_been_planned Screening_has_been_planned(Guid screening, DateTime date, Guid cinema) => new Screening_has_been_planned(screening, date, cinema);
        protected Seat_cannot_be_reserved Seat_cannot_be_reserved(Guid screening, Guid seat) => new Seat_cannot_be_reserved(screening, seat);
        protected My_reservations My_reservations(Guid customer) => new My_reservations(customer);
        protected ReservationInfo Reservations(params Reservation[] reservations) => new ReservationInfo(reservations.ToList()); 
        protected Reservation Reservation(Guid screening, Guid seat) => new Reservation(screening, seat);
        
        
        // Any Value that is needed to describe a test, but that has no respective ValueType in the domain.
        // Keeping your test clean and easier to write through exploration with intellisense
        protected DateTime December_2nd_2020() => new DateTime(2020, 12, 02);


        // These are Semantic UUID Identifiers for various things in the domain. 
        // It is easier to reason about "Marco" in a test than about "99A83F47..." ;)
        // Pro-Tip: Of course you could use the value type "CustomerReference" instead of a Guid!
        protected Guid Marco() => Guid.Parse("99A83F47-32D2-4007-A5B8-87C2F1BFD197"); // Reference to the Customer Marco H.
        protected Guid Tina() => Guid.Parse("BC6BF2B8-A198-4877-980C-E454A4E42628"); // Reference to the Customer Tina F.
        protected Guid Cinema_1() => Guid.Parse("EC15243D-58D5-470D-922D-730FAAFB1B36"); // Reference to Cinema 1 in the first floor
        protected Guid Seat_A1() => Guid.Parse("108F0340-C954-44E3-84C2-6CD453351553"); // Reference to the first seat in row A in Cinema 1
        protected Guid Seat_A2() => Guid.Parse("4E5DAC6B-E898-4653-91C6-941204E64BFC"); // Reference to the second seat in row A in Cinema 1
        protected Guid Seat_A3() => Guid.Parse("FF87BBFF-6C84-4DD8-A658-9FCE5D94CF1B"); // Reference to the third seat in row A in Cinema 1
        protected Guid Screening_1() => Guid.Parse("42F573B3-257B-48C3-BC94-7ACC37C5D3F4"); // Reference to the screening of "Avengers End Game" on december the second.}
    }

}