using Resistance.Characters;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPickerView : MonoBehaviour
{
	[SerializeField]
	private CharacterGroup group;
	[SerializeField]
	private RectTransform container;


	protected void Start()
	{
		UpdateView();

		// Select first character
	}

	public string GetSelectedCharacterId()
	{
		return "";
	}

	private void UpdateView()
	{
		// Clear container


		// Add Portraits to container
		foreach (CharacterData cd in group.Characters)
		{
			GameObject go = new GameObject(cd.CharacterName);
			go.AddComponent<RectTransform>();
			go.AddComponent<Image>();
		}
	}
}
