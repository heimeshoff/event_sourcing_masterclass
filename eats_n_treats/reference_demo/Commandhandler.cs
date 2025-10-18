using System;
using System.Collections.Generic;
using CQRS_Demo.Domain.Aggregates;
using CQRS_Demo.Domain.Commands;

namespace CQRS_Demo.Infrastucture
{
    // A very simple commandhandler that is not yet optimised for real life usage.
    // Use it as a starting point. Depending on your needs you could add:
    //   Real routing, pluggable logging/auth/etc., 
    //   Chain of responsibility pattern through parent class... or the opposite
    //   go all in on functional programming.
    // Whatever you do, the basic pattern of separating infrastructure from domain
    // starts here.
    public class Commandhandler
    {
        private readonly IEnumerable<object> _history;
        private readonly Action<object> _publish;

        public Commandhandler(IEnumerable<object> history, Action<object> publish)
        {
            _history = history;
            _publish = publish;
        }

        public void handle(object command)
        {
            if (command is Reserve_seat cmd)
            {
                var state = new Screening_reservations_state(_history);
                var screening_reservations = new Screening_reservations(state, _publish);
                screening_reservations.reserve(cmd.Screening, cmd.Seat, cmd.Customer);
            }
        }
    }

}
