using Resistance.Game.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickerView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;
    [SerializeField]
    private Toggle toggle;

    private int playerIndex = -1;

    public void SetData(Player player)
    {
        playerIndex = player.Id;

    }

}

