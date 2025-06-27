using System;
using System.Collections.Generic;

namespace CyberLockChatbot
{
    public class CyberBot
    {
        private string userName;
        private string lastTopic = "";
        private string userInterest = "";
        private Random random = new Random();

        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> {
                "Use at least 12 characters with uppercase, lowercase, numbers and symbols.",
                "Never reuse passwords across accounts.",
                "Avoid using your name or birthday in passwords." }
            },
            { "phishing", new List<string> {
                "Be cautious of emails asking for personal info.",
                "Phishers often pose as banks or popular services.",
                "Don’t click unknown links or attachments." }
            },
            { "scam", new List<string> {
                "Scams often rely on urgency or fake rewards.",
                "Always verify the source before responding.",
                "If it seems too good to be true, it probably is." }
            },
            { "privacy", new List<string> {
                "Check your social media privacy settings.",
                "Use incognito/private mode when browsing sensitive topics.",
                "Avoid oversharing personal info online." }
            }
        };

        public CyberBot(string name)
        {
            userName = name;
        }

        public string RespondTo(string input)
        {
            input = CleanInput(input);

            // Sentiment detection
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("concerned"))
            {
                return "It's okay to feel that way. Scammers can be convincing. Let me help keep you safe.";
            }

            // Remember user interests
            if (input.Contains("interested in"))
            {
                int start = input.IndexOf("interested in") + "interested in".Length;
                userInterest = input.Substring(start).Trim();
                return $"Great! I'll remember that you're interested in {userInterest}.";
            }

            // Recall interest
            if (input.Contains("remind me") && !string.IsNullOrEmpty(userInterest))
            {
                return $"As someone interested in {userInterest}, remember to stay cautious and review your security settings.";
            }

            // Keyword detection
            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    lastTopic = keyword;
                    return keywordResponses[keyword][random.Next(keywordResponses[keyword].Count)];
                }
            }

            // Clarify topic
            if ((input.Contains("more") || input.Contains("explain")) && !string.IsNullOrEmpty(lastTopic))
            {
                return keywordResponses[lastTopic][random.Next(keywordResponses[lastTopic].Count)];
            }

            // Fallback
            return "I didn’t quite understand that. Could you rephrase?";
        }

        private string CleanInput(string input)
        {
            var punctuation = new[] { '.', '?', '!', ',', ';', ':' };
            foreach (var p in punctuation)
                input = input.Replace(p.ToString(), "");
            return input;
        }
    }
}
