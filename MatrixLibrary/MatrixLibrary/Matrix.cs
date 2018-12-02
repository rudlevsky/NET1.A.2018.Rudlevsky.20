using MatrixLibrary.Models;
using System;
using System.Text;

namespace MatrixLibrary
{
    /// <summary>
    /// Abstract class for all matrixes.
    /// </summary>
    /// <typeparam name="T">Type of the metrix.</typeparam>
    public abstract class Matrix<T>
    {
        /// <summary>
        /// Size of the matrix.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Event of adding an element.
        /// </summary>
        public event EventHandler<DataEventArgs> Set;

        protected T[,] matrixArray;

        private const int STANDARD_SIZE = 5;

        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public Matrix()
        {
            matrixArray = new T[STANDARD_SIZE, STANDARD_SIZE];
            Size = STANDARD_SIZE;
        }

        /// <summary>
        /// Constructor with one parameter.
        /// </summary>
        /// <param name="size">Size of the matrix.</param>
        public Matrix(int size)
        {
            if (size < 2)
            {
                throw new ArgumentException($"{nameof(size)} can't be less than 2.");
            }

            matrixArray = new T[size, size];
            Size = size;
        }

        /// <summary>
        /// Indexator of the matrix.
        /// </summary>
        /// <param name="i">First index of the matrix.</param>
        /// <param name="j">Second index of the matrix.</param>
        /// <returns>Element of the matrix.</returns>
        public T this[int i, int j]
        {
            get
            {
                ValidateArguments(i, j);
                return GetElement(i, j);
            }
            set
            {
                ValidateArguments(i, j);
                SetElement(value, i, j);
                OnSet(new DataEventArgs());
            }
        }

        /// <summary>
        /// Prints all matrix.
        /// </summary>
        /// <returns>Printed matrix.</returns>
        public virtual string PrintMatrix()
        {
            var builder = new StringBuilder();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    builder.Append(matrixArray[i, j] + " ");
                }

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }

        protected abstract T GetElement(int i, int j);
        protected abstract void SetElement(T value, int i, int j);

        protected virtual void OnSet(DataEventArgs e)
        {
            Set?.Invoke(this, e);
        }

        private void ValidateArguments(int i, int j)
        {
            if (i < 0 || i > Size)
            {
                throw new ArgumentException($"{nameof(i)} was incorrect.");
            }

            if (j < 0 || j > Size)
            {
                throw new ArgumentException($"{nameof(j)} was incorrect.");
            }
        }
    }
}
