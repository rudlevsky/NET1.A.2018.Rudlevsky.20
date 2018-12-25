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
        /// <param name="size">Size of the matrix.</param>
        public SquareMatrix(int size)
        {
            if (size < 2)
            {
                throw new ArgumentException($"{nameof(size)} can't be less than 2.");
            }

            matrixArray = new T[size * size];
            Size = size;
        }

        protected override T GetElement(int i, int j)
        {
            return matrixArray[FindIndex(i, j)];
        }

        protected override void SetElement(T value, int i, int j)
        {
            matrixArray[FindIndex(i, j)] = value;
        }

        private int FindIndex(int i, int j)
        {
            return ((i * Size)) + j;
        }
    }
}
