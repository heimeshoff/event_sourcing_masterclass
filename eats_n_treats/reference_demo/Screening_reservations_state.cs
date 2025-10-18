using System;
using System.Collections.Generic;
using CQRS_Demo.Domain.Events;

namespace CQRS_Demo.Domain.Aggregates
{
    // The Aggregate that protects the invariants around reserving seats
    // Ensures transactional consistent and domain-correct behaviour for all involved classes
    // In a real life implementation you wouldn't store this in an Aggregate.cs,
    // but put it in domain specific files and folders.


    public class Screening_reservations_state
    {
        public Dictionary<Guid, List<Guid>> Screenings;

        public Screening_reservations_state(IEnumerable<object> events)
        {
            foreach (dynamic e in events)
            {
                Apply(e);
            }
        }

        private void Apply(object e) {}
        private void Apply(Screening_has_been_planned e) => Screenings = new Dictionary<Guid, List<Guid>> {{e.Screening, new List<Guid>()}};
        private void Apply(Seat_has_been_reserved e) => Screenings[e.Screening].Add(e.Seat);
    }
}
