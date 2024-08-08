using System.Diagnostics;

namespace LC3_VM
{
    class Program
    {
        static void Main(string[] args)
        {
            string image = Debugger.IsAttached ? "2048.obj" : args[0];
            if (VM.Load(image))
                VM.Run();
        }
    }
}