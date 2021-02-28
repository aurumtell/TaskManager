using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    [Serializable]
    public class Story: Epic
    {
        public Story(string status, string name) : base(status, name)
        {
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
