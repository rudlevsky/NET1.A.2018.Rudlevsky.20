using MatrixLibrary.Models;
using System;

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
        public int Size { get; set; }

        /// <summary>
        /// Event of adding an element.
        /// </summary>
        public event EventHandler<DataEventArgs> Set;

        protected T[] matrixArray;

        private const int STANDARD_SIZE = 9;

        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public Matrix()
        {
            matrixArray = new T[STANDARD_SIZE];
            Size = STANDARD_SIZE;
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
