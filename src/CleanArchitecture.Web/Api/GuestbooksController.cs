﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDGuestbook.Core.Entities;
using DDDGuestbook.Core.Interfaces;
using DDDGuestbook.Web.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDGuestbook.Web.Api
{
    [Route("api/[controller]")]
    [VerifyGuestbookExists]
    [ValidateModel]
    public class GuestbooksController : Controller
    {
        private IGuestbookRepository _guestbookRepository;

        public GuestbooksController(IGuestbookRepository guestbookRepository)
        {
            _guestbookRepository = guestbookRepository;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var guestbook = _guestbookRepository.GetById(id);
            return Ok(guestbook);
        }

        [HttpPost("{id}/NewEntry")]
        public IActionResult NewEntry(int id, [FromBody] GuestbookEntry entry)
        {
            var guestbook = _guestbookRepository.GetById(id);
            guestbook.AddEntry(entry);
            _guestbookRepository.Update(guestbook);

            return Ok(guestbook);
        }
    }
}