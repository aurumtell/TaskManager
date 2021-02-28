using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    [Serializable]
    public class Epic: Tasks, IAssignable
   {
        public List<Tasks> subtasks = new List<Tasks>();
        public Epic(string status, string name) :base(status, name)
        {
             
        }

        /// <summary>
        /// Свойство максимального кол-ва исполнителей.
        /// </summary>
        public int maximum { get; set; } = 5;
        /// <summary>
        /// Метод для добавления исполнителя в задачу.
        /// </summary>
        /// <param name="user">Исполнитель.</param>
        public virtual void AddUser(User user)
        {
            if (executors.Count >= maximum)
            {
                Console.WriteLine("Вы превысили максимальное кол-во пользователей");
                return;
            }
            executors.Add(user);
        }

        /// <summary>
        /// Метод для удаления исполнителя в задачу.
        /// </summary>
        /// <param name="user">Исполнитель.</param>
        public virtual void RemoveUser(User user)
        {
            executors.Remove(user);
        }

        /// <summary>
        /// Метод для вывода исполнителей.
        /// </summary>
        /// <returns>Возвращает строку с исполнителями.</returns>
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


