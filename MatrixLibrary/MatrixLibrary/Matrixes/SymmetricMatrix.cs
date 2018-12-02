using System;

namespace MatrixLibrary.Matrixes
{
    /// <summary>
    /// Class for symmetric matrix.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SymmetricMatrix<T> : Matrix<T>
    {
        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public SymmetricMatrix() { }

        /// <summary>
        /// Constructor with one parameter.
        /// </summary>
        public SymmetricMatrix(int size) : base(size) { }

        /// <summary>
        /// Adds custom matrix.
        /// </summary>
        /// <param name="matrix"></param>
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

        /// <summary>
        /// Add operation for two operands.
        /// </summary>
        /// <param name="symmetric">First operand.</param>
        /// <param name="diagonal">Second operand.</param>
        /// <returns>Result of adding.</returns>
        public static SymmetricMatrix<T> operator +(SymmetricMatrix<T> symmetric, DiagonalMatrix<T> diagonal)
        {
            CheckSizes(symmetric, diagonal);

            return GenerateSymmetric(symmetric, diagonal);
        }

        /// <summary>
        /// Add operation for two operands.
        /// </summary>
        /// <param name="symmetric1">First operand.</param>
        /// <param name="symmetric2">Second operand.</param>
        /// <returns>Result of adding.</returns>
        public static SymmetricMatrix<T> operator +(SymmetricMatrix<T> symmetric1, SymmetricMatrix<T> symmetric2)
        {
            CheckSizes(symmetric1, symmetric2);

            return GenerateSymmetric(symmetric1, symmetric2);
        }

        protected override T GetElement(int i, int j) => matrixArray[i, j];

        protected override void SetElement(T value, int i, int j)
        {
            if (i != j)
            {
                throw new InvalidOperationException("Such matrix won't be symmetric.");
            }

            matrixArray[i, j] = value;
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
    }
}
