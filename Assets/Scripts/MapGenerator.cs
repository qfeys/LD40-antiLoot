using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Camera mainCam;

    public Sprite landAll;
    public Sprite landNES;
    public Sprite landNSW;
    public Sprite landNEW;
    public Sprite landESW;
    public Sprite landNE;
    public Sprite landNS;
    public Sprite landNW;
    public Sprite landES;
    public Sprite landEW;
    public Sprite landSW;
    public Sprite landN;
    public Sprite landE;
    public Sprite landS;
    public Sprite landW;

    public Sprite wetAll;
    public Sprite wetNES;
    public Sprite wetNSW;
    public Sprite wetNEW;
    public Sprite wetESW;
    public Sprite wetNE;
    public Sprite wetNS;
    public Sprite wetNW;
    public Sprite wetES;
    public Sprite wetEW;
    public Sprite wetSW;
    public Sprite wetN;
    public Sprite wetE;
    public Sprite wetS;
    public Sprite wetW;

    bool[,] chunk00;    // Left bottom
    Vector2Int chunk00pos;
    bool[,] chunk01;    // Left top
    Vector2Int chunk01pos;
    bool[,] chunk10;    // Right bottom
    Vector2Int chunk10pos;
    bool[,] chunk11;    // Right top
    Vector2Int chunk11pos;

    int hChunk;
    int vChunk;

    const int CHUNK_SIZE = 20;

    // Use this for initialization
    void Start () {
        hChunk = -1;
        vChunk = -1;
        Generate();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Generate()
    {
        // Chunk 00
        bool hEven = hChunk % 2 == 0;
        bool vEven = vChunk % 2 == 0;
        if (chunk00pos != new Vector2Int(hChunk + (hEven ? 0 : +1), vChunk + (vEven ? 0 : +1)))
        {
            chunk00pos = new Vector2Int(hChunk + (hEven ? 0 : +1), vChunk + (vEven ? 0 : +1));
            chunk00 = GenerateChunk(chunk00pos);
        }
        if (chunk01pos != new Vector2Int(hChunk + (hEven ? 0 : +1), vChunk + (vEven ? +1 : 0)))
        {
            chunk01pos = new Vector2Int(hChunk + (hEven ? 0 : +1), vChunk + (vEven ? +1 : 0));
            chunk01 = GenerateChunk(chunk01pos);
        }
        if (chunk10pos != new Vector2Int(hChunk + (hEven ? +1 : 0), vChunk + (vEven ? 0 : +1)))
        {
            chunk10pos = new Vector2Int(hChunk + (hEven ? +1 : 0), vChunk + (vEven ? 0 : +1));
            chunk10 = GenerateChunk(chunk10pos);
        }
        if (chunk11pos != new Vector2Int(hChunk + (hEven ? +1 : 0), vChunk + (vEven ? +1 : 0)))
        {
            chunk11pos = new Vector2Int(hChunk + (hEven ? +1 : 0), vChunk + (vEven ? +1 : 0));
            chunk11 = GenerateChunk(chunk11pos);
        }

    }

    private bool[,] GenerateChunk(Vector2Int chunkPos)
    {
        bool[,] chunk = new bool[CHUNK_SIZE, CHUNK_SIZE];
        int startx = chunkPos.x * CHUNK_SIZE;
        int starty = chunkPos.y * CHUNK_SIZE;
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            for (int j = 0; j < CHUNK_SIZE; j++)
            {
                chunk[i, j] = IsSwamp(startx + i, starty + j);
            }
        }
        return chunk;
    }

    public bool IsSwamp(int x, int y)
    {
        float p = Mathf.PerlinNoise(x / 10.0f, y / 10.0f);
        return p < 0.1f;
    }
}
