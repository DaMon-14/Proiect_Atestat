#include <SPI.h>
#include <MFRC522.h>
#include <LiquidCrystal_I2C.h>

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

String serialInfo;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  lcd.begin();
  SPI.begin();
  mfrc522.PCD_Init();
  Serial.println("Scan a MIFARE 1K Tag to write data...");
}

void loop() {
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
  for (int j=0 ; j<16 ; j++)
   {
     lcd.setCursor(j,1);
     lcd.write(readBlockData[j]);
     Serial.write(readBlockData[j]);
   }
  delay(3000);
  
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
