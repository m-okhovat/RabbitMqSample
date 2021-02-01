using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using System.Threading.Tasks;

namespace Ticketings.Microservice.Controllers
{
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;
        public TicketController(IBus bus)
        {
            _bus = bus;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                return BadRequest();

            ticket.Book();
            var endPoint = await _bus.GetSendEndpoint(Uris.TicketQueue);
            await endPoint.Send(ticket);
            return Ok();
        }
    }
}