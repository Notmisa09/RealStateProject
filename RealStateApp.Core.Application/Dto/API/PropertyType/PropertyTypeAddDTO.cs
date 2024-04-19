using Newtonsoft.Json;

namespace RealStateApp.Core.Application.Dto.API.PropertyType;

public class PropertyTypeAddDTO
{
    [JsonIgnore]
    public int Id { get; set; }
    public string PropertyTypeName { get; set; }
    public string Description { get; set; }
}