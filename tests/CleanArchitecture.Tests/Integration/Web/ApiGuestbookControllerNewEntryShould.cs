﻿using DDDGuestbook.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace DDDGuestbook.Tests.Integration.Web
{
    public class ApiGuestbookControllerNewEntryShould : BaseWebTest
    {
        [Fact]
        public void ReturnGuestbookWithOneItem()
        {
            int validId = 1;
            string message = Guid.NewGuid().ToString();
            var entryToPost = new { EmailAddress = "test@test.com", Message = message };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(entryToPost), Encoding.UTF8, "application/json");
            var response = _client.PostAsync($"/api/guestbooks/{validId}/NewEntry", jsonContent).Result;
            response.EnsureSuccessStatusCode();
            var stringResponse = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Guestbook>(stringResponse);

            Assert.Equal(validId, result.Id);
            Assert.Contains(result.Entries, e => e.Message == message);

        }

        [Fact]
        public void Return404GivenInvalidId()
        {
            string invalidId = "100";
            var entryToPost = new { EmailAddress = "test@test.com", Message = "test" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(entryToPost), Encoding.UTF8, "application/json");
            var response = _client.PostAsync($"/api/guestbooks/{invalidId}/NewEntry", jsonContent).Result;
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            Assert.Equal(invalidId.ToString(), stringResponse);

        }
    }
}