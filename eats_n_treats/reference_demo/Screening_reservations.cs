using System;
using CQRS_Demo.Domain.Events;

namespace CQRS_Demo.Domain.Aggregates
{
    public class Screening_reservations
    {
        private readonly Action<object> _publish;
        private Screening_reservations_state _state;
        public Screening_reservations(Screening_reservations_state state, Action<object> publish)
        {
            _publish = publish;
            _state = state;
        }

        public void reserve(Guid screening, Guid seat, Guid customer)
        {
            if (seat_already_reserved(screening, seat))
                _publish(new Seat_cannot_be_reserved(screening, seat));
            else
                _publish(new Seat_has_been_reserved(screening, seat, customer));
        }

        private bool seat_already_reserved(Guid screening, Guid seat) => _state.Screenings[screening].Contains(seat);
    }
}