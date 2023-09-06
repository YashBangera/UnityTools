using TMPro;
using UnityEngine;
using static UnityTools.ScriptableObjects.LocalizedString;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// TextMeshPro component that supports localization based on a selected locale.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [AddComponentMenu("Localized UI/Localized TextMeshProUGUI", 12)]
    public class LocalizedTextMeshProUGUI : TextMeshProUGUI
    {
        #region Data
        #endregion Data

        #region Private Fields
        // Serialized
        
        [SerializeField] private LocalizedString m_localizedString;
        private SupportedLocale m_currentLocale = SupportedLocale.EN_US;

        // Non-Serialized
        #endregion Private Fields

        #region Public Fields

        public new string text
        {
            get
            {
                if (m_localizedString != null)
                    return m_localizedString.GetString(m_currentLocale);
                else
                    return base.text;
            }
            set
            {
                base.text = value;
            }
        }

        #endregion Public Fields

        #region Monobehavior Methods

        protected override void Start()
        {
            // Ensures that the text displays correctly when the game starts.
            ForceMeshUpdate();
        }

        #endregion Monobehavior Methods

        #region Private Methods
        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Updates the localized text to match the selected locale.
        /// </summary>
        /// <param name="newLocale">New locale to set.</param>
        public void UpdateLocalization(SupportedLocale newLocale)
        {
            m_currentLocale = newLocale;

            // Forces a mesh update, which in turn calls the 'text' getter again.
            ForceMeshUpdate();
        }

        #endregion Public Methods

        #region Coroutines
        #endregion Coroutines

        #region Events
        #endregion Events
    }
}
