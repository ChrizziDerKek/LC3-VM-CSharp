namespace LC3_VM
{
    class ITPUTS : Instruction
    {
        public ITPUTS(ushort raw) : base(raw) { }

        protected override void Execute()
        {
            ushort pc = VM.ReadReg(ERegisters.PC);
            VM.WriteReg(ERegisters.R7, pc);
            ushort i = VM.ReadReg(ERegisters.R0);
            while (true)
            {
                ushort output = VM.ReadMem(i++);
                if (output == 0)
                    break;
                Utility.Write(output);
            }
            Utility.Flush();
        }

        public override string ToString() => "PUTS";
    }
}