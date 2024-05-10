﻿using Medicine.Models;
using Medicine.Repository;
using Medicine.Repozitorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        RepoContact RepoContact;

        IContact _contact1;
        public ContactController(IContact contact1)
        {
            _contact1 = contact1;
        }
        [HttpPost]
        public IActionResult Add(Contact newCont)
        {
            if (newCont == null)
            {
                return BadRequest("The request body must contain data for creating a contact.");
            }

            if (string.IsNullOrEmpty(newCont.Name)) 
            {
                return BadRequest("The 'Name' field is required.");
            }

            try
            {
                _contact1.Insert(newCont);
                _contact1.Save();
                return Ok("Contact created successfully.");
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while creating the contact. Please try again later.");
            }
        }


    }


}

