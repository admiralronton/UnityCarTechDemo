using UnityEditor;
using UnityEngine;
using System.Collections;

/// <summary>
/// This is a Wizard plugin for the Unity editor.  A new menu item, "Terrain" has a "Generate Perlin noise" option.
/// </summary>
/// <remarks>
/// Obtained from http://wiki.unity3d.com/index.php/TerrainPerlinNoise
/// </remarks>
public class TerrainPerlinNoise : ScriptableWizard
{
    [Tooltip("How far apart the hills are.  Lower is more sparse.")]
    public float Tiling = 5.0f;

    [Tooltip("How high the hills are.  Higher value creates lower hills.")]
    public float HeightDivisor = 30.0f;

    [MenuItem("Terrain/Generate from Perlin Noise")]
    public static void CreateWizard(MenuCommand command)
    {
        ScriptableWizard.DisplayWizard("Perlin Noise Generation Wizard", typeof(TerrainPerlinNoise));
    }

    void OnWizardUpdate()
    {
        helpString = "This small generation tool allows you to generate perlin noise for your terrain.  Make sure to have your terrain object selected before pushing Create.";
    }

    void OnWizardCreate()
    {
        GameObject obj = Selection.activeGameObject;

        if (obj.GetComponent<Terrain>())
        {
            GenerateHeights(obj.GetComponent<Terrain>(), Tiling, HeightDivisor);
        }
    }

    /// <summary>
    /// Create heights and assign it to the passed terrain
    /// </summary>
    /// <param name="terrain">Terrain</param>
    /// <param name="tileSize">Density of the hills</param>
    /// <param name="height">Height divisor.  Lower is larger</param>
    public void GenerateHeights(Terrain terrain, float tileSize, float height)
    {
        float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];

        for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
            {
                heights[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize, ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize) / height;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}