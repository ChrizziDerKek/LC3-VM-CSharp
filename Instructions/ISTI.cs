namespace LC3_VM
{
    class ISTI : Instruction
    {
        private readonly ushort SrcReg;
        private readonly ushort Offset;

        public ISTI(ushort raw) : base(raw)
        {
            SrcReg = R0();
            Offset = PCOFFSET();
        }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            ushort addr = VM.ReadMem((ushort)(pc + Offset));
            ushort val = VM.ReadReg(SrcReg);
            VM.WriteMem(addr, val);
        }

        public override string ToString() => string.Format("STI: m[{5}] = {1} (r[{2}]) (m[{0}]) (r[{3}] + {4})", (ushort)(VM.ReadReg(ERegisters.PC) + Offset), VM.ReadReg(SrcReg), SrcReg, (ushort)ERegisters.PC, Offset, VM.ReadMem((ushort)(VM.ReadReg(ERegisters.PC) + Offset)));
    }
}