using System.Collections.Generic;
using System.Linq;
using CQRS_Demo.Domain.Readmodels;
using CQRS_Demo.Infrastucture;
using FluentAssertions;
using NUnit.Framework;

namespace CQRS_Demo.Tests
{
    public partial class Test_base
    {
        private List<object> _history;
        private List<object> _published_events;
        private object _queried_response;
        private Customer_reservations _readmodel; 

        [SetUp]
        public void Setup()
        {
            _history = new List<object>();
            _published_events = new List<object>();
            _queried_response = null;
        }


        protected void Given(params object[] events)
        {
            _history = events.ToList();
            _readmodel = new Customer_reservations(_history);
        }

        protected void When(object command)
        {

            var handler = new Commandhandler(_history, e =>
            {
                _published_events.Add(e);
                _readmodel.Project(e); // This is a simplyfied version of the observer pattern
            });
            handler.handle(command);
        }

        protected void When_Query(object query)
        {
            var handler = new QueryHandler(_readmodel, _history, r => _queried_response = r);
            handler.handle(query);
        }

        protected void Then_expect(params object[] expected_events) => _published_events.Should().Equal(expected_events);
        protected void Then_expect_response(object expected_response) => _queried_response.Should().BeEquivalentTo(expected_response);
    }

}
