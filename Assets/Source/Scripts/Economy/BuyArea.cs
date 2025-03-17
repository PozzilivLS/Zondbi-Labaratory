using UnityEngine;

public class BuyArea : MonoBehaviour
{
    [SerializeField] private GameCurrency _gameCurrency;

    private void BuyIngridients(int value)
    {
        if (_gameCurrency.Money < value) return;

        _gameCurrency.DecreaseMoneyValue(value);
    }
}