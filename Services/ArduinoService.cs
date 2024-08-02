using System.IO.Ports;
using ArduinoSiriAPI.DTOs;
using ArduinoSiriAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ArduinoSiriAPI.Services;

public class ArduinoService : IArduinoService
{
    private static readonly SerialPort _serialPort;
    private static readonly string ARDUINO_SERIAL_COMM = "/dev/cu.usbmodem14101";
    // this is the port format on MacOS on Windows it should be something like 'COM3'
    private static readonly string UserName = "Eric"; // Can Replace with own
    private static bool IsTurnedOn;

    static ArduinoService()
    {
        try
        {
            _serialPort = new SerialPort(ARDUINO_SERIAL_COMM, 9600);
            _serialPort.Open();
            Console.WriteLine("PORT Connected SUCCESSFULLY!!");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Access to the port is denied: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    
    public async Task<ActionResult<ArduinoDto>> Configure()
    {
        ArduinoDto arduinoDto = new ArduinoDto();
        
        if (_serialPort.IsOpen)
        {
            
            _serialPort.WriteLine(" ");
            Console.WriteLine($"System Connected Successfully!");
            arduinoDto.ReturnMessage = $"Hey {UserName}, I have connected Successfully. What do you want me to do?";
        }
        else
        {
            Console.WriteLine("Internal Server Error! Serial Port is not open!");
            arduinoDto.ReturnMessage = "I have Experienced a Problem Trying to connect to your system.";
            return arduinoDto;
        }
        return arduinoDto;
    }
    
    public async Task<ActionResult<ArduinoDto>> Start()
    {
        ArduinoDto arduinoDto = new ArduinoDto();
        
        if (_serialPort.IsOpen)
        {
            _serialPort.WriteLine("W");
            Console.WriteLine($"Turned On The Lights and Set Color to White.");
            
            arduinoDto.ReturnMessage = !IsTurnedOn ? $"I have turned on the lights, and set the color to White!" +
                                       $" Is there anything else you want me to do?"
                : "I have already turned on your lights. Anything else you want me to do?";

            IsTurnedOn = true;
        }
        else
        {
            Console.WriteLine("Internal Server Error! Serial Port is not open!");
            arduinoDto.ReturnMessage = "I have Experienced a Problem Trying to change the color";
            return arduinoDto;
        }
        return arduinoDto;
    }

    public async Task<ActionResult<ArduinoDto>> SendCommand(ArduinoModel body)
    {
        ArduinoDto arduinoDto = new ArduinoDto();
        arduinoDto.Adapt<ArduinoModel>();
        arduinoDto.Command = body.Command;
        
        string initialColor = arduinoDto.Command;
        string colorCharacter = arduinoDto.Command[0].ToString();
        Console.WriteLine("The Color Received is " + body.Command);
        
        // Send the data to the serial port on Arduino. This should denote the color we want.
        if (_serialPort.IsOpen)
        {
            _serialPort.WriteLine(colorCharacter);
            Console.WriteLine($"Color Char Sent to Arduino is '{colorCharacter}'");
        }
        else
        {
            Console.WriteLine("Internal Server Error! Serial Port is not open!");
            arduinoDto.ReturnMessage = $"Hey {UserName}, I have Experienced a Problem Trying to change the color";
            return arduinoDto;
        }
        
        arduinoDto.ReturnMessage = $"Hey {UserName}, I have set your Lighting Color to {initialColor}. " +
                                   $"Is there anything else you want me to do?";
        return arduinoDto;
    }
}