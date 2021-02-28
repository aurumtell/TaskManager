using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    [Serializable]
    public class User
    {
        public string UserName { get; set; }

        public User(string name)
        {
            UserName = name;
        }

        /// <summary>
        /// Метод для вывода имени пользователя.
        /// </summary>
        /// <returns>Результат в виде строки.</returns>
        public override string ToString()
        {
            return UserName;
        }
    }
}
