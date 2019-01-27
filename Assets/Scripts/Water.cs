using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float leftBound = -10;
    public float width = 20;
    public float toppityTop = 0;
    public float bottomyBottom = -3;

    LineRenderer Body;

    float[] xPositions;
    float[] yPositions;
    float[] velocities;
    float[] accelerations;
    
    GameObject[] meshObjects;
    Mesh[] meshes;
    GameObject[] colliders;

    public GameObject splash;

    public Material mat;

    public GameObject waterMesh;

    public const float springConstant = 0.02f;
    public const float damping = 0.04f;
    public const float spread = 0.05f;
    public const float z = -1f;

    float baseHeight;
    float left;
    float bottom;

    
    
    


    // Start is called before the first frame update
    void Start()
    {
        SpawnWater(leftBound, width, toppityTop, bottomyBottom);
    }

    public void Splash(float xpos, float velocity)
    {
        if (xpos >= xPositions[0] && xpos <= xPositions[xPositions.Length - 1])
        {
            xpos -= xPositions[0];

            int index = Mathf.RoundToInt((xPositions.Length - 1) * (xpos / (xPositions[xPositions.Length - 1] - xPositions[0])));

            velocities[index] += velocity;

            float lifetime = 0.93f + Mathf.Abs(velocity) * 0.07f;

            splash.GetComponent<ParticleSystem>().startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            splash.GetComponent<ParticleSystem>().startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            splash.GetComponent<ParticleSystem>().startLifetime = lifetime;

            Vector3 position = new Vector3(xPositions[index], yPositions[index] - 0.35f, 5);

            Quaternion rotation = Quaternion.LookRotation(new Vector3(xPositions[Mathf.FloorToInt(xPositions.Length / 2)], baseHeight + 8, 5) - position);

            GameObject splish = Instantiate(splash, position, rotation) as GameObject;
            Destroy(splish, lifetime + 0.3f);
        }
    }


    public void SpawnWater(float Left, float width, float Top, float Bottom)
    {
        int edgeCount = Mathf.RoundToInt(width) * 5;
        int nodeCount = edgeCount + 1;

        Body = gameObject.AddComponent<LineRenderer>();
        Body.material = mat;
        Body.material.renderQueue = 1000;
        Body.SetVertexCount(nodeCount);
        Body.SetWidth(0.1f, 0.1f);

        xPositions = new float[nodeCount];
        yPositions = new float[nodeCount];
        velocities = new float[nodeCount];
        accelerations = new float[nodeCount];

        meshObjects = new GameObject[edgeCount];
        meshes = new Mesh[edgeCount];
        colliders = new GameObject[edgeCount];

        baseHeight = Top;
        bottom = Bottom;
        left = Left;

        for (int i = 0; i < nodeCount; i++)
        {
            yPositions[i] = Top;
            xPositions[i] = Left + width * i / edgeCount;
            Body.SetPosition(i, new Vector3(xPositions[i], Top, z));
            accelerations[i] = 0;
            velocities[i] = 0;
        }

        for (int i = 0; i < edgeCount; i++)
        {
            meshes[i] = new Mesh();

            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(xPositions[i], yPositions[i], z);
            vertices[1] = new Vector3(xPositions[i + 1], yPositions[i + 1], z);
            vertices[2] = new Vector3(xPositions[i], bottom, z);
            vertices[3] = new Vector3(xPositions[i + 1], bottom, z);

            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };

            meshes[i].vertices = vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            meshObjects[i] = Instantiate(waterMesh, Vector3.zero, Quaternion.identity) as GameObject;
            meshObjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshObjects[i].transform.parent = transform;

            colliders[i] = new GameObject();
            colliders[i].name = "Trigger";
            colliders[i].AddComponent<BoxCollider2D>();
            colliders[i].transform.parent = transform;

            colliders[i].transform.position = new Vector3(Left + width * (i + 0.5f) / edgeCount, Top - 0.5f, 0);
            colliders[i].transform.localScale = new Vector3(width / edgeCount, 1, 1);

            colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
            colliders[i].AddComponent<WaterDetector>();
        }

    }

    void UpdateMeshes()
    {
        for(int i = 0; i < meshes.Length; i++)
        {
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(xPositions[i], yPositions[i], z);
            vertices[1] = new Vector3(xPositions[i + 1], yPositions[i + 1], z);
            vertices[2] = new Vector3(xPositions[i], bottom, z);
            vertices[3] = new Vector3(xPositions[i + 1], bottom, z);

            meshes[i].vertices = vertices;
        }
    }

    void FixedUpdate()
    {
        for(int i = 0; i < xPositions.Length; i++)
        {
            float force = springConstant * (yPositions[i] - baseHeight) + velocities[i] * damping;
            accelerations[i] = -force;
            yPositions[i] += velocities[i];
            velocities[i] += accelerations[i];
            Body.SetPosition(i, new Vector3(xPositions[i], yPositions[i], z));
        }

        float[] leftDeltas = new float[xPositions.Length];
        float[] rightDeltas = new float[xPositions.Length];

        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < xPositions.Length; i++)
            {
                if(i>0)
                {
                    leftDeltas[i] = spread * (yPositions[i] - yPositions[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if(i < xPositions.Length -1)
                {
                    rightDeltas[i] = spread * (yPositions[i] - yPositions[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }
        }

        for (int i = 0; i < xPositions.Length; i++)
        {
            if (i > 0)
            {
                yPositions[i - 1] += leftDeltas[i];
            }
            if(i < xPositions.Length-1)
            {
                yPositions[i + 1] += rightDeltas[i];
            }
        }

        UpdateMeshes();
    }

    
}
