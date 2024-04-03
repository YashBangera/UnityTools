using TMPro;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("Localized UI/Localized TextMeshProUGUI", 12)]
	public class LocalizedTextMeshProUGUI : TextMeshProUGUI
	{
		[SerializeField] private LocalizedString m_localizedString;
		[SerializeField] private LocalizedString.SupportedLocale m_currentLocale = LocalizedString.SupportedLocale.EN_US;

		public LocalizedString ReferencedLocalizedString => m_localizedString;

		protected override void Start()
		{
			RefreshLocalizedText();
		}
#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();
			RefreshLocalizedText();
		}
#endif
		public void SetLocalizedString(LocalizedString i_localizedString)
		{
			m_localizedString = i_localizedString;
		}

		public void UpdateLocalization(LocalizedString.SupportedLocale i_newLocale)
		{
			m_currentLocale = i_newLocale;
			RefreshLocalizedText();
		}

		public void RefreshLocalizedText()
		{
			if (m_localizedString != null)
			{
				var currentLocalizedString = m_localizedString.GetString(m_currentLocale);

				// Check if the current localized string differs from the displayed text
				if (currentLocalizedString != base.text)
				{
					base.text = currentLocalizedString;
				}
			}
		}

		private void ForceTextMeshProUpdate()
		{
			if (TryGetComponent(out TMP_Text tmpText))
			{
				tmpText.ForceMeshUpdate();
			}
		}
	}
}
