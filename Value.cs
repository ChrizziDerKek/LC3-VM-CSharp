using System;

namespace LC3_VM
{
    class Value : Instruction
    {
        public Value(ushort raw) : base(raw) { }

        protected override void Execute() => throw new NotImplementedException();
    }
}