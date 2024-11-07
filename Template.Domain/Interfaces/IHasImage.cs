using Microsoft.AspNetCore.Http;

namespace Template.Domain.Interfaces;

public class IHasImage
{
    public IFormFile Image { get; set; }
}