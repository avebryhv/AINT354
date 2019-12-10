using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    public Material baseMaterial;
    public Material transparentMaterial;

    bool isTransparent;
    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        baseMaterial = mesh.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTransparent()
    {
        isTransparent = true;
        mesh.material = transparentMaterial;
    }

    public void RemoveTransparent()
    {
        isTransparent = false;
        mesh.material = baseMaterial;
    }
}
