using System;
using System.Media;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Speech.Synthesis; // Added for speech synthesis

namespace Chatbot
{
    class Program
    {
        static string userName = "User"; // Default user name
        static SpeechSynthesizer synth = new SpeechSynthesizer(); // Initialize speech synthesizer

        static void Main(string[] args)
        {
            // 1. Voice Greeting (using SpeechSynthesizer)
            SpeakGreeting("Hello! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.");

            // 3. Text-Based Greeting and User Interaction (Enhanced)
            Console.WriteLine(); // Add an empty line for spacing
            DisplayWelcomeMessage();

            // Ask for the user's name
            Console.Write("Please enter your name: ");
            string inputName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputName))
            {
                userName = inputName;
            }
            Console.WriteLine($"\nHello, {userName}! Welcome to the Cybersecurity Awareness Bot!");
            Console.WriteLine("How can I help you today?");

            // Basic user interaction loop (expanded)
            string userInput;
            do
            {
                Console.Write("> ");
                userInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    // 4. Basic Response System
                    string response = GetBasicResponse(userInput);
                    if (!string.IsNullOrEmpty(response))
                    {
                        TypeText(response); // Use typing effect
                        SpeakResponse(response); // Speak the response
                        Console.WriteLine();
                    }
                    else
                    {
                        // 5. Input Validation
                        TypeText("I didn't quite understand that. Could you rephrase?");
                        SpeakResponse("I didn't quite understand that. Could you rephrase?");
                        Console.WriteLine();
                    }
                }
                else
                {
                    // 5. Input Validation (Empty Input)
                    TypeText("Please enter something.");
                    SpeakResponse("Please enter something.");
                    Console.WriteLine();
                }
            } while (userInput.ToLower() != "exit");

            Console.WriteLine("\nGoodbye, {0}!", userName);
            SpeakGoodbye($"Goodbye, {userName}!");
        }

        // Replaced PlayVoiceGreeting with SpeakGreeting using SpeechSynthesizer
        static void SpeakGreeting(string greeting)
        {
            try
            {
                synth.Speak(greeting);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during speech synthesis: {ex.Message}");
                Console.WriteLine("Make sure your system supports speech synthesis.");
            }
        }

        static void DisplayAsciiLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // Set color for the logo

            Console.WriteLine(@"
  _   _   _   _   _   _   _   _   _   _   _   _   _
 / \ / \ / \ / \ / \ / \ / \ / \ / \ / \ / \ / \ / \
( C | y | b | e | r | s | e | c | u | r | i | t | y )
 \_/ \_/ \_/ \_/ \_/ \_/ \_/ \_/ \_/ \_/ \_/ \_/ \_/
      _       _       _       _       _
     / \     / \     / \     / \     / \
    ( A | w | a | r | e | n | e | s | s )
     \_/     \_/     \_/     \_/     \_/
          _   _   _
         / \ / \ / \
        ( B | o | t )
         \_/ \_/ \_/
");

            Console.ResetColor(); // Reset the console color
        }

        static void DisplayWelcomeMessage()
        {
            Console.WriteLine("--------------------------------------------------");
            DisplayAsciiLogo();
            Console.WriteLine("--------------------------------------------------");
        }

        static string GetBasicResponse(string query)
        {
            string lowerQuery = query.ToLower();
            if (lowerQuery.Contains("how are you"))
            {
                return "I'm doing well, thank you for asking! Ready to help you with your cybersecurity questions.";
            }
            else if (lowerQuery.Contains("what's your purpose") || lowerQuery.Contains("what are you"))
            {
                return "My purpose is to provide information and raise awareness about cybersecurity topics.";
            }
            else if (lowerQuery.Contains("what can i ask you about"))
            {
                return "You can ask me about topics like password safety, phishing, safe browsing, malware, and general cybersecurity best practices.";
            }
            else if (lowerQuery.Contains("password safety"))
            {
                return "For strong password safety, I recommend using a combination of uppercase and lowercase letters, numbers, and symbols. Aim for a password that is at least 12 characters long and avoid using easily guessable information.";
            }
            else if (lowerQuery.Contains("phishing"))
            {
                return "Phishing is a type of online fraud where attackers try to trick you into revealing sensitive information like passwords or credit card details, often through deceptive emails or websites. Be cautious of unsolicited messages and verify the sender's authenticity.";
            }
            else if (lowerQuery.Contains("safe browsing"))
            {
                return "To browse safely, keep your web browser updated, be wary of suspicious links and websites, use a reputable antivirus program, and consider using browser extensions that enhance security.";
            }
            return null; // No specific response found
        }

        static void TypeText(string text, int delay = 0) // Added typing effect
        {
            Console.ForegroundColor = ConsoleColor.Green; // Set response color
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.ResetColor();
        }

        static void SpeakResponse(string text)
        {
            try
            {
                synth.SpeakAsync(text); // Speak asynchronously so the chatbot doesn't freeze
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during speech synthesis: {ex.Message}");
            }
        }

        static void SpeakGoodbye(string goodbyeMessage)
        {
            try
            {
                synth.Speak(goodbyeMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during goodbye speech: {ex.Message}");
            }
        }
    }
}