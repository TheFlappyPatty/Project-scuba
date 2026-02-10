using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanableObject : MonoBehaviour
{
    public Material brushMaterial;
    public int DirtX = 512;
    public int DirtY = 512;
    private RenderTexture maskTexture;

    private void Start()
    {
        maskTexture = new RenderTexture(DirtX, DirtY, 0);
        RenderTexture.active = maskTexture;
        GL.Clear(true, true, Color.white);
        RenderTexture.active = null;
        foreach(Material m in gameObject.GetComponent<Renderer>().sharedMaterials)
        {
            m.SetTexture("_MaskTex", maskTexture);
        }
 //       gameObject.GetComponent<Renderer>().material.SetTexture("_MaskTex", maskTexture);


    }

    public void CleanAt(Vector2 uv,float Size,float Strength)
    {
        brushMaterial.SetTexture("_MaskTex",maskTexture);
        brushMaterial.SetVector("_BrushPos", new Vector4(uv.x, uv.y, 0, 0));
        brushMaterial.SetFloat("_BrushSize", Size);
        brushMaterial.SetFloat("_BrushSoftness", 0.05f);
        brushMaterial.SetFloat("_BrushOpacity", Strength);
        Debug.Log(Size + " " + Strength);


       RenderTexture temp = RenderTexture.GetTemporary(maskTexture.width, maskTexture.height);

       Graphics.Blit(temp, maskTexture, brushMaterial);

       Graphics.Blit(temp,maskTexture);
       RenderTexture.ReleaseTemporary(temp);
    }
}
