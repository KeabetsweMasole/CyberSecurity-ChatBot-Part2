using System;
using System.Collections.Generic;
using System.IO;

namespace POE
{
    //This class is responsible for managing the memory of the chat history.
    public class MemoryManager
    {
        //This method is used to get the file path for the chat history.
        private string GetFilePath()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string rootPath = baseDir.Replace("bin\\Debug\\", "");
            return Path.Combine(rootPath, "chat_history.txt");
        }

        //This method is used to ensure that the file for chat history exists.
        public void EnsureFileExists()
        {
            string path = GetFilePath();
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

        }//End of EnsureFileExists method.

        //This method is used to save the conversation entries to the chat history file.
        public void SaveConversation(List<string> entries)
        {
            string path = GetFilePath();
            EnsureFileExists();
            File.AppendAllLines(path, entries);
        }

        //This method is used to clear the chat history file.
        public List<string> GetHistory()
        {
            string path = GetFilePath();
            EnsureFileExists();
            return new List<string>(File.ReadAllLines(path));

        }//End of GetHistory.

    }//End of MemoryManager class.

}//End of namespace.
