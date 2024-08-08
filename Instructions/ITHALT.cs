namespace LC3_VM
{
    class ITHALT : Instruction
    {
        public ITHALT(ushort raw) : base(raw) { }

        protected override void Execute()
        {
            Utility.Write("HALT");
            Utility.Flush();
            VM.Kill();
        }

        public override string ToString() => "HALT";
    }
}