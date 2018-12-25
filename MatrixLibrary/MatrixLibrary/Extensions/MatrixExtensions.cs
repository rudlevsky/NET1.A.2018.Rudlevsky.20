using MatrixLibrary.Matrixes;
using System;
using System.Collections.Generic;

namespace MatrixLibrary.Extensions
{
    public static class MatrixExtensions
    {
        public static Matrix<T> Add<T>(this Matrix<T> matrix1, Matrix<T> matrix2)
        {
            CheckSizes(matrix1, matrix2);

            dynamic matrixTemp1 = matrix1, matrixTemp2 = matrix2;

            return GenerateMatrix(matrixTemp1, matrixTemp2);
        }

        private static SquareMatrix<T> GenerateMatrix<T>(SquareMatrix<T> matrix1, SymmetricMatrix<T> matrix2)
            => GetSquareMatrix<T>(matrix1, matrix2);

        private static SquareMatrix<T> GenerateMatrix<T>(SymmetricMatrix<T> matrix2, SquareMatrix<T> matrix1)
            => GetSquareMatrix<T>(matrix1, matrix2);

        private static SquareMatrix<T> GenerateMatrix<T>(DiagonalMatrix<T> matrix1, SquareMatrix<T> matrix2)
            => GetSquareMatrix<T>(matrix1, matrix2);

        private static SquareMatrix<T> GenerateMatrix<T>(SquareMatrix<T> matrix2, DiagonalMatrix<T> matrix1)
            => GetSquareMatrix<T>(matrix1, matrix2);

        private static SquareMatrix<T> GenerateMatrix<T>(SquareMatrix<T> matrix1, SquareMatrix<T> matrix2)
            => GetSquareMatrix<T>(matrix1, matrix2);

        private static SymmetricMatrix<T> GenerateMatrix<T>(SymmetricMatrix<T> matrix1, DiagonalMatrix<T> matrix2)
            => GenerateSymmetric(matrix1, matrix2);

        private static SymmetricMatrix<T> GenerateMatrix<T>(DiagonalMatrix<T> matrix1, SymmetricMatrix<T> matrix2)
            => GenerateSymmetric(matrix1, matrix2);

        private static SymmetricMatrix<T> GenerateMatrix<T>(SymmetricMatrix<T> symmetric1, SymmetricMatrix<T> symmetric2)
            => GenerateSymmetric(symmetric1, symmetric2);

        private static DiagonalMatrix<T> GenerateMatrix<T>(DiagonalMatrix<T> diagonal1, DiagonalMatrix<T> diagonal2)
        {
            var matrix = new DiagonalMatrix<T>(diagonal1.Size);
            int j = 0;

            for (int i = 0; i < diagonal1.Size; i++)
            {
                dynamic temp1 = diagonal1[i, j], temp2 = diagonal2[i, j];

                matrix[i, j] = temp1 + temp2;
                j++;
            }

            return matrix;
        }

        private static SquareMatrix<T> GetSquareMatrix<T>(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            var matrix = new SquareMatrix<T>(matrix1.Size);

            for (int i = 0; i < matrix1.Size; i++)
            {
                for (int j = 0; j < matrix1.Size; j++)
                {
                    dynamic temp1 = matrix1[i, j], temp2 = matrix2[i, j];

                    matrix[i, j] = temp1 + temp2;
                }
            }

            return matrix;
        }

        private static SymmetricMatrix<T> GenerateSymmetric<T>(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            var matrix = new List<T>();
            int count = 1;

            for (int k = 0; k < matrix1.Size; k++)
            {
                for (int m = 0; m < count; m++)
                {
                    dynamic temp1 = matrix1[k, m], temp2 = matrix2[k, m];
                    matrix.Add(temp1 + temp2);
                }

                count++;
            }

            var symmetric = new SymmetricMatrix<T>(matrix1.Size);
            symmetric.AddCustomMatrix(matrix.ToArray());

            return symmetric;
        }

        private static void CheckSizes<T>(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1.Size != matrix2.Size)
            {
                throw new InvalidOperationException("Sizes of the matrixes are not equal.");
            }
        }
    }
}
