using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ImageUtils
{

    public static Texture2D DecodeBase64Image(string base64EncodedImage)
    {
        if (string.IsNullOrEmpty(base64EncodedImage)) return null; 
        var imageBytes = Convert.FromBase64String(base64EncodedImage);
        var texture = new Texture2D(1, 1);
        texture.LoadImage(imageBytes);
        return texture;
    }

    public static Sprite CreateSpriteFromTexture(Texture2D texture, float pixelPerUnits = 1f)
    {
        if (texture == null) return null;
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelPerUnits);
    }

}