namespace LC3_VM
{
    class IAND : Instruction
    {
        private readonly ushort DstReg;
        private readonly ushort SrcReg1;
        private readonly ushort SrcReg2;

        public IAND(ushort raw) : base(raw)
        {
            DstReg = R0();
            SrcReg1 = R1();
            SrcReg2 = R2();
        }

        protected override void Execute()
        {
            ushort v1 = VM.ReadReg(SrcReg1);
            ushort v2 = VM.ReadReg(SrcReg2);
            VM.WriteReg(DstReg, (ushort)(v1 & v2));
        }

        protected override ushort Flag() => DstReg;

        public override string ToString() => string.Format("AND: r[{0}] = {1} (r[{2}] & r[{3}])", DstReg, VM.ReadReg(DstReg), SrcReg1, SrcReg2);
    }
}