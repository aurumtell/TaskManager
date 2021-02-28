using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PeerGrade8
{
    [Serializable]
    class Program
    {
        static List<User> users = new List<User>();
        static List<Project> projects = new List<Project>();

        /// <summary>
        /// Метод для создания нового пользователя.
        /// </summary>
        public static void CreateUser()
        {
            Console.WriteLine("Введите имя пользователя: ");
            users.Add(new User(Console.ReadLine()));
        }

        /// <summary>
        /// Метод для удаления пользователя.
        /// </summary>
        public static void RemoveUser()
        {
            int n;
            SeeUsers();
            Console.WriteLine("Введите номер пользователя, которого вы хотите удалить: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > users.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            users.RemoveAt(n);
        }

        /// <summary>
        /// Метод для просмотра списка пользователей.
        /// </summary>
        public static int SeeUsers()
        {
            Console.WriteLine("Пользователи:");
            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"{i}. {users[i]}");
            return users.Count;
        }

        /// <summary>
        /// Метод для создания проекта.
        /// </summary>
        public static void CreateProject()
        {
            int n;
            Console.WriteLine("Введите максимальное количество задач в проекте: (от 1 до 5)");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > 6)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Введите название для проекта");
            projects.Add(new Project(n, Console.ReadLine()));
        }

        /// <summary>
        /// Метод для просмотра списка проектов.
        /// </summary>
        public static int SeeProjects()
        {
            Console.WriteLine("Проекты:");
            for (int i = 0; i < projects.Count; i++)
                Console.WriteLine($"{i}. {projects[i].ToString()}");
            return projects.Count;
        }

        /// <summary>
        /// Метод для удаления проекта.
        /// </summary>
        public static void DeleteProject()
        {
            int n;
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, которого вы хотите удалить: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            projects.RemoveAt(n);
        }

        /// <summary>
        /// Метод для изменения названия проекта.
        /// </summary>
        public static void RenameProject()
        {
            int n;
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, название которого вы хотите изменить: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Введите новое название:");
            projects[n].ProjectTitle = Console.ReadLine();
        }

        /// <summary>
        /// Метод для создания задачи.
        /// </summary>
        public static void CreateTask()
        {
            int n;
            string stat = "";
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, в который вы хотите добавить задачу: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            if (projects[n].MaxCapacity == projects[n].tasks.Count)
            {
                Console.WriteLine("Вы превысили максимальное кол-во задач в проекте.");
                return;
            }
            Console.WriteLine("Введите название задачи:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите статус задачи('Open task', 'Completed task', 'In process'):");
            stat = Console.ReadLine();
            if (stat != "Open task" && stat != "Completed task" && stat != "In process")
            {
                Console.WriteLine("Вы ввели неверный статус, он будет по умолчанию Open task");
                stat = "Open task";
            }
            Console.WriteLine("Выберите тип задачи(Epic - 0, Bug - 1, Task - 2, Story - 3)");
            // Проверка на тип задачи.
            switch (Console.ReadLine())
            {
                case "0":
                    projects[n].tasks.Add(new Epic(stat, name));
                    break;
                case "1":
                    projects[n].tasks.Add(new Bug(stat, name));
                    break;
                case "2":
                    projects[n].tasks.Add(new Task(stat, name));
                    break;
                case "3":
                    projects[n].tasks.Add(new Story(stat, name));
                    break;
                default:
                    Console.WriteLine("Вы ввели неверный тип");
                    break;
            }
        }

        /// <summary>
        /// Метод для назначения исполнителя задачи.
        /// </summary>
        public static void AppointExecutor()
        {
            int n, m, l;
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, в который вы хотите добавить исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Задачи проекта:");
            if (projects[n].tasks.Count == 0)
            {
                Console.WriteLine("У вас нет задач(");
                return;
            }
            Console.WriteLine(projects[n].ToString());
            Console.WriteLine("Введите номер задачи, в который вы хотите добавить исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out m) || m < 0 || m > projects[n].tasks.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            if (SeeUsers() == 0)
            {
                Console.WriteLine("У вас нет пользователей(");
                return;
            }
            Console.WriteLine("Введите номер пользователя, которого вы хотите добавить в качестве исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out l) || l < 0 || l > users.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            if (projects[n].tasks[m] is Epic)
                (projects[n].tasks[m] as Epic).AddUser(users[l]);
            if (projects[n].tasks[m] is Story)
                (projects[n].tasks[m] as Story).AddUser(users[l]);
            if (projects[n].tasks[m] is Task)
                (projects[n].tasks[m] as Task).AddUser(users[l]);
            if (projects[n].tasks[m] is Bug)
                (projects[n].tasks[m] as Bug).AddUser(users[l]);
        }

        /// <summary>
        /// Метод для изменения исполнителя.
        /// </summary>
        public static void RenameExecutor()
        {
            int n, m, l;
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, в который вы хотите добавить исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Задачи проекта:");
            Console.WriteLine(projects[n].ToString());
            if (projects[n].tasks.Count == 0)
            {
                Console.WriteLine("У вас нет задач(");
                return;
            }
            Console.WriteLine("Введите номер задачи, в которой вы хотите изменить исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out m) || m < 0 || m > projects[n].tasks.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            if (projects[n].tasks[m] is Epic)
                (projects[n].tasks[m] as Epic).ToString();
            if (projects[n].tasks[m] is Story)
                (projects[n].tasks[m] as Story).ToString();
            if (projects[n].tasks[m] is Task)
                (projects[n].tasks[m] as Task).ToString();
            if (projects[n].tasks[m] is Bug)
                (projects[n].tasks[m] as Bug).ToString();
            if (projects[n].tasks[m].executors.Count == 0)
            {
                Console.WriteLine("У вас нет пользователей(");
                return;
            }
            Console.WriteLine("Введите номер пользователя, которого вы хотите изменить в качестве исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out l) || l < 0 || l > projects[n].tasks[m].executors.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            // Удаляем исполнителя.
            if (projects[n].tasks[m] is Epic)
                (projects[n].tasks[m] as Epic).RemoveUser(users[l]);
            if (projects[n].tasks[m] is Story)
                (projects[n].tasks[m] as Story).RemoveUser(users[l]);
            if (projects[n].tasks[m] is Task)
                (projects[n].tasks[m] as Task).RemoveUser(users[l]);
            if (projects[n].tasks[m] is Bug)
                (projects[n].tasks[m] as Bug).RemoveUser(users[l]);
            if (SeeUsers() == 0)
            {
                Console.WriteLine("У вас нет пользователей(");
                return;
            }
            Console.WriteLine("Введите номер пользователя, которого вы хотите добавить в качестве исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out l) || l < 0 || l > users.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            // Добавляем нового исполнителя.
            if (projects[n].tasks[m] is Epic)
                (projects[n].tasks[m] as Epic).AddUser(users[l]);
            if (projects[n].tasks[m] is Story)
                (projects[n].tasks[m] as Story).AddUser(users[l]);
            if (projects[n].tasks[m] is Task)
                (projects[n].tasks[m] as Task).AddUser(users[l]);
            if (projects[n].tasks[m] is Bug)
                (projects[n].tasks[m] as Bug).AddUser(users[l]);
        }

        /// <summary>
        /// Метод для удаления исполнителя.
        /// </summary>
        public static void DeleteExecutor()
        {
            int n, m, l;
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, в котором вы хотите удалить исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Задачи проекта:");
            Console.WriteLine(projects[n].ToString());
            if (projects[n].tasks.Count == 0)
            {
                Console.WriteLine("У вас нет задач(");
                return;
            }
            Console.WriteLine("Введите номер задачи, в котором вы хотите удалить исполнителя: ");
            while (!int.TryParse(Console.ReadLine(), out m) || m < 0 || m > projects[n].tasks.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            if(projects[n].tasks[m] is Epic)
                (projects[n].tasks[m] as Epic).ToString();
            if (projects[n].tasks[m] is Story)
                (projects[n].tasks[m] as Story).ToString();
            if (projects[n].tasks[m] is Task)
                (projects[n].tasks[m] as Task).ToString();
            if (projects[n].tasks[m] is Bug)
                (projects[n].tasks[m] as Bug).ToString();
            if (projects[n].tasks[m].executors.Count == 0)
            {
                Console.WriteLine("У вас нет пользователей(");
                return;
            }
            Console.WriteLine("Введите номер исполнителя, которого вы хотите удалить: ");
            while (!int.TryParse(Console.ReadLine(), out l) || l < 0 || l > projects[n].tasks[m].executors.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            if (projects[n].tasks[m] is Epic)
                (projects[n].tasks[m] as Epic).RemoveUser(users[l]);
            if (projects[n].tasks[m] is Story)
                (projects[n].tasks[m] as Story).RemoveUser(users[l]);
            if (projects[n].tasks[m] is Task)
                (projects[n].tasks[m] as Task).RemoveUser(users[l]);
            if (projects[n].tasks[m] is Bug)
                (projects[n].tasks[m] as Bug).RemoveUser(users[l]);
        }

        /// <summary>
        /// Метод для изменения статуса.
        /// </summary>
        public static void RenameStatus()
        {
            int n, m;
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, в котором вы хотите изменить статус задачи: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Задачи проекта:");
            Console.WriteLine(projects[n].ToString());
            if (projects[n].tasks.Count == 0)
            {
                Console.WriteLine("У вас нет задач(");
                return;
            }
            Console.WriteLine("Введите номер задачи, в котором вы хотите изменить статус задачи: ");
            while (!int.TryParse(Console.ReadLine(), out m) || m < 0 || m > projects[n].tasks.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Введите новый статус задачи('Open task', 'Completed task', 'In process'):");
            string stat = Console.ReadLine();
            if (stat != "Open task" && stat != "Completed task" && stat != "In process")
            {
                Console.WriteLine("Вы ввели неверный статус, он будет по умолчанию Open task");
                stat = "Open task";
            }
            projects[n].tasks[m].Status = stat;
        }

        /// <summary>
        /// Метод для просмотра списка задач.
        /// </summary>
        public static void SeeTasks()
        {
            int i = 0;
            foreach (var project in projects)
                Console.WriteLine($"{i++}. {project.ToString()}");
        }
                
        /// <summary>
        /// Метод для группировки задач по статусу.
        /// </summary>
        public static void GroupTasks()
        {
            int n;
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, в котором вы хотите изменить статус задачи: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Задачи со статусом Open task");
            for (int i = 0; i < projects[n].tasks.Count; i++)
            {
                if (projects[n].tasks[i].Status == "Open task")
                    Console.WriteLine(projects[n].tasks[i].TaskName);
            }
            Console.WriteLine("Задачи со статусом In process");
            for (int i = 0; i < projects[n].tasks.Count; i++)
            {
                if (projects[n].tasks[i].Status == "In process")
                    Console.WriteLine(projects[n].tasks[i].TaskName);
            }
            Console.WriteLine("Задачи со статусом Completed task");
            for (int i = 0; i < projects[n].tasks.Count; i++)
            {
                if (projects[n].tasks[i].Status == "Completed task")
                    Console.WriteLine(projects[n].tasks[i].TaskName);
            }
        }

        /// <summary>
        /// Метод для удаления задачи.
        /// </summary>
        public static void DeleteTask()
        {
            int n, m;
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, в котором вы хотите удалить задачу: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Задачи проекта:");
            Console.WriteLine(projects[n].ToString());
            if (projects[n].tasks.Count == 0)
            {
                Console.WriteLine("У вас нет задач(");
                return;
            }
            Console.WriteLine("Введите номер задачи, которую вы хотите удалить: ");
            while (!int.TryParse(Console.ReadLine(), out m) || m < 0 || m > projects[n].tasks.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            projects[n].tasks.RemoveAt(m);
        }

        /// <summary>
        /// Метод для добавления и удаления подзадачи.
        /// </summary>
        /// <param name="flag"></param>
        public static void AddAndRemoveSubTask(bool flag)
        {
            int n, m, l;
            string stat = "", name = "";
            if (SeeProjects() == 0)
            {
                Console.WriteLine("У вас нет проектов(");
                return;
            }
            Console.WriteLine("Введите номер проекта, в котором вы хотите добавить или удалить подзадачу: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > projects.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            Console.WriteLine("Задачи проекта:");
            Console.WriteLine(projects[n].ToString());
            if (projects[n].tasks.Count == 0)
            {
                Console.WriteLine("У вас нет задач(");
                return;
            }
            Console.WriteLine("Введите номер задачи, в которую вы хотите добавить или удалить подзадачу: ");
            while (!int.TryParse(Console.ReadLine(), out m) || m < 0 || m > projects[n].tasks.Count - 1)
                Console.WriteLine("Попробуйте еще раз, число введено неверно");
            if (flag)
            {
                Console.WriteLine("Введите название подзадачи: ");
                name = Console.ReadLine();
                Console.WriteLine("Введите статус задачи('Open task', 'Completed task', 'In process'):");
                stat = Console.ReadLine();
                if (stat != "Open task" && stat != "Completed task" && stat != "In process")
                {
                    Console.WriteLine("Вы ввели неверный статус, он будет по умолчанию Open task");
                    stat = "Open task";
                }
                Console.WriteLine("Выберите тип задачи(Bug - 1, Story - 2)");
            }
            // Проверка на тип задачи.
            if (projects[n].tasks[m] is Epic)
            {
                if (flag)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            if (projects[n].tasks[m] is Epic)
                            {
                                (projects[n].tasks[m] as Epic).subtasks.Add(new Bug(stat, name));
                            }
                            break;
                        case "2":
                            (projects[n].tasks[m] as Epic).subtasks.Add(new Story(stat, name));
                            break;
                        default:
                            Console.WriteLine("Вы ввели неверный тип");
                            break;
                    }
                }
                else
                {
                    if ((projects[n].tasks[m] as Epic).subtasks.Count == 0)
                    {
                        Console.WriteLine("У вас нет подзадач(");
                        return;
                    }
                    Console.WriteLine("Выберите номер подзадачи, которую хотите удалить");
                    for (int i = 0; i < (projects[n].tasks[m] as Epic).subtasks.Count; i++)
                        Console.WriteLine($"{i} {(projects[n].tasks[m] as Epic).subtasks[i].ToString()}");
                    while (!int.TryParse(Console.ReadLine(), out l) || l < 0 || l > (projects[n].tasks[m] as Epic).subtasks.Count - 1)
                        Console.WriteLine("Попробуйте еще раз, число введено неверно");
                    (projects[n].tasks[m] as Epic).subtasks.RemoveAt(l);
                }
            }
            else
            {
                Console.WriteLine("Задача не типа Epic.");
                return;
            }
        }

        static void Main(string[] args)
        {
            int n;
            // Десериализация.
            BinaryFormatter binary = new BinaryFormatter();
            BinaryFormatter binary2 = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream("data000.dat", FileMode.OpenOrCreate))
                {
                    projects = (List<Project>)binary2.Deserialize(fs);
                }
                using (FileStream fs = new FileStream("data.dat", FileMode.OpenOrCreate))
                {
                    users = (List<User>)binary.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
            Console.WriteLine("Здравствуйте!Вас приветствует приложение 'Управление задачами'.Нажмите Enter.");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Нажмите Enter, если захотите вызвать меню выбора!А если хотите выйти из приложения, нажмите Escape");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Предлагаем вам меню, для выбора действий:");
                    Console.WriteLine("1. Создание пользователя");
                    Console.WriteLine("2. Просмотр списка пользователей");
                    Console.WriteLine("3. Удаление пользователя");
                    Console.WriteLine("4. Создание проекта");
                    Console.WriteLine("5. Просмотр списка проектов");
                    Console.WriteLine("6. Изменение названия проекта");
                    Console.WriteLine("7. Удаление проекта");
                    Console.WriteLine("8. Добавление новой задачи в проект");
                    Console.WriteLine("9. Назначение исполнителей из списка пользователей для выполнения задачи");
                    Console.WriteLine("10. Изменение исполнителей задачи");
                    Console.WriteLine("11. Удаление исполнителей из задачи");
                    Console.WriteLine("12. Изменение статуса задачи");
                    Console.WriteLine("13. Просмотр списка задач");
                    Console.WriteLine("14. Группировка задач по статусу");
                    Console.WriteLine("15. Удаление задач из проекта");
                    Console.WriteLine("16. Назначение подзадач(для задач Epic)");
                    Console.WriteLine("17. Удаление подзадач(для задач Epic)");
                    Console.WriteLine("Введите выбранный номер:");
                    while (!int.TryParse(Console.ReadLine(), out n) || n < 1 || n > 18)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Попробуйте еще раз, число введено неверно");
                    }
                    Console.ResetColor();
                    switch (n)
                    {
                        case 1:
                            CreateUser();
                            break;
                        case 2:
                            SeeUsers();
                            break;
                        case 3:
                            RemoveUser();
                            break;
                        case 4:
                            CreateProject();
                            break;
                        case 5:
                            SeeProjects();
                            break;
                        case 6:
                            RenameProject();
                            break;
                        case 7:
                            DeleteProject();
                            break;
                        case 8:
                            CreateTask();
                            break;
                        case 9:
                            AppointExecutor();
                            break;
                        case 10:
                            RenameExecutor();
                            break;
                        case 11:
                            DeleteExecutor();
                            break;
                        case 12:
                            RenameStatus();
                            break;
                        case 13:
                            SeeTasks();
                            break;
                        case 14:
                            GroupTasks();
                            break;
                        case 15:
                            DeleteTask();
                            break;
                        case 16:
                            AddAndRemoveSubTask(true);
                            break;
                        case 17:
                            AddAndRemoveSubTask(false);
                            break;
                        default:
                            Console.WriteLine("Вы ввели неверное число.");
                            break;
                    }
                }
                // Сериализация.
                try
                {
                    using (FileStream fs = new FileStream("data.dat", FileMode.OpenOrCreate))
                    {
                        binary.Serialize(fs, users);
                    }
                    using (FileStream fs = new FileStream("data000.dat", FileMode.OpenOrCreate))
                    {
                        binary2.Serialize(fs, projects);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Нажмите Enter для продолжения.");
            }
        }
    }
}

