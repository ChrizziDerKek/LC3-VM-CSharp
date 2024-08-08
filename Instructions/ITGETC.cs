namespace LC3_VM
{
    class ITGETC : Instruction
    {
        public ITGETC(ushort raw) : base(raw) { }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            VM.WriteReg(ERegisters.R7, pc);
            ushort input = Utility.Read();
            VM.WriteReg(ERegisters.R0, input);
        }

        protected override ushort Flag() => (ushort)ERegisters.R0;

        public override string ToString() => string.Format("GETC: r[0] = {0}", VM.ReadReg(ERegisters.R0));
    }
}