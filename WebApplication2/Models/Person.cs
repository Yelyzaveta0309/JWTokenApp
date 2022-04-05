using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан логин!")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Длина строки должна быть от 7 до 15 символов!")]
        //[Remote(action: "CheckLogin", controller: "AccountController", ErrorMessage = "Login уже используется!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль!")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Длина строки должна быть от 7 до 15 символов!")]
        [RegularExpression(@"/^[a-zA-Z][a-zA-Z0-9-_.]{6,15}$", ErrorMessage = "Некорректный пароль!")]
        public string Password { get; set; }

        public int Role { get; set; }

        [Required(ErrorMessage = "Не указан токен!")]
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
