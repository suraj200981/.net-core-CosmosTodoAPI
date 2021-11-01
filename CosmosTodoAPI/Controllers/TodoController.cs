using CosmosTodoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosTodoAPI.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly IDocumentClient _docmentClient;

        readonly string databaseId;

        readonly string collectionId;
        //IConfiguration gives us access to the json configuration files 
        public IConfiguration _configuration { get; }




        public TodoController(IDocumentClient documentClient, IConfiguration configuration )
        {
            _docmentClient = documentClient;
            _configuration = configuration;
            databaseId = _configuration["DatabaseId"];
            collectionId = "TodosItems";

            BuildCollection().Wait();
        }

        private async Task BuildCollection() {

            await _docmentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId });
            await _docmentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseId), new DocumentCollection {Id = collectionId });

        }

             // GET: api/todos/
             [HttpGet]
            public IQueryable<TodoItem> GetAllTodos()
            {

            return _docmentClient.CreateDocumentQuery<TodoItem>(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), new FeedOptions { MaxItemCount = 3 });

        }

        // GET: TodoController/Details/5
        [HttpGet("{id}")]
        public IQueryable<TodoItem> GetByID(int id)
        {
            return _docmentClient.CreateDocumentQuery<TodoItem>(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), new FeedOptions { MaxItemCount = 1 }).Where(i => i.Id == id);
        }

        // POST: api/todos/
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TodoItem item)
        {
            var response = await _docmentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), item);

            return Ok();

        }
   
        // GET: TodoController/Delete/5
        public ActionResult Delete(int id)
        {
            return null;
        }
    }
}
