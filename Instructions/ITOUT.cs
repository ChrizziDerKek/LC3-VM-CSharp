namespace LC3_VM
{
    class ITOUT : Instruction
    {
        public ITOUT(ushort raw) : base(raw) { }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            VM.WriteReg(ERegisters.R7, pc);
            ushort output = VM.ReadReg(ERegisters.R0);
            Utility.Write(output);
            Utility.Flush();
        }

        public override string ToString() => "OUT";
    }
}