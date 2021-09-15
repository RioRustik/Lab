using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication10.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите своё имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свою фамилию")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите своё отчество")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите телефон")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свой e-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный e-mail")]
        public string Email { get; set; }
    }
}