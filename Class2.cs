using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dip
{
    static class Functions
    {
        class ss
        {
            static float[] InverseMatrix(float[] InMatrix)
            {
                int i = 0;
                int j = 0;
                float inverseVariable = 0;
                for (int i = 0; i < InMatrix.GetLength.i; i++)
                {
                    for (int j = 0; j < InMatrix.GetLength.j; j++)
                    {
                        inverseVariable = InMatrix[i, j];
                        InMatrix[i, j] = (1 / inverseVariable);
                    }
                }

            }
            float[] mass = new float { { 1, 3, 5 }, { 2, 4, 6 }, { 7, 8, 9 } };
            Console.WriteLine(mass);
        }
    }
}

