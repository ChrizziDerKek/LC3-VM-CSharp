using System;
using System.Collections.Generic;
using System.IO;

namespace LC3_VM
{
    class VM
    {
        private static Dictionary<ERegisters, ushort> Registers;
        private static Instruction[] Addresses;
        private static bool Initialized;
        private static bool Loaded;
        
        public static bool Running { get; private set; }

        private static void Init(ushort start = 0x3000)
        {
            if (!Initialized)
            {
                Utility.SetInputBuffering(false);
                Initialized = true;
            }
            Loaded = false;
            Running = true;
            Addresses = new Instruction[ushort.MaxValue + 1];
            Registers = new Dictionary<ERegisters, ushort>();
            for (ushort i = 0; i < (ushort)ERegisters.COUNT; i++)
                Registers.Add((ERegisters)i, 0);
            WriteReg(ERegisters.COND, (ushort)EFlags.ZRO);
            WriteReg(ERegisters.PC, start);
        }

        private static void WriteLog(string str)
        {
            string file = "trace.txt";
            if (!File.Exists(file))
                File.Create(file).Close();
            using (FileStream stream = new FileStream(file, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(stream))
                    sw.WriteLine(str);
        }

        public static void Kill() => Running = false;

        public static void WriteReg(ERegisters reg, ushort val) => Registers[reg] = val;

        public static ushort ReadReg(ERegisters reg) => Registers[reg];

        public static void WriteReg(ushort reg, ushort val) => WriteReg((ERegisters)reg, val);

        public static ushort ReadReg(ushort reg) => ReadReg((ERegisters)reg);

        public static void WriteMem(ushort addr, ushort val) => Addresses[addr] = new Value(val);

        public static ushort ReadMem(ushort addr)
        {
            if (addr == (ushort)EMemory.KBSR)
            {
                if (Utility.CheckKey())
                {
                    WriteMem((ushort)EMemory.KBSR, short.MaxValue + 1);
                    WriteMem((ushort)EMemory.KBDR, Utility.Read());
                }
                else WriteMem((ushort)EMemory.KBSR, 0);
            }
            if (Addresses[addr] == null)
                return 0;
            return Addresses[addr].RAW;
        }

        public static Instruction ReadInstr()
        {
            ushort pc = ReadReg(ERegisters.PC);
            Instruction instr = Addresses[pc];
            WriteReg(ERegisters.PC, (ushort)(pc + 1));
            return instr;
        }

        public static void UpdateFlags(ushort reg)
        {
            ushort val = ReadReg(reg);
            if (val == 0)
                WriteReg(ERegisters.COND, (ushort)EFlags.ZRO);
            else if ((val >> 15) != 0)
                WriteReg(ERegisters.COND, (ushort)EFlags.NEG);
            else
                WriteReg(ERegisters.COND, (ushort)EFlags.POS);
        }

        public static void Run()
        {
            if (!Loaded)
                return;
            while (Running)
            {
                Instruction instr = ReadInstr();
                ushort pc = instr.Run();
                //WriteLog(instr.ToString());
                WriteReg(ERegisters.PC, pc);
            }
        }

        public static bool Load(string image)
        {
            try
            {
                if (!File.Exists(image))
                    return false;
                Init();
                FileStream stream = new FileStream(image, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                ushort origin = Utility.Swap(reader.ReadUInt16());
                ushort max = (ushort)(Addresses.Length - origin);
                Instruction[] buffer = new Instruction[max];
                for (int i = 0; i < max; i++)
                {
                    if (reader.BaseStream.Position == reader.BaseStream.Length)
                        break;
                    buffer[i] = Parse(Utility.Swap(reader.ReadUInt16()));
                }
                Array.Copy(buffer, 0, Addresses, origin, buffer.Length);
                reader.Close();
                stream.Close();
                Loaded = true;
                return true;
            }
            catch (IOException) { }
            return false;
        }

        private static Instruction Parse(ushort raw)
        {
            EInstructions op = Utility.GetOpcode(raw);
            switch (op)
            {
                case EInstructions.BR:
                    return new IBR(raw);
                case EInstructions.ADD:
                    if (Utility.IsImm(raw))
                        return new IADDI(raw);
                    return new IADD(raw);
                case EInstructions.LD:
                    return new ILD(raw);
                case EInstructions.ST:
                    return new IST(raw);
                case EInstructions.JSR:
                    if (Utility.IsFar(raw))
                        return new IJSRF(raw);
                    return new IJSR(raw);
                case EInstructions.AND:
                    if (Utility.IsImm(raw))
                        return new IANDI(raw);
                    return new IAND(raw);
                case EInstructions.LDR:
                    return new ILDR(raw);
                case EInstructions.STR:
                    return new ISTR(raw);
                case EInstructions.RTI:
                    return new IRTI(raw);
                case EInstructions.NOT:
                    return new INOT(raw);
                case EInstructions.LDI:
                    return new ILDI(raw);
                case EInstructions.STI:
                    return new ISTI(raw);
                case EInstructions.JMP:
                    return new IJMP(raw);
                case EInstructions.RES:
                    return new IRES(raw);
                case EInstructions.LEA:
                    return new ILEA(raw);
                case EInstructions.TRAP:
                    switch (Utility.GetTrap(raw))
                    {
                        case ETraps.GETC:
                            return new ITGETC(raw);
                        case ETraps.OUT:
                            return new ITOUT(raw);
                        case ETraps.PUTS:
                            return new ITPUTS(raw);
                        case ETraps.IN:
                            return new ITIN(raw);
                        case ETraps.PUTSP:
                            return new ITPUTSP(raw);
                        case ETraps.HALT:
                            return new ITHALT(raw);
                    }
                    break;
            }
            return new Value(raw);
        }
    }
}