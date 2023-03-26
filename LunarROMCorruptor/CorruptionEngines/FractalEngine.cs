namespace LunarROMCorruptor.CorruptionEngines
{
    internal class FractalEngine
    {

        public static byte[] CorruptByte(byte[] ROM, long i)
        {
            // Set up parameters for the Lorenz Attractor algorithm
            double x = 0.1, y = 0.0, z = 0.0;
            double dt = 0.01, sigma = 10.0, rho = 28.0, beta = 8.0 / 3.0;

            // Calculate the next value in the Lorenz Attractor sequence
            double dx = sigma * (y - x);
            double dy = x * (rho - z) - y;
            double dz = x * y - beta * z;
            x += dx * dt;
            y += dy * dt;
            z += dz * dt;
            double value = x;

            // Use the value to modify the byte at the current location in the file
            byte oldValue = ROM[i];
            byte newValue = (byte)(oldValue + value);
            ROM[i] = newValue;

            return ROM;
        }
    }
}
