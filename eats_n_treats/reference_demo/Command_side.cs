using NUnit.Framework;

namespace CQRS_Demo.Tests
{
    // These are domain-driven tests about the command side of your system
    // Ideally only written in Given / When / Then using Events and Commands
    public class Command_side : Test_base
    {
        [Test]
        public void A_free_seat_can_be_reserved()
        {
            Given(
                Screening_has_been_planned(Screening_1(), December_2nd_2020(), Cinema_1()),
                Seat_has_been_reserved(Screening_1(), Seat_A1(), Tina()) );

            When(
                Reserve_seat(Screening_1(), Seat_A2(), Marco()) );

            Then_expect(
                Seat_has_been_reserved(Screening_1(), Seat_A2(), Marco()) );
        }

        
        [Test]
        public void An_already_reserved_seat_cannot_be_reserved()
        {
            Given(
                Screening_has_been_planned(Screening_1(), December_2nd_2020(), Cinema_1()),
                Seat_has_been_reserved(Screening_1(), Seat_A1(), Tina()) );

            When(
                Reserve_seat(Screening_1(), Seat_A1(), Marco()) );

            Then_expect(
                Seat_cannot_be_reserved(Screening_1(), Seat_A1()) );
        }
    }
}