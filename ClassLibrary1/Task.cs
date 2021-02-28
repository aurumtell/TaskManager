using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    [Serializable]
    public class Task: Epic
    {
        public Task(string status, string name) :base(status, name)
        {
            
        }

        /// <summary>
        /// Метод для добавления исполнителя в задачу.
        /// </summary>
        /// <param name="user">Исполнитель.</param>
        public override void AddUser(User user)
        {
            if (executors.Count != 0) return;
            executors.Add(user);
        }

        /// <summary>
        /// Метод для удаления исполнителя в задаче.
        /// </summary>
        /// <param name="user">Исполнитель.</param>
        public override void RemoveUser(User user)
        {
            executors.Remove(user);
        }

        /// <summary>
        /// Метод для вывод исполнителей задачи.
        /// </summary>
        /// <returns>Результат в виде строки.</returns>
        public override string ToString()
        {
            string str = " Users: ";
            for (int i = 0; i < executors.Count; i++)
            {
                str += $"{i} " + executors[i] + " ";
            }
            str += "\n";
            return base.ToString() + str;
        }


    }
}
