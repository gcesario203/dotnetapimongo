using System;
using DotnetApi.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace DotnetApi.Data
{
    public class Mongodb
    {
        public IMongoDatabase DataBaseContext { get;}

        public Mongodb(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DataBaseContext = client.GetDatabase("dbname");
                MapClasses();
            }
            catch(Exception exception)
            {
                throw new MongoException("Não foi possível conectar ao MongoDB", exception);
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack{new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", conventionPack, t=>true);

            if(!BsonClassMap.IsClassMapRegistered(typeof(Infected)))
            {
                BsonClassMap.RegisterClassMap<Infected>(item=>
                {
                    item.AutoMap();
                    item.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}