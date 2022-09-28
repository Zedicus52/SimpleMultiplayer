using UnityEngine;

public class PlayerGraphic : MonoBehaviour
{
  [SerializeField] private MeshRenderer meshRenderer;
  [SerializeField] private Color otherPlayerColor;

  public void ChangeColor()
  {
    meshRenderer.materials[0].color = otherPlayerColor;
  }
}
