using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassLibrary1
{
    [Serializable]
    public class Bug: Tasks, IAssignable
    {
        public Bug(string status, string name) :base(status, name)
        {
            
        }

        /// <summary>
        /// Метод для добавления исполнителя в задачу.
        /// </summary>
        /// <param name="user">Исполнитель.</param>
        public void AddUser(User user)
        {
            if (executors.Count != 0) return;
            if (executors.Count >= maximum)
            {
                Console.WriteLine("Вы превысили максимальное кол-во пользователей");
                return;
            }
            executors.Add(user);
        }

        /// <summary>
        /// Свойство максимального кол-ва исполнителей.
        /// </summary>
        public int maximum { get; set; } = 5;

        /// <summary>
        /// Метод для удаления исполнителя в задаче.
        /// </summary>
        /// <param name="user">Исполнитель.</param>
        public void RemoveUser(User user)
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
