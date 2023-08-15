using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Services
{
    public static class _3dObjectConstructor
    {
        // Methods for creating parallelepipeds
        public static Mesh CreateFlatSidePipe(float sizeByX, float height, float sizeByZ, float radius)
        {
            Vector3 vectorByWidth = new(sizeByX, 0, 0);
            Vector3 vectorByHeight = new(0, height, 0);
            Vector3 vectorByLength = new(0, 0, sizeByZ - radius * 2);

            Mesh mesh = new();

            var corner0 = -vectorByWidth / 2 - vectorByLength / 2 - vectorByHeight / 2;
            var corner1 = vectorByWidth / 2 + vectorByLength / 2 + vectorByHeight / 2;

            var combine = new CombineInstance[6];
            combine[0].mesh = Quad(corner0, vectorByLength, vectorByWidth);
            combine[1].mesh = Quad(corner0, vectorByWidth, vectorByHeight);
            combine[2].mesh = Quad(corner0, vectorByHeight, vectorByLength);
            combine[3].mesh = Quad(corner1, -vectorByWidth, -vectorByLength);
            combine[4].mesh = Quad(corner1, -vectorByHeight, -vectorByWidth);
            combine[5].mesh = Quad(corner1, -vectorByLength, -vectorByHeight);

            mesh.CombineMeshes(combine, true, false);
            return mesh;
        }

        private static Mesh Quad(Vector3 origin, Vector3 width, Vector3 length)
        {
            var normal = Vector3.Cross(length, width).normalized;
            var mesh = new Mesh
            {
                vertices = new[] { origin, origin + length, origin + length + width, origin + width },
                normals = new[] { normal, normal, normal, normal },
                uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) },
                triangles = new[] { 0, 1, 2, 0, 2, 3 }
            };
            return mesh;
        }

        // Methods for creating rounded corner

        public static Mesh CreateRoundedCorner(float xSize, float zSize, float ySize, float thickness, float roundness)
        {
            int scale = 10;
            int xSizeInt = (int)Math.Round(xSize * scale, 1, MidpointRounding.AwayFromZero);
            int zSizeInt = (int)Math.Round(zSize * scale, 1, MidpointRounding.AwayFromZero);
            int ySizeInt = (int)Math.Round(ySize * scale, 1, MidpointRounding.AwayFromZero);
            int thicknessInt = (int)Math.Round(thickness * scale, 1, MidpointRounding.AwayFromZero);
            int radiusInt = (int)Math.Round(roundness * scale, 1, MidpointRounding.AwayFromZero);
            Mesh mesh = new();
            int[] ySizes = { 0, ySizeInt };
            Vector3[] vertices = null;
            Vector3[] normals = null;
            CreateVertices(vertices, normals, mesh, xSizeInt, zSizeInt, thicknessInt, ySizes, radiusInt);
            CreateTriangles(xSizeInt, zSizeInt, thicknessInt, mesh);
            return mesh;
        }

        private static void CreateVertices(Vector3[] vertices, Vector3[] normals, Mesh mesh, int xSize, int zSize, int thickness, int[] ySizes, int roundness)
        {
            int cornerVertices = 12;
            int edgeVertices = (xSize + zSize - 2) * 2 + (xSize + zSize - 2 * thickness - 2) * 2;
            int faceVertices = 0;
            vertices ??= new Vector3[cornerVertices + edgeVertices + faceVertices];
            normals ??= new Vector3[vertices.Length];
            int v = 0;
            int i = 0;
            for (int y = 0; y < ySizes.Length; y++) // external rounded side
            {
                for (int x = 0; x <= xSize; x++)
                {
                    i++;
                    SetVertex(v++, x, ySizes[y], 0, vertices, xSize, zSize, thickness, roundness, normals);
                    //vertices[v++] = new Vector3(x, ySizes[y], 0);
                    Debug.Log($"{x}\t{ySizes[y]}\t{i}");
                    // yield return wait;
                }
                for (int z = 1; z <= zSize; z++)
                {
                    i++;
                    SetVertex(v++, xSize, ySizes[y], z, vertices, xSize, zSize, thickness, roundness, normals);
                    //vertices[v++] = new Vector3(xSize, ySizes[y], z);
                    Debug.Log($"{z}\t{ySizes[y]}\t{i}");
                    // yield return wait;
                }
            }
            for (int y = ySizes.Length - 1; y >= 0; y--) // internal rounded side
            {
                for (int x = 0; x <= xSize - thickness; x++)
                {
                    i++;
                    SetVertex(v++, x, ySizes[y], thickness, vertices, xSize, zSize, thickness, roundness, normals);
                    //vertices[v++] = new Vector3(x, ySizes[y], thickness);
                    Debug.Log($"{x}\t{ySizes[y]}\t{i}");
                    // yield return wait;
                }
                for (int z = thickness + 1; z <= zSize; z++)
                {
                    i++;
                    SetVertex(v++, xSize - thickness, ySizes[y], z, vertices, xSize, zSize, thickness, roundness, normals);
                    //vertices[v++] = new Vector3(xSize - thickness, ySizes[y], z);
                    Debug.Log($"{z}\t{ySizes[y]}\t{i}");
                    // yield return wait;
                }
            }
            mesh.vertices = vertices;
            mesh.normals = normals;
        }

        private static void SetVertex(int i, int x, int y, int z, Vector3[] vertices, int xSize, int zSize, int thickness, int roundness, Vector3[] normals)
        {
            int radius; int radiusWithThick;
            int sizeByX; int sizeByZ;
            int zWithoutThick;
            Vector3 inner = vertices[i] = new Vector3(x, y, z);
            if (i < (xSize + zSize + 1) * 2)
            {
                sizeByX = xSize;
                sizeByZ = zSize;
                radius = roundness;
                zWithoutThick = z;
                radiusWithThick = radius;
            }
            else
            {
                sizeByX = xSize - thickness;
                sizeByZ = zSize - thickness;
                radius = roundness - thickness;
                zWithoutThick = z - thickness;
                radiusWithThick = radius + thickness;
            }
            if (x > sizeByX - radius)
            {
                inner.x = sizeByX - radius;
            }
            if (zWithoutThick < radius)
            {
                inner.z = radiusWithThick;
            }
            else if (z > sizeByZ - radius)
            {
                if (zWithoutThick < sizeByZ / 2)
                    inner.z = sizeByZ - radius;
            }

            normals[i] = (vertices[i] - inner).normalized;
            vertices[i] = inner + normals[i] * radius;
        }

        private static void CreateTriangles(int xSize, int zSize, int thickness, Mesh mesh)
        {
            int quads = xSize + zSize + 2 + xSize + zSize - 2 * thickness + (xSize + zSize - thickness) * 2;
            int[] triangles = new int[quads * 6];
            mesh.triangles = triangles;
            int halfRingExternal = (xSize + zSize) + 1;
            int halfRingInternal = (xSize + zSize - 2 * thickness) + 1;
            int t = 0, v = 0;
            for (int q = 0; q < halfRingExternal - 1; q++, v++)
            {
                t = SetQuad(triangles, t, v, v + 1, v + halfRingExternal, v + halfRingExternal + 1);
            }
            v += halfRingExternal + 1;
            for (int q = 0; q < halfRingInternal - 1; q++, v++)
            {
                t = SetQuad(triangles, t, v, v + 1, v + halfRingInternal, v + halfRingInternal + 1);
            }
            v = 0;
            int cornerVar = 0;
            int additVar = 2 * halfRingExternal + halfRingInternal;
            for (int q = 0; q < halfRingExternal - 1; q++, v++)
            {
                if (v == xSize - thickness)
                    cornerVar = q + additVar;
                if (v >= xSize - thickness && v < halfRingExternal - (zSize - thickness) - 1)
                {
                    t = SetAngle(triangles, t, v, cornerVar, v + 1);
                    additVar--;
                }
                else
                    t = SetQuad(triangles, t, v, v + additVar, v + 1, v + additVar + 1);
            }
            additVar = halfRingExternal;
            v = halfRingExternal;
            for (int q = 0; q < halfRingExternal - 1; q++, v++)
            {
                if (v == xSize - thickness + halfRingExternal)
                    cornerVar = q + 2 * additVar;
                if (v >= xSize + halfRingExternal - thickness && v < halfRingExternal * 2 - (zSize - thickness) - 1)
                {
                    t = SetAngle(triangles, t, cornerVar, v, v + 1);
                    additVar--;
                }
                else
                    t = SetQuad(triangles, t, v + additVar, v, v + additVar + 1, v + 1);
            }
            //v = halfRingExternal  - 1;
            //t = SetQuad(triangles, t, v, v + halfRingExternal + 2 * halfRingInternal, v + halfRingExternal, v + halfRingExternal + halfRingInternal); // end face

            mesh.triangles = triangles;
        }

        private static int SetAngle(int[] triangles, int i, int v00, int v10, int v01)
        {
            triangles[i] = v00;
            triangles[i + 1] = v01;
            triangles[i + 2] = v10;
            return i + 3;
        }

        private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
        {
            triangles[i] = v00;
            triangles[i + 1] = triangles[i + 4] = v01;
            triangles[i + 2] = triangles[i + 3] = v10;
            triangles[i + 5] = v11;
            return i + 6;
        }
    }
}
