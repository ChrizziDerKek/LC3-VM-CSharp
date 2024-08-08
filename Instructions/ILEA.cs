namespace LC3_VM
{
    class ILEA : Instruction
    {
        private readonly ushort DstReg;
        private readonly ushort Offset;

        public ILEA(ushort raw) : base(raw)
        {
            DstReg = R0();
            Offset = PCOFFSET();
        }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            ushort val = (ushort)(pc + Offset);
            VM.WriteReg(DstReg, val);
        }

        protected override ushort Flag() => DstReg;

        public override string ToString() => string.Format("LEA: r[{0}] = {1} (r[{2}] + {3})", DstReg, VM.ReadReg(DstReg), (ushort)ERegisters.PC, Offset);
    }
}