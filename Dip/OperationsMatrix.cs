using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Dip
{
    public class OperationsMatrix
    {
        public Matrix<double> DirectCosts(Matrix<double> branch, Matrix<double> X)
        {
            //Вычисление матрицы коэффициентов прямых затрат  
            Matrix<double> A = Matrix<double>.Build.DenseOfMatrix(branch);
            for (int i = 0; i < branch.RowCount; i++)
            {
                for (int j = 0; j < branch.ColumnCount; j++)
                {
                    A[i, j] = branch[i, j] / X[j, 0];
                }

            }
            return A;
        }
        public Matrix<double> CostsManufacturing(Matrix<double> A)
        {
            //Вычисление матрицы "затраты-выпуск"
            var E = Matrix<double>.Build.Diagonal(A.RowCount, A.ColumnCount, 1);
            return E.Subtract(A);
        }

        public Matrix<double> PlanInterFlow(Matrix<double> A, Matrix<double> X1)
        {
            //Вычисление плановых объёмов межотраслевых потоков:
            Matrix<double> x = Matrix<double>.Build.DenseOfMatrix(A);
            for (int i = 0; i < A.RowCount; i++)
            {
                for (int j = 0; j < A.ColumnCount; j++)
                {
                    x[i, j] = A[i, j] * X1[j, 0];
                }

            }
            return x;
        }

        public Matrix<double> PlanNetProd(Matrix<double> X1, Matrix<double> x)
        {
            //Вычисление плановых объёмов чистой продукции:
            Matrix<double> W = Matrix<double>.Build.DenseOfMatrix(X1);
            for (int i = 0; i < x.ColumnCount; i++)
            {
                
                W[0, i] = X1[0, i] - x.Column(i).Sum();
            }
            return W;
        }

    }
}