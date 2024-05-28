using DataAggregator.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DataAggregator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(ICustomerService customerService, ILogger<NotificationController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost("notify-quiet-customers")]
        public async Task<IActionResult> NotifyQuietCustomers()
        {
            try
            {
                _logger.LogInformation("Starting notification process for quiet customers.");

                await _customerService.NotifyQuietCustomersAsync();

                _logger.LogInformation("Notification process for quiet customers completed successfully.");
                return Ok(new { message = "Notifications sent to quiet customers." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing notifications for quiet customers.");
                return StatusCode(500, new { error = "An error occurred while processing notifications. Please try again later." });
            }
        }
    }
}
