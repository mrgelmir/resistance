using Resistance.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resistance.Scenes.Start
{
	public class CharacterDataView : MonoBehaviour
	{
		[SerializeField]
		private Image characterPortrait;
		[SerializeField]
		private TextMeshProUGUI characterNameLabel;
		[SerializeField]
		private Toggle toggle;

		public Toggle Toggle
		{ get { return toggle; } }

		public void SetData(CharacterData data)
		{
			characterNameLabel.text = data.CharacterName;
			characterPortrait.sprite = data.CharacterPortrait;
		}
	}
}
