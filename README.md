# st10367702_Poe_Prog6211
The goal of the C# console-based chatbot Cyber Lock is to educate South Africans about cybersecurity.  Through an interactive experience, the chatbot allows users to ask concerns about scams, password security, phishing, and basic online privacy.
 Enhancing the chatbot's intelligence and conversational skills is the main goal of Part 2 of the project.  In addition to responding more organically, the chatbot can now identify emotional tones in human input, retain user preferences, and reply more consistently to follow-up queries.
 This project aims to show consumers useful C# programming principles in a real-world application in addition to educating them.

Program.cs – This is the entry point of the application. It handles user input, displays welcome messages, and calls the chatbot logic.

CyberBot.cs – This class manages the chatbot's thinking. It handles keyword recognition, stores what the user is interested in, detects sentiment, and provides appropriate responses.

Utility.cs – This class manages things like the voice greeting and the ASCII logo displayed when the chatbot starts.

greeting.wav – A recorded audio file that plays when the chatbot launches, adding a personal touch.

How to Use the Chatbot
Open the solution in Visual Studio 2022.
Ensure the greeting.wav file is in the project and:
Set its Build Action to "Content".
Set "Copy to Output Directory" to "Copy if newer".
Run the application by pressing Ctrl + F5.
Enter your name when prompted.
Type questions or statements into the console window.
Type exit or bye when you are finished to close the application


Questions You Can Ask
Here are some example questions and commands you can try:
What is phishing?
Give me an example of a strong password.
I’m interested in privacy.
Tell me more about scams.
I’m worried about my online safety
Remind me what I was interested in.
What can I ask you about?
You can ask your questions casually. The chatbot is designed to handle punctuation and variation in phrasing.


Features Added in Part Two
 Keyword recognition: To offer targeted recommendations, the bot looks for terms like "password," "phishing," "scam," or "privacy."
 Randomized responses: The bot chooses answers at random from a list of suggestions to make dialogues seem more organic.
 Sentiment detection: The chatbot modifies its tone in response to human expressions of anxiety, fear, or uncertainty.
 Memory and recall: The bot retains information and can bring it up later in the chat if a user expresses an interest.
 Follow-up responses, such "tell me more" or "remind me," allow the bot to carry on the discussion.
 Support for natural language  To comprehend the input, flawless grammar and punctuation are not necessary.

This application is a user-friendly Cybersecurity Awareness Chatbot, built as the final part of my PROG6211 project. 
It lets you chat with a helpful bot about online safety, manage your own security-related tasks and reminders, test your cybersecurity knowledge with a fun multiple-choice quiz, and keep track of everything in an activity log—all in one place. 
Just open the project in Visual Studio, run it, and explore each tab: you can ask the bot questions about things like passwords or scams, add or complete tasks (with pop-up reminders), answer quiz questions, and see a full history of your actions.
 It’s designed to make learning about cybersecurity interactive, practical, and simple to use.