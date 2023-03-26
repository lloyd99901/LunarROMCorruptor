using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LunarROMCorruptor.CorruptionInternals
{
    public class ProcessCorruptionCore
    {
        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead); //For the Process Memory Corruption

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten); //For the Process Memory Corruption

        public static bool CorruptSelectedProcess(int processID)
        {
            // Get the process by ID
            Process process = Process.GetProcessById(processID);

            ProcessModule module = process.MainModule;

            // Get the base address of the module
            var baseAddress = module.BaseAddress;

            // Allocate a buffer to hold the memory we want to read
            byte[] buffer = new byte[512];

            // Read the memory
            ReadProcessMemory(process.Handle, baseAddress, buffer, buffer.Length, out IntPtr bytesRead);

            // Display the memory contents
            Console.WriteLine("Memory contents:");
            for (int i = 0; i < bytesRead.ToInt64(); i++)
            {
                Console.Write(buffer[i].ToString("X2") + " ");
            }
            Console.WriteLine();

            IntPtr dataAddress = module.BaseAddress + 0x0;

            IntPtr byteValue;
            int bytesWritten = 0;
            //WriteProcessMemory(process.Handle, dataAddress, ref byteValue, 1, out bytesWritten);

            return false;
        }
    }
}
