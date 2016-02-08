using UnityEditor;
using UnityEngine;
using System.Collections;

/// <summary>
/// This is a Wizard plugin for the Unity editor.  A new menu item, "Terrain" has a "Generate Perlin noise" option.
/// </summary>
public class WaterPlacer : ScriptableWizard
{
    [Tooltip("Deepest the water is allowed to be")]
    public float MaxWaterHeight = 0.5f;

    public GameObject Water;

    [MenuItem("Terrain/Adjust Water Height")]
    public static void CreateWizard(MenuCommand command)
    {
        ScriptableWizard.DisplayWizard("Water Placer", typeof(WaterPlacer));
    }

    void OnWizardUpdate()
    {
        helpString = "This will automatically adjust the Y coordinate of the water so that the deepest point on the map does not exceed the specified value.";
    }

    void OnWizardCreate()
    {
        GameObject obj = Selection.activeGameObject;

        if (obj != null && Water != null && obj.GetComponent<Terrain>())
        {
            SetWaterYCoordinate(obj.GetComponent<Terrain>());
        }
    }

    /// <summary>
    /// Create heights and assign it to the passed terrain
    /// </summary>
    /// <param name="terrain">Terrain</param>
    public void SetWaterYCoordinate(Terrain terrain)
    {
        Vector3 lowestPoint = new Vector3(100f, 100f, 100f);

        for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
            {
                Vector3 worldPoint = terrain.transform.TransformPoint(new Vector3(i, 0f, k));
                float height = terrain.SampleHeight(worldPoint);
                if (height < lowestPoint.z)
                {
                    lowestPoint = new Vector3(i, k, height);
                }
            }

        }
        //Debug.Log("Lowest point:" + lowestPoint.x + ", " + lowestPoint.y + ", " + lowestPoint.z);

        // Set the water at this plus the water depth
        Vector3 waterPosition = Water.transform.position;
        waterPosition.y = lowestPoint.z + MaxWaterHeight;
        Water.transform.position = waterPosition;
    }
}