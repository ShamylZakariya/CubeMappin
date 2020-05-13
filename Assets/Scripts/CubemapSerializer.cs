using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubemapSerializer : MonoBehaviour
{
    [SerializeField]
    public Cubemap cubemap;

    public void RenderToCubemap()
    {
        GameObject go = new GameObject("CubemapCamera");
        go.AddComponent<Camera>();

        go.transform.position = this.transform.position;
        go.transform.rotation = Quaternion.identity;
        go.GetComponent<Camera>().RenderToCubemap(cubemap);

        // cleanup
        DestroyImmediate(go);
    }

    public void SaveCubemap(string destFolder)
    {
        RenderToCubemap();
        
        SaveCubemapFace(CubemapFace.PositiveX, "right", "jpg", destFolder);
        SaveCubemapFace(CubemapFace.NegativeX, "left", "jpg", destFolder);
        SaveCubemapFace(CubemapFace.PositiveZ, "front", "jpg", destFolder);
        SaveCubemapFace(CubemapFace.NegativeZ, "back", "jpg", destFolder);
        SaveCubemapFace(CubemapFace.PositiveY, "top", "jpg", destFolder);
        SaveCubemapFace(CubemapFace.NegativeY, "bottom", "jpg", destFolder);
        Debug.Log("[SaveCubemapFace] - Done");
    }

    private void SaveCubemapFace(CubemapFace face, string prefix, string type, string destFolder)
    {
        int width = cubemap.width;
        int height = cubemap.height;
        Texture2D tex = new Texture2D(width, height, cubemap.format, 0, false);

        // we need to flip our textures on Y before saving
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                tex.SetPixel(x, height - 1 - y, cubemap.GetPixel(face, x, y));                
            }
        }

        tex.Apply();
        byte[] data = null;
        if (type == "png")
        {
            data = tex.EncodeToPNG();
        }
        else if (type == "jpg")
        {
            data = tex.EncodeToJPG(100);
        }

        if (data != null)
        {
            System.IO.File.WriteAllBytes(destFolder + "/" + prefix + "." + type, data);
        }
        else
        {
            Debug.LogError("[SaveCubemapFace] - failed to encode tex2d");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
