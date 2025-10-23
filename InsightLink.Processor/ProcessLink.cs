using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace InsightLink.Processor
{
    /// <summary>
    /// Processes link click messages from the Azure Storage Queue.
    /// </summary>
    public class ProcessLinkClick
    {
        private readonly ILogger<ProcessLinkClick> _logger;

        public ProcessLinkClick(ILogger<ProcessLinkClick> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This function is triggered whenever a new message is added to the "clicks" queue.
        /// The connection string for the storage account is read from the configuration,
        /// specifically from a setting named "AzureWebJobsStorage".
        /// </summary>
        /// <param name="message">The content of the queue message.</param>
        [Function(nameof(ProcessLinkClick))]
        public void Run([QueueTrigger("clicks", Connection = "AzureWebJobsStorage")] string message)
        {
            // Log the message content received from the queue.
            // In a real-world scenario, you would parse this message (e.g., from JSON)
            // and perform database operations to record the analytics data.
            _logger.LogInformation("C# Queue trigger function processed: {message}", message);
        }
    }
}
