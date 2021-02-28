using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    [Serializable]
    public class Tasks
    {
        public List<User> executors = new List<User>();
        public string TaskName { get; set; }
        public DateTime CreateData { get; set; }
        public string Status {get;set;}
        public Tasks() { }

        public Tasks(string stat, string name = "")
        {
            TaskName = name;
            CreateData = DateTime.Now;
            Status = stat;
        }

        /// <summary>
        /// Метод для вывода информации о задаче.
        /// </summary>
        /// <returns>Результат в виде строки.</returns>
        public override string ToString()
        {
            return $"Task name: {TaskName}, Status: {Status}, Creation Data: {CreateData}, Type: {this.GetType().ToString().Remove(0, 14)},";
        }
    }
}
