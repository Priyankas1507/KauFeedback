using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace KauFeedback.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QRCodeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetQRCode()
    {
        var url = "http://localhost:5173";

        using var generator = new QRCodeGenerator();
        var data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

        var png = new PngByteQRCode(data);
        var bytes = png.GetGraphic(20);

        return File(bytes, "image/png");
    }
}