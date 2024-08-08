namespace LC3_VM
{
    class IRES : Instruction
    {
        public IRES(ushort raw) : base(raw) { }

        protected override void Execute() { }

        public override string ToString() => "RES";
    }
}