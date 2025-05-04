#include <SPI.h>
#include <MFRC522.h>
#include <LiquidCrystal_I2C.h>
#include <WiFi.h>
#include <HTTPClient.h>

const char* ssid = "monosib";
const char* password = "ZeroSugar0000";
const char* UID = "a13fab95-ce9c-4325-896f-5cbc4691aa28";

//Your Domain name with URL path or IP address with path
String serverName = "https://192.168.68.52:7172/ESP32/add";

LiquidCrystal_I2C lcd(0x27, 16, 2); // I2C address 0x27, 16 column and 2 rows

#define SS_PIN 5  /* Slave Select Pin */
#define RST_PIN 32  /* Reset Pin */

/* Create an instance of MFRC522 */
MFRC522 mfrc522(SS_PIN, RST_PIN);
/* Create an instance of MIFARE_Key */
MFRC522::MIFARE_Key key;
MFRC522::StatusCode status;

int idBlock = 1;
byte idBlockData[16];

byte bufferLen = 18;
byte readBlockData[18];

int cardId;
int scannerId = 3;

const int buzzer = 2;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  WiFi.begin(ssid, password);
  Serial.println("Connecting");
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
  lcd.begin();
  SPI.begin();
  mfrc522.PCD_Init();
  Serial.println("Scan a MIFARE 1K Tag to write data...");
  pinMode(buzzer, OUTPUT);
}

void loop() {
  cardId=0;
  noTone(buzzer);
  // put your main code here, to run repeatedly:
  lcd.clear();                 // clear display
  lcd.setCursor(0, 0);         // move cursor to   (0, 0)
  lcd.print("Scan card!");        // print message at (0, 0)
  for (byte i = 0; i < 6; i++)
  {
    key.keyByte[i] = 0xFF;
  }
  while ( ! mfrc522.PICC_IsNewCardPresent() || ! mfrc522.PICC_ReadCardSerial())
  {
    
  }
  ReadDataFromBlock(idBlock, readBlockData);
  Serial.print("Data writen: ");
  for (int j=0 ; j<16 ; j++){
    if(readBlockData[j]!= 0 && readBlockData[j]!= 10){ //null or LineFeed
      //lcd.setCursor(j,1);
      //lcd.write(readBlockData[j]);
      cardId = cardId*10+(readBlockData[j]-48);
    }
     Serial.write(readBlockData[j]);
  }
  Serial.println(cardId);
  mfrc522.PICC_HaltA(); // halt PICC
  mfrc522.PCD_StopCrypto1(); // stop encryption on PCD
  delay(500);
  if(WiFi.status()== WL_CONNECTED){
      HTTPClient http;

      String serverPath = serverName;
      
      // Your Domain name with URL path or IP address with path
      http.begin(serverPath.c_str());
      http.addHeader("Content-Type", "application/json");
      http.addHeader("UID", UID); // Custom header
      
      // If you need Node-RED/server authentication, insert user and password below
      //http.setAuthorization("REPLACE_WITH_SERVER_USERNAME", "REPLACE_WITH_SERVER_PASSWORD");
      String jsonPayload = "{\"cardId\": "+String(cardId) +",\"scannerId\": "+ String(scannerId)+"}";
      Serial.println(jsonPayload);
      // Send HTTP GET request
      int httpResponseCode = http.POST(jsonPayload);
      Serial.print("HTTP Response code: ");
      Serial.println(httpResponseCode);
      String payload = http.getString();
      Serial.println(payload);
      lcd.clear(); 
      lcd.setCursor(0,0);
      
      if(httpResponseCode != 200 && httpResponseCode != 404){
        lcd.setCursor(0,0);
        lcd.print("Server error");
        tone(buzzer, 5000);
      }else{
        if(payload == "" && httpResponseCode==200){
          lcd.setCursor(0,0);
          lcd.print("Succes");
          tone(buzzer, 1000,500);
        }else{
          payload = payload.substring(12);
          payload[payload.length()-1]=32;
          payload[payload.length()-2]=32;
          payload.trim();
          Serial.println(payload);
          lcd.setCursor(0,0);
          tone(buzzer, 3000,1500);
          lcd.print(payload);
          for(int i = 16; i<=payload.length()-1;i++){
            delay(500);
            lcd.scrollDisplayLeft();
          }
        }
      }
      
      // Free resources
      http.end();
      delay(2000);
    }
    else {
      lcd.clear(); 
      lcd.setCursor(0,0);
      lcd.print("WiFi");
      lcd.setCursor(0,1);
      lcd.print("Disconnected");
      Serial.println("WiFi Disconnected");
      while(true){}
    }  
    
}


void ReadDataFromBlock(int blockNum, byte readBlockData[]) 
{
  /* Authenticating the desired data block for Read access using Key A */
  byte status = mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, blockNum, &key, &(mfrc522.uid));

  if (status != MFRC522::STATUS_OK)
  {
     Serial.print("Authentication failed for Read: ");
     Serial.println(status);
     return;
  }
  else
  {
    Serial.println("Authentication success");
  }

  /* Reading data from the Block */
  status = mfrc522.MIFARE_Read(blockNum, readBlockData, &bufferLen);
  if (status != MFRC522::STATUS_OK)
  {
    Serial.print("Reading failed: ");
    Serial.println(status);
    return;
  }
  else
  {
    Serial.println("Block was read successfully");  
  }
  
}
