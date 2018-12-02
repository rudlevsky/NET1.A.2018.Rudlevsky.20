using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary.Matrixes
{
    public class DiagonalMatrix<T> : Matrix<T>
    {
        public DiagonalMatrix() { }

        public DiagonalMatrix(int size) : base(size) { }

        protected override T GetElement(int i, int j) => matrixArray[i, j];

        protected override void SetElement(T value, int i, int j)
        {
            if(i != j)
            {
                throw new InvalidOperationException("Only diagonal elements can be changed.");
            }

            matrixArray[i, j] = value;
        }

        public static SquareMatrix<T> operator +(DiagonalMatrix<T> diagonal, SquareMatrix<T> square)
        {
            CheckSizes(diagonal, square);

            return GenerateSquareMatrix(diagonal, square);
        }

        public static DiagonalMatrix<T> operator +(DiagonalMatrix<T> diagonal1, DiagonalMatrix<T> diagonal2)
        {
            CheckSizes(diagonal1, diagonal2);

            return GenerateDiagonalMatrix(diagonal1, diagonal2);
        }

        private static SquareMatrix<T> GenerateSquareMatrix(DiagonalMatrix<T> diagonal, SquareMatrix<T> square)
        {
            var matrix = new SquareMatrix<T>(diagonal.Size);

            for (int i = 0; i < diagonal.Size; i++)
            {
                for (int j = 0; j < diagonal.Size; j++)
                {
                    dynamic temp1 = diagonal[i, j], temp2 = square[i, j];

                    matrix[i, j] = temp1 + temp2;
                }
            }

            return matrix;
        }

        private static DiagonalMatrix<T> GenerateDiagonalMatrix(DiagonalMatrix<T> diagonal1, DiagonalMatrix<T> diagonal2)
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

        private static void CheckSizes(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            if (matrix1.Size != matrix2.Size)
            {
                throw new InvalidOperationException("Sizes of the matrixes are not equal.");
            }
        }
    }
}
