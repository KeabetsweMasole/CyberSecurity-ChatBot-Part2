using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace POE
{
    //This class is responsible for handling user questions and providing appropriate responses.
    public class QuestionHandler
    {
        private List<string> replies = new List<string>();
        private List<string> ignore = new List<string>();
        private MemoryManager memoryManager = new MemoryManager();
        private Dictionary<string, string> sentimentResponses = new Dictionary<string, string>()
        {
            {"worried", "I see you're worried. Let's talk about it."},
            {"concerned", "I understand your concern. Cybersecurity is important."},
            {"help", "I can help you with that. Cybersecurity is crucial."},
            {"feelings", "I understand your feelings. Cybersecurity is a big deal."},
            {"scared", "I understand that you're scared. Let's work through it together."},
            {"nervous", "It's normal to feel nervous. I'm here to help."},
            {"anxious", "I see you're anxious. Let's address your concerns."},
            {"overwhelmed", "I understand that you're overwhelmed. Let's break it down."},
            {"stressed", "I can tell that you're stressed. Let me help ease that step by step."},
            {"unsafe", "I understand that you feel unsafe. Let's discuss how to improve your security."}
        };

        //This constructor initializes the QuestionHandler class and stores the replies and ignored words.
        public QuestionHandler()
        {
            StoreIgnore();
            StoreReplies();
        }

        //This method handles user questions and provides appropriate responses.
        public void HandleQuestions(string userName)
        {
            Random rand = new Random();

            //Ensures the memory manager file exists.
            while (true)
            {
                Console.WriteLine("CyberSec. Bot: Enter your question (or 'exit' to go back to the main menu):");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(userName + " -> ");
                string question = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Cyan;

                //If the user input is empty or whitespace, prompt for a valid question.
                if (string.IsNullOrWhiteSpace(question))
                {
                    Console.WriteLine("Chat AI -> Please enter a valid question.");
                    continue;
                }

                //If the user types 'exit', break out of the loop and return to the main menu.
                if (question.Trim().ToLower() == "exit")
                {
                    Console.WriteLine("Goodbye! " + userName);
                    return;
                }

                //If the user types 'clear', clear the chat history.
                string[] words = question.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                List<string> filteredWords = words
                    .Select(word => word.ToLower())
                    .Where(word => !ignore.Contains(word))
                    .ToList();

                //If the user types 'history' it will show the chat history.
                HashSet<string> matchedReplies = new HashSet<string>();
                List<string> historyEntries = new List<string>();

                //Checks for matching replies.
                foreach (string reply in replies)
                {
                    foreach (string word in filteredWords)
                    {
                        if (reply.StartsWith(word + ":", StringComparison.OrdinalIgnoreCase))
                        {
                            matchedReplies.Add(reply);
                        }
                    }
                }

                //Checks for sentiment responses.
                foreach (string word in filteredWords)
                {
                    if (sentimentResponses.ContainsKey(word))
                    {
                        Console.WriteLine("Chat AI -> " + sentimentResponses[word]);
                        historyEntries.Add("User -> " + question);
                        historyEntries.Add("Chat AI -> " + sentimentResponses[word]);
                    }
                }

                //Show matched replies if found.
                if (matchedReplies.Count > 0)
                {
                    //Group the matched replies by their first word.
                    var grouped = matchedReplies.GroupBy(r => r.Split(':')[0]);

                    //Select a random reply from each group.
                    foreach (var group in grouped)
                    {
                        string randomReply = group.OrderBy(x => rand.Next()).First();
                        Console.WriteLine("Chat AI -> " + randomReply);
                        historyEntries.Add("User -> " + question);
                        historyEntries.Add("Chat AI -> " + randomReply);
                    }
                }
                else if (!sentimentResponses.Keys.Any(word => filteredWords.Contains(word)))
                {
                    string noMatch = "I'm only allowed to provide information about cybersecurity. Please ask a cybersecurity-related question.";
                    Console.WriteLine("Chat AI -> " + noMatch);
                    historyEntries.Add("User -> " + question);
                    historyEntries.Add("Chat AI -> " + noMatch);
                }

                memoryManager.SaveConversation(historyEntries);

            }//End of while loop.

        }//End of HandleQuestions.

        //This method stores the replies for the chatbot.
        private void StoreReplies()
        {
            //Stores the replies for the chatbot.
            replies = new List<string>
            {
                //Additional phishing responses.
                "phishing: Be cautious of emails or messages asking for personal details; verify the sender.",
                "phishing: Don’t click on suspicious links or attachments in unsolicited emails.",
                "phishing: Hover over links to verify their destination before clicking.",
                "phishing: Check for grammatical errors and unusual requests in emails.",
                "phishing: Always verify the sender’s email address, even if it looks familiar.",

                //Additional cybersecurity responses.
                "cybersecurity: Cybersecurity is the practice of protecting systems and data from digital attacks.",
                "cybersecurity: Cybersecurity involves defending computers, servers, and data from malicious attacks.",
                "cybersecurity: Stay updated with software patches to protect against known vulnerabilities.",
                "cybersecurity: Being aware and informed is the first step in strong cybersecurity practices.",
                "cybersecurity: Regularly audit your digital security to stay protected from evolving threats.",

                //Additional password responses.
                "password: Always use strong passwords with a mix of letters, numbers, and symbols.",
                "password: Use complex passwords with at least 12 characters, including symbols and numbers.",
                "password: Avoid using the same password for multiple accounts.",
                "password: Change your passwords regularly and avoid dictionary words.",
                "password: Consider using a password manager to generate and store secure passwords.",

                //Other additional responses.
                "malware: Avoid downloading files from unknown sources to prevent malware infections.",
                "firewall: Firewalls help block unauthorized access to your network.",
                "vpn: A VPN encrypts your internet traffic, keeping your online activity private.",
                "encryption: Encryption helps protect your sensitive data from unauthorized access.",
                "2fa: Two-Factor Authentication adds an extra layer of security to your accounts.",
                "ransomware: Never open suspicious attachments, and always back up your important files.",
                "antivirus: Keep your antivirus software updated to protect against threats.",
                "social engineering: Cybercriminals use manipulation to steal confidential information, stay alert.",
                "hacking: Ethical hackers help organizations secure their systems, but illegal hacking is a crime.",
                "data breach: A data breach is a security incident where sensitive information is exposed."
            };

        }//End of StoreReplies.

        private void StoreIgnore()
        {
            ignore.AddRange(new List<string>
            {
                "is", "the", "a", "an", "what", "how", "there", "why", "when", "where", "does", "do", "tell",
                "are", "can", "could", "should", "would", "i", "me", "my", "you", "your", "we", "our",
                "it", "to", "this", "that", "these", "those", "has", "have", "had", "was", "were",
                "and", "or", "but", "so", "on", "at", "with", "by", "from", "in", "of", "for", "about"
            });

        }//End of StoreIgnore.

    }//End of QuestionHandler class.

}//End of namespace.
