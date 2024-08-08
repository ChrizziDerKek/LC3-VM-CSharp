namespace LC3_VM
{
    class IANDI : Instruction
    {
        private readonly ushort DstReg;
        private readonly ushort SrcReg;
        private readonly ushort ImmVal;

        public IANDI(ushort raw) : base(raw)
        {
            DstReg = R0();
            SrcReg = R1();
            ImmVal = IMMVAL();
        }

        protected override void Execute()
        {
            ushort val = VM.ReadReg(SrcReg);
            VM.WriteReg(DstReg, (ushort)(val & ImmVal));
        }

        protected override ushort Flag() => DstReg;

        public override string ToString() => string.Format("ANDI: r[{0}] = {1} (r[{2}] & {3})", DstReg, VM.ReadReg(DstReg), SrcReg, ImmVal);
    }
}