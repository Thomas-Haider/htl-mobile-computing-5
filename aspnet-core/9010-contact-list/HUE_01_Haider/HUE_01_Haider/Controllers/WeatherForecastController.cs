using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HUE_01_Haider.Controllers
{
    [ApiController]
    [Route("contactList")]
    public partial class ToDoController : ControllerBase
    {

        private static readonly List<Contact> contactList = new List<Contact> { new Contact(0, "Thomas", "Haider", "test@test.test") };

        [HttpGet]
        [Route("{name}", Name = "GetSpecificItem")]
        public IActionResult GetItem(string name)
        {
            foreach(Contact contact in contactList)
            {
                if (contact.lastName.Equals(name)){
                    return Ok("successful operation");
                }
            }
            return BadRequest("Invalid or missing name");
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(contactList);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] Contact newItem)
        {
            contactList.Add(newItem);
            return CreatedAtRoute("GetSpecificTodoItem", new { index = contactList.IndexOf(newItem) }, newItem);
        }

        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteItem(int index)
        {
            if (index >= 0 && index < contactList.Count)
            {
                contactList.RemoveAt(index);
                return Ok("Successful operation");
            }

            return BadRequest("Invalid ID supplied");
        }
    }
}
