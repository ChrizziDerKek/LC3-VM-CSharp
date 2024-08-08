namespace LC3_VM
{
    class ITIN : Instruction
    {
        public ITIN(ushort raw) : base(raw) { }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            VM.WriteReg(ERegisters.R7, pc);
            Utility.Write("Enter a character: ");
            ushort input = Utility.Read();
            Utility.Write(input);
            Utility.Flush();
            VM.WriteReg(ERegisters.R0, input);
        }

        protected override ushort Flag() => (ushort)ERegisters.R0;

        public override string ToString() => string.Format("IN: r[0] = {0}", VM.ReadReg(ERegisters.R0));
    }
}