using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;
    public GameObject bonusPrefab;
    public bool HasBonus => bonusPrefab != null;
    public void HandleBonus()
    {
        if (HasBonus)
        {
            GameObject bonus = Instantiate(bonusPrefab, transform.position, Quaternion.identity);
        }
    }
}
