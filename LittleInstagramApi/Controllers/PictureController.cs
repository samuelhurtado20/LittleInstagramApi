using LittleInstagramApi.Interfaces;
using LittleInstagramApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace LittleInstagramApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PictureController : ControllerBase
    {
        private IConfiguration _config;
        private IUserRepository _userRepository;
        private IPictureRepository _pictureRepository;

        public PictureController(IConfiguration config, IUserRepository userRepository, IPictureRepository pictureRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _pictureRepository = pictureRepository;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<PictureEntity> pictures = _pictureRepository.GetList();

                if (pictures != null)
                {
                    return Ok(pictures);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public IActionResult Save(AddPicture addPicture)
        {
            try
            {
                PictureEntity pictureEntity = new PictureEntity
                {
                    CreatedAt = DateTime.Now,
                    ImageBase64 = addPicture.ImageBase64,
                    PictureEntityId = Guid.NewGuid()
                };

                _pictureRepository.Insert(pictureEntity);
                _pictureRepository.Save();

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string pictureId)
        {
            try
            {

                _pictureRepository.Delete(Guid.Parse(pictureId));
                _pictureRepository.Save();

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
