using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }

        public Person() { }
        public Person(string login, string passsword, int role, string token)
        {
            Login = login;
            Password = passsword;
            Role = role;
            Token = token;
        }
    }
}
