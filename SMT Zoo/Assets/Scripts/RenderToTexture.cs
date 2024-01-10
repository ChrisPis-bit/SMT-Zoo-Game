#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

public class RenderToTexture : MonoBehaviour
{
    public Camera camera;

    [ContextMenu("Render")]
    public void TakePicture()
    {
        if (camera.targetTexture == null)
        {
            Debug.LogError("Set a renderTexture as the target of the camera");
            return;
        }

        RenderTexture.active = camera.targetTexture;
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height, TextureFormat.RGB48, false);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        RenderTexture.active = null;

        // Generated png.
        byte[] bytes = image.EncodeToPNG();

        string directory = AssetDatabase.GetAssetPath(camera.targetTexture);
        int nameLen = (camera.targetTexture.name + ".renderTexture").Length;
        directory = directory.Substring(0, directory.Length - nameLen);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        string imageFilePath = $"{directory}/{camera.targetTexture.name}.png";
        File.WriteAllBytes(imageFilePath, bytes);

        AssetDatabase.Refresh();
    }

}

#endif