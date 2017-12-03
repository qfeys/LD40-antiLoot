using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public Transform viewPoint;

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
    public Sprite landNon;

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
    public Sprite wetNon;

    bool[,] chunk00;    // Left bottom
    Vector2Int chunk00pos;
    GameObject[,] chunk00gos;
    SpriteRenderer[,] chunk00Spr;
    bool[,] chunk01;    // Left top
    Vector2Int chunk01pos;
    GameObject[,] chunk01gos;
    SpriteRenderer[,] chunk01Spr;
    bool[,] chunk10;    // Right bottom
    Vector2Int chunk10pos;
    GameObject[,] chunk10gos;
    SpriteRenderer[,] chunk10Spr;
    bool[,] chunk11;    // Right top
    Vector2Int chunk11pos;
    GameObject[,] chunk11gos;
    SpriteRenderer[,] chunk11Spr;

    int hChunk;
    int vChunk;

    const int CHUNK_SIZE = 48;

    public static MapGenerator instance;

    // Use this for initialization
    void Start () {
        if (instance != null)
            throw new Exception("2x instances of Map generator");
        instance = this;
        Generate();
	}

    private void CreateGos()
    {
        chunk00gos = new GameObject[CHUNK_SIZE, CHUNK_SIZE];
        chunk01gos = new GameObject[CHUNK_SIZE, CHUNK_SIZE];
        chunk10gos = new GameObject[CHUNK_SIZE, CHUNK_SIZE];
        chunk11gos = new GameObject[CHUNK_SIZE, CHUNK_SIZE];
        chunk00Spr = new SpriteRenderer[CHUNK_SIZE, CHUNK_SIZE];
        chunk01Spr = new SpriteRenderer[CHUNK_SIZE, CHUNK_SIZE];
        chunk10Spr = new SpriteRenderer[CHUNK_SIZE, CHUNK_SIZE];
        chunk11Spr = new SpriteRenderer[CHUNK_SIZE, CHUNK_SIZE];
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            for (int j = 0; j < CHUNK_SIZE; j++)
            {
                chunk00gos[i, j] = new GameObject("Chunk 00-" + i + "-" + j);
                chunk00Spr[i, j] = chunk00gos[i, j].AddComponent<SpriteRenderer>();
                chunk00Spr[i, j].sortingLayerName = "Terrain";
                chunk01gos[i, j] = new GameObject("Chunk 01-" + i + "-" + j);
                chunk01Spr[i, j] = chunk01gos[i, j].AddComponent<SpriteRenderer>();
                chunk01Spr[i, j].sortingLayerName = "Terrain";
                chunk10gos[i, j] = new GameObject("Chunk 10-" + i + "-" + j);
                chunk10Spr[i, j] = chunk10gos[i, j].AddComponent<SpriteRenderer>();
                chunk10Spr[i, j].sortingLayerName = "Terrain";
                chunk11gos[i, j] = new GameObject("Chunk 11-" + i + "-" + j);
                chunk11Spr[i, j] = chunk11gos[i, j].AddComponent<SpriteRenderer>();
                chunk11Spr[i, j].sortingLayerName = "Terrain";
            }
        }
    }


    /// <summary>
    /// Generate the tiles for the first time
    /// </summary>
    public void Generate()
    {
        CreateGos();
        hChunk = -1;
        vChunk = -1;
        chunk00pos = new Vector2Int(0, 0);
        chunk01pos = new Vector2Int(0, -1);
        chunk10pos = new Vector2Int(-1, 0);
        chunk11pos = new Vector2Int(-1, -1);
        chunk00 = GenerateChunk(chunk00pos);
        chunk01 = GenerateChunk(chunk01pos);
        chunk10 = GenerateChunk(chunk10pos);
        chunk11 = GenerateChunk(chunk11pos);
        PlaceChunk(chunk00gos, chunk00pos);
        PlaceChunk(chunk01gos, chunk01pos);
        PlaceChunk(chunk10gos, chunk10pos);
        PlaceChunk(chunk11gos, chunk11pos);
        TileChunk(0, 0);
        TileChunk(0, 1);
        TileChunk(1, 0);
        TileChunk(1, 1);
        SpawnEnemies(0, 0);
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
        float p = Mathf.PerlinNoise(x / 4.0f, y / 2.0f);
        float bias = Mathf.Pow((2 + y / 1000) * (x + 20 * Mathf.Sin(x / 20f)) - (3 + x / 2000) * (y + 12 * Mathf.Sin(y / 12f)), 2) / 16000 - .7f;
        return p > -bias;
    }

    private void PlaceChunk(GameObject[,] gos, Vector2Int pos)
    {
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            for (int j = 0; j < CHUNK_SIZE; j++)
            {
                gos[i, j].transform.position = (pos * CHUNK_SIZE + new Vector2(i, j));
            }
        }
    }

    private void TileChunk(int x, int y)
    {
        bool[,] chunkData = x == 0 ? (y == 0 ? chunk00 : chunk01) : (y == 0 ? chunk10 : chunk11);
        SpriteRenderer[,] spr = x == 0 ? (y == 0 ? chunk00Spr : chunk01Spr) : (y == 0 ? chunk10Spr : chunk11Spr);
        bool[,] c = new bool[CHUNK_SIZE + 2, CHUNK_SIZE + 2];
        for (int i = 0; i < CHUNK_SIZE; i++)
            for (int j = 0; j < CHUNK_SIZE; j++)
                c[i + 1, j + 1] = chunkData[i, j];
        bool[,] vChunk = x == 1 ? (y == 0 ? chunk00 : chunk01) : (y == 0 ? chunk10 : chunk11);
        bool[,] hChunk = x == 0 ? (y == 1 ? chunk00 : chunk01) : (y == 1 ? chunk10 : chunk11);
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            c[i + 1, 0] = hChunk[i, CHUNK_SIZE - 1];
            c[i + 1, CHUNK_SIZE + 1] = hChunk[i, 0];
            c[0, i + 1] = vChunk[CHUNK_SIZE - 1, i];
            c[CHUNK_SIZE + 1, i + 1] = vChunk[0, i];
        }

        for (int i = 1; i < CHUNK_SIZE+1; i++)
        {
            for (int j = 1; j < CHUNK_SIZE+1; j++)
            {
                // Remember: true == swamp
                if (c[i, j])    // own
                {
                    if (c[i, j + 1])    // North
                        if (c[i + 1, j])    // East
                            if (c[i, j - 1])    // South
                                if (c[i - 1, j]) spr[i - 1, j - 1].sprite = wetAll;
                                else spr[i - 1, j - 1].sprite = wetW;
                            else                // Not South
                                if (c[i - 1, j]) spr[i - 1, j - 1].sprite = wetS;
                            else spr[i - 1, j - 1].sprite = wetSW;
                        else                // Not East
                            if (c[i, j - 1])    // South
                            if (c[i - 1, j]) spr[i - 1, j - 1].sprite = wetE;
                            else spr[i - 1, j - 1].sprite = wetEW;
                        else                // Not South
                                if (c[i - 1, j]) spr[i - 1, j - 1].sprite = wetES;
                        else spr[i - 1, j - 1].sprite = wetESW;
                    else                // Not North
                        if (c[i + 1, j])    // East
                        if (c[i, j - 1])    // South
                            if (c[i - 1, j]) spr[i - 1, j - 1].sprite = wetN;
                            else spr[i - 1, j - 1].sprite = wetNW;
                        else                // Not South
                            if (c[i - 1, j]) spr[i - 1, j - 1].sprite = wetNS;
                        else spr[i - 1, j - 1].sprite = wetNSW;
                    else                // Not East
                            if (c[i, j - 1])    // South
                        if (c[i - 1, j]) spr[i - 1, j - 1].sprite = wetNE;
                        else spr[i - 1, j - 1].sprite = wetNEW;
                    else                // Not South
                                if (c[i - 1, j]) spr[i - 1, j - 1].sprite = wetNES;
                    else spr[i - 1, j - 1].sprite = wetNon;
                }
                else            // Not own - Remeber to reverse all statements for land
                {
                    if (c[i, j + 1])    // Not North
                        if (c[i + 1, j])    // Not East
                            if (c[i, j - 1])    // Not South
                                if (c[i - 1, j]) spr[i - 1, j - 1].sprite = landNon;
                                else spr[i - 1, j - 1].sprite = landNES;
                            else                // South
                                if (c[i - 1, j]) spr[i - 1, j - 1].sprite = landNEW;
                            else spr[i - 1, j - 1].sprite = landNE;
                        else                // East
                            if (c[i, j - 1])    // Not South
                            if (c[i - 1, j]) spr[i - 1, j - 1].sprite = landNSW;
                            else spr[i - 1, j - 1].sprite = landNS;
                        else                // South
                                if (c[i - 1, j]) spr[i - 1, j - 1].sprite = landNW;
                        else spr[i - 1, j - 1].sprite = landN;
                    else                // North
                        if (c[i + 1, j])    // Not East
                        if (c[i, j - 1])    // Not South
                            if (c[i - 1, j]) spr[i - 1, j - 1].sprite = landESW;
                            else spr[i - 1, j - 1].sprite = landES;
                        else                // South
                            if (c[i - 1, j]) spr[i - 1, j - 1].sprite = landEW;
                        else spr[i - 1, j - 1].sprite = landE;
                    else                // East
                            if (c[i, j - 1])    // Not South
                        if (c[i - 1, j]) spr[i - 1, j - 1].sprite = landSW;
                        else spr[i - 1, j - 1].sprite = landS;
                    else                // South
                                if (c[i - 1, j]) spr[i - 1, j - 1].sprite = landW;
                    else spr[i - 1, j - 1].sprite = landAll;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (viewPoint.position.x < (hChunk * CHUNK_SIZE + CHUNK_SIZE / 2f))
        {
            hChunk--;
            RecalculateChunkPos();
            SpawnEnemies(hChunk, vChunk);
            SpawnEnemies(hChunk, vChunk + 1);
            if (hChunk % 2 == 0) // is even
                RedrawH0();
            else
                RedrawH1();
        }
        else if (viewPoint.position.x > ((hChunk + 2) * CHUNK_SIZE - CHUNK_SIZE / 2f))
        {
            hChunk++;
            RecalculateChunkPos();
            SpawnEnemies(hChunk + 1, vChunk);
            SpawnEnemies(hChunk + 1, vChunk + 1);
            if (hChunk % 2 == 0) // is even
                RedrawH1();
            else
                RedrawH0();
        }
        if (viewPoint.position.y < (vChunk * CHUNK_SIZE + CHUNK_SIZE / 2f))
        {
            vChunk--;
            RecalculateChunkPos();
            SpawnEnemies(hChunk, vChunk);
            SpawnEnemies(hChunk + 1, vChunk);
            if (vChunk % 2 == 0) // is even
                RedrawV0();
            else
                RedrawV1();
        }
        else if (viewPoint.position.y > ((vChunk + 2) * CHUNK_SIZE - CHUNK_SIZE / 2f))
        {
            vChunk++;
            RecalculateChunkPos();
            SpawnEnemies(hChunk, vChunk + 1);
            SpawnEnemies(hChunk + 1, vChunk + 1);
            if (vChunk % 2 == 0) // is even
                RedrawV1();
            else
                RedrawV0();
        }
    }

    private void RecalculateChunkPos()
    {
        bool hEven = hChunk % 2 == 0;
        bool vEven = vChunk % 2 == 0;
        chunk00pos = new Vector2Int(hChunk + (hEven ? 0 : +1), vChunk + (vEven ? 0 : +1));
        chunk01pos = new Vector2Int(hChunk + (hEven ? 0 : +1), vChunk + (vEven ? +1 : 0));
        chunk10pos = new Vector2Int(hChunk + (hEven ? +1 : 0), vChunk + (vEven ? 0 : +1));
        chunk11pos = new Vector2Int(hChunk + (hEven ? +1 : 0), vChunk + (vEven ? +1 : 0));
    }

    private void RedrawH0()
    {
        chunk00 = GenerateChunk(chunk00pos);
        chunk01 = GenerateChunk(chunk01pos);
        PlaceChunk(chunk00gos, chunk00pos);
        PlaceChunk(chunk01gos, chunk01pos);
        TileChunk(0, 0);
        TileChunk(0, 1);
    }

    private void RedrawH1()
    {
        chunk10 = GenerateChunk(chunk10pos);
        chunk11 = GenerateChunk(chunk11pos);
        PlaceChunk(chunk10gos, chunk10pos);
        PlaceChunk(chunk11gos, chunk11pos);
        TileChunk(1, 0);
        TileChunk(1, 1);
    }

    private void RedrawV0()
    {
        chunk00 = GenerateChunk(chunk00pos);
        chunk01 = GenerateChunk(chunk10pos);
        PlaceChunk(chunk00gos, chunk00pos);
        PlaceChunk(chunk10gos, chunk10pos);
        TileChunk(0, 0);
        TileChunk(1, 0);
    }

    private void RedrawV1()
    {
        chunk10 = GenerateChunk(chunk01pos);
        chunk11 = GenerateChunk(chunk11pos);
        PlaceChunk(chunk01gos, chunk01pos);
        PlaceChunk(chunk11gos, chunk11pos);
        TileChunk(0, 1);
        TileChunk(1, 1);
    }

    private void SpawnEnemies(int hChunk, int vChunk)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(hChunk, 2) + Mathf.Pow(vChunk, 2));
        float difficulty = Mathf.Max( Mathf.Pow(distance / 100, 1.5f),1);
        float value = Mathf.Max(Mathf.Pow(distance, 2), 1);
        int j = 0;
        while (value > 0)
        {
            float[] rand = Enumerable.Repeat(0, 5).Select(i => UnityEngine.Random.value).ToArray();
            rand = rand.Select(f => f / rand.Sum()).ToArray();
            float speed = 3 + rand[0] * 3;
            int hitpoints = 1 + (int)rand[1] * 1;
            int damage = 1 + (int)rand[2] * 1;
            float lungeRange = .5f + rand[3] * .5f;
            float attackSpeed = 2f - Mathf.Sqrt(rand[4]);
            Vector2 pos = new Vector2(hChunk * CHUNK_SIZE + UnityEngine.Random.Range(0f, CHUNK_SIZE), vChunk * CHUNK_SIZE + UnityEngine.Random.Range(0f, CHUNK_SIZE));
            SpawnManager.SpawnEnemy(pos, speed, hitpoints, damage, lungeRange, attackSpeed);
            float hisValue = (speed - 3) * 3 + (hitpoints - 1) + (damage - 1) + (lungeRange - .5f) * .5f + Mathf.Pow(2 - attackSpeed, 2);
            value -= hisValue;
            j++;
        }
        Debug.Log("" + j + "# spawns in chunk " + hChunk + ", " + vChunk);
    }

    internal bool IsValidTerrain(Vector2 position)
    {
        return !IsSwamp((int)position.x, (int)position.y);
    }
}
