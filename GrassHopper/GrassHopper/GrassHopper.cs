using System.Text;

namespace GrassHopper
{
    public class GrassHopper
    {
        readonly byte[][] iterC = new byte[32][]; // массив итерационных констант
        readonly byte[][] iterK = new byte[10][]; // массив итерационных ключей

        static readonly byte[] LVec = new byte[]
        { 148, 32, 133, 16, 194, 192, 1, 251, 1, 192, 194, 16, 133, 32, 148, 1 };

        static readonly byte[] Pi = new byte[256]
        {
            252, 238, 221, 17, 207, 110, 49, 22, 251, 196, 250, 218, 35, 197, 4, 77, 233, 119, 240, 219, 147, 
            46, 153, 186, 23, 54, 241, 187, 20, 205, 95, 193, 249, 24, 101, 90, 226, 92, 239, 33, 129, 28, 60, 
            66, 139, 1, 142, 79, 5, 132, 2, 174, 227, 106, 143, 160, 6, 11, 237, 152, 127, 212, 211, 31, 235, 
            52, 44, 81, 234, 200, 72, 171, 242, 42, 104, 162, 253, 58, 206, 204, 181, 112, 14, 86, 8, 12, 118, 
            18, 191, 114, 19, 71, 156, 183, 93, 135, 21, 161, 150, 41, 16, 123, 154, 199, 243, 145, 120, 111, 
            157, 158, 178, 177, 50, 117, 25, 61, 255, 53, 138, 126, 109, 84, 198, 128, 195, 189, 13, 87, 223, 
            245, 36, 169, 62, 168, 67, 201, 215, 121, 214, 246, 124, 34, 185, 3, 224, 15, 236, 222, 122, 148, 
            176, 188, 220, 232, 40, 80, 78, 51, 10, 74, 167, 151, 96, 115, 30, 0, 98, 68, 26, 184, 56, 130, 100, 
            159, 38, 65, 173, 69, 70, 146, 39, 94, 85, 47, 140, 163, 165, 125, 105, 213, 149, 59, 7, 88, 179, 64, 
            134, 172, 29, 247, 48, 55, 107, 228, 136, 217, 231, 137, 225, 27, 131, 73, 76, 63, 248, 254, 141, 83,
            170, 144, 202, 216, 133, 97, 32, 113, 103, 164, 45, 43, 9, 91, 203, 155, 37, 208, 190, 229, 108, 82,
            89, 166, 116, 210, 230, 244, 180, 192, 209, 102, 175, 194, 57, 75, 99, 182
        };

        static readonly byte[] Pi_Reverse = new byte[256]
        {  
            165, 45, 50, 143, 14, 48, 56, 192, 84, 230, 158, 57, 85, 126, 82, 145, 100, 3, 87, 90, 28, 96, 7,
            24, 33, 114, 168, 209, 41, 198, 164, 63, 224, 39, 141, 12, 130, 234, 174, 180, 154, 99, 73, 229,
            66, 228, 21, 183, 200, 6, 112, 157, 65, 117, 25, 201, 170, 252, 77, 191, 42, 115, 132, 213, 195,
            175, 43, 134, 167, 177, 178, 91, 70, 211, 159, 253, 212, 15, 156, 47, 155, 67, 239, 217, 121, 182,
            83, 127, 193, 240, 35, 231, 37, 94, 181, 30, 162, 223, 166, 254, 172, 34, 249, 226, 74, 188, 53,
            202, 238, 120, 5, 107, 81, 225, 89, 163, 242, 113, 86, 17, 106, 137, 148, 101, 140, 187, 119, 60,
            123, 40, 171, 210, 49, 222, 196, 95, 204, 207, 118, 44, 184, 216, 46, 54, 219, 105, 179, 20, 149,
            190, 98, 161, 59, 22, 102, 233, 92, 108, 109, 173, 55, 97, 75, 185, 227, 186, 241, 160, 133, 131,
            218, 71, 197, 176, 51, 250, 150, 111, 110, 194, 246, 80, 255, 93, 169, 142, 23, 27, 151, 125, 236,
            88, 247, 31, 251, 124, 9, 13, 122, 103, 69, 135, 220, 232, 79, 29, 78, 4, 235, 248, 243, 62, 61, 189,
            138, 136, 221, 205, 11, 19, 152, 2, 147, 128, 144, 208, 36, 52, 203, 237, 244, 206, 153, 16, 68, 64,
            146, 58, 1, 38, 18, 26, 72, 104, 245, 129, 139, 199, 214, 32, 10, 8, 0, 76, 215, 116
        };

        public void SetKey(string newKey) //Вычисление итерационных ключей для нового ключа
        {
            var res = newKey;
            var len = newKey.Length;
            if (len < 32)
            {
                var builder = new StringBuilder(newKey);
                for (int i = len; i < 32; i++)
                    builder.Append(newKey.AsSpan(i % len, 1));
                res = builder.ToString();
            }
            if (len > 32)
                res = res[..32];
            var masterKey = Encoding.Default.GetBytes(res);
            byte[][] iterNum = new byte[32][];
            for (int i = 0; i < 32; i++)
            {
                iterNum[i] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Convert.ToByte(i + 1) };
                iterC[i] = HopperL(iterNum[i]);
            }

            byte[] A = new byte[16];
            byte[] B = new byte[16];
            for (int i = 0; i < 16; i++) { A[i] = masterKey[i]; B[i] = masterKey[i + 16]; }
            iterK[0] = B;
            iterK[1] = A;

            byte[] C = new byte[16];
            byte[] D = new byte[16];

            for (int i = 0; i < 4; i++)
            {
                var iteration = i << 3;
                HopperF(A, B, ref C, ref D, iterC[0 + iteration]);
                HopperF(C, D, ref A, ref B, iterC[1 + iteration]);
                HopperF(A, B, ref C, ref D, iterC[2 + iteration]);
                HopperF(C, D, ref A, ref B, iterC[3 + iteration]);
                HopperF(A, B, ref C, ref D, iterC[4 + iteration]);
                HopperF(C, D, ref A, ref B, iterC[5 + iteration]);
                HopperF(A, B, ref C, ref D, iterC[6 + iteration]);
                HopperF(C, D, ref A, ref B, iterC[7 + iteration]);
                iterK[2 * i + 2] = A;
                iterK[2 * i + 3] = B;
            }
        }

        static byte HopperMulInGF(byte a, byte b) //умножение в поле галуа
        {
            byte p = 0;
            byte counter;
            byte hi_bit_set;
            for (counter = 0; counter < 8 && a != 0 && b != 0; counter++)
            {
                if ((b & 1) != 0)
                    p ^= a;
                hi_bit_set = (byte)(a & 128);
                a <<= 1;
                if (hi_bit_set != 0)
                    a ^= 195; /* x^8 + x^7 + x^6 + x + 1 */
                b >>= 1;
            }
            return p;
        }

        static byte[] HopperX(byte[] input1, byte[] input2) // Преобразование Х (сложение 2х веторов по модулю 2)
        {
            byte[] output = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                output[i] = Convert.ToByte(input1[i] ^ input2[i]);
            }
            return output;
        }

        private static void HopperF(byte[] input1, byte[] input2, ref byte[] output1, ref byte[] output2, byte[] round_C)
        {
            byte[] state;
            state = HopperX(input1, round_C);
            state = HopperS(state);
            state = HopperL(state);
            output1 = HopperX(state, input2);
            output2 = input1;
        }

        static byte[] HopperS(byte[] input) // Прямое нелинейное преобразование S (Стрибог)
        {
            byte[] output = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                output[i] = Pi[input[i]];
            }
            return output;
        }

        static byte[] HopperSReverse(byte[] input) // Обратное нелинейное преобразование S
        {
            byte[] output = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                output[i] = Pi_Reverse[input[i]];
            }
            return output;
        }

        static byte[] HopperR(byte[] input)
        {
            byte a_15 = 0;
            byte[] state = new byte[16];
            for (int i = 0; i <= 15; i++)
            {
                a_15 ^= HopperMulInGF(input[i], LVec[i]);
            }
            for (int i = 15; i > 0; i--)
            {
                state[i] = input[i - 1];
            }
            state[0] = a_15;
            return state;
        }

        static byte[] HopperRReverse(byte[] input)
        {
            byte a_15 = input[0];
            byte[] state = new byte[16];
            for (int i = 0; i < 15; i++)
            {
                state[i] = input[i + 1];
            }
            for (int i = 15; i >= 0; i--)
            {
                a_15 ^= HopperMulInGF(state[i], LVec[i]);
            }
            state[15] = a_15;
            return state;
        }

        static byte[] HopperL(byte[] input) //Сеть Фейстеля
        {
            byte[] state = input;
            for (int i = 0; i < 16; i++)
            {
                state = HopperR(state);
            }
            return state;
        }

        static byte[] HopperLReverse(byte[] input)
        {
            byte[] state = input;
            for (int i = 0; i < 16; i++)
            {
                state = HopperRReverse(state);
            }
            return state;
        }

        public byte[] Encode(byte[] file)
        {
            int NumOfBlocks = (file.Length >> 4) + ((file.Length & 15) == 0 ? 0 : 1);
            byte[] encrText = new byte[NumOfBlocks << 4];
            if ((file.Length & 15) != 0)
            {
                Array.Resize(ref file, NumOfBlocks << 4);
                file[^1] = 129;
                var i = file.Length - 2;
                while (i >= 0 && file[i] == 0) i--;
                file[i + 1] = 1;
            }
            for (int i = 0; i < NumOfBlocks; i++)
            {
                byte[] block = new byte[16];
                var blockInd = i << 4;
                for (int j = 0; j < 16; j++)
                {
                    block[j] = file[blockInd + j];
                }
                for (int j = 0; j < 9; j++)
                {
                    block = HopperX(block, iterK[j]);
                    block = HopperS(block);
                    block = HopperL(block);
                }
                block = HopperX(block, iterK[9]);
                for (int j = 0; j < 16; j++)
                {
                    encrText[blockInd + j] = block[j];
                }
            }
            return encrText;
        }

        public byte[] Decode(byte[] file)
        {
            int NumOfBlocks = file.Length >> 4;
            byte[] decrText = new byte[file.Length];
            for (int i = 0; i < NumOfBlocks; i++)
            {
                byte[] block = new byte[16];
                var blockInd = i << 4;
                for (int j = 0; j < 16; j++)
                {
                    block[j] = file[blockInd + j];
                }
                block = HopperX(block, iterK[9]);
                for (int j = 8; j >= 0; j--)
                {
                    block = HopperLReverse(block);
                    block = HopperSReverse(block);
                    block = HopperX(block, iterK[j]);
                }
                for (int j = 0; j < 16; j++)
                {
                    decrText[blockInd + j] = block[j];
                }
            }
            if (decrText[^1] == 129)
            {
                int zeroesEnd = decrText.Length - 1;
                while (decrText[zeroesEnd] != 1) zeroesEnd--;
                Array.Resize(ref decrText, zeroesEnd);
            }
            return decrText;
        }

        public byte[] EncodeCBC(byte[] file, byte[] initVector)
        {
            int NumOfBlocks = (file.Length >> 4) + ((file.Length & 15) == 0 ? 0 : 1);
            byte[] encrText = new byte[NumOfBlocks << 4];
            if ((file.Length & 15) != 0)
            {
                Array.Resize(ref file, NumOfBlocks << 4);
                file[^1] = 129;
                var i = file.Length - 2;
                while (i >= 0 && file[i] == 0) i--;
                file[i + 1] = 1;
            }
            for (int i = 0; i < NumOfBlocks; i++)
            {
                byte[] block = new byte[16];
                var blockInd = i << 4;
                for (int j = 0; j < 16; j++)
                {
                    block[j] = file[blockInd + j];
                }
                block = HopperX(block, initVector);
                for (int j = 0; j < 9; j++)
                {
                    block = HopperX(block, iterK[j]);
                    block = HopperS(block);
                    block = HopperL(block);
                }
                block = HopperX(block, iterK[9]);
                initVector = block;
                for (int j = 0; j < 16; j++)
                {
                    encrText[blockInd + j] = block[j];
                }
            }
            return encrText;
        }

        public byte[] DecodeCBC(byte[] file, byte[] initVector)
        {
            int NumOfBlocks = file.Length >> 4;
            byte[] decrText = new byte[file.Length];
            for (int i = 0; i < NumOfBlocks; i++)
            {
                byte[] block = new byte[16];
                var blockInd = i << 4;
                for (int j = 0; j < 16; j++)
                {
                    block[j] = file[blockInd + j];
                }
                var newVect = block;
                block = HopperX(block, iterK[9]);
                for (int j = 8; j >= 0; j--)
                {
                    block = HopperLReverse(block);
                    block = HopperSReverse(block);
                    block = HopperX(block, iterK[j]);
                }
                block = HopperX(block, initVector);
                initVector = newVect;
                for (int j = 0; j < 16; j++)
                {
                    decrText[blockInd + j] = block[j];
                }
            }
            if (decrText[^1] == 129)
            {
                int zeroesEnd = decrText.Length - 1;
                while (decrText[zeroesEnd] != 1) zeroesEnd--;
                Array.Resize(ref decrText, zeroesEnd);
            }
            return decrText;
        }
    }
}