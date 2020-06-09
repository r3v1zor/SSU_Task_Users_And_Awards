using System;
using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        
        public List<Award> Awards { get; set; }

        public User()
        {
        }

        public User(long id, string name, DateTime dateOfBirth, int age)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
            Awards = new List<Award>();
        }

        public User(string name, DateTime dateOfBirth, int age)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
            Awards = new List<Award>();
        }

        public override string ToString()
        {
            return $"Id = {Id}, Name = {Name}, Date of birth = {DateOfBirth}";
        }
    }
}