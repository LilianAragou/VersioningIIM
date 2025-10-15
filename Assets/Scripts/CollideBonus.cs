using UnityEngine;

public class CollideBonus : MonoBehaviour
{
    void ApplyBonus(int ID)
    {
        if (ID == 0) //Multi-pizzas (+1)
        {

        }
        else if (ID == 1) //Jauge de faim bloquée (5sec)
        {
            //HungerBar.Instance.FreezeTimer(5f);
        }
        else if (ID == 2) //Pizza transperçante (5 sec)
        {
            foreach (var pizza in FindObjectsOfType<Pizza>())
                pizza.gameObject.GetComponent<Pizza>().transperceTimer = 5f;
        }
        else if (ID == -1) //Pizza accéléré (5sec)
        {
            foreach (var pizza in FindObjectsOfType<Pizza>())
                pizza.gameObject.GetComponent<Pizza>().speedTimer = 5f;
        }
        else if (ID == -2) //Réduire la barre du player (5 sec)
        {
            Movement.Instance.ReduceSize(5f);
        }
        else if (ID == -3) //Balle qui clignote (disparaît) (5sec)
        {

        }
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
