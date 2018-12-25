using System;

namespace MatrixLibrary.Matrixes
{
    /// <summary>
    /// Class of the diagonal matrix.
    /// </summary>
    /// <typeparam name="T">Type of the matrix arguments.</typeparam>
    public class DiagonalMatrix<T> : Matrix<T>
    {
        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public DiagonalMatrix() { }

        /// <summary>
        /// Constructor with one parameter.
        /// </summary>
        /// <param name="size">Size of the matrix.</param>
        public DiagonalMatrix(int size)
        {
            if (size < 2)
            {
                throw new ArgumentException($"{nameof(size)} can't be less than 2.");
            }

            matrixArray = new T[size];
            Size = size;
        }

        protected override T GetElement(int i, int j)
        {
            if (i != j) return default;

            return matrixArray[i];
        }

        protected override void SetElement(T value, int i, int j)
        {
            if(i != j)
            {
                throw new InvalidOperationException("Only diagonal elements can be changed.");
            }

            matrixArray[i] = value;
        }
    }
}
