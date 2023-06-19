using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using Firebase.Extensions.TaskExtension; // for ContinueWithOnMainThread

public class FirebaseDatabaseForParent : MonoBehaviour
{

    FirebaseAuth auth;
    FirebaseUser user;
    DatabaseReference databaseReference;

    [Space]
    [Header("Children of a Parent")]
    public GameObject childButtonPrefab;
    public GameObject childrenButtonsPanel;


    [Space]
    [Header("Levels")]
    public string categoryNumber;
    public GameObject Level1Button;
    public GameObject Level2Button;
    public GameObject Level3Button;
    public GameObject Level4Button;
    public GameObject Level5Button;
    public GameObject Level6Button;

    public void Start()
    {

        //ResetLevelCheckmarks();  //Bu gereksiz olabilir. 

        InitializeFirebase();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        //ReadProgressionData(categoryNumber);
        ReadChildrenOfParentData();
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



    public void ReadProgressionData(string catNumber)
    {
        Debug.Log("1");
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).Child("Level_" + catNumber)
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("User Data")
        databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).GetValueAsync().ContinueWithOnMainThread(task =>
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
                    Level1Button.GetComponentInChildren<Toggle>().isOn = true;
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


    public void ReadChildrenOfParentData()
    {
        Debug.Log("1");
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).Child("Level_" + catNumber)
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("User Data")
        databaseReference.Child("Users").Child("Parents").Child(user.UserId).Child("Children").GetValueAsync().ContinueWithOnMainThread(task =>
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


                foreach (DataSnapshot child in snapshot.Children)
                {
                    //bu sekilde her bir cocugun key'ine ulasiliyor
                    Debug.Log(child.Key);

                    databaseReference.Child("Users").Child("Children").Child(child.Key).Child("User Data").GetValueAsync().ContinueWithOnMainThread(task2 =>
                    {
                        if (task2.IsFaulted)
                        {
                            Debug.Log("2");
                            // Handle the error...
                        }
                        else if (task2.IsCompleted)
                        {
                            DataSnapshot dataSnapshot = task2.Result;

                            string firstName = null;
                            string lastName = null;

                            foreach (DataSnapshot userData in dataSnapshot.Children)
                            {
                                Debug.Log(userData.Key);
                                //if (userData.Key != "firstName" || userData.Key != "lastName")
                                //    continue;
                                if (userData.Key == "firstName")
                                    firstName = userData.Value.ToString();
                                if (userData.Key == "lastName")
                                    lastName = userData.Value.ToString();
                                
                            }

                            Debug.Log(firstName);
                            Debug.Log(lastName);
                            //burada buton olusturulacak panele eklenecek
                            GameObject button = (GameObject)Instantiate(childButtonPrefab);
                            button.transform.SetParent(childrenButtonsPanel.transform);//Setting button parent
                            button.GetComponent<RectTransform>().localScale = Vector3.one;
                            //button.GetComponent<RectTransform>().anchorMax = new Vector2(3.0f, 3.0f);
                            //button.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 1.0f);

                            button.GetComponent<Button>().onClick.AddListener(OnClick);//Setting what button does when clicked
                                                                                       //Next line assumes button has child with text as first gameobject like button created from GameObject->UI->Button
                            button.transform.GetChild(0).GetComponent<TMP_Text>().text = firstName + " " + lastName;//Changing text


                        }
                    });
                }



            }
        });
    }
    void OnClick()
    {
        Debug.Log("clicked!");
        
    }
}
