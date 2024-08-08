namespace LC3_VM
{
    class ILDI : Instruction
    {
        private readonly ushort  DstReg;
        private  readonly ushort Offset;

        public ILDI(ushort raw) : base(raw)
        {
            DstReg = R0();
            Offset = PCOFFSET();
        }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            ushort addr = VM.ReadMem((ushort)(pc + Offset));
            ushort val = VM.ReadMem(addr);
            VM.WriteReg(DstReg, val);
        }

        protected override ushort Flag() => DstReg;

        public override string ToString() => string.Format("LDI: r[{0}] = {1} (m[{5}]) (m[{2}]) (r[{3}] + {4})", DstReg, VM.ReadReg(DstReg), (ushort)(VM.ReadReg(ERegisters.PC) + Offset), (ushort)ERegisters.PC, Offset, VM.ReadMem((ushort)(VM.ReadReg(ERegisters.PC) + Offset)));
    }
}