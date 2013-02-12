//Chris Hilgenberg
//Used for Labs 6&7
//2/22/12
//LCD.c is used to initialize the LCD and Read/Write Data and commands to it.

#include "defs.h"
#include "protos.h"

#define HOME_DISPLAY 0x02
#define CLEAR_DISPLAY 0x01
#define SECOND_LINE 0xC0

void InitializeLCDIO()  //Initializes the starting state of the I/O for LCD.
{
  DDRK.ddrk=0xff;
  DDRD.ddrd=0xe0;
  PORTE.eport=0x0c;
  DDRE.DDE=0x0c;
  InitializeDisplay();
}

void InitializeDisplay() //Creates blinking cursor after sending code to LCD.
{
  int count = 0;
  unsigned char Initialize[] = {0x30,0x30,0x30,0x38,0x0f,0x01,0x06};
  for(count=0;count<7;++count)
  {
    CheckSwitches();
    Write();
    Command();
    PORTK.portk = Initialize[count];
    Toggle();
    Delay(1000);
    
  }
}

void Toggle()   //Used to toggle enable of LCD PORTD
{
  PORTD.ENABLE=1;
  PORTD.ENABLE=0;
  Delay(1000);
}

void Write()  //Changes LCD to Write Enable
{
  PORTD.RSLINE = 1;
  PORTD.RWLINE = 0;
}
void Command() //Changes LCD to accept Writing Commands
{
  PORTD.RWLINE = 0;
  PORTD.RSLINE=0;
}
void Read()     //Changes LCD to Read Enable
{
  PORTD.RWLINE = 1;
  PORTD.RSLINE = 1;
}
void ReadFromLCD()
{
  PORTD.RSLINE = 0;
  PORTD.RWLINE = 1;
  
}

void Delay(long time) //Delays for time value sent to it
{
  while(time!=0)
  {
    --time;
  }
}
void Data()  //Changes LCD to accept Writing Data to the display
{
  PORTD.RSLINE = 1;
}
void SendData(char* message)  //Accepts a message and writes it to the LCD.
{
  int index = 0;
  while (message[index]!=0x00)
  {
    if(index==0)
    {
     ResetDisplay();
     BusyCheck();
    }
    if(index==16)
    {
      SecondLine();
      BusyCheck();
    }
    CheckSwitches();
    Write();
    Data();
    PORTK.portk = message[index];
    Toggle();
    BusyCheck(); 
    CheckSwitches();
    
    ++index;
  }
}
 void ResetDisplay()   //Resets and clears the LCD 
 {
  BusyCheck();
  Write();
  Command();
  PORTK.portk = CLEAR_DISPLAY;
  
  Toggle();
  PORTK.portk = HOME_DISPLAY;
  
  Toggle();
  BusyCheck();
  CheckSwitches();
 }
 
 void CheckSwitches()   //Checks the DIP switches for color changes for the LCD
 {
   if((PORTB.Bit0==0)&&(PORTB.Bit1==0))
   {
     PORTE.ebits.Bit2=0;
     PORTE.ebits.Bit3=0;
   }
   if((PORTB.Bit0==1)&&(PORTB.Bit1==0))
   {
    PORTE.ebits.Bit2=1;
    PORTE.ebits.Bit3=0;
   }
   if((PORTB.Bit0==0)&&(PORTB.Bit1==1))
   {
    PORTE.ebits.Bit2=0;
    PORTE.ebits.Bit3=1;
   }
   if((PORTB.Bit0==1)&&(PORTB.Bit1==1))
   {
    PORTE.ebits.Bit2=1;
    PORTE.ebits.Bit3=1;
   }
 }
   
 void ReadK()       //Sets PortK to an input
 {
  DDRK.ddrk = 0x00;
 }
 
 void WriteK()     //Sets PortK to an output
 {
  DDRK.ddrk = 0xff;
 }
 
 void ELOW()    //Takes the enable line low
 {
  PORTD.ENABLE=0;
 }
 
 void EHIGH()     //Takes the enable line high
 {
  PORTD.ENABLE=1;
 }
 
 void SecondLine()   //Command to display a new line in the LCD
 {
  BusyCheck();
  Write();
  Command();
  
  PORTK.portk = SECOND_LINE;
  Toggle();
  BusyCheck();
 }
 void BusyCheck()   //Checks activity on PortK and will wait until activity is done
 {
   char doneFlag = 0;
   ReadK();
   //Read();
   //Command();
   CheckSwitches();
   ReadFromLCD();
   while(doneFlag==0)
   {
    ELOW();
    EHIGH();
    if(PORTK.kportbits.Bit7==0)
    {
      doneFlag = 1;
    }
    ELOW();
   }
   WriteK();
  
 }
