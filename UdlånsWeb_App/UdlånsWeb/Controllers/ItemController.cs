using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdlånsWeb.DataHandling;
using UdlånsWeb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UdlånsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        ConvertItemData data = new ConvertItemData();
        ItemViewModel model = new ItemViewModel();
        public ItemController()
        {
            model.Items = data.GetItems().Items;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public ItemViewModel Get()
        {
            return model;
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return model.Items.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //Remake
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //Remake
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Remake
        }
    }
}
