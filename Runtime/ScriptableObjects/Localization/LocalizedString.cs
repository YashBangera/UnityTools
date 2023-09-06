using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.ScriptableObjects
{
    /// <summary>
    /// Represents a string that can be localized based on the selected locale.
    /// </summary>
    [CreateAssetMenu(fileName = "LocalizedString", menuName = "Unity Tools/Scriptable Objects/Localization/Localized String")]
    public class LocalizedString : ScriptableObject
    {
        #region Data

        public enum SupportedLocale
        {
            EN_US,   // U.S. English
            EN_GB,   // British English
            ES_ES,   // Spain Spanish
            ES_MX    // Mexican Spanish
            // ... Add any other locales you support
        }

        [System.Serializable]
        public struct LocaleStringPair
        {
            public SupportedLocale locale;
            [TextArea] public string localizedString;
        }

        #endregion Data

        #region Public Fields

        [SerializeField] LocaleStringPair[] m_localizedStrings;

        #endregion Public Fields

        #region Public Methods

        /// <summary>
        /// Retrieves the localized string for the given locale.
        /// </summary>
        /// <param name="i_locale">The desired locale.</param>
        /// <returns>The localized string.</returns>
        public string GetString(SupportedLocale i_locale)
        {
            foreach (var pair in m_localizedStrings)
            {
                if (pair.locale == i_locale)
                {
                    return pair.localizedString;
                }
            }
            return "N/A"; // Return some default value if not found
        }

        #endregion Public Methods
    }
}
