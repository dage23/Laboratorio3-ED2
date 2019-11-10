﻿using Laboratorio3_ED2.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio3_ED2.Services
{
    public class PizzaService
    {
        private readonly IMongoCollection<PizzaModel> _pizzas;

        public PizzaService(IPizzasDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pizzas = database.GetCollection<PizzaModel>(settings.PizzasCollectionName);
        }

        public List<PizzaModel> Get() =>
            _pizzas.Find(PizzaModel => true).ToList();

        public PizzaModel Get(string id) =>
            _pizzas.Find<PizzaModel>(PizzaModel => PizzaModel.Id == id).FirstOrDefault();

        public PizzaModel Create(PizzaModel pizza)
        {
            _pizzas.InsertOne(pizza);
            return pizza;
        }

        public void Update(string id, PizzaModel newPizza) =>
            _pizzas.ReplaceOne(PizzaModel => PizzaModel.Id == id, newPizza);

        public void Remove(PizzaModel newPizza) =>
            _pizzas.DeleteOne(PizzaModel => PizzaModel.Id == newPizza.Id);

        public void Remove(string id) =>
            _pizzas.DeleteOne(PizzaModel => PizzaModel.Id == id);
    }
}
