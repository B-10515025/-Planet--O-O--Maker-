using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlanetPlugin : Editor {
    [MenuItem("PlanetPlugin/CreatePlanet")]
    static void CreatePlanet()
    {
        GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0;
        GameObject highlighter = new GameObject("highlighter");
        highlighter.AddComponent<Highlighter>();
        GameObject planet = new GameObject("planet");
        planet.AddComponent<Planet>();
        MeshRenderer mr = planet.AddComponent<MeshRenderer>();
        MeshFilter filter = planet.AddComponent<MeshFilter>();
        mr.material = new Material(Shader.Find("Specular"));
    }
}
