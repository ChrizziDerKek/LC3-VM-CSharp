namespace LC3_VM
{
    class INOT : Instruction
    {
        private readonly ushort Reg;

        public INOT(ushort raw) : base(raw) => Reg = R0();

        protected override void Execute()
        {
            ushort val = (ushort)~VM.ReadReg(Reg);
            VM.WriteReg(Reg, val);
        }

        protected override ushort Flag() => Reg;

        public override string ToString() => string.Format("NOT: r[{0}] = {1} (~r[{0}])", Reg, VM.ReadReg(Reg));
    }
}