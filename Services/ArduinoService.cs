using System.IO.Ports;
using ArduinoSiriAPI.DTOs;
using ArduinoSiriAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ArduinoSiriAPI.Services;

public class ArduinoService : IArduinoService
{
    private static SerialPort _serialPort;
    private static readonly string ARDUINO_SERIAL_COMM = "/dev/cu.usbmodem14101";
    // this is the port format on MacOS on Windows it should be something like 'COM3'
    private static readonly string UserName = "Eric"; // Can Replace with own

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
    
    public async Task<ActionResult<ArduinoDto>> Start()
    {
        ArduinoDto arduinoDto = new ArduinoDto();
        
        if (_serialPort.IsOpen)
        {
            _serialPort.WriteLine("W");
            Console.WriteLine($"Turned On The Lights and Set Color to White.");
            arduinoDto.ReturnMessage = $"Hey {UserName}, I have turned on the lights, and set the color to White!";
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
        
        arduinoDto.ReturnMessage = $"Hey {UserName}, I have set your Lighting Color to " + initialColor;
        return arduinoDto;
    }
}