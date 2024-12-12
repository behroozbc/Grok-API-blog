using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using OpenAI;


var xAI_key = new ConfigurationBuilder().AddUserSecrets<Program>().Build().GetValue<string>("xAI-key") ?? "";

var client = (new OpenAIClient(new(xAI_key), new() { Endpoint = new("https://api.x.ai/v1") })).GetChatClient("grok-beta");
List<ChatMessage> messages =
[
    new SystemChatMessage(File.ReadAllText("systemIntialMessage.txt")),
    new UserChatMessage(File.ReadAllText("userMessage.txt")),
];

ChatCompletion completion = client.CompleteChat(messages);
File.WriteAllText("grok-response.txt",completion.Content[0].Text);