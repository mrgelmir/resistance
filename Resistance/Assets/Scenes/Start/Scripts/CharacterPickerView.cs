using Resistance.Helpers;
using Resistance.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Resistance.Scenes.Start
{
	public class CharacterPickerView : MonoBehaviour
	{
		[Header("Scene References")]
		[SerializeField]
		private RectTransform container;
		[SerializeField]
		private ToggleGroup toggleGroup;

		[Header("Project References")]
		[SerializeField]
		private CharacterDataView dataViewPrefab;

		private CharacterGroup group = null;

		private string selectedCharacterID = "";

		protected void Start()
		{
			UpdateView();
		}

		public void SetCharacterGroup(CharacterGroup characterGroup)
		{
			group = characterGroup;
			UpdateView();
		}

		public string GetSelectedCharacterId()
		{
			return selectedCharacterID;
		}

		private void SetSelectedCharacterID(string selectedCharacter)
		{
			//print("new selected character id is " + selectedCharacter);
			selectedCharacterID = selectedCharacter;
		}

		private void UpdateView()
		{
            // Store first character id, this should be selected
            string firstId = null;

			// Clear container
			container.DestroyChildren();

			if (group == null)
				return;

			// Add Portraits to container
			foreach (CharacterData cd in group.Characters)
			{
				CharacterDataView view = Instantiate(dataViewPrefab, container, false);
				view.SetData(cd);

				view.Toggle.group = toggleGroup;
				view.Toggle.isOn = false;
				view.Toggle.onValueChanged.AddListener((bool selected) =>
				{
					if (selected)
					{
						SetSelectedCharacterID(cd.CharacterID);
					}
				});     
                
                if(firstId == null)
                {
                    firstId = cd.CharacterID;
                }
			}

            SetSelectedCharacterID(firstId);
		}
	}

}