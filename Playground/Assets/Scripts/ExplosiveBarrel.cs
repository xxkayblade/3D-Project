using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// allows things to run even in the editor
// this is something to be careful
[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour
{
    // sets a range a numbers that the variables under this header
    // can vary between
    [Range(1f, 8f)]
    public float radius = 1f;

    public float damage = 10;
    public Color color = Color.red;

    private void Awake()
    {
        Shader shader = Shader.Find("Default/Diffuse");
        // does not save the asset and avoids leaks 
        Material mat = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };

        /*// will dupliate material and can cause leaks
        GetComponent<MeshRenderer>().material.color = Color.red;

        // will modify the material asset
        GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;*/
    }

    // update loop to the editor where you can draw to the scene
    // helps you visual anything you want
    /*void OnDrawGizmos()
    {
        // receives a center and radius as parameters
        Gizmos.DrawWireSphere(transform.position, radius); 
    }*/

    // automatically adds and removes placed barrels into this list
    private void OnEnable() => ExplosiveBarrelManager.allBarrels.Add(this);
    private void OnDisable() => ExplosiveBarrelManager.allBarrels.Remove(this);
    

    // same as OnDrawGizmos but only draws them when object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
