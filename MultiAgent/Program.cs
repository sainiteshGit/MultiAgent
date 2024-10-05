// Import necessary libraries for Semantic Kernel and OpenAI integration
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Plugins; // Import custom plugin (EmailPlugin) for email-related tasks

// Create a Kernel Builder instance
var builder = Kernel.CreateBuilder();

// Add the EmailPlugin to the Kernel, which will help in handling email-related tasks
builder.Plugins.AddFromType<EmailPlugin>();

// Build the Kernel with the configured plugins (including EmailPlugin)
Kernel kernel = builder.Build();


// Create chat completion service
//AddOpenAIChatCompletion
  OpenAIChatCompletionService chatCompletionService = new (
    modelId: "gpt-4",
    apiKey: "xxxx" 
  );



// Define the initial system message for the assistant's behavior
// This message provides instructions on how the assistant should behave in the conversation.
ChatHistory chatMessages = new ChatHistory("""
    You are a friendly assistant who likes to follow the rules. You will complete required steps
    and request approval before taking any consequential actions. If the user doesn't provide
    enough information for you to complete a task, you will keep asking questions until you have
    enough information to complete the task.
    """);

// Start an infinite loop to keep the conversation going
while (true)
{
    // Get user input from the console and add it as a message to the chat history
    System.Console.Write("User > ");
    chatMessages.AddUserMessage(Console.ReadLine()!); // Add user input to the chat

    // Define the execution settings for AI chat completions
    // Here, the AI can automatically invoke kernel functions if needed
    OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
    {
        #pragma warning disable SKEXP0001
        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
    };

    // Call the AI chat completion service to generate responses based on the chat history
    var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
        chatMessages,                          // The conversation history
        executionSettings: openAIPromptExecutionSettings, // How the AI should handle responses
        kernel: kernel                         // The kernel with plugins (for function invocations)
    );

    // Stream and display the AI's response to the user in real-time
    string fullMessage = ""; // Initialize a string to hold the complete AI message
    await foreach (var content in result) // Stream the AI's response as it's generated
    {
        System.Console.Write(content.Content); // Print the AI's response to the console
        fullMessage += content.Content;        // Append the response to the full message
    }
    System.Console.WriteLine(); // Move to the next line in the console after the response

    // Add the AI's full response to the chat history for future context in the conversation
    chatMessages.AddAssistantMessage(fullMessage);
}
