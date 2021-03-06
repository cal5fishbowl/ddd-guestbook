﻿using DDDGuestbook.Core.Entities;
using DDDGuestbook.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDGuestbook.Core.Services
{
    public class GuestbookService : IGuestbookService
    {
        private IRepository<Guestbook> _guestbookRepository;
        private IMessageSender _messageSender;

        public GuestbookService(IRepository<Guestbook> guestbookRepository, IMessageSender messageSender)
        {
            _guestbookRepository = guestbookRepository;
            _messageSender = messageSender;
        }

        public void RecordEntry(Guestbook guestbook, GuestbookEntry newEntry)
        {
            guestbook.Entries.Add(newEntry);
            _guestbookRepository.Update(guestbook);
        }
    }
}
