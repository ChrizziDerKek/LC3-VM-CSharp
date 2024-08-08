namespace LC3_VM
{
    class IRTI : Instruction
    {
        public IRTI(ushort raw) : base(raw) { }

        protected override void Execute() { }

        public override string ToString() => "RTI";
    }
}