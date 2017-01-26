using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetPlugin : Editor {
    [MenuItem("PlanetPlugin/CreatePlanet")]
    static void CreatePlanet()
    {
        GameObject planet = new GameObject("planet");
        planet.gameObject.AddComponent<Planet>();
        MeshRenderer mr = planet.AddComponent<MeshRenderer>();
        MeshFilter filter = planet.AddComponent<MeshFilter>();
        mr.material = new Material(Shader.Find("Specular"));
        planet.GetComponent<Planet>().make();
    }
}
