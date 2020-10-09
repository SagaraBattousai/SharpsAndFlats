using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var x = GetComponent<Tilemap>();
        Vector3Int position = new Vector3Int(-2, 0, 0);
        Debug.Log(x.origin);
        
        x.SetTileFlags(position, TileFlags.None);
        x.SetColor(position, new Color(0f, 0.5f, 1.0f));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
