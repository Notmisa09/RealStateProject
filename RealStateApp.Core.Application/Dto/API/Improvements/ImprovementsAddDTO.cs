using Newtonsoft.Json;

namespace RealStateApp.Core.Application.Dto.API.Improvements;

public class ImprovementsAddDTO
{
    [JsonIgnore]
    public int Id { get; set; }
    public string ImprovementName { get; set; }
    public string Description { get; set; }
}