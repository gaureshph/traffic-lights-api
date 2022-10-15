namespace TrafficLights.Api.Controllers
{
    public class LightDto
    {
        public int Id { get; set; }
        public int JunctionId { get; set; }
        public string Color { get; set; }
        public bool IsOn { get; set; }
    }
}