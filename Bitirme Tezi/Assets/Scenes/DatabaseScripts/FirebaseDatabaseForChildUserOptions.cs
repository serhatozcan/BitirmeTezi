
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//using Firebase.Extensions.TaskExtension; // for ContinueWithOnMainThread

public class FirebaseDatabaseForChildUserOptions : MonoBehaviour
{
  

    FirebaseAuth auth;
    FirebaseUser user;
    DatabaseReference databaseReference;

    [Space]
    [Header("Options For Child")]
    public GameObject childUserOptionsButton;
    public GameObject childUserOptionsPanel;

    [Space]
    [Header("SubjectsPanel")]
    public GameObject subjectsPanel;

    

    public void Start()
    {
        
        

        InitializeFirebase();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        childUserOptionsButton.GetComponent<Button>().GetComponentInChildren<TMP_Text>().SetText(user.DisplayName);
        
    }

    public void OpenAndCloseChildUserOptionsMenu()
    {
        if (!childUserOptionsPanel.activeSelf)
        {
            childUserOptionsPanel.SetActive(true);
            Time.timeScale = 0;
            
            foreach (Transform child in subjectsPanel.transform)
            {
               
                child.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            childUserOptionsPanel.SetActive(false);
            Time.timeScale = 1;
            
            foreach (Transform child in subjectsPanel.transform)
            {
                
                child.gameObject.GetComponent<Button>().interactable = true;
            }
            
        }


    }


    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

   
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }



    public void OpenSubjectsMenu()
    {
        SceneManager.LoadScene("Subjects Menu");
    }

    public void SignOut()
    {
        auth.SignOut();
        SceneManager.LoadScene("Real Main Menu");
        Debug.Log("Main menu");
    }
}
