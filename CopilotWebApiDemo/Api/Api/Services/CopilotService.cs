using System;
using System.Threading;
using System.Threading.Tasks;
using GitHub.Copilot.SDK;

namespace Api.Services
{
    public sealed class CopilotService : ICopilotService, IAsyncDisposable
    {
        private readonly CopilotClient _client;

        public CopilotService()
        {
            // You can pass options here (e.g., explicit CLI path, default model).
            _client = new CopilotClient();
        }

        public async Task<string> CompleteAsync(string prompt, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                return string.Empty;

            // Start the client once (safe to call if already started)
            await _client.StartAsync(ct).ConfigureAwait(false);

            // Create a short-lived session. You can persist/reuse sessions if you want chat-like context.
            var session = await _client.CreateSessionAsync(new()).ConfigureAwait(false);

            try
            {
                var result = await session.SendAndWaitAsync(new()
                {
                    Prompt = prompt
                }).ConfigureAwait(false);
                // The SDK normalizes responses; for simple use we return the text content.
                var text = result?.Data?.Content ?? string.Empty;
                return text;
            }
            finally
            {
                // Dispose the session; keep the client for reuse if you register it as a singleton.
                await session.DisposeAsync().ConfigureAwait(false);
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _client.StopAsync().ConfigureAwait(false);
        }
    }
}