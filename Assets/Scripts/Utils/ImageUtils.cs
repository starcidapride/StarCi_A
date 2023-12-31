﻿using SFB;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ImageUtils
{

    public static Texture2D DecodeBase64Image(string base64EncodedImage)
    {
        if (string.IsNullOrEmpty(base64EncodedImage)) return null;
        try
        {
            var imageBytes = Convert.FromBase64String(base64EncodedImage);
            var texture = new Texture2D(1, 1);

            texture.LoadImage(imageBytes);
            return texture;
        } catch (Exception ex) {
            
            Debug.Log(ex); 
            return null;    
        
        }


    }

    public static string EncodeBase64Image(Texture2D texture)
    {
        if (texture == null) return null;

        byte[] imageBytes = texture.EncodeToPNG();

        string base64EncodedImage = Convert.ToBase64String(imageBytes);

        return base64EncodedImage;
    }

    public static Sprite CreateSpriteFromTexture(Texture2D texture, float pixelPerUnits = 1f)
    {
        if (texture == null) return null;
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelPerUnits);
    }

    public static Texture2D LoadImageFromFile()
    {
      
        var extension = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        };

        var imagePaths = StandaloneFileBrowser.OpenFilePanel("Select picture", "", extension, false);

        if (imagePaths.Length == 0) return null;

        var imagePath = imagePaths[0];

        if (string.IsNullOrEmpty(imagePath)) return null;

        var texture = new Texture2D(1, 1);

        byte[] imageData = File.ReadAllBytes(imagePath);

        texture.LoadImage(imageData);

        return texture;
    }
}