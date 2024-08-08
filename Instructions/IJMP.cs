namespace LC3_VM
{
    class IJMP : Instruction
    {
        private readonly ushort Reg;

        public IJMP(ushort raw) : base(raw) => Reg = R1();

        protected override void Execute() { }

        protected override ushort Decide() => VM.ReadReg(Reg);

        public override string ToString() => string.Format("JMP: pc = {0}", VM.ReadReg(Reg));
    }
}