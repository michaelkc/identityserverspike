﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api
{
    [Route("test")]
    public class TestController : ControllerBase
    {
        public IActionResult Get()  
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return new JsonResult(claims);
        }
    }
}
