using Resistance.Characters;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPickerView : MonoBehaviour
{
	[SerializeField]
	private CharacterGroup group;
	[SerializeField]
	private RectTransform container;

	private string selectedCharacterID = "";

	protected void Start()
	{
		UpdateView();

		// Select first character
	}

	public string GetSelectedCharacterId()
	{
		return selectedCharacterID;
	}

	private void SetSelectedCharacterID(string selectedCharacter)
	{
		print("new selected character id is " + selectedCharacter);

	}

	private void UpdateView()
	{
		// TODOE: Clear container

		var toggleGroup = container.GetComponent<ToggleGroup>();

		// Add Portraits to container
		foreach (CharacterData cd in group.Characters)
		{
			GameObject go = new GameObject(cd.CharacterName);
			go.AddComponent<RectTransform>().SetParent(container, false);
			var image = go.AddComponent<Image>();
			image.sprite = cd.CharacterPortrait;
			image.preserveAspect = true;
			var toggle = go.AddComponent<Toggle>();
			toggle.onValueChanged.AddListener((bool selected) =>
			{
				if (selected)
				{
					SetSelectedCharacterID(cd.CharacterID);
				}
			});

			toggleGroup.RegisterToggle(toggle);
		}

	}
}
