using Microsoft.AspNetCore.Mvc;
using DotnetApi.Data.Collections;
using MongoDB.Driver;
using DotnetApi.Models;
using System;

namespace DotnetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        Data.Mongodb _mongoDB;
        IMongoCollection<Infected> _infectedCollections;

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

            return allInfected.Count != 0 ? Ok(allInfected) : StatusCode(404, "Sem infectados cadastrados");
        }

        [HttpPut]
        public IActionResult UpdateInfected([FromBody] InfectedDto pInfectedDto)
        {
            _infectedCollections.UpdateOne(Builders<Infected>.Filter.Where(_=>_.BirthDate == pInfectedDto.BirthDate), Builders<Infected>.Update.Set("gender", pInfectedDto.Gender));

            return Ok("Atualizado com sucesso");
        }

        [HttpDelete]
        public IActionResult DeleteInfected(DateTime pBirthDate)
        {
            _infectedCollections.DeleteOne(Builders<Infected>.Filter.Where(_=>_.BirthDate == pBirthDate));

            return Ok("Deletado com sucesso");
        }
    }
}