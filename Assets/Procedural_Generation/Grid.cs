using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public float waterLevel = .9f;
    public int size = 1;
    public float scale = .1f;

   public Spawn_Object spawn;

    Cell[,] grid;

    private void Start()
    {
        float[,] noiseMap = new float[size, size];

        float xOffset = Random.Range(-1000f, 1000f);
        float yOffset = Random.Range(-1000f, 1000f);

        for (int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        float[,] fallOffMap = new float[size, size];

        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                fallOffMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }


        grid = new Cell[size, size];

        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                Cell cell = new Cell();
                float noiseValue = noiseMap[x, y];

                //  noiseValue -= fallOffMap[x, y];

                 cell.isWater = noiseValue < waterLevel;

               // cell.isWater = true;
                grid[x, y] = cell;
            }
        }

        // DrawTerrianMesh(grid);
        drawMap();
    }

    void DrawTerrianMesh(Cell[,] grid)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if(!cell.isWater)
                {
                    Vector3 a = new Vector3(x - .5f, y + .5f);
                    Vector3 b = new Vector3(x + .5f, y + .5f);
                    Vector3 c = new Vector3(x - .5f, y - .5f);
                    Vector3 d = new Vector3(x + .5f, y - .5f);
                    Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                    for(int k = 0; k < 6; k++)
                    {
                        vertices.Add(v[k]);
                        triangles.Add(triangles.Count);
                    }
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
    }


   /* private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (cell.isWater)
                    Gizmos.color = Color.blue;
                else
                    Gizmos.color = Color.green;
                Vector3 pos = new Vector3(x, 0, y);
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }*/

    public void drawMap()
    {

        for (int y = 0; y < size; y+=20)
        {
            for (int x = 0; x < size; x+=20)
            {
                Cell cell = grid[x, y];
                Vector3 trf = new Vector3(x, transform.position.y, y);

                if (cell.isWater)
                {
                    
                    spawn.spawnLand(trf, transform.rotation);
                }
                else
                {
                    spawn.spawnTree(trf, transform.rotation);
                }
               // else
               //     Gizmos.color = Color.green;
                Vector3 pos = new Vector3(x, 0, y);
                
            }
            
        }
        
    }
}
