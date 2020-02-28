using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIApplication.Dtos;

namespace WebAPIApplication.Controllers
{
    [RoutePrefix("api/initialization")]
    public class InitializationController : ApiController
    {
        //---------------------------------------------------------------------

        [HttpGet]
        public IHttpActionResult GetInitializationData()
        {
            var dto = new InitializationDataDto();

            return Ok(dto);
        }

        //---------------------------------------------------------------------
    }
}
