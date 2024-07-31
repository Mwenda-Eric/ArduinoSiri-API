using ArduinoSiriAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;
using System.Threading.Tasks;
using ArduinoSiriAPI.DTOs;
using ArduinoSiriAPI.Services;

namespace ArduinoSiriAPI.Controllers;

[ApiController]
[Route("api/v1/")]
public class ArduinoController : ControllerBase
{
    private IArduinoService _arduinoService;
    public ArduinoController(IArduinoService arduinoService)
    {
        _arduinoService = arduinoService;
    }
    
    [HttpGet]
    [Route("Start")]
    public async Task<ActionResult<ArduinoDto>> Start()
    {
        var result = await _arduinoService.Start();
        var returnDto = result.Value;
        return Ok(returnDto);
    }
    
    [HttpPost]
    [Route("SendCommand")]
    public async Task<ActionResult<ArduinoDto>> SendCommand(ArduinoModel body)
    {
        var result = await _arduinoService.SendCommand(body);
        var returnDto = result.Value;
        return Ok(returnDto);
    }
}