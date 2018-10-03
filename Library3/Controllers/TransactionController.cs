using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library3.Models;
using Library3.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        //this works because the IBookRepository has been added as transient in startup.cs
        public TransactionController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //CRUD operations

        [HttpGet]
        public async Task<IEnumerable<TransactionItem>> Get()
        {
            return await _bookRepository.GetAllTransactions();
        }

        [HttpGet("type/{type}")]
        public async Task<IEnumerable<TransactionItem>> Get(int type)
        {
            return await _bookRepository.GetTransactionsByType(type);
        }

        [HttpGet("{userId}")]
        public async Task<IEnumerable<TransactionItem>> Get(string userId)
        {
            return await _bookRepository.GetTransactionsByUser(userId);
        }

        // POST api/transaction - creates a new transaction
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionParam newTransaction)
        {
            var result = await _bookRepository.AddTransaction(new TransactionItem
            {
                TransactionType = newTransaction.TransactionType,
                UserName = newTransaction.UserName,
                BookName = newTransaction.BookName
            });
            if (result)
            {
                return Ok();
            }else
            {
                return BadRequest("Invalid UserName, transaction type, or BookName");
            }
            
        }
    }

    public class TransactionParam
    {
        public string TransactionType { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string BookName { get; set; } = string.Empty;
    }
}