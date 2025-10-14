using UnityEngine;

public class CollideBonus : MonoBehaviour
{
    void ApplyBonus(int ID)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bonus"))
        {
            BonusID bonus = other.gameObject.GetComponent<BonusID>();
            if (bonus != null)
            {
                ApplyBonus(bonus.ID);
            }
            Destroy(other.gameObject);
        }
    }
}
