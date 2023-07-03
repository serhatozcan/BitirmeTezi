
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
    [Header("CategoriesPanel")]
    public GameObject categoriesPanel;

    //private static string currentObservedChild;

    public void Start()
    {
        
        //ResetLevelCheckmarks();  //Bu gereksiz olabilir. 

        InitializeFirebase();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        childUserOptionsButton.GetComponent<Button>().GetComponentInChildren<TMP_Text>().SetText(user.DisplayName);
        //ReadProgressionData(categoryNumber);
        //ReadChildrenOfParentData();
    }

    public void OpenAndCloseChildUserOptionsMenu()
    {
        if (!childUserOptionsPanel.activeSelf)
        {
            childUserOptionsPanel.SetActive(true);
            Time.timeScale = 0;
            //runButton.GetComponent<Button>().interactable = false;
            //addNewChildButton.GetComponent<Button>().interactable = false;
            foreach (Transform child in categoriesPanel.transform)
            {
                //Destroy(child.gameObject);
                child.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            childUserOptionsPanel.SetActive(false);
            Time.timeScale = 1;
            //addNewChildButton.GetComponent<Button>().interactable = true;
            foreach (Transform child in categoriesPanel.transform)
            {
                //Destroy(child.gameObject);
                child.gameObject.GetComponent<Button>().interactable = true;
            }
            //runButton.GetComponent<Button>().interactable = true;
        }


    }


   

    //private string userId;

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
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

    // Handle removing subscription and reference to the Auth instance.
    // Automatically called by a Monobehaviour after Destroy is called on it.
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }



    

    public void OpenCat1Level5()
    {
        SceneManager.LoadScene("Cat1Level5");
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
