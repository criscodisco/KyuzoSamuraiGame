using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyMeshColor : MonoBehaviour
{
    #region Variables
    [Header ("Mesh Renderers")]
    [SerializeField] public SkinnedMeshRenderer[] skinnedMeshRenderers;

    [Header ("Colors")]
    private Color[] randomColors;
    private Color randomColor;

    #endregion

    #region Colors
    public Color[] PopulateColorArray()
    {
        randomColors = new Color[10];
        randomColors[0] = Color.white;
        randomColors[1] = Color.red;
        randomColors[2] = Color.green;
        randomColors[3] = Color.blue;
        randomColors[4] = Color.black;
        randomColors[5] = Color.cyan;
        randomColors[6] = Color.gray;
        randomColors[7] = Color.green;
        randomColors[8] = Color.magenta;
        randomColors[9] = Color.yellow;

        return randomColors;
    }

    public Color GetRandomColor(Color[] colors)
    {
        int randomNumberChosen = Random.Range(0, colors.Length);
        randomColor = colors[randomNumberChosen];

        return randomColor;
    }

    #endregion

    #region ChangeMesh
    public void ChangeEnemyMesh(Color[] color)
    {   
        for (int i = 0; i < skinnedMeshRenderers.Length; i++) 
        {
            skinnedMeshRenderers[i].sharedMaterial.SetColor("_Color", GetRandomColor(color));
        }  
    }

    #endregion
}
