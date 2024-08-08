namespace LC3_VM
{
    class ILDR : Instruction
    {
        private readonly ushort DstReg;
        private readonly ushort Offset;
        private readonly ushort SrcReg;

        public ILDR(ushort raw) : base(raw)
        {
            DstReg = R0();
            Offset = OFFSET();
            SrcReg = R1();
        }

        protected override void Execute()
        {
            ushort reg = VM.ReadReg(SrcReg);
            ushort val = VM.ReadMem((ushort)(reg + Offset));
            VM.WriteReg(DstReg, val);
        }

        protected override ushort Flag() => DstReg;

        public override string ToString() => string.Format("LDR: r[{0}] = {1} (m[{2}]) (r[{3}] + {4})", DstReg, VM.ReadReg(DstReg), (ushort)(VM.ReadReg(SrcReg) + Offset), SrcReg, Offset);
    }
}