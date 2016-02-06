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
    [Tooltip("How sparse hills are.  Lower is farther.")]
    public float Tiling = 5.0f;

    [Tooltip("How high the hills are.  Higher value creates lower hills.")]
    public float HeightDivisor = 30.0f;

    [Tooltip("How rounded the hills are.  Higher is sharper.")]
    public float Rounding = 1;

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

        if (obj != null && obj.GetComponent<Terrain>())
        {
            GenerateHeights(obj.GetComponent<Terrain>(), Tiling, HeightDivisor, Rounding);
        }
    }

    /// <summary>
    /// Create heights and assign it to the passed terrain
    /// </summary>
    /// <param name="terrain">Terrain</param>
    /// <param name="tileSize">Density of the hills</param>
    /// <param name="height">Height divisor.  Lower is larger</param>
    /// <param name="rounding">How round the terrain is.  Higher is sharper.</param>
    public void GenerateHeights(Terrain terrain, float tileSize, float height, float rounding)
    {
        float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];

        for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
            {
                // Generate a random number between 0 and 1
                float value = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize,
                    ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize);

                // Multiply by some sort of curve, taking the rounding factor into account.  Rounding should be between 0 and 1, 
                // where 0 leaves things as they are, and 1 makes the hills almost into plateaus
                float padding = (1f - value) - ((1f - value) * rounding);
                value += padding;

                // Divide by our height to flatten things out
                heights[i, k] = value / height;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}