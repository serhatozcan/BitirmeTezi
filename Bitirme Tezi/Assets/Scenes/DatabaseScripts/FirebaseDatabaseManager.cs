
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using Firebase.Extensions.TaskExtension; // for ContinueWithOnMainThread

public class FirebaseDatabaseManager : MonoBehaviour
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

    [Space]
    [Header("Levels")]
    public string subjectNumber;
    public GameObject Level1Button;
    public GameObject Level2Button;
    public GameObject Level3Button;
    public GameObject Level4Button;
    public GameObject Level5Button;
    public GameObject Level6Button;

    public void Start()
    {
        
        ResetLevelCheckmarks();  //Bu gereksiz olabilir. 

        InitializeFirebase();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        ReadProgressionData(subjectNumber);
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
            foreach (Transform child in subjectsPanel.transform)
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
            foreach (Transform child in subjectsPanel.transform)
            {
                //Destroy(child.gameObject);
                child.gameObject.GetComponent<Button>().interactable = true;
            }
            //runButton.GetComponent<Button>().interactable = true;
        }


    }


    public void ResetLevelCheckmarks()
    {
        Level1Button.GetComponentInChildren<Toggle>().isOn = false;
        Level2Button.GetComponentInChildren<Toggle>().isOn = false;
        Level3Button.GetComponentInChildren<Toggle>().isOn = false;
        Level4Button.GetComponentInChildren<Toggle>().isOn = false;
        Level5Button.GetComponentInChildren<Toggle>().isOn = false;
        Level6Button.GetComponentInChildren<Toggle>().isOn = false;
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



    public void ReadProgressionData(string subjectNumber)
    {
        Debug.Log("1");
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).Child("Level_" + catNumber)
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("User Data")
        databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + subjectNumber).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("2");
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                Debug.Log("3");
                DataSnapshot snapshot = task.Result;
               



                //Sayfa degistirince veya yeni kullanici gelince hepsi resetlenecek mi? Kontrol etmek lazim.
                if (snapshot.HasChild("Level_1"))
                    Level1Button.GetComponentInChildren<Toggle>().isOn=true;
                if (snapshot.HasChild("Level_2"))
                    Level2Button.GetComponentInChildren<Toggle>().isOn = true;
                if (snapshot.HasChild("Level_3"))
                    Level3Button.GetComponentInChildren<Toggle>().isOn = true;
                if (snapshot.HasChild("Level_4"))
                    Level4Button.GetComponentInChildren<Toggle>().isOn = true;
                if (snapshot.HasChild("Level_5"))
                    Level5Button.GetComponentInChildren<Toggle>().isOn = true;
                if (snapshot.HasChild("Level_6"))
                    Level6Button.GetComponentInChildren<Toggle>().isOn = true;
                
            }
        });
    }

    public void OpenSubject1Level5()
    {
        SceneManager.LoadScene("Subject1Level5");
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
