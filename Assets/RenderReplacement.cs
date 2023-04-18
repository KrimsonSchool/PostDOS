// This script goes onto your according Camera
using UnityEngine;

public class RenderReplacement : MonoBehaviour
{
    public Texture2D replacement;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // To overwrite the entire screen
        //Graphics.Blit(replacement, null);

        // Or to overwrite only what this specific Camera renders
        Graphics.Blit(replacement, dest);
    }
}