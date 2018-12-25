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
        /// </summary>en
        public SymmetricMatrix() { }

        /// <summary>
        /// Constructor with one parameter.
        /// </summary>
        /// <param name="size">Size of the matrix.</param>
        public SymmetricMatrix(int size)
        {
            if (size < 2)
            {
                throw new ArgumentException($"{nameof(size)} can't be less than 2.");
            }

            int sumSize = 0;
            Size = size;

            while (size != 0)
            {
                sumSize += size;
                size--;
            }

            matrixArray = new T[sumSize];
        }

        /// <summary>
        /// Adds custom matrix.
        /// </summary>
        /// <param name="matrix"></param>
        public void AddCustomMatrix(T[] matrix)
        {
            if (matrix.Length % 3 != 0)
            {
                throw new ArgumentException("Such matrix won't be symmetric.");
            }

            matrixArray = matrix;
        }

        protected override T GetElement(int i, int j)
        {
            return FindElem(i, j);
        }

        protected override void SetElement(T value, int i, int j)
        {
            if (i != j)
            {
                throw new InvalidOperationException("Such matrix won't be symmetric.");
            }

            if (i == 0)
            {
                matrixArray[0] = value;
                return;
            }

            int index = 0;
            i++;

            while(i > 0)
            {
                index += i;
                i--;
            }

            matrixArray[--index] = value;
        }

        private T FindElem(int i, int j)
        {
            T[,] tempArray = new T[Size, Size];
            int count = 1, counter = 0;

            for (int k = 0; k < Size; k++)
            {
                for (int m = 0; m < count; m++)
                {
                    tempArray[k, m] = matrixArray[counter];
                    counter++;
                }

                count++;
            }

            if(tempArray[i, j] == default)
            {
                return tempArray[j, i];
            }

            return tempArray[i, j];
        }
    }
}
