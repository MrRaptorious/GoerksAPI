using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoerksAPI.Models;
using AnanasCore;
using Microsoft.AspNetCore.Authorization;

namespace GoerksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : GoerksBaseController
    {
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Measurement>> GetMeasurements([FromQuery(Name = "from")] string from, [FromQuery(Name = "to")] string to)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            if (!long.TryParse(from, out long dfrom)
            & !long.TryParse(to, out long dto))
                return BadRequest();

            DateTime datefrom = DateTimeFromUnixTimeStamp(dfrom);
            DateTime dateTo = DateTimeFromUnixTimeStamp(dto);

            return user.Measurements?.Where(x => x.Date > datefrom && x.Date < dateTo).ToList();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddMeasurement([FromBody] Measurement requestMeasurement)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            Measurement m = new Measurement(os);
            m.Date = requestMeasurement.Date;
            m.Weight = requestMeasurement.Weight;
            m.Height = requestMeasurement.Height;

            user.Measurements.Add(m);
            os.CommitChanges();
            return Ok();

        }

        [HttpDelete]
        [Authorize]
        public ActionResult<Measurement> DeleteMeasurement([FromQuery(Name = "id")] string id)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            if (id is null || !Guid.TryParse(id, out Guid guid))
                return BadRequest();

            var measurement = user.Measurements?.FirstOrDefault(x => x.ID.Equals(guid));

            if (measurement is null)
                return BadRequest();

            measurement.Delete();

            os.CommitChanges();

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Measurement>> UpdateMeasurements([FromBody] MeasurementRequest measurementRequest)
        {
            var user = GetUserFromToken();

            if (user == null)
                return BadRequest();

            var meas = user.Measurements.FirstOrDefault(x => x.ID == measurementRequest.ID);

            if (meas is null)
                return BadRequest();

            meas.SetPropertyValue(nameof(Measurement.Date), measurementRequest.Date);
            meas.SetPropertyValue(nameof(Measurement.Height), measurementRequest.Height);
            meas.SetPropertyValue(nameof(Measurement.Weight), measurementRequest.Weight);

            os.CommitChanges();

            return Ok();
        }


    }
    public class MeasurementRequest
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
    }
}
