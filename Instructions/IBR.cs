namespace LC3_VM
{
    class IBR : Instruction
    {
        private readonly ushort JumpOffset;
        private readonly ushort Condition;

        public IBR(ushort raw) : base(raw)
        {
            JumpOffset = PCOFFSET();
            Condition = COND();
        }

        protected override void Execute() { }

        protected override ushort Decide()
        {
            ushort offset = base.Decide();
            ushort val = VM.ReadReg(ERegisters.COND);
            if ((Condition & val) != 0)
                offset += JumpOffset;
            return offset;
        }

        public override string ToString()
        {
            ushort val = VM.ReadReg(ERegisters.COND);
            bool cond = (Condition & val) != 0;
            ushort offset = (ushort)(cond ? JumpOffset : 1);
            string equals = cond ? "!=" : "==";
            return string.Format("BR: pc = {0} (+ {1}) ({2} & {3}) {4} 0)", VM.ReadReg(ERegisters.PC), offset, Condition, val, equals);
        }
    }
}