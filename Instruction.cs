using System;

namespace LC3_VM
{
    abstract class Instruction
    {
        public ushort RAW { get; private set; }
        public ushort R0() => (ushort)((RAW >> 9) & 7);
        public ushort R1() => (ushort)((RAW >> 6) & 7);
        public ushort R2() => (ushort)(RAW & 7);
        public EInstructions OP() => (EInstructions)(RAW >> 12);
        public ETraps TRAP() => (ETraps)(RAW & 0xFF);
        public ushort COND() => R0();
        public bool IMM() => ((RAW >> 5) & 1) != 0;
        public ushort IMMVAL() => Utility.Extend((ushort)(RAW & 0x1F), 5);
        public bool FAR() => ((RAW >> 11) & 1) != 0;
        public ushort PCOFFSET() => Utility.Extend((ushort)(RAW & 0x1FF), 9);
        public ushort OFFSET() => Utility.Extend((ushort)(RAW & 0x3F), 6);
        public ushort FAROFFSET() => Utility.Extend((ushort)(RAW & 0x7FF), 11);

        public Instruction(ushort raw) => RAW = raw;

        public ushort Run()
        {
            Execute();
            ushort flag = Flag();
            if (flag != ushort.MaxValue)
                VM.UpdateFlags(flag);
            return Decide();
        }

        public override string ToString() => throw new NotImplementedException();

        protected abstract void Execute();

        protected virtual ushort Flag() => ushort.MaxValue;

        protected virtual ushort Decide() => VM.ReadReg(ERegisters.PC);
    }
}