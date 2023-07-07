
using UnityEngine;

using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;


public class FirebaseDatabaseForParent : MonoBehaviour
{

    FirebaseAuth auth;
    FirebaseUser user;
    DatabaseReference databaseReference;


    [Space]
    [Header("Panels")]
    public GameObject childrenOfParentCanvas;
    public GameObject progressionOfChildCanvas;


    [Space]
    [Header("Children of a Parent")]
    public GameObject childButtonPrefab;
    public GameObject childrenButtonsPanel;


   

    [Space]
    [Header("Levels")]
   

    [Header("Category 1")]
    public GameObject Cat1_Level1;
    public GameObject Cat1_Level2;
    public GameObject Cat1_Level3;
    public GameObject Cat1_Level4;
    public GameObject Cat1_Level5;
    public GameObject Cat1_Level6;
    [Header("Category 2")]
    public GameObject Cat2_Level1;
    public GameObject Cat2_Level2;
    public GameObject Cat2_Level3;
    public GameObject Cat2_Level4;
    public GameObject Cat2_Level5;
    public GameObject Cat2_Level6;
    [Header("Category 3")]
    public GameObject Cat3_Level1;
    public GameObject Cat3_Level2;
    public GameObject Cat3_Level3;
    public GameObject Cat3_Level4;
    public GameObject Cat3_Level5;
    public GameObject Cat3_Level6;
    [Header("Category 4")]
    public GameObject Cat4_Level1;
    public GameObject Cat4_Level2;
    public GameObject Cat4_Level3;
    public GameObject Cat4_Level4;
    public GameObject Cat4_Level5;
    public GameObject Cat4_Level6;
    [Header("Category 5")]
    public GameObject Cat5_Level1;
    public GameObject Cat5_Level2;
    public GameObject Cat5_Level3;
    public GameObject Cat5_Level4;
    public GameObject Cat5_Level5;
    public GameObject Cat5_Level6;
    [Header("Category 6")]
    public GameObject Cat6_Level1;
    public GameObject Cat6_Level2;
    public GameObject Cat6_Level3;
    public GameObject Cat6_Level4;
    public GameObject Cat6_Level5;
    public GameObject Cat6_Level6;


   


    public void Start()
    {

        //ResetLevelCheckmarks();  //Bu gereksiz olabilir. 

        InitializeFirebase();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        

        
       
        ReadChildrenOfParentData();
    }

    public void ResetLevelCheckmarks()
    {
       
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


   

    void OnClickOpenLevel(string catNumber, string levelNumber)
    {
        
    }


    
    public void ReadProgressionData(string childKey)
    {
       

        Debug.Log("1");
        DatabaseReference progressionDatabaseRef = databaseReference.Child("Users").Child("Children").Child(childKey).Child("Progression");
        
      


        progressionDatabaseRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
               
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                ProgressionOfChild(snapshot);
            }
        });
        
    }


    public void ProgressionOfChild(DataSnapshot snapshot)
    {
        ProgressionOfSubject1(snapshot);
        ProgressionOfSubject2(snapshot);
        ProgressionOfSubject3(snapshot);
        ProgressionOfSubject4(snapshot);
        ProgressionOfSubject5(snapshot);
        ProgressionOfSubject6(snapshot);

    }

    public void ProgressionOfSubject1(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_1").HasChild("Level_1"))
            Cat1_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_2"))
            Cat1_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_3"))
            Cat1_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_4"))
            Cat1_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_5"))
            Cat1_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_6"))
            Cat1_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject2(DataSnapshot snapshot)
    {

        if (snapshot.Child("Subject_2").HasChild("Level_1"))
            Cat2_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_2"))
            Cat2_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_3"))
            Cat2_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_4"))
            Cat2_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_5"))
            Cat2_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_6"))
            Cat2_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject3(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_3").HasChild("Level_1"))
            Cat3_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_2"))
            Cat3_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_3"))
            Cat3_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_4"))
            Cat3_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_5"))
            Cat3_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_6"))
            Cat3_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject4(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_4").HasChild("Level_1"))
            Cat4_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_2"))
            Cat4_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_3"))
            Cat4_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_4"))
            Cat4_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_5"))
            Cat4_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_6"))
            Cat4_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject5(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_5").HasChild("Level_1"))
            Cat5_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_2"))
            Cat5_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_3"))
            Cat5_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_4"))
            Cat5_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_5"))
            Cat5_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_6"))
            Cat5_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject6(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_6").HasChild("Level_1"))
            Cat6_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_2"))
            Cat6_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_3"))
            Cat6_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_4"))
            Cat6_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_5"))
            Cat6_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_6"))
            Cat6_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }

    public void ReadChildrenOfParentData()
    {
        Debug.Log("1");
       
        databaseReference.Child("Users").Child("Parents").Child(user.UserId).Child("Children").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("2");
                
            }
            else if (task.IsCompleted)
            {
                Debug.Log("3");
                DataSnapshot snapshot = task.Result;


                foreach (DataSnapshot child in snapshot.Children)
                {
                    
                    Debug.Log(child.Key);

                    databaseReference.Child("Users").Child("Children").Child(child.Key).Child("User Data").GetValueAsync().ContinueWithOnMainThread(task2 =>
                    {
                        if (task2.IsFaulted)
                        {
                            Debug.Log("2");
                            
                        }
                        else if (task2.IsCompleted)
                        {
                            DataSnapshot dataSnapshot = task2.Result;

                            string firstName = null;
                            string lastName = null;

                            foreach (DataSnapshot userData in dataSnapshot.Children)
                            {
                                Debug.Log(userData.Key);
                               
                                
                                if (userData.Key == "firstName")
                                    firstName = userData.Value.ToString();
                                if (userData.Key == "lastName")
                                    lastName = userData.Value.ToString();

                            }

                            Debug.Log(firstName);
                            Debug.Log(lastName);
                           
                            GameObject button = (GameObject)Instantiate(childButtonPrefab);
                            button.transform.SetParent(childrenButtonsPanel.transform);
                            button.GetComponent<RectTransform>().localScale = Vector3.one;
                            

                            button.GetComponent<Button>().onClick.AddListener(() => OnClick(child.Key));
                                                                                                        
                            button.transform.GetChild(0).GetComponent<TMP_Text>().text = firstName + " " + lastName;


                        }
                    });
                }



            }
        });
    }
    void OnClick(string childKey)
    {
        OpenProgressionOfChildCanvas();
        ReadProgressionData(childKey);
    }


    public void GoBackToChildrenCanvas()
    {
        progressionOfChildCanvas.SetActive(false);
        childrenOfParentCanvas.SetActive(true);
    }

    public void OpenProgressionOfChildCanvas()
    {
        childrenOfParentCanvas.SetActive(false);
        progressionOfChildCanvas.SetActive(true);

    }


    //-------------------------------------------
    
    public void SignOut()
    {
        auth.SignOut();
        SceneManager.LoadScene("Login Menu");
    }

    public void OpenUserDataPage()
    {
        
    }

    public void OpenAddChildPage()
    {
        SceneManager.LoadScene("Add New Child of a Parent Menu");
    }

    public void DropDownMenu(int val)
    {
       
            
    }
}
