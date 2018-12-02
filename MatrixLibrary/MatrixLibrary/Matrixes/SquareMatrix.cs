using System;

namespace MatrixLibrary.Matrixes
{
    /// <summary>
    /// Class of the square matrix.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SquareMatrix<T> : Matrix<T>
    {
        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public SquareMatrix() { }

        /// <summary>
        /// Constructor with one parameter.
        /// </summary>
        public SquareMatrix(int size) : base(size) { }

        /// <summary>
        /// Add operation for two operands.
        /// </summary>
        /// <param name="square">First operand.</param>
        /// <param name="symmetric">Second operand.</param>
        /// <returns>Result of adding.</returns>
        public static SquareMatrix<T> operator +(SquareMatrix<T> square, SymmetricMatrix<T> symmetric)
        {
            CheckSizes(square, symmetric);

            return GenerateMatrix(symmetric, square);
        }

        /// <summary>
        /// Add operation for two operands.
        /// </summary>
        /// <param name="square1">First operand.</param>
        /// <param name="dsquare2">Second operand.</param>
        /// <returns>Result of adding.</returns>
        public static SquareMatrix<T> operator +(SquareMatrix<T> square1, SquareMatrix<T> square2)
        {
            CheckSizes(square1, square2);

            return GenerateMatrix(square1, square2);
        }

        protected override T GetElement(int i, int j) => matrixArray[i, j];

        protected override void SetElement(T value, int i, int j) => matrixArray[i, j] = value;

        private static SquareMatrix<T> GenerateMatrix(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            var matrix = new SquareMatrix<T>(matrix1.Size);

            for (int i = 0; i < matrix1.Size; i++)
            {
                for (int j = 0; j < matrix2.Size; j++)
                {
                    dynamic temp1 = matrix1[i, j], temp2 = matrix2[i, j];

                    matrix[i, j] = temp1 + temp2;
                }
            }

            return matrix;
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
