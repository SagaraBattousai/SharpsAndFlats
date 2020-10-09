using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrue : MonoBehaviour
{
    public GameObject visMesh;
    private GameObject[] meshes = new GameObject[AudioPeer.NUMBER_OF_BANDS];
    public float maxScale = 25.0f;
    public bool useBuffer = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int index = 0; index < AudioPeer.NUMBER_OF_BANDS; index++)
        {
            GameObject mesh = Instantiate<GameObject>(visMesh, this.transform);
            mesh.transform.position = Vector3.left * ((-8) + (index * 2));
            mesh.name = "Visuliser Mesh: " + index;
            meshes[index] = mesh;
        }


    }

    // Update is called once per frame
    void Update()
    {
        for (int index = 0; index < AudioPeer.NUMBER_OF_BANDS; index++)
        {
            GameObject mesh = meshes[index];
            if (mesh != null)
            {
                if (useBuffer)
                {
                    mesh.transform.localScale = new Vector3(1, (AudioPeer.bufferBands[index] * maxScale) + 2, 1);
                }
                else
                {
                    mesh.transform.localScale = new Vector3(1, (AudioPeer.freqBands[index] * maxScale) + 2, 1);
                }
            }
        }

    }
}
