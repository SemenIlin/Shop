﻿using System.IO;
using RecordInJsonFile;
using Exceptions;
using Shop.BLL.Models;

namespace Shop.PL.Models
{
    public class SignInModel
    {
        public SignInModel(string login, string password)
        {
            if (Directory.Exists(login))
            {
                ValidationUser validation = new ValidationUser(login, password, DirectoryForJson<UserDTO>.ReadJson(login + "\\" + login + ".json"));
                validation.CheckExistUser();

                Login = login;
                Password = password;
            }
            else
            {
                throw new ValidationException("Такого пользователя не существует.", "");            
            }            
        }

        public string Login { get; set; }
        public string Password { get; set; }

        
    }
}
