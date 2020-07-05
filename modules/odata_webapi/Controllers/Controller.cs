using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace @(Parameters["Namespace"]).Controllers
{
    public class @Parameters["Name"]Controller : ODataController
    {
        [EnableQuery]
        public ActionResult<IQueryable<@Parameters["Type"]>> Get([FromServices] DbModel db)
        {
            return Ok(db.@Parameters["Name"]);
        }
    }
}
