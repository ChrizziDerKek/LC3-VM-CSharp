namespace LC3_VM
{
    class ILD : Instruction
    {
        private readonly ushort DstReg;
        private readonly ushort Offset;

        public ILD(ushort raw) : base(raw)
        {
            DstReg = R0();
            Offset = PCOFFSET();
        }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            ushort val = VM.ReadMem((ushort)(pc + Offset));
            VM.WriteReg(DstReg, val);
        }

        protected override ushort Flag() => DstReg;

        public override string ToString() => string.Format("LD: r[{0}] = {1} (m[{2}]) (r[{3}] + {4})", DstReg, VM.ReadReg(DstReg), (ushort)(VM.ReadReg(ERegisters.PC) + Offset), (ushort)ERegisters.PC, Offset);
    }
}