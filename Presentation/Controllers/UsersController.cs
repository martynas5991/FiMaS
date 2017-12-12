﻿using Data.Domain.Interfeces;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs.UserModel;
using System;
using System.Collections.Generic;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;

        public UsersController (
            IUserRepository userRepository,
            ICountryRepository countryRepository,
            ICityRepository cityRepository)
        {
            _userRepository = userRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public List<GetUserModel> Get()
        {
            var entities = _userRepository.GetAll();
            var getUsersModel = new List<GetUserModel>();

            foreach (var user in entities)
            {
                var userModel = new GetUserModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    Country = user.Country.Name,
                    City = user.City.Name
                };

                getUsersModel.Add(userModel);
            }

            return getUsersModel;
        }

        [HttpGet("{id:guid}")]
        public GetUserModel Get(Guid id)
        {
            var entity = _userRepository.GetById(id);
            var getUserModel = new GetUserModel {
                Name = entity.Name,
                Email = entity.Email,
                Country = entity.Country.Name,
                City = entity.City.Name
            };

            return getUserModel;
        }

        [HttpPost]
        public void Post([FromBody]CreateUserModel user)
        {
            var country = _countryRepository.GetByName(user.Country);
            var city = _cityRepository.GetByName(user.City);

            var entity = Data.Domain.Entities.User.Create(user.Name, user.Email, user.Password, country, city);
            _userRepository.Add(entity);
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]UpdateUserModel user)
        {
            var entity = _userRepository.GetById(id);
            var country = _countryRepository.GetByName(user.Country);
            var city = _cityRepository.GetByName(user.City);

            entity.Update(user.Name, user.Email, user.Password, country, city);
            _userRepository.Edit(entity);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _userRepository.Delete(id);
        }
    }
}