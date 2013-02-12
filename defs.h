//Chris Hilgenberg
//Used for Labs 6&7
//2-22/12
//Definitions for LCD.c and Main.
//Be sure not to include derivative/microprocessor.c file

/*This seems excessive as the derivative microprocessor.c file has all these ports declared, but 
I wanted knowledge of using type definitions and structures in C. We covered them, but never really used them in practice.
As an extra challenge, I decided to write a definitions file that would do the same thing as the pre-defined one.

Also, the port definitions are non-ANSI specified declarations....which happens to be slightly nicer than what they are.*/


typedef unsigned char UCHAR;

struct CONTROLLINES    //PortD used to Control LCD
{
  UCHAR Bit0 :1;
  UCHAR Bit1 :1;
  UCHAR Bit2 :1;
  UCHAR Bit3 :1;
  UCHAR Bit4 :1;
  UCHAR RSLINE :1;
  UCHAR RWLINE :1;
  UCHAR ENABLE :1;
} PORTD@0x05; 

struct BPORT           //PortB used to read DIP switches on board
{
  UCHAR Bit0 :1;
  UCHAR Bit1 :1;
  UCHAR Bit2 :1;
  UCHAR Bit3 :1;
  UCHAR Bit4 :1;
  UCHAR Bit5 :1;
  UCHAR Bit6 :1;
  UCHAR Bit7 :1;
}PORTB@0x01;

struct EDataDirection     //Sets data direction I/O for PortE
{
  UCHAR Bit0 :1;
  UCHAR Bit1 :1;
  UCHAR Bit2 :1;
  UCHAR Bit3 :1;
  UCHAR Bit4 :1;
  UCHAR Bit5 :1;
  UCHAR Bit6 :1;
  UCHAR Bit7 :1;
};
union EDD
{
  UCHAR DDE;
  struct EDataDirection EDATA;
};
volatile union EDD DDRE@0x09;
struct EPORT                 //PortE used to control LCD color
{
  UCHAR Bit0 :1;
  UCHAR Bit1 :1;
  UCHAR Bit2 :1;
  UCHAR Bit3 :1;
  UCHAR Bit4 :1;
  UCHAR Bit5 :1;
  UCHAR Bit6 :1;
  UCHAR Bit7 :1;
};
union EPORT_UNION
{
  UCHAR eport;
  struct EPORT ebits;
};
volatile union EPORT_UNION PORTE@0x08;
struct DDataDirection    //DDRD used to flow IO for LCD
{
  UCHAR Bit0 :1;
  UCHAR Bit1 :1;
  UCHAR Bit2 :1;
  UCHAR Bit3 :1;
  UCHAR Bit4 :1;
  UCHAR Bit5 :1;
  UCHAR Bit6 :1;
  UCHAR Bit7 :1;
};// DDRD@0x07;
union DDATA_UNION
{
  UCHAR ddrd;
  struct DDataDirection ddatabits;
};
volatile union DDATA_UNION DDRD@0x07;

struct KDataDirection         //DDRK is used to flow IO for LCD
{
  UCHAR Bit0 :1;
  UCHAR Bit1 :1;
  UCHAR Bit2 :1;
  UCHAR Bit3 :1;
  UCHAR Bit4 :1;
  UCHAR Bit5 :1;
  UCHAR Bit6 :1;
  UCHAR Bit7 :1;
};// DDRK@0x33;
union KDATA_UNION
{
  UCHAR ddrk;
  struct KDataDirection kdatabits;
};
volatile union KDATA_UNION DDRK@0x33;    //PORTK writes to LCD

struct KPort
{
  UCHAR Bit0 :1;
  UCHAR Bit1 :1;
  UCHAR Bit2 :1;
  UCHAR Bit3 :1;
  UCHAR Bit4 :1;
  UCHAR Bit5 :1;
  UCHAR Bit6 :1;
  UCHAR Bit7 :1;
}; //PORTK@0x32; 
union KPORT_UNION
{
  UCHAR portk;
  struct KPort kportbits;
};
volatile union KPORT_UNION PORTK@0x32;
