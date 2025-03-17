using UnityEngine;

public class SellArea : MonoBehaviour
{
    [SerializeField] GameCurrency _gameCurrency;
    
    private void SellPotion(int value)
    { 
        _gameCurrency.IncreaseMoneyValue(value);
    }
}