using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication2.Data;
using System.Text.RegularExpressions;
using System.Linq;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        //private WebApplication2Context _dbContext;
        //public AccountController(WebApplication2Context dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        private PersonRepository _personRepository;
        private UnitofWork unitofWork;
        public AccountController(PersonRepository personRepository)
        {
            _personRepository = personRepository;
            unitofWork = new UnitofWork(_personRepository);
        }


        //public bool CheckLogin(Person person)
        //{
        //    foreach (var item in _dbContext.People)
        //    {
        //        if (person.Login == item.Login)
        //            return false;
        //    }

        //    return true;
        //}

        private IActionResult Create(Person person)
        {
            Person newPerson = new Person(person.Login, person.Password, person.Role, person.Token);
            //Regex rgx = new Regex("/^[a-zA-Z][a-zA-Z0-9-_.]{6,15}$");

            //foreach (var item in _dbContext.People)
            //{
            //    if (person.Login == item.Login)
            //        return StatusCode(400, "Такой логин уже зарегистрирован!");
            //}
            //if (string.IsNullOrEmpty(person.Login) || string.IsNullOrEmpty(person.Password))
            //    return StatusCode(400, "Логин или пароль не введен!");

            //else if (person.Login.Length < 7 || person.Login.Length > 15 || person.Password.Length < 7 || person.Password.Length > 15)
            //    return StatusCode(400, "Длина строки должна быть от 7 до 15 символов!");

            //else if (rgx.IsMatch(person.Login) || rgx.IsMatch(person.Password))
            //    return StatusCode(400, "Логин или пароль введен некорректно!");

            //if (ModelState.IsValid)
            //{
            //    _dbContext.People.Add(newPerson);
            //    _dbContext.SaveChanges();
            //}
            //return BadRequest(new {errorText = ModelState.Keys.Last<string>()});

            if (ModelState.IsValid)
            {
                unitofWork.People.Create(newPerson);
                //unitofWork.Save();
            }
            return BadRequest(new { errorText = ModelState.Keys.Last<string>() });

        }

        [HttpPost("/admin")]

        public IActionResult CreateAdminAccount(string username, string password)
        {
            
            Person newPerson = new Person(username, password, 1, Token(username, "admin"));

            return Create(newPerson);

        }

        [HttpPost("/user")]

        public IActionResult CreateUserAccount(string username, string password)
        {
            Person newPerson = new Person(username, password, 2, Token(username, "user"));

            return Create(newPerson);
        }

        private string Token(string username,string role)
        {

            if(ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
          
                var claims = new List<Claim> { new Claim(username, role)};

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return encodedJwt;
            }
            return BadRequest(new { errorText = ModelState.Keys.Last()}).ToString();
        }
    }
}
