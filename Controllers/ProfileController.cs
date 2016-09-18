using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using auth0documentdb.Extensions;
using auth0documentdb.Models.Db;
using auth0documentdb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth0documentdb.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IDocumentDbService _dbService;
        public ProfileController(IDocumentDbService dbService)
        {
            _dbService = dbService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotificationPreferences()
        {
            var currentUser = User.CurrentId();
            return Json(_dbService.GetNotificationPreferences(currentUser)??new NotificationPreferences(){User=currentUser});
        }

        [HttpPost]
        public async Task<IActionResult> NotificationPreferences([FromBody] NotificationPreferences preferences)
        {
            if(string.IsNullOrEmpty(preferences.Id)){
                preferences.Id = await _dbService.AddNotificationPreferences(preferences);
            }
            else{
                await _dbService.UpdateNotificationPreferences(preferences);
            }
            return Json(preferences);
        }

        [HttpPost]
        public async Task<IActionResult> ContactAddresses([FromBody] ContactAddress address)
        {
            if(string.IsNullOrEmpty(address.Id)){
                var currentUser = User.CurrentId();
                address.User = currentUser;
                address.Id = await _dbService.AddContactAddress(address);
            }
            else{
                await _dbService.UpdateContactAddress(address);
            }
            return Json(address);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveContactAddresses(string id)
        {
            await _dbService.DeleteContactAddress(id);
            return Ok();
        }

        public async Task<IActionResult> ContactAddresses([FromQuery] string continuationToken = "")
        {
            var currentUser = User.CurrentId();
            return Json(await _dbService.GetContactAddresses(currentUser,5,continuationToken));
        }
    }
}
