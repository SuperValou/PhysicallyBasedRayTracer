using System;
using System.Numerics;
using Pbrt.Core;

namespace Pbrt.Shapes
{
    public class Triangle
    {
        /// <summary>
        /// The index of this triangle in the mesh it belongs to.
        /// </summary>
        public int Index { get; }

        public Triangle(int index)
        {
            Index = index;
        }
    }

    public class TriangleMesh
    {
        private readonly float[] _vertexPositions;
        private readonly float[] _vertexNormals;
        private readonly float[] _vertexUVs;

        public int VertexCount { get; }

        public int TriangleCount { get; set; }

        public TriangleMesh(Transform transform, int triangleCount, float[] vertexPositions, float[] vertexNormals, float[] vertexUVs)
        {
            _vertexPositions = vertexPositions ?? throw new ArgumentNullException(nameof(vertexPositions));
            VertexCount = _vertexPositions.Length / 3;
            if (_vertexPositions.Length % 3 != 0)
            {
                throw new ArgumentException($"The '{nameof(vertexPositions)}' parameter is supposed to hold the x,y,z position of each vertex, "
                                            + "meaning the array length is expected to be a multiple of 3, "
                                            + $"but it was a length of {vertexPositions.Length} instead.");
            }

            for (int i = 0; i < _vertexPositions.Length; i++)
            {
                throw new NotImplementedException("FIX ME");
                //_vertexPositions[i] = transform.ApplyToPoint(_vertexPositions[i]);
            }
                

            _vertexNormals = vertexNormals ?? throw new ArgumentNullException(nameof(vertexNormals));
            if (_vertexNormals.Length != _vertexPositions.Length)
            {
                throw new ArgumentException($"'{nameof(vertexNormals)}' doesn't have the same number of vertices as '{nameof(vertexPositions)}'.");
            }

            _vertexUVs = vertexUVs ?? throw new ArgumentNullException(nameof(vertexUVs));
            if (_vertexUVs.Length != 2 * VertexCount)
            {
                throw new ArgumentException($"'{nameof(vertexUVs)}' doesn't have the same number of vertices as '{nameof(vertexPositions)}'.");
            }
        }

        public Vector3 GetVertexPosition(int vertexIndex)
        {
            if (vertexIndex >= VertexCount)
            {
                throw new ArgumentException($"Unable to get vertex at index {vertexIndex} "
                                            + $"because there is only {VertexCount} vertices in this mesh.");
            }

            float x = _vertexPositions[3 * vertexIndex];
            float y = _vertexPositions[3 * vertexIndex + 1];
            float z = _vertexPositions[3 * vertexIndex + 2];
            return new Vector3(x, y, z);
        }

        public Vector3 GetVertexNormal(int vertexIndex)
        {
            if (vertexIndex >= VertexCount)
            {
                throw new ArgumentException($"Unable to get vertex at index {vertexIndex} "
                                            + $"because there is only {VertexCount} vertices in this mesh.");
            }

            float x = _vertexNormals[3 * vertexIndex];
            float y = _vertexNormals[3 * vertexIndex + 1];
            float z = _vertexNormals[3 * vertexIndex + 2];
            return new Vector3(x, y, z);
        }

        public Vector2 GetVertexUV(int vertexIndex)
        {
            if (vertexIndex >= VertexCount)
            {
                throw new ArgumentException($"Unable to get vertex at index {vertexIndex} "
                                            + $"because there is only {VertexCount} vertices in this mesh.");
            }

            float u = _vertexUVs[2 * vertexIndex];
            float v = _vertexUVs[2 * vertexIndex + 1];
            return new Vector2(u, v);
        }
    }
}
