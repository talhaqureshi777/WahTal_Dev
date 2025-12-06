using UnityEngine;
using UnityEngine.UI;

namespace WajTal_Dev.UsernameSystem
{
    public class UsernameManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject usernamePanel;
        [SerializeField] private InputField inputField;
        [SerializeField] private Text statusText;
        [SerializeField] private Button saveButton;

        private const string USERNAME_KEY = "PLAYER_USERNAME";

        private void Start()
        {
            saveButton.onClick.AddListener(SaveUsername);

            // Check If Username Exists
            if (PlayerPrefs.HasKey(USERNAME_KEY))
            {
                ClosePanel();
            }
            else
            {
                OpenPanel();
                statusText.text = "Please Enter Your Name";
                statusText.color = Color.white;
            }
        }

        #region PANEL CONTROL

        public void OpenPanel()
        {
            usernamePanel.SetActive(true);

            // Load saved name if exists
            if (PlayerPrefs.HasKey(USERNAME_KEY))
            {
                inputField.text = PlayerPrefs.GetString(USERNAME_KEY);
                statusText.text = "";   // No message needed
            }
            else
            {
                inputField.text = "";
                statusText.text = "Please Enter Your Name";
                statusText.color = Color.white;
            }
        }

        public void ClosePanel()
        {
            // If no name saved, user cannot close
            if (!PlayerPrefs.HasKey(USERNAME_KEY))
            {
                statusText.text = "Name is not saved yet!";
                statusText.color = Color.red;
                return;
            }

            usernamePanel.SetActive(false);
        }

        #endregion

        #region SAVE USERNAME

        private void SaveUsername()
        {
            string name = inputField.text.Trim();

            // 1. Empty Check
            if (string.IsNullOrEmpty(name))
            {
                statusText.text = "Can't Save Empty Name";
                statusText.color = Color.red;
                return;
            }

            // 2. Letters Only Check
            if (!IsAlphabetOnly(name))
            {
                statusText.text = "Only alphabet letters are allowed";
                statusText.color = Color.red;
                return;
            }

            // 3. Save Name
            bool nameWasSavedBefore = PlayerPrefs.HasKey(USERNAME_KEY);
            PlayerPrefs.SetString(USERNAME_KEY, name);
            PlayerPrefs.Save();

            // 4. Status Messages
            if (!nameWasSavedBefore)
            {
                statusText.text = "Name Saved Successfully!";
                statusText.color = Color.green;
            }
            else
            {
                statusText.text = "New Name Saved Successfully!";
                statusText.color = Color.green;
            }
        }

        #endregion

        #region VALIDATION

        private bool IsAlphabetOnly(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsLetter(c))
                    return false;
            }
            return true;
        }

        #endregion
    }
}
