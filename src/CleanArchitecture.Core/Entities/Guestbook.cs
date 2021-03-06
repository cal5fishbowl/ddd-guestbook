﻿using DDDGuestbook.Core.Events;
using DDDGuestbook.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDGuestbook.Core.Entities
{
    public class Guestbook : BaseEntity
    {
        public string Name { get; set; }
        public List<GuestbookEntry> Entries { get; } = new List<GuestbookEntry>();

        public void AddEntry(GuestbookEntry newEntry)
        {
            Entries.Add(newEntry);
            Events.Add(new EntryAddedEvent(this.Id, newEntry));
        }
    }
}
