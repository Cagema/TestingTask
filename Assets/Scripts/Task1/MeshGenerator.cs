using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    public int MeshSize;
    bool[,,] _blocks;

    Mesh _mesh;

    List<Vector3> vertices = new();
    List<int> triangles = new();
    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Grid";

        CreateMesh();
    }

    private void CreateMesh()
    {
        SetBlocks();

        vertices.Clear();
        triangles.Clear();
        _mesh.Clear();

        for (int z = 0; z < MeshSize; z++)
        {
            for (int y = 0; y < MeshSize; y++)
            {
                for (int x = 0; x < MeshSize; x++)
                {
                    GenerateBlock(x, y, z);
                }
            }
        }

        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = triangles.ToArray();

        _mesh.RecalculateNormals();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateMesh();
        }
    }

    private void SetBlocks()
    {
        _blocks = new bool[MeshSize, MeshSize, MeshSize];
        for (int z = 0; z < MeshSize; z++)
        {
            for (int y = 0; y < MeshSize; y++)
            {
                for (int x = 0; x < MeshSize; x++)
                {
                    _blocks[x, y, z] = Random.value > 0.5f;
                }
            }
        }
    }

    private void GenerateBlock(int x, int y, int z)
    {
        if (_blocks[x, y, z])
        {
            var blockPosition = new Vector3Int(x, y, z);
            if (GetBlockAtPosition(blockPosition + Vector3Int.right)) GenerateRightSide(blockPosition);
            if (GetBlockAtPosition(blockPosition + Vector3Int.left)) GenerateLeftSide(blockPosition);
            if (GetBlockAtPosition(blockPosition + Vector3Int.forward)) GenerateFrontSide(blockPosition);
            if (GetBlockAtPosition(blockPosition + Vector3Int.back)) GenerateBackSide(blockPosition);
            if (GetBlockAtPosition(blockPosition + Vector3Int.up)) GenerateTopSide(blockPosition);
            if (GetBlockAtPosition(blockPosition + Vector3Int.down)) GenerateBottomSide(blockPosition);
        }
    }

    bool GetBlockAtPosition(Vector3Int blockPosition)
    {
        if (blockPosition.x >= 0 && blockPosition.x < MeshSize &&
            blockPosition.y >= 0 && blockPosition.y < MeshSize &&
            blockPosition.z >= 0 && blockPosition.z < MeshSize)
        {
            return !_blocks[blockPosition.x, blockPosition.y, blockPosition.z]; ;
        }
        else
            return true;
    }

    private void GenerateRightSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        vertices.Add(new Vector3(1, 1, 0) + blockPosition);
        vertices.Add(new Vector3(1, 0, 1) + blockPosition);
        vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticesSquare();
    }


    private void GenerateLeftSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        vertices.Add(new Vector3(0, 1, 1) + blockPosition);

        AddLastVerticesSquare();
    }

    private void GenerateFrontSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        vertices.Add(new Vector3(1, 0, 1) + blockPosition);
        vertices.Add(new Vector3(0, 1, 1) + blockPosition);
        vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticesSquare();
    }

    private void GenerateBackSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        vertices.Add(new Vector3(1, 1, 0) + blockPosition);

        AddLastVerticesSquare();
    }

    private void GenerateTopSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 1, 0) + blockPosition);
        vertices.Add(new Vector3(0, 1, 1) + blockPosition);
        vertices.Add(new Vector3(1, 1, 0) + blockPosition);
        vertices.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticesSquare();
    }

    private void GenerateBottomSide(Vector3Int blockPosition)
    {
        vertices.Add(new Vector3(0, 0, 0) + blockPosition);
        vertices.Add(new Vector3(1, 0, 0) + blockPosition);
        vertices.Add(new Vector3(0, 0, 1) + blockPosition);
        vertices.Add(new Vector3(1, 0, 1) + blockPosition);

        AddLastVerticesSquare();
    }

    private void AddLastVerticesSquare()
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);


        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 1);
        triangles.Add(vertices.Count - 2);
    }
}
