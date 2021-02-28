using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    interface IAssignable
    {
        int maximum { get { return 3; }}
        // Метод для добавления исполнителя.
        void AddUser(User user);
        // Метод для удаления исполнителя.
        void RemoveUser(User user);
    }
}
