namespace LC3_VM
{
	enum ERegisters : ushort
	{
		R0,
		R1,
		R2,
		R3,
		R4,
		R5,
		R6,
		R7,
		PC,
		COND,
		COUNT,
	};

	enum EInstructions : ushort
	{
		BR, //branch
		ADD, //add
		LD, //load
		ST, //store
		JSR, //jump register
		AND, //bitwise and
		LDR, //load register
		STR, //store register
		RTI, //unused
		NOT, //bitwise not
		LDI, //load indirect
		STI, //store indirect
		JMP, //jump
		RES, //reserved
		LEA, //load effective address
		TRAP, //execute trap
	};

	enum EFlags : ushort
	{
		POS = 1 << 0, //P
		ZRO = 1 << 1, //Z
		NEG = 1 << 2, //N
	};

	enum ETraps : ushort
	{
		GETC = 0x20, //get char from keyboard without console output
		OUT, //output a character
		PUTS, //output a word string
		IN, //get char from keyboard with console output
		PUTSP, //output a byte string
		HALT, //halts the program
	};

	enum EMemory : ushort
	{
		KBSR = 0xFE00, //keyboard status
		KBDR = 0xFE02, //keyboard data
	};
}