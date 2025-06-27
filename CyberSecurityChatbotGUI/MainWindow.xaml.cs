using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CyberSecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        //Models
        public class TaskItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Reminder { get; set; }
            public bool IsCompleted { get; set; }
        }

        public class QuizQuestion
        {
            public string Question { get; set; }
            public List<string> Answers { get; set; }
            public int CorrectIndex { get; set; }
            public string Explanation { get; set; }
        }

        // Fields 
        private List<TaskItem> tasks = new List<TaskItem>();
        private List<string> activityLog = new List<string>();
        private int logPage = 0;
        private int logPageSize = 10;

        // Quiz fields
        private List<QuizQuestion> quizQuestions;
        private int quizIndex = 0;
        private int quizScore = 0;

        //  Reminder Timer
        private DispatcherTimer reminderTimer;

        public MainWindow()
        {
            InitializeComponent();
            LoadQuizQuestions();
            ShowQuizQuestion();
            RefreshTaskList();
            RefreshActivityLog();

            // Reminder Timer for Tasks
            reminderTimer = new DispatcherTimer();
            reminderTimer.Interval = TimeSpan.FromSeconds(30);
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        // Chatbot TAB

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string userInput = ChatInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput)) return;
            AddToChat("You: " + userInput);

            // Improved response: context-aware answers
            string botResponse = HandleChatbotInput(userInput);

            AddToChat("Bot: " + botResponse);
            AddToActivityLog($"Chat: \"{userInput}\" → {botResponse}");

            ChatInput.Clear();
        }

        private void AddToChat(string text)
        {
            TextBlock tb = new TextBlock() { Text = text, TextWrapping = TextWrapping.Wrap };
            ChatHistoryPanel.Children.Add(tb);
        }

        //  Chatbot Logic 
        private string HandleChatbotInput(string input)
        {
            input = input.ToLower();

            // Specific patterns first
            if (input.Contains("how do i create a strong password") || input.Contains("strong password example"))
                return "A strong password should be at least 12 characters and use a mix of upper- and lowercase letters, numbers, and special symbols. Example: L$7fR!9#qB2x.";

            if (input.Contains("what is phishing") || input.Contains("phishing"))
                return "Phishing is a type of online scam where attackers try to trick you into revealing personal information, often by pretending to be someone you trust. Watch out for suspicious emails or messages that ask for your login details or direct you to fake websites.";

            if (input.Contains("how can i avoid phishing") || input.Contains("avoid phishing"))
                return "To avoid phishing scams: don't click on suspicious links, check the sender's email address, look for poor spelling or grammar, and never share personal info via email.";

            if ((input.Contains("what is a scam") || input.Contains("what is an online scam")) && input.Contains("scam"))
                return "An online scam is any scheme that uses the internet to trick you into giving away personal information, money, or access to your accounts. Common scams include fake emails, social media links, and online shopping fraud.";

            if (input.Contains("how do i recognize a scam") || input.Contains("recognize a scam"))
                return "Recognize scams by being cautious with unsolicited emails, urgent requests for money or info, offers that seem too good to be true, and emails with odd spelling or sender addresses.";

            if (input.Contains("how do i protect my privacy") || input.Contains("protect my privacy"))
                return "Protect your privacy online by using strong, unique passwords, enabling two-factor authentication, checking your social media privacy settings, and avoiding oversharing personal info.";

            if (input.Contains("what is two-factor authentication") || input.Contains("what is 2fa"))
                return "Two-factor authentication (2FA) is an extra layer of security that requires both your password and a second code (often sent to your phone) to log in. Enable 2FA on all important accounts for better protection.";

            if (input.Contains("public wifi") || (input.Contains("wifi") && input.Contains("safe")))
                return "Public Wi-Fi networks are not always secure. Avoid accessing sensitive information or logging into important accounts when using public Wi-Fi. Use a VPN if you need to connect to public networks.";

            if (input.Contains("antivirus"))
                return "Antivirus software helps protect your device from viruses and malware. Keep your antivirus up to date and remember to avoid downloading files from untrusted sources.";

            if (input.Contains("social media"))
                return "Be careful on social media: avoid sharing personal details like your home address or vacation plans, and check your privacy settings to control who can see your posts.";

            if (input.Contains("how do i create a password") || input.Contains("how to create a password"))
                return "To create a strong password, use a combination of uppercase and lowercase letters, numbers, and special symbols. Make sure it’s unique for every account you have.";

            if (input.Contains("is it safe to use the same password everywhere") || (input.Contains("same password") && input.Contains("everywhere")))
                return "It’s not safe to use the same password everywhere. If one account gets hacked, all your accounts become vulnerable. Always use unique passwords for each account.";

            if (input.Contains("thank"))
                return "You're welcome! If you have any more questions, just ask. Stay safe online!";

            // Greeting
            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
                return "Hello! How can I help you with cybersecurity today?";

            // Sentiment
            if (input.Contains("worried") || input.Contains("scared"))
                return "It's normal to feel worried about cybersecurity. Staying informed is the best defense! Would you like some safety tips?";
            if (input.Contains("frustrated"))
                return "Cybersecurity can be frustrating, but I’m here to help. What would you like to know more about?";

            // Fallback
            return "I'm not sure I understand. Could you try asking about passwords, scams, phishing, privacy, antivirus, Wi-Fi, or social media?";
        }

        //  TASKS TAB

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitle.Text)) { MessageBox.Show("Enter a title."); return; }
            var reminder = TaskDate.SelectedDate?.ToShortDateString();
            if (!string.IsNullOrWhiteSpace(TaskTime.Text)) reminder += " " + TaskTime.Text;

            tasks.Add(new TaskItem
            {
                Title = TaskTitle.Text,
                Description = TaskDesc.Text,
                Reminder = reminder,
                IsCompleted = false
            });
            AddToActivityLog($"Task Created: {TaskTitle.Text}");
            RefreshTaskList();
            TaskTitle.Clear(); TaskDesc.Clear(); TaskTime.Clear();
        }

        private void RefreshTaskList()
        {
            TaskListView.ItemsSource = null;
            TaskListView.ItemsSource = tasks;
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as TaskItem;
            if (item != null) { item.IsCompleted = true; AddToActivityLog($"Task Completed: {item.Title}"); }
            RefreshTaskList();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as TaskItem;
            if (item != null) { tasks.Remove(item); AddToActivityLog($"Task Deleted: {item.Title}"); }
            RefreshTaskList();
        }

        //  REMINDER TIMER LOGIC 
        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            foreach (var task in tasks)
            {
                if (!task.IsCompleted && !string.IsNullOrWhiteSpace(task.Reminder) &&
                    DateTime.TryParse(task.Reminder.Split(' ')[0], out DateTime remindDate))
                {
                    // Try parse time if given
                    DateTime remindTime = remindDate;
                    if (task.Reminder.Split(' ').Length > 1 &&
                        TimeSpan.TryParse(task.Reminder.Split(' ')[1], out TimeSpan tspan))
                    {
                        remindTime = remindDate.Date + tspan;
                    }

                    if (remindTime <= DateTime.Now && !task.Reminder.Contains("[ALERTED]"))
                    {
                        MessageBox.Show($"Reminder: {task.Title}\n{task.Description}", "Task Reminder");
                        task.Reminder += " [ALERTED]";
                        AddToActivityLog($"Reminder shown for task: {task.Title}");
                        RefreshTaskList();
                    }
                }
            }
        }

        // QUIZ TAB 

        private void LoadQuizQuestions()
        {
            quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion{ Question="True/False: You should use the same password everywhere.", Answers=new List<string>{"True","False"}, CorrectIndex=1, Explanation="Always use unique passwords for each account."},
                new QuizQuestion{ Question="What is phishing?", Answers=new List<string>{"Fishing online","A scam to steal info","Playing games"}, CorrectIndex=1, Explanation="Phishing is a scam to steal your info."},
                new QuizQuestion{ Question="Which is a strong password?", Answers=new List<string>{"password123", "MyDog2023", "L$7fR!9#q"}, CorrectIndex=2, Explanation="Strong passwords use symbols and aren't easy to guess."},
                new QuizQuestion{ Question="True/False: You should click on suspicious links to see what they are.", Answers=new List<string>{"True","False"}, CorrectIndex=1, Explanation="Never click suspicious links."},
                new QuizQuestion{ Question="What should you do if you receive an email from an unknown source?", Answers=new List<string>{"Open the attachment","Ignore/delete it","Send money"}, CorrectIndex=1, Explanation="Ignore or delete unknown emails."},
                new QuizQuestion{ Question="What is two-factor authentication?", Answers=new List<string>{"A second password","Extra login step","Logging in twice"}, CorrectIndex=1, Explanation="It's an extra login step for security."},
                new QuizQuestion{ Question="True/False: Public Wi-Fi is always safe.", Answers=new List<string>{"True","False"}, CorrectIndex=1, Explanation="Public Wi-Fi can be unsafe."},
                new QuizQuestion{ Question="Which of these is a phishing attempt?", Answers=new List<string>{"Bank asking for password","App update notification","Calendar invite"}, CorrectIndex=0, Explanation="Banks never ask for your password."},
                new QuizQuestion{ Question="How often should you update your passwords?", Answers=new List<string>{"Never","Sometimes","Regularly"}, CorrectIndex=2, Explanation="Update passwords regularly."},
                new QuizQuestion{ Question="True/False: Antivirus protects you from all threats.", Answers=new List<string>{"True","False"}, CorrectIndex=1, Explanation="No tool is perfect—stay alert!"}
            };
        }

        private void ShowQuizQuestion()
        {
            if (quizIndex >= quizQuestions.Count)
            {
                QuizQuestionText.Text = $"Quiz complete! Your score: {quizScore}/{quizQuestions.Count}";
                QuizScore.Text = quizScore > 7 ? "Great job on cybersecurity!" : "Keep learning and stay safe!";
                QuizAnswers.Children.Clear();
                QuizNextButton.Visibility = Visibility.Collapsed;
                return;
            }

            var q = quizQuestions[quizIndex];
            QuizQuestionText.Text = $"Q{quizIndex + 1}: {q.Question}";
            QuizFeedback.Text = "";
            QuizScore.Text = $"Score: {quizScore}";
            QuizAnswers.Children.Clear();

            for (int i = 0; i < q.Answers.Count; i++)
            {
                var btn = new Button { Content = q.Answers[i], Tag = i, Margin = new Thickness(0, 0, 0, 5) };
                btn.Click += QuizAnswer_Click;
                QuizAnswers.Children.Add(btn);
            }
            QuizNextButton.Visibility = Visibility.Collapsed;
        }

        private void QuizAnswer_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            int selected = (int)btn.Tag;
            var q = quizQuestions[quizIndex];
            bool correct = selected == q.CorrectIndex;
            if (correct) quizScore++;
            QuizFeedback.Text = correct ? "Correct! " : "Incorrect. ";
            QuizFeedback.Text += q.Explanation;
            AddToActivityLog($"Quiz: \"{q.Question}\" → {q.Answers[selected]} ({(correct ? "Correct" : "Incorrect")})");
            foreach (Button b in QuizAnswers.Children) b.IsEnabled = false;
            QuizNextButton.Visibility = Visibility.Visible;
        }

        private void QuizNextButton_Click(object sender, RoutedEventArgs e)
        {
            quizIndex++;
            ShowQuizQuestion();
        }

        //  ACTIVITY LOG TAB 

        private void AddToActivityLog(string log)
        {
            activityLog.Insert(0, $"{DateTime.Now}: {log}");
            RefreshActivityLog();
        }

        private void RefreshActivityLog()
        {
            ActivityLogList.ItemsSource = null;
            int from = logPage * logPageSize;
            int to = Math.Min(from + logPageSize, activityLog.Count);
            if (from < activityLog.Count)
                ActivityLogList.ItemsSource = activityLog.GetRange(from, to - from);
        }

        private void ShowMoreLog_Click(object sender, RoutedEventArgs e)
        {
            logPage++;
            RefreshActivityLog();
        }
    }
}
