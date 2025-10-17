using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public int life = 3;
    public GameObject Panel1;
    public GameObject Panel1Up;
    public GameObject Panel1Down;
    public GameObject Panel2;
    public GameObject Panel2Up;
    public GameObject Panel2Down;
    public GameObject Panel3;
    public GameObject Panel3Up;
    public GameObject Panel3Down;
    void Start()
    {
        Panel1 = GameObject.Find("Panel1");
        Panel1Up = Panel1.transform.GetChild(0).gameObject;
        Panel1Down = Panel1.transform.GetChild(1).gameObject;
        Panel2 = GameObject.Find("Panel2");
        Panel2Up = Panel2.transform.GetChild(0).gameObject;
        Panel2Down = Panel2.transform.GetChild(1).gameObject;
        Panel3 = GameObject.Find("Panel3");
        Panel3Up = Panel3.transform.GetChild(0).gameObject;
        Panel3Down = Panel3.transform.GetChild(1).gameObject;
    }
    void Update()
    {
        life = GameManager.Instance.life;
        if (life <= 0)
        {
            Panel1Up.SetActive(false);
            Panel1Down.SetActive(true);
            Panel2Up.SetActive(false);
            Panel2Down.SetActive(true);
            Panel3Up.SetActive(false);
            Panel3Down.SetActive(true);
        }
        else if (life >= 3)
        {
            Panel1Up.SetActive(true);
            Panel1Down.SetActive(false);
            Panel2Up.SetActive(true);
            Panel2Down.SetActive(false);
            Panel3Up.SetActive(true);
            Panel3Down.SetActive(false);
        }
        else if (life == 2)
        {
            Panel1Up.SetActive(false);
            Panel1Down.SetActive(true);
            Panel2Up.SetActive(true);
            Panel2Down.SetActive(false);
            Panel3Up.SetActive(true);
            Panel3Down.SetActive(false);
        }
        else if (life == 1)
        {
            Panel1Up.SetActive(false);
            Panel1Down.SetActive(true);
            Panel2Up.SetActive(false);
            Panel2Down.SetActive(true);
            Panel3Up.SetActive(true);
            Panel3Down.SetActive(false);
        }
    }
}
