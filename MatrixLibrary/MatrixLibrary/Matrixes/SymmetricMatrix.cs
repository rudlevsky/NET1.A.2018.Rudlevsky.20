using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary.Matrixes
{
    public class SymmetricMatrix<T> : Matrix<T>
    {
        public SymmetricMatrix() { }

        public SymmetricMatrix(int size) : base(size) { }

        protected override T GetElement(int i, int j) => matrixArray[i, j];

        protected override void SetElement(T value, int i, int j)
        {
            if (i != j)
            {
                throw new InvalidOperationException("Such matrix won't be symmetric.");
            }

            matrixArray[i, j] = value;
        }

        public void AddCustomMatrix(T[,] matrix)
        {
            if(CheckCustomMatrix(matrix))
            {
                matrixArray = matrix;
            } 
            else
            {
                throw new InvalidOperationException($"{nameof(matrix)} is not symmetric.");
            }
        }
        
        private bool CheckCustomMatrix(T[,] matrix)
        {
            int count = 0;
            int length = (int)Math.Sqrt(matrix.Length);

            for (int i = 0; i < length; i++)
            { 
                for (int j = count; j < length - i; j++)
                {
                    if (i != j)
                    {
                        dynamic temp1 = matrix[i, j];
                        dynamic temp2 = matrix[j, i];

                        if (temp1 != temp2)
                        {
                            return false;
                        }
                    }
                }

                count++;
            }

            return true;
        }

        public static SymmetricMatrix<T> operator +(SymmetricMatrix<T> symmetric, DiagonalMatrix<T> diagonal)
        {
            CheckSizes(symmetric, diagonal);

            return GenerateSymmetric(symmetric, diagonal);
        }

        public static SymmetricMatrix<T> operator +(SymmetricMatrix<T> symmetric1, SymmetricMatrix<T> symmetric2)
        {
            CheckSizes(symmetric1, symmetric2);

            return GenerateSymmetric(symmetric1, symmetric2);
        }

        private static SymmetricMatrix<T> GenerateSymmetric(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            var matrix = new T[matrix1.Size, matrix1.Size];

            for (int i = 0; i < matrix1.Size; i++)
            {
                for (int j = 0; j < matrix2.Size; j++)
                {
                    dynamic temp1 = matrix1[i, j], temp2 = matrix2[i, j];

                    matrix[i, j] = temp1 + temp2;
                }
            }

            var symmetric = new SymmetricMatrix<T>(matrix1.Size);
            symmetric.AddCustomMatrix(matrix);

            return symmetric;
        }

        private static void CheckSizes(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1.Size != matrix2.Size)
            {
                throw new InvalidOperationException("Sizes of the matrixes are not equal.");
            }
        }
    }
}
