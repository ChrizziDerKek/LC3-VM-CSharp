namespace LC3_VM
{
    class IJSR : Instruction
    {
        private readonly ushort Reg;

        public IJSR(ushort raw) : base(raw) => Reg = R1();

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            VM.WriteReg(ERegisters.R7, pc);
        }

        protected override ushort Decide() => VM.ReadReg(Reg);

        public override string ToString() => string.Format("JSR: pc = {0}", VM.ReadReg(Reg));
    }
}