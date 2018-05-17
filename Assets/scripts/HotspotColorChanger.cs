/* Attach to game objects intended to be hand draggable (cubes). 
 * This script works in conjuction with HotSpot.cs. */

using HoloToolkit.Unity.InputModule;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HotspotColorChanger : MonoBehaviour
{

    /* Pick object's intial color */
    public Color activeColor = Color.red; 
    private Material cachedMaterial;

    private void Awake()
    {
        
        /* Set object's initial color */ 
        cachedMaterial = GetComponent<Renderer>().material;
        cachedMaterial.SetColor("_Color", Color.white);
    }

    /* Prevent memory leaks */
    private void OnEnable()
    {
        Hotspot.OnEntered += OnHotspotEnter;
        Hotspot.OnExited += OnHotspotExit;
    }

    /* Prevent memory leaks */
    private void OnDisable()
    {
        Hotspot.OnEntered -= OnHotspotEnter;
        Hotspot.OnExited -= OnHotspotExit;
    }

    private void OnHotspotEnter(GameObject cube)
    {

        /* Change this object's color upon hotspot collision */
        cachedMaterial = cube.GetComponent<Renderer>().material;
        cachedMaterial.SetColor("_Color", activeColor);

        /* Consider disabling hand dragging */
        cube.GetComponent<HandDraggable>().enabled = false;
    }

    private void OnHotspotExit(GameObject cube)
    {
        
        /* Change this object's exited color */ 
        //cachedMaterial.SetColor("_Color", Color.white);
    }

    private void OnDestroy()
    {
        DestroyImmediate(cachedMaterial);
    }

}
