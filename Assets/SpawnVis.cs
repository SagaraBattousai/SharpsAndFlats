using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVis : MonoBehaviour
{
    public GameObject visMesh;
    private GameObject[] meshes = new GameObject[AudioPeer.SPECTRUM_SIZE];
    public float maxScale = 2500.0f;
    private float rot = -1 * (360.0f / AudioPeer.SPECTRUM_SIZE);

    // Start is called before the first frame update
    void Start()
    {
        for(int index = 0; index < AudioPeer.SPECTRUM_SIZE; index++)
        {
            GameObject mesh = Instantiate<GameObject>(visMesh, this.transform);
            mesh.transform.position = Vector3.forward * 100;
            mesh.name = "Visuliser Mesh: " + index;
            this.transform.eulerAngles = new Vector3(0, rot * index, 0);
            meshes[index] = mesh;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        for(int index = 0; index < AudioPeer.SPECTRUM_SIZE; index++)
        {
            GameObject mesh = meshes[index];
            if (mesh != null)
            {
                mesh.transform.localScale = new Vector3(1, (AudioPeer.spectrum[index] * maxScale), 1);
            }
        }
        
    }
}
