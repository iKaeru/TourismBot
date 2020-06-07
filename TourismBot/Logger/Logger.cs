using System;
using System.IO;
using TourismBot.Models;

namespace TourismBot.Logger
{
    public class Logger
    {
        public Action<LoggingInfo> OutputWriter { get; }

        private readonly string FileAddressPath =
            string.Format("..{0}..{0}..{0}..{0}Logs.txt", Path.DirectorySeparatorChar);

        public Logger()
        {
            OutputWriter = WriteToFile;
        }

        private void WriteToFile(LoggingInfo info)
        {
            using (StreamWriter file = File.AppendText(FileAddressPath))
            {
                file.WriteLine($"Received a text message in chat {info.ChatId}.");
                file.WriteLine($"Received message from {info.FirstName} {info.LastName}.");
                file.WriteLine($"Received message at {info.Date}.");
                file.WriteLine($"Sender user name @{info.UserName}.");
                file.WriteLine($"Input was: \"{info.Question}\".");
                file.WriteLine($"Output was: \"{info.Answer}\".");
                file.WriteLine("");
            }
        }
    }
}