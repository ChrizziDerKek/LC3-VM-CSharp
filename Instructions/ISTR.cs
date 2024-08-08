namespace LC3_VM
{
    class ISTR : Instruction
    {
        private readonly ushort SrcReg;
        private readonly ushort Offset;
        private readonly ushort DstReg;

        public ISTR(ushort raw) : base(raw)
        {
            SrcReg = R0();
            Offset = OFFSET();
            DstReg = R1();
        }

        protected override void Execute()
        {
            ushort reg = VM.ReadReg(DstReg);
            ushort val = VM.ReadReg(SrcReg);
            VM.WriteMem((ushort)(reg + Offset), val);
        }

        public override string ToString() => string.Format("STR: m[{0}] = {1} (r[{2}]) (r[{3}] + {4})", (ushort)(VM.ReadReg(DstReg) + Offset), VM.ReadReg(SrcReg), SrcReg, DstReg, Offset);
    }
}