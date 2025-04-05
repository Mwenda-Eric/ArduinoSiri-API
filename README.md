# ArduinoSiriAPI

A system that connects Siri to an Arduino-controlled LED strip, allowing voice commands to control lighting effects.

![Demo](https://github.com/Mwenda-Eric/ArduinoSiri-API/blob/main/SiriDemo.mp4)

## Overview

ArduinoSiriAPI is a bridge application that enables Siri voice commands to control an addressable LED strip connected to an Arduino. The system architecture consists of:

1. **Siri** - Provides the voice interface for user commands
2. **.NET Core API** - Backend that processes requests and communicates with the Arduino
3. **Arduino** - Controls the physical LED strip based on commands received from the API

With this setup, you can use voice commands like "Hey Siri, turn on the lights" or "Hey Siri, make the lights purple" to control your LED strip.

## Video Tutorial

Check out the complete step-by-step tutorial on YouTube:

[📺 Siri Voice-Controlled Home Lighting: Connect Siri to Arduino LED Strips with a custom .NET Backend](https://youtu.be/9_POcxUy07Q)

## Features

- 🎙️ Voice control of LED lighting using Siri
- 💡 Control LED strip power (on/off)
- 🌈 Change LED strip colors (White, Red, Green, Blue, Purple, Cyan, Yellow, etc.)
- 🔌 Simple serial communication between .NET API and Arduino
- 🔄 RESTful API endpoints for easy integration
- 📱 Compatible with iOS devices through Siri

## Prerequisites

- Arduino board (tested with Arduino Uno)
- Addressable LED strip (WS2812B)
- .NET 6.0 SDK or later
- macOS device with Siri (for Siri integration)
- USB cable for Arduino connection

## Hardware Setup

1. Connect the LED strip to your Arduino:
   - Connect the LED data pin to pin 8 on the Arduino
   - Ensure proper power supply for the LED strip

2. Connect the Arduino to your computer via USB

## Software Installation

### Arduino Setup

1. Install the required libraries:
   - FastLED
   - SoftwareSerial

2. Upload the Arduino code to your board:
   ```arduino
   // Arduino code is provided in the repository
   ```

### .NET API Setup

1. Clone this repository
   ```bash
   git clone https://github.com/yourusername/ArduinoSiriAPI.git
   ```

2. Navigate to the project directory
   ```bash
   cd ArduinoSiriAPI
   ```

3. Update the serial port in `ArduinoService.cs` to match your system:
   - For macOS: `/dev/cu.usbmodem14101` (or similar)
   - For Windows: `COM3` (or similar)

4. Build and run the application
   ```bash
   dotnet build
   dotnet run
   ```

## API Endpoints

| Method | Endpoint                  | Description                            |
|--------|---------------------------|----------------------------------------|
| GET    | `/api/v1/Arduino/Config`  | Initialize connection with Arduino     |
| GET    | `/api/v1/Arduino/Start`   | Turn on LED strip (White color)        |
| POST   | `/api/v1/Arduino/SendCommand` | Send specific color command to Arduino |

### Sample Request for SendCommand

```json
{
  "command": "Purple"
}
```

The first letter of the command is sent to the Arduino to set the corresponding color:
- "R" - Red
- "G" - Green
- "B" - Blue
- "W" - White
- "P" - Purple
- "C" - Cyan
- "Y" - Yellow
- "T" - Turn off

## Siri Integration

To integrate with Siri, you can use the iOS Shortcuts app to create custom commands that trigger API calls. For example:

1. Create a new shortcut in the Shortcuts app
2. Add an action to call your API endpoint
3. Configure the trigger phrase (e.g., "Turn lights blue")
4. Setup the proper URL and HTTP method to target your API

## Architecture

```
┌───────┐     ┌────────────┐     ┌─────────┐     ┌──────────┐
│ Siri  │────▶│ .NET API   │────▶│ Arduino │────▶│ LED Strip│
└───────┘     └────────────┘     └─────────┘     └──────────┘
```

### Components

- **ArduinoController**: Handles HTTP requests and routes them to the service
- **ArduinoService**: Core service that manages communication with the Arduino
- **Arduino Sketch**: Receives commands and controls the LED strip

## Configuration

You can customize the following settings:

- In the .NET API (`ArduinoService.cs`):
  - `ARDUINO_SERIAL_COMM`: Serial port for Arduino connection
  - `UserName`: Personalized name for response messages

- In the Arduino code:
  - `TOTAL_LEDS`: Number of LEDs in your strip
  - `LED_DATA_PIN`: Pin connected to LED data line
  - `MAX_POWER_MILLIAMPS`: Power limit for the LEDs

## Troubleshooting

Common issues and their solutions:

1. **Serial Port Not Found**
   - Ensure Arduino is properly connected
   - Check the port name in `ArduinoService.cs`
   - Verify proper drivers are installed

2. **LED Strip Not Responding**
   - Check power connections
   - Verify data pin connection
   - Ensure `TOTAL_LEDS` matches your strip

3. **API Not Connecting**
   - Check if Arduino is connected before starting the API
   - Ensure no other applications are using the serial port

## License

[MIT License](LICENSE)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Acknowledgements

- FastLED library for Arduino
- .NET Core framework
