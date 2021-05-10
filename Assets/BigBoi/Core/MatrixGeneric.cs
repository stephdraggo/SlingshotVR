using System;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi
{
    public class MatrixGeneric : MonoBehaviour
    {
        public int rowCount, columnCount;

        public UnityEngine.Object[,] matrix;

        [Serializable]
        public struct Row
        {
            public UnityEngine.Object[] boxes;
        }

        public Row[] rows;

        private void Start()
        {
            if (rowCount < rows.Length) rowCount = rows.Length;
            for (int i = 0; i < rowCount; i++)
            {
                if (columnCount < rows[i].boxes.Length) columnCount = rows[i].boxes.Length;
            }

            matrix = new UnityEngine.Object[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (j < rows[i].boxes.Length)
                    {
                        matrix[i, j] = rows[i].boxes[j];
                    }
                    else
                    {
                        matrix[i, j] = new UnityEngine.Object();
                    }
                }
            }
        }

    }
}