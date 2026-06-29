using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QRCoder;

namespace KauFeedback.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QrCodeController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public QrCodeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var url = _configuration["AppSettings:FrontendUrl"];

        using var generator = new QRCodeGenerator();
        using var data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

        var qrCode = new PngByteQRCode(data);
        byte[] bytes = qrCode.GetGraphic(20);

        return File(bytes, "image/png");
    }
}