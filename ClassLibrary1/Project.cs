using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    [Serializable]
    public class Project
    {
        public string ProjectTitle { get; set; }
        public List<Tasks> tasks = new List<Tasks>();
        // Свойство максимального кол-ва задач.
        public int MaxCapacity { get; set; }
        public Project(int max, string name)
        {
            MaxCapacity = max;
            ProjectTitle = name;
        }

        /// <summary>
        /// Метод для вывода списка задач в проекте.
        /// </summary>
        /// <returns>Результат в виде строки.</returns>
        public override string ToString()
        {
            string str = "";
            str += ProjectTitle + "  Tasks: ";
            for (int i = 0; i < tasks.Count; i++)
            {
                str += "№" + i + "." + tasks[i] + "\n";
            }
            return str;
        }
    }
}
