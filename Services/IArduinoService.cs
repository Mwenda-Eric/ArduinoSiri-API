using ArduinoSiriAPI.DTOs;
using ArduinoSiriAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArduinoSiriAPI.Services;

public interface IArduinoService
{
    public Task<ActionResult<ArduinoDto>> Start();
    public  Task<ActionResult<ArduinoDto>> SendCommand(ArduinoModel body);
}