using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerGraphic : MonoBehaviour
{
  [SerializeField] private SkinnedMeshRenderer meshRenderer;
  [SerializeField] private List<Mesh> characterMeshes;
  [SerializeField] private Animator animator;
  private static readonly int DirectionX = Animator.StringToHash("XDirection");
  private static readonly int DirectionY= Animator.StringToHash("YDirection");

  public void ChangeModel() => 
    meshRenderer.sharedMesh = characterMeshes[Random.Range(0, characterMeshes.Count)];


  public void SetDirection(Vector3 direction)
  {
    animator.SetFloat(DirectionX,direction.x);
    animator.SetFloat(DirectionY,direction.z);
  }
}
