using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanableObject : MonoBehaviour
{
    public Material brushMaterial;
    public RenderTexture maskTexture;


    private void Start()
    {
        RenderTexture.active = maskTexture;
        GL.Clear(true, true, Color.white);
        RenderTexture.active = null;
    }

    public void CleanAt(Vector2 uv,float Size,float Strength)
    {
        brushMaterial.SetVector("_BrushPos", new Vector4(uv.x, uv.y, 0, 0));
        brushMaterial.SetFloat("_BrushSize", Size);
        brushMaterial.SetFloat("_BrushOpacity", Strength);
        Debug.Log(Size + " " + Strength);

        RenderTexture temp = RenderTexture.GetTemporary(maskTexture.width, maskTexture.height);

        Graphics.Blit(maskTexture, temp, brushMaterial);

        Graphics.Blit(temp, maskTexture);
        RenderTexture.ReleaseTemporary(temp);
    }
}
