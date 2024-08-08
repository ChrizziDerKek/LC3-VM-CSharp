namespace LC3_VM
{
    class IST : Instruction
    {
        private readonly ushort SrcReg;
        private readonly ushort Offset;

        public IST(ushort raw) : base(raw)
        {
            SrcReg = R0();
            Offset = PCOFFSET();
        }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            ushort val = VM.ReadReg(SrcReg);
            VM.WriteMem((ushort)(pc + Offset), val);
        }

        public override string ToString() => string.Format("ST: m[{0}] = {1} (r[{2}]) (r[{3}] + {4})", (ushort)(VM.ReadReg(ERegisters.PC) + Offset), VM.ReadReg(SrcReg), SrcReg, (ushort)ERegisters.PC, Offset);
    }
}