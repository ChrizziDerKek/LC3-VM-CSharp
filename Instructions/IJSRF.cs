namespace LC3_VM
{
    class IJSRF : Instruction
    {
        private readonly ushort Offset;

        public IJSRF(ushort raw) : base(raw) => Offset = FAROFFSET();

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            VM.WriteReg(ERegisters.R7, pc);
        }

        protected override ushort Decide() => (ushort)(base.Decide() + Offset);

        public override string ToString() => string.Format("JSRF: pc = {0} (+ {1})", VM.ReadReg(ERegisters.PC), Offset);
    }
}