using System;
using System.Collections.Generic;
using System.IO;
using BLL;
using DAL;

namespace PL
{
    public class AppStarter
    {
        private readonly string _helpString;
        private readonly IUsersBlo _usersBlo;
        private readonly IAwardsBlo _awardsBlo;
        private readonly Dictionary<string, string> _config;

        public AppStarter()
        {
            var isDb = false;
            string connectionString;
            
            _helpString = File.ReadAllText("/home/r3v1zor/RiderProjects/SSU_Task_Users_And_Awards/PL/help.txt");
            using (var reader = new StreamReader(new FileStream("/home/r3v1zor/RiderProjects/SSU_Task_Users_And_Awards/PL/application.properties", FileMode.Open)))
            {
                _config = new Dictionary<string, string>();
                
                while (!reader.EndOfStream)
                {
                    var keyValue = reader.ReadLine()?.Split(':');

                    if (keyValue != null) _config.Add(keyValue[0], keyValue[1]);
                }
            }

            if (_config.ContainsKey("dal"))
            {
                if (_config["dal"].Equals("database"))
                {
                    isDb = true;
                }
            }

            if (_config.ContainsKey("datasource.url"))
            {
                connectionString = _config["datasource.url"];
            }
            else
            {
                throw new Exception("WTF Dude?!");
            }

            _usersBlo = new UsersBlo(isDb ? (IUsersDao) new UsersDao(connectionString) : new InMemoryUsersDal());
            _awardsBlo = new AwardsBlo(isDb ? (IAwardsDao) new AwardsDao(connectionString) : new InMemoryAwardsDal());
        }

        public void Start()
        {
            var condition = true;
            Console.WriteLine(_helpString);

            while (condition)
            {
                Console.WriteLine("Выберите действие");
                var req = Console.ReadLine();

                switch (req)
                {
                    case "1":
                        Console.WriteLine("Введите имя пользователя");
                        var userName = Console.ReadLine();
                        
                        Console.WriteLine("Введите даты рождения в формате (год-месяц-день)");
                        var date = Console.ReadLine();
                        DateTime.TryParse(date, out var dateOfBirth);

                        _usersBlo.Save(userName, dateOfBirth);
                        Console.WriteLine("Пользователь успешно сохранен");
                        break;
                    case "2":
                        Console.WriteLine("Введите id пользователя, которого вы хотите удалить");
                        long.TryParse(Console.ReadLine(), out var userId);

                        _usersBlo.Delete(userId);
                        Console.WriteLine("Пользователь успешно удален");
                        break;
                    case "3":
                        Console.WriteLine("Введите заголовок награды");
                        var title = Console.ReadLine();

                        _awardsBlo.Save(title);
                        Console.WriteLine("Награда была добавлена");
                        break;
                    case "4":
                        Console.WriteLine("Введите id награды");
                        long.TryParse(Console.ReadLine(), out var awardId);

                        _awardsBlo.Delete(awardId);
                        Console.WriteLine("Награда была удалена");
                        break;
                    case "5":
                        Console.WriteLine("ВВедите id пользователя:");
                        long.TryParse(Console.ReadLine(), out var id);

                        Console.WriteLine("Введите id награды");
                        long.TryParse(Console.ReadLine(), out var awId);

                        var award = _awardsBlo.FindById(awId);
                        _usersBlo.AddAward(id, award);
                        Console.WriteLine("Награда была добавлена пользователю");
                        break;
                    case "6":
                        Console.WriteLine("ВВедите id пользователя:");
                        long.TryParse(Console.ReadLine(), out var uId);

                        Console.WriteLine("Введите id награды");
                        long.TryParse(Console.ReadLine(), out var aId);

                        _usersBlo.DeleteAward(uId, aId);
                        Console.WriteLine("Награда была удалена");
                        break;
                    case "7":
                        Console.WriteLine("Награды:");
                        _awardsBlo.FindAll().ForEach(a => Console.WriteLine(a.ToString()));
                        break;
                    case "8":
                        Console.WriteLine("Пользователи");
                        _usersBlo.FindAll().ForEach(user => Console.WriteLine(user.ToString()));
                        break;
                    case "9":
                        Console.WriteLine("Введите id пользователя");
                        long.TryParse(Console.ReadLine(), out userId);

                        _usersBlo.FindById(userId).Awards.ForEach(a => Console.WriteLine(a.ToString()));
                        break;
                    case "q":
                        condition = false;
                        break;
                }
            }
        }
    }
}