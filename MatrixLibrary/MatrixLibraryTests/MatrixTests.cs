using System;
using MatrixLibrary;
using MatrixLibrary.Matrixes;
using MatrixLibrary.Models;
using NUnit.Framework;

namespace MatrixLibraryTests
{
    [TestFixture]
    public class MatrixTests
    {
        private string data;

        private void GetData(object obj, DataEventArgs e)
        {
            data = e.message;
        }

        private void AssertMatrixTest_MatrixAndExpectedResult(Matrix<int> matrix, int[] expectedResult)
        {
            int count = 0;

            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    Assert.AreEqual(matrix[i, j], expectedResult[count]);
                    count++;
                }
            }
        }

        [Test]
        public void CheckSizeTest_StandardSize()
        {
            const int STANDARD_SIZE = 5;

            var matrix = new SquareMatrix<int>();

            Assert.AreEqual(STANDARD_SIZE, matrix.Size);
        }

        [Test]
        public void CheckSizeTest_CustomSize()
        {
            const int CUSTOM_SIZE = 10;

            var matrix = new SquareMatrix<int>(CUSTOM_SIZE);

            Assert.AreEqual(CUSTOM_SIZE, matrix.Size);
        }

        [Test]
        public void MethodPrintTest_StandardResult()
        {
            var matrix = new SquareMatrix<int>(2);

            string print = "0 0 " + Environment.NewLine + "0 0 " + Environment.NewLine;

            Assert.AreEqual(print, matrix.PrintMatrix());
        }

        [Test]
        public void MethodPrintTest_CustomResult()
        {
            var matrix = new SquareMatrix<int>(2);

            matrix[0, 0] = 1;
            matrix[0, 1] = 1;

            string print = "1 1 " + Environment.NewLine + "0 0 " + Environment.NewLine;

            Assert.AreEqual(print, matrix.PrintMatrix());
        }

        [Test]
        public void EventsTest_CorrectMessage()
        {
            const string STANDARD_MESSAGE = "Element was changed.";
            var matrix = new SquareMatrix<int>(2);
            data = null;

            matrix.Set += GetData;         
            matrix[0, 0] = 1;
           
            Assert.AreEqual(STANDARD_MESSAGE, data);
        }

        [Test]
        public void SumTest_SquarePlusSquare_Square()
        {
            var matrix1 = new SquareMatrix<int>(2);
            matrix1[0, 0] = 1;

            var matrix2 = new SquareMatrix<int>(2);
            matrix2[0, 0] = 1;
            matrix2[0, 1] = 2;

            var matrix = matrix1 + matrix2;

            int[] array = { 2, 2, 0, 0 };

            AssertMatrixTest_MatrixAndExpectedResult(matrix, array);
        }

        [Test]
        public void SumTest_SquarePlusSymmetric_Square()
        {
            var matrix1 = new SquareMatrix<int>(2);
            matrix1[0, 0] = 1;

            var matrix2 = new SymmetricMatrix<int>(2);
            matrix2[0, 0] = 1;
            matrix2[1, 1] = 2;

            var matrix = matrix1 + matrix2;

            int[] array = { 2, 0, 0, 2 };

            AssertMatrixTest_MatrixAndExpectedResult(matrix, array);
        }

        [Test]
        public void SumTest_DiagonalPlusDiagonal_Diagonal()
        {
            var matrix1 = new DiagonalMatrix<int>(2);
            matrix1[0, 0] = 1;
            matrix1[1, 1] = 2;

            var matrix2 = new DiagonalMatrix<int>(2);
            matrix2[0, 0] = 1;
            matrix2[1, 1] = 2;

            var matrix = matrix1 + matrix2;

            int[] array = { 2, 0, 0, 4 };

            AssertMatrixTest_MatrixAndExpectedResult(matrix, array);
        }

        [Test]
        public void SumTest_DiagonalPlusSquare_Square()
        {
            var matrix1 = new DiagonalMatrix<int>(2);
            matrix1[0, 0] = 1;
            matrix1[1, 1] = 2;

            var matrix2 = new SquareMatrix<int>(2);
            matrix2[1, 0] = 2;
            matrix2[1, 1] = 2;

            var matrix = matrix1 + matrix2;

            int[] array = { 1, 0, 2, 4 };

            AssertMatrixTest_MatrixAndExpectedResult(matrix, array);
        }

        [Test]
        public void SumTest_SymmetricPlusDiagonal_Symmetric()
        {
            var matrix1 = new SymmetricMatrix<int>(2);

            matrix1.AddCustomMatrix(new int[,] { { 1, 2 }, { 2, 1 } });

            var matrix2 = new DiagonalMatrix<int>(2);
            matrix2[0, 0] = 1;
            matrix2[1, 1] = 1;

            var matrix = matrix1 + matrix2;

            int[] array = { 2, 2, 2, 2 };

            AssertMatrixTest_MatrixAndExpectedResult(matrix, array);
        }

        [Test]
        public void SumTest_SymmetricPlusSymmetric_Symmetric()
        {
            var matrix1 = new SymmetricMatrix<int>(2);

            matrix1.AddCustomMatrix(new int[,] { { 1, 2 }, { 2, 1 } });

            var matrix2 = new SymmetricMatrix<int>(2);

            matrix2.AddCustomMatrix(new int[,] { { 1, 2 }, { 2, 1 } });

            var matrix = matrix1 + matrix2;

            int[] array = { 2, 4, 4, 2 };

            AssertMatrixTest_MatrixAndExpectedResult(matrix, array);
        }

        [Test]
        public void ValidationTest_UncorrectDataFirstIndex_ArgumentException()
        {
            var matrix = new SquareMatrix<int>(2);

            Assert.Throws<ArgumentException>(() => matrix[-1, 0] = 5);
        }

        [Test]
        public void ValidationTest_UncorrectDataSecondIndex_ArgumentException()
        {
            var matrix = new SquareMatrix<int>(2);

            Assert.Throws<ArgumentException>(() => matrix[0, -1] = 5);         
        }

        [Test]
        public void ValidationTest_UncorrectMatrixSize_ArgumentException()
            => Assert.Throws<ArgumentException>(() => new SquareMatrix<int>(0));

        [Test]
        public void ValidationTest_SquarePlusSquare_InvalidOperationException()
        {
            var matrix1 = new SquareMatrix<int>(2);
            var matrix2 = new SquareMatrix<int>(3);
            SquareMatrix<int> matrix;

            Assert.Throws<InvalidOperationException>(() => matrix = matrix1 + matrix2);
        }

        [Test]
        public void ValidationTest_SquarePlusSymmetric_InvalidOperationException()
        {
            var matrix1 = new SquareMatrix<int>(2);
            var matrix2 = new SymmetricMatrix<int>(3);
            SquareMatrix<int> matrix;

            Assert.Throws<InvalidOperationException>(() => matrix = matrix1 + matrix2);
        }

        [Test]
        public void ValidationTest_SymmetricUncorrectValue_InvalidOperationException()
        {
            var matrix = new SymmetricMatrix<int>(3);

            Assert.Throws<InvalidOperationException>(() => matrix[0, 1] = 5);
        }

        [Test]
        public void ValidationTest_SymmetricPlusSymmetric_InvalidOperationException()
        {
            var matrix1 = new SymmetricMatrix<int>(2);
            var matrix2 = new SymmetricMatrix<int>(3);
            SymmetricMatrix<int> matrix;

            Assert.Throws<InvalidOperationException>(() => matrix = matrix1 + matrix2);
        }

        [Test]
        public void ValidationTest_SymmetricPlusDiagonal_InvalidOperationException()
        {
            var matrix1 = new SymmetricMatrix<int>(2);
            var matrix2 = new DiagonalMatrix<int>(3);
            SymmetricMatrix<int> matrix;

            Assert.Throws<InvalidOperationException>(() => matrix = matrix1 + matrix2);
        }

        [Test]
        public void ValidationTest_DiagonalUncorrectValue_InvalidOperationException()
        {
            var matrix = new DiagonalMatrix<int>(3);

            Assert.Throws<InvalidOperationException>(() => matrix[0, 1] = 5);
        }

        [Test]
        public void ValidationTest_DiagonalPlusDiagonal_InvalidOperationException()
        {
            var matrix1 = new DiagonalMatrix<int>(2);
            var matrix2 = new DiagonalMatrix<int>(3);
            DiagonalMatrix<int> matrix;

            Assert.Throws<InvalidOperationException>(() => matrix = matrix1 + matrix2);
        }

        [Test]
        public void ValidationTest_DiagonalPlusSquare_InvalidOperationException()
        {
            var matrix1 = new DiagonalMatrix<int>(2);
            var matrix2 = new SquareMatrix<int>(3);
            SquareMatrix<int> matrix;

            Assert.Throws<InvalidOperationException>(() => matrix = matrix1 + matrix2);
        }
    }
}
