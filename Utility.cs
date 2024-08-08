using System;

namespace LC3_VM
{
    static class Utility
    {
        public static bool CheckKey()
        {
            IntPtr hstdin = External.GetStdHandle(-10);
            return External.WaitForSingleObject(hstdin, 1000) == 0 && Console.KeyAvailable;
        }

        public static EInstructions GetOpcode(ushort raw) => new Value(raw).OP();

        public static ETraps GetTrap(ushort raw) => new Value(raw).TRAP();

        public static bool IsImm(ushort raw) => new Value(raw).IMM();

        public static bool IsFar(ushort raw) => new Value(raw).FAR();

        private static uint OldMode;

        public static void SetInputBuffering(bool enabled)
        {
            IntPtr hstdin = External.GetStdHandle(-10);
            if (!enabled)
            {
                Console.CancelKeyPress += HandleInterrupt;
                External.GetConsoleMode(hstdin, out OldMode);
                External.SetConsoleMode(hstdin, OldMode ^ 4 ^ 2);
                External.FlushConsoleInputBuffer(hstdin);
            }
            else External.SetConsoleMode(hstdin, OldMode);
        }

        public static void HandleInterrupt(object sender, ConsoleCancelEventArgs e)
        {
            SetInputBuffering(true);
            Write('\n');
            Environment.Exit(-2);
        }

        public static ushort Read() => (ushort)Console.Read();

        public static void Flush() => Console.Out.Flush();

        public static void Write(char c) => Console.Write(c);

        public static void Write(string s) => Console.Write(s);

        public static void Write(ushort i) => Console.Write((char)i);

        public static ushort Extend(ushort value, int bits)
        {
            if (((value >> (bits - 1)) & 1) == 0)
                return value;
            return (ushort)(value | (ushort.MaxValue << bits));
        }

        public static ushort Swap(ushort value) => (ushort)((value << 8) | (value >> 8));
    }
}