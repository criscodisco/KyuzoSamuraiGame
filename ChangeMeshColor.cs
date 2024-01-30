using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMeshColor : MonoBehaviour
{
    #region Variables

    [Header ("Mesh Renderer")]
    [SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header ("Materials")]
    [SerializeField] public Material meshMaterial;

    [Header ("Colors")]
    public Color newColor;
    public Color oldColor;
    #endregion

    #region Start
    private void Start()
    {      
        oldColor = Color.white;
    }

    #endregion

    #region ChangeMesh
    public void ChangeMesh()
    {      
        skinnedMeshRenderer.sharedMaterial.SetColor("_Color", newColor);
    }

    public void ChangeMeshBack()
    {
        skinnedMeshRenderer.sharedMaterial.SetColor("_Color", oldColor);
    }

    #endregion

    #region ChangeCoroutine
    private IEnumerator DelayMaterialChangeCoroutine()
    {
        yield return new WaitForSeconds(2f);
    }
    #endregion
}
