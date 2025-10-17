using UnityEngine;
using System.Collections;

public class CollideBonus : MonoBehaviour
{
    void ApplyBonus(int ID)
    {
        if (ID == 0) //Multi-pizzas (+1)
        {
            SoundManager.Instance.PlayBonus();
            //si je code ça ça va créer 25 bugs
        }
        else if (ID == 1) //Jauge de faim bloquée (5sec)
        {
            SoundManager.Instance.PlayBonus();
            //HungerBar.Instance.FreezeTimer(5f);
        }
        else if (ID == 2) //Pizza transperçante (5 sec)
        {
            SoundManager.Instance.PlayBonus();
            foreach (var pizza in FindObjectsOfType<Pizza>())
                pizza.gameObject.GetComponent<Pizza>().transperceTimer = 5f;
        }
        else if (ID == -1) //Pizza accéléré (5sec)
        {
            SoundManager.Instance.PlayMalus();
            foreach (var pizza in FindObjectsOfType<Pizza>())
                pizza.gameObject.GetComponent<Pizza>().speedTimer = 5f;
        }
        else if (ID == -2) //Réduire la barre du player (5 sec)
        {
            SoundManager.Instance.PlayMalus();
            Movement.Instance.ReduceSize(5f);
        }
        else if (ID == -3) //Balle qui clignote (disparaît) (5sec)
        {
            SoundManager.Instance.PlayMalus();
            foreach (var pizza in FindObjectsOfType<Pizza>())
            {
                pizza.StartCoroutine(BlinkPizza(pizza, 5f));
            }
        }
    }
    private IEnumerator BlinkPizza(Pizza pizza, float duration)
    {
        float timer = 0f;
        float interval = 0.5f;
        bool visible = true;
        while (timer < duration)
        {
            visible = !visible;
            pizza.SendMessage("SetRenderersActive", visible);
            timer += interval;
            yield return new WaitForSeconds(interval);
        }
        pizza.SendMessage("SetRenderersActive", true);
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
