using LabWork10.DTOs;
using LabWork10.Model;

namespace LabWork10.Extensions
{
    public static class VisitorExtension
    {
        public static VisitorDto? ToDto(this Visitor visitor)
            => visitor is null ? null : new VisitorDto
            {
                Phone = visitor.Phone,
                TicketsAmount = visitor.Tickets.Count()
            };

        public static IEnumerable<VisitorDto?> ToDtos(this IEnumerable<Visitor> visitors)
            => visitors.Select(v => v.ToDto());
    }
}
