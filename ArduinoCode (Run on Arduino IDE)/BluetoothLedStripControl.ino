#include <SoftwareSerial.h>
#include <FastLED.h>

#define TOTAL_LEDS 300
#define LED_DATA_PIN 8
#define LED_MODEL WS2812B
#define LED_COLOR_SCHEME GRB

#define MAX_POWER_VOLTS 5
#define MAX_POWER_MILLIAMPS 1000

#define BLUETOOTH_TX 2
#define BLUETOOTH_RX 3

SoftwareSerial bluetooth(BLUETOOTH_TX, BLUETOOTH_RX);

CRGB leds[TOTAL_LEDS];

String inputString = "";

void setup() {
  Serial.begin(9600);  //Start the serial monitor.
  bluetooth.begin(9600);
  digitalWrite(LED_BUILTIN, LOW);

  FastLED.addLeds<LED_MODEL, LED_DATA_PIN, LED_COLOR_SCHEME>(leds, TOTAL_LEDS);
  FastLED.setMaxPowerInVoltsAndMilliamps(MAX_POWER_VOLTS, MAX_POWER_MILLIAMPS);
  FastLED.clear();
  FastLED.show();
}

void loop() {
  if (Serial.available()) {
    char character = Serial.read();
    Serial.println(character);
    switch (character) {
      case 'R':
        setColorToStrip(CRGB::Red);
        break;
      case 'G':
        setColorToStrip(CRGB::Green);
        break;
      case 'B':
        setColorToStrip(CRGB::Blue);
        break;
      case 'W':
        setColorToStrip(CRGB::White);
        break;
      case 'P':
        setColorToStrip(CRGB::Purple);
        break;
      case 'C':
        setColorToStrip(CRGB::Cyan);
        break;
      case 'Y':
        setColorToStrip(CRGB::Yellow);
        break;
    }
    character = ' ';
  }

// Has more colors.
  if (bluetooth.available()) {
    char singleCharacter = bluetooth.read();
    //inputString += singleCharacter;
    Serial.println(singleCharacter);

    switch (singleCharacter) {
      case 'R':
        setColorToStrip(CRGB::Red);
        break;
      case 'G':
        setColorToStrip(CRGB::Green);
        break;
      case 'B':
        setColorToStrip(CRGB::Blue);
        break;
      case 'M':
        setColorToStrip(CRGB::Magenta);
        break;
      case 'W':
        setColorToStrip(CRGB::White);
        break;
      case 'V':
        setColorToStrip(CRGB::Violet);
        break;
      case 'P':
        setColorToStrip(CRGB::Purple);
        break;
      case 'C':
        setColorToStrip(CRGB::Cyan);
        break;
      case 'Y':
        setColorToStrip(CRGB::Yellow);
        break;
    }
    singleCharacter = ' ';
    //inputString = "";
  }
  //setColorThenShow();
}

//Function to set the selected color used as parameter.
void setColorToStrip(CRGB color) {
  for (int i = 0; i < TOTAL_LEDS; i++) {
    leds[i] = color;
  }
  FastLED.show();
}

// These are just different visualizations. Nothing much here.
void setColorFromBluetooth() {
  for (int i = 0; i < TOTAL_LEDS; i++) {
    leds[i] = CRGB::Magenta;
  }
  FastLED.show();
}

void setColorThenShow() {
  for (int i = 0; i < TOTAL_LEDS; i++) {
    leds[i] = CRGB::Magenta;
  }
  FastLED.show();
  delay(1000);
  for (int i = 0; i < TOTAL_LEDS; i++) {
    leds[i] = CRGB::Red;
  }
  FastLED.show();
  delay(1000);
  for (int i = 0; i < TOTAL_LEDS; i++) {
    leds[i] = CRGB::Blue;
  }
  FastLED.show();
  delay(1000);
}

void loopShow() {
  for (int i = 0; i < TOTAL_LEDS; i++) {
    leds[i] = CRGB(255, 0, 0);
    FastLED.show();
  }

  for (int i = TOTAL_LEDS - 1; i >= 0; i--) {
    leds[i] = CRGB(0, 255, 0);
    FastLED.show();
  }

  for (int i = 0; i < TOTAL_LEDS; i++) {
    leds[i] = CRGB(0, 0, 255);
    FastLED.show();
  }

  for (int i = TOTAL_LEDS - 1; i >= 0; i--) {
    leds[i] = CRGB::DarkMagenta;
    FastLED.show();
  }
}
