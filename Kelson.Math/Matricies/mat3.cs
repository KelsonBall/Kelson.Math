using System.Runtime.InteropServices;

namespace Kelson.Math.Matricies
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Mat3Buffer
    {
        public double A;
        public double B;
        public double C;
        public double D;
        public double E;
        public double F;
        public double G;
        public double H;
        public double I;

        public double this[int row, int col]
        {
            get => this[(row * 3) + col];
            set => this[(row * 3) + col] = value;
        }

        public double this[int i]
        {
            get
            {
                unsafe
                {
                    fixed(void* data = &this)
                        return ((double*)data)[i];
                }
                
            }
            set
            {
                unsafe
                {
                    fixed (void* data = &this)
                        ((double*)data)[i] = value;
                }
            }
        }

        public Mat3Buffer Copy()
        {
            var ret = new Mat3Buffer();
            for (int i = 0; i < 9; i++)
                ret[i] = this[i];
            return ret;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct mat3
    {
        private const int SIZE = 3;

        private readonly Mat3Buffer buffer;

        public Mat3Buffer GetBufferCopy() => buffer.Copy();

        public mat3(double i0, double i1, double i2, double j0, double j1, double j2, double k0, double k1, double k2)
        {
            buffer = new Mat3Buffer();
            buffer[0] = i0; buffer[1] = i1; buffer[2] = i2;
            buffer[3] = j0; buffer[4] = j1; buffer[5] = j2;
            buffer[6] = k0; buffer[7] = k1; buffer[8] = k2;
        }

        public mat3(double[] rowMajorArray)
        {
            buffer = new Mat3Buffer();
            for (int i = 0; i < SIZE * SIZE; i++)
                buffer[i] = rowMajorArray[i];
        }

        public mat3(Mat3Buffer buf) =>
            buffer = buf.Copy();

        /// <summary>
        /// Create a mat3 from a buffer without a copy
        /// Don't modify buffer after calling this ctor
        /// </summary>
        /// <param name="move"></param>
        private mat3(in Mat3Buffer move) =>
            buffer = move;

        public double this[int index] => buffer[index];

        public double this[int row, int col] => buffer[row, col];            
                
        public mat3 Multiply(in mat3 m)
        {
            var ret = new Mat3Buffer();
            for (int r = 0; r < SIZE; r++)
                for (int c = 0; c < SIZE; c++)
                    ret[r, c] = buffer[r, 0] * m[0, c] + buffer[r, 1] * m[1, c] + buffer[r, 2] * m[2, c];
            return new mat3(in ret);
        }

        public mat3 Invserse()
        {
            mat3 t = Transpose();
            var adjugate = new mat3(
                t.AdjugateComponent(0, 0), t.AdjugateComponent(0, 1), t.AdjugateComponent(0, 2),
                t.AdjugateComponent(1, 0), t.AdjugateComponent(1, 1), t.AdjugateComponent(1, 2),
                t.AdjugateComponent(2, 0), t.AdjugateComponent(2, 1), t.AdjugateComponent(2, 2));
            return (1 / Determinate()) * adjugate;
        }

        public double AdjugateComponent(int row, int col)
        {
            int r1 = (row + 1) % SIZE;
            int r2 = (row + 2) % SIZE;
            if (r1 > r2)
            {
                int r = r2;
                r2 = r1;
                r1 = r;
            }

            int c1 = (col + 1) % SIZE;
            int c2 = (col + 2) % SIZE;
            if (c1 > c2)
            {
                int c = c2;
                c2 = c1;
                c1 = c;
            }

            double result = buffer[r1, c1] * buffer[r2, c2] - buffer[r2, c1] * buffer[r1, c2];

            if (
                (row == 0 && col == 1)
             || (row == 1 && col == 0)
             || (row == 1 && col == 2)
             || (row == 2 && col == 1))
                return -result;
            else
                return result;
        }

        public double Determinate() =>        
           (buffer[0] * buffer[4] * buffer[8]) 
         + (buffer[1] * buffer[5] * buffer[6]) 
         + (buffer[2] * buffer[3] * buffer[7]) 
         - (buffer[2] * buffer[4] * buffer[6]) 
         - (buffer[1] * buffer[3] * buffer[8]) 
         - (buffer[0] * buffer[5] * buffer[7]);
        
        public mat3 Transpose()
        {
            var next = new Mat3Buffer();
            for (int r = 0; r < SIZE; r++)
                for (int c = 0; c < SIZE; c++)
                    next[c, r] = buffer[r, c];
            return new mat3(in next);
        }      

        public static mat3 operator *(double scalar, mat3 m) =>
            new mat3(m[0] * scalar, m[1] * scalar, m[2] * scalar, m[3] * scalar, m[4] * scalar, m[5] * scalar, m[6] * scalar, m[7] * scalar, m[8] * scalar);

        public static mat3 operator *(mat3 m, double scalar) => 
            new mat3(m[0] * scalar, m[1] * scalar, m[2] * scalar, m[3] * scalar, m[4] * scalar, m[5] * scalar, m[6] * scalar, m[7] * scalar, m[8] * scalar);

        public static mat3 operator *(mat3 a, mat3 b) =>
            a.Multiply(b);
    }
}
