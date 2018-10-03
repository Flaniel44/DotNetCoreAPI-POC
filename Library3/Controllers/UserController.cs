using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public UserController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


    }
}