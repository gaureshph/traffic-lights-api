using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace TrafficLights.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JunctionsController : ControllerBase
    {

        private static List<JunctionDto> Junctions = new List<JunctionDto>
        {
            new JunctionDto
            {
                Id = 1,
                Name="Ribandar"
            },
            new JunctionDto
            {
                Id = 2,
                Name="Panaji"
            }
        };

        private static List<LightDto> Lights = new List<LightDto>
        {
                    new LightDto{ Id = 1, Color = "Green", IsOn = true, JunctionId = 1 },
                    new LightDto{ Id = 2, Color = "Red", IsOn = true, JunctionId = 1 },
                    new LightDto{ Id = 3, Color = "Red", IsOn = true, JunctionId = 1 },
                    new LightDto{ Id = 4, Color = "Red", IsOn = true, JunctionId = 1 },
                    new LightDto{ Id = 5, Color = "Green", IsOn = true, JunctionId = 2 },
                    new LightDto{ Id = 6, Color = "Red", IsOn = true, JunctionId = 2 },
                    new LightDto{ Id = 7, Color = "Red", IsOn = true, JunctionId = 2 },
                    new LightDto{ Id = 8, Color = "Red", IsOn = true, JunctionId = 2 }
        };

        private readonly ILogger<JunctionsController> _logger;

        public JunctionsController(ILogger<JunctionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<JunctionDto>> GetAllJunctions()
        {
            return Ok(Junctions);
        }

        [HttpGet("{junctionId}", Name = "GetJunctionById")]
        public ActionResult<List<JunctionDto>> GetJunctionById(int junctionId)
        {
            var junction = Junctions.First(junction => junction.Id == junctionId);
            
            if (junction == null)
                return NotFound();

            return Ok(junction);
        }

        [HttpPost]
        public ActionResult CreateJunction([FromBody] JunctionCreateDto junctionCreateDto)
        {
            int newId = Junctions.Max(junction => junction.Id) + 1;
            var newJunction = new JunctionDto { Id = newId, Name = junctionCreateDto.Name };
            Junctions.Add(newJunction);
            return CreatedAtRoute("GetJunctionById", new { junctionId = newId }, newJunction);
        }

        [HttpPut("{junctionId}")]
        public ActionResult EditJunction(int junctionId, [FromBody] JunctionEditDto junctionEditDto)
        {
            Junctions.ForEach(junction =>
            {
                if (junction.Id == junctionId)
                {
                    junction.Name = junctionEditDto.Name;
                }
            });
            return NoContent();
        }

        [HttpDelete("{junctionId}")]
        public ActionResult DeleteJunction(int junctionId)
        {
            Junctions = Junctions.Where(junction => junction.Id != junctionId).ToList();
            return NoContent();
        }

        [HttpPatch("{junctionId}")]
        public ActionResult UpdateJunctionStatus(int junctionId, [FromBody] JsonPatchDocument<JunctionDto> jsonPatchDocument)
        {
            Junctions.ForEach(junction =>
            {
                if (junction.Id == junctionId)
                {
                    jsonPatchDocument.ApplyTo(junction);
                }
            });

            return NoContent();
        }
    }
}