using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNeuralNetwork
{
    public class MatrixFloat
    {
        public int Rows { get; }
        public int Columns { get; }
        private float[] _array;

        public MatrixFloat(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _array = new float[rows * columns];
        }

        public MatrixFloat(float[,] array)
        {
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            _array = new float[Rows * Columns];
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    this[y, x] = array[y, x];
                }
            }
        }

        public float this[int row, int column]
        {
            get
            {
                return _array[row * Columns + column];
            }
            set
            {
                _array[row * Columns + column] = value;
            }
        }

        public MatrixFloat Apply(Func<float, float> func)
        {
            var a = new MatrixFloat(Rows, Columns);
            for (int i = 0; i < Rows * Columns; i++)
            {
                a._array[i] = func(_array[i]);
            }
            return a;
        }

        public void Clear()
        {
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = 0;
            }
        }

        public MatrixFloat Add(MatrixFloat other)
        {
#if DEBUG
            if (Columns != other.Columns || Rows != other.Rows)
                throw new Exception($"Columns {Columns} does not equal other.Columns {other.Columns} or Rows {Rows} does not equal other.Rows {other.Rows}");
#endif

            var a = new MatrixFloat(Rows, Columns);
            for (int i = 0; i < Rows * Columns; i++)
            {
                a._array[i] = _array[i] + other._array[i];
            }
            return a;
        }

        public MatrixFloat Substract(MatrixFloat other)
        {
#if DEBUG
            if (Columns != other.Columns || Rows != other.Rows)
                throw new Exception($"Columns {Columns} does not equal other.Columns {other.Columns} or Rows {Rows} does not equal other.Rows {other.Rows}");
#endif

            var a = new MatrixFloat(Rows, Columns);
            for (int i = 0; i < Rows * Columns; i++)
            {
                a._array[i] = _array[i] - other._array[i];
            }
            return a;
        }

        public MatrixFloat Multiply(MatrixFloat other)
        {
#if DEBUG
            if (Columns != other.Rows)
                throw new Exception($"Columns {Columns} does not equal other.Rows {other.Rows}");
#endif

            var result = new MatrixFloat(Rows, other.Columns);

            for (int cColumn = 0; cColumn < result.Columns; cColumn++)
            {
                for (int cRow = 0; cRow < result.Rows; cRow++)
                {
                    for (int i = 0; i < Columns; i++)
                    {
                        result[cRow, cColumn] += this[cRow, i] * other[i, cColumn];
                    }
                }
            }

            return result;
        }

        public MatrixFloat HadamardProduct(MatrixFloat other)
        {
#if DEBUG
            if (Columns != other.Columns || Rows != other.Rows)
                throw new Exception($"Columns {Columns} does not equal other.Columns {other.Columns} or Rows {Rows} does not equal other.Rows {other.Rows}");
#endif

            var a = new MatrixFloat(Rows, Columns);
            for (int i = 0; i < Rows * Columns; i++)
            {
                a._array[i] = _array[i] * other._array[i];
            }
            return a;
        }

        public MatrixFloat Transpose()
        {
            var t = new MatrixFloat(Columns, Rows);
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    t[column, row] = this[row, column];
                }
            }
            return t;
        }

        public override string ToString()
        {
            var str = "";
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    str += this[row, column] + " ";
                }
                str += "\n";
            }
            return str;
        }

        public static MatrixFloat operator +(MatrixFloat m1, MatrixFloat m2)
        {
            return m1.Add(m2);
        }

        public static MatrixFloat operator -(MatrixFloat m1, MatrixFloat m2)
        {
            return m1.Substract(m2);
        }

        public static MatrixFloat operator *(MatrixFloat m1, float f)
        {
            return m1.Apply((x) => x * f);
        }

        public static MatrixFloat operator *(float f, MatrixFloat m1)
        {
            return m1.Apply((x) => x * f);
        }

        public static MatrixFloat operator *(MatrixFloat m1, MatrixFloat m2)
        {
            return m1.HadamardProduct(m2);
        }
    }
}
