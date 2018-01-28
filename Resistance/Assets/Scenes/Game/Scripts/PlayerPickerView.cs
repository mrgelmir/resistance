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

	public System.Action<int> OnSelected;
	public System.Action<int> OnDeselected;

	public void SetData(Player player)
	{
		playerIndex = player.Id;

		nameLabel.text = player.PlayerName;
		toggle.isOn = false;
	}

	public void SetInteractibility(bool interactable)
	{
		toggle.interactable = interactable;
	}

	public void ToggleValueChanged(bool enabled)
	{
		if (enabled)
		{
			if (OnSelected != null)
				OnSelected(playerIndex);
		}
		else
		{
			if (OnDeselected != null)
				OnDeselected(playerIndex);
		}
	}

}

