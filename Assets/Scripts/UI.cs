using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance = null;
    private int MoneyCur = 0;
    public Text MoneyMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        MoneyUpdate();
    }

    public void Money(int _Count)
    {
        MoneyCur += _Count;
        MoneyUpdate();
    }

    private void MoneyUpdate()
    {
        MoneyMenu.text = MoneyCur.ToString() + " $";
    }
}
