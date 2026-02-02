namespace Api.Models
{
    public sealed class CopilotRequest
    {
        public string Prompt { get; set; } = string.Empty;
    }

    public sealed class CopilotResponse
    {
        public string Content { get; set; } = string.Empty;
    }
}