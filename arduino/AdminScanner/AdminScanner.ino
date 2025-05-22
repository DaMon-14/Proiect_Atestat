#include <SPI.h>
#include <MFRC522.h>

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
  SPI.begin();
  mfrc522.PCD_Init();
  //Serial.println("Scan a MIFARE 1K Tag to write data...");
}

void loop() {
  serialInfo="";
  // put your main code here, to run repeatedly:
  /* Prepare the ksy for authentication */
  /* All keys are set to FFFFFFFFFFFFh at chip delivery from the factory */
  for (byte i = 0; i < 6; i++)
  {
    key.keyByte[i] = 0xFF;
  }

  Serial.println("Input card id");
  while(serialInfo == ""){
    serialInfo= Serial.readString();// read the incoming data as string
  }
  Serial.println(serialInfo);
  serialInfo.getBytes(idBlockData, 16);
  Serial.println("Scan card!");

  /* Look for new cards */
  /* Reset the loop if no new card is present on RC522 Reader */
  while ( ! mfrc522.PICC_IsNewCardPresent() || ! mfrc522.PICC_ReadCardSerial())
  {
    
  }
  Serial.print("\n");
  Serial.println("**Card Detected**");
  WriteDataToBlock(idBlock, idBlockData);

  ReadDataFromBlock(idBlock, readBlockData);
  Serial.print("Data writen: ");
  for (int j=0 ; j<16 ; j++)
   {
     Serial.write(readBlockData[j]);
   }
   mfrc522.PICC_HaltA(); // halt PICC
   mfrc522.PCD_StopCrypto1(); // stop encryption on PCD
}

void WriteDataToBlock(int blockNum, byte blockData[]) 
{
  /* Authenticating the desired data block for write access using Key A */
  status = mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, blockNum, &key, &(mfrc522.uid));
  if (status != MFRC522::STATUS_OK)
  {
    Serial.print("Authentication failed for Write: ");
    Serial.println(mfrc522.GetStatusCodeName(status));
    return;
  }
  else
  {
    Serial.println("Authentication success");
  }

  
  /* Write data to the block */
  status = mfrc522.MIFARE_Write(blockNum, blockData, 16);
  if (status != MFRC522::STATUS_OK)
  {
    Serial.print("Writing to Block failed: ");
    Serial.println(mfrc522.GetStatusCodeName(status));
    return;
  }
  else
  {
    Serial.println("Data was written into Block successfully");
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
