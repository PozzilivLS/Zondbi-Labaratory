using UnityEngine;

public class GameCurrency : MonoBehaviour
{
    [SerializeField] private int _money;

    public int Money => _money;
    
    public void IncreaseMoneyValue(int value) 
    {
        _money += value;
    }
    public void DecreaseMoneyValue(int value) 
    {
        _money -= value;
    }
}