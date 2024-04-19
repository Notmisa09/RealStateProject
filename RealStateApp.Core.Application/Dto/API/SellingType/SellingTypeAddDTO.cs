using Newtonsoft.Json;

namespace RealStateApp.Core.Application.Dto.API.SellingType;

public class SellingTypeAddDTO
{
    [JsonIgnore]
    public int Id { get; set; }
    public string SellingTypeName { get; set; }
    public string Description {  get; set; }
}