using Microsoft.AspNetCore.Mvc;
using DotnetApi.Data.Collections;
using MongoDB.Driver;
using DotnetApi.Models;

namespace DotnetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        private readonly Data.Mongodb _mongoDB;
        private IMongoCollection<Infected> _infectedCollections;

        public InfectedController(Data.Mongodb mongoContext)
        {
            this._mongoDB = mongoContext;
            this._infectedCollections = _mongoDB.DataBaseContext.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        [HttpPost]
        public IActionResult SaveInfected([FromBody] InfectedDto pInfectedDto)
        {
            var infected = new Infected
            (
                pInfectedDto.BirthDate,
                pInfectedDto.Gender,
                pInfectedDto.Latitude,
                pInfectedDto.Longitude
            );

            _infectedCollections.InsertOne(infected);

            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public IActionResult GetInfected()
        {
            var allInfected = _infectedCollections.Find(Builders<Infected>.Filter.Empty).ToList();
            return Ok(allInfected);
        }
    }
}