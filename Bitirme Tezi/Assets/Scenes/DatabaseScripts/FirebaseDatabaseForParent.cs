using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor.Animations;
//using Firebase.Extensions.TaskExtension; // for ContinueWithOnMainThread

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


    //[Space]
    //[Header("Categories")]
    //public GameObject Category1;
    //public GameObject Category2;
    //public GameObject Category3;
    //public GameObject Category4;
    //public GameObject Category5;
    //public GameObject Category6;

    [Space]
    [Header("Levels")]
    //public string categoryNumber;

    //Direkt Category ler burada atanabilir sanirim. ÖNEMLÝ ÖNEMLÝ  ÖNEMLÝ

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


    private List<List<GameObject>> categories;
    private List<GameObject> Cat1Levels;
    private List<GameObject> Cat2Levels;
    private List<GameObject> Cat3Levels;
    private List<GameObject> Cat4Levels;
    private List<GameObject> Cat5Levels;
    private List<GameObject> Cat6Levels;


    public void Start()
    {

        //ResetLevelCheckmarks();  //Bu gereksiz olabilir. 

        InitializeFirebase();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        categories = new List<List<GameObject>>();
        Cat1Levels = new List<GameObject>();
        Cat2Levels = new List<GameObject>();
        Cat3Levels = new List<GameObject>();
        Cat4Levels = new List<GameObject>();
        Cat5Levels = new List<GameObject>();
        Cat6Levels = new List<GameObject>();

        AddLevelsToList();
        //ReadProgressionData(categoryNumber);
        ReadChildrenOfParentData();
    }

    public void ResetLevelCheckmarks()
    {
        //Level1Button.GetComponentInChildren<Toggle>().isOn = false;
        //Level2Button.GetComponentInChildren<Toggle>().isOn = false;
        //Level3Button.GetComponentInChildren<Toggle>().isOn = false;
        //Level4Button.GetComponentInChildren<Toggle>().isOn = false;
        //Level5Button.GetComponentInChildren<Toggle>().isOn = false;
        //Level6Button.GetComponentInChildren<Toggle>().isOn = false;
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


    public void AddLevelsToList()
    {
        //Category1.Find("Level 1").GetComponentInChildren<Button>().onClick.AddListener(() => OnClickOpenLevel("1","1"));
        //GameObject.Find("Category1/Level 1/Button").GetComponent<Button>().onClick.AddListener(() => OnClickOpenLevel("1", "1"));


        categories.Add(Cat1Levels);
        categories.Add(Cat2Levels);
        categories.Add(Cat3Levels);
        categories.Add(Cat4Levels);
        categories.Add(Cat5Levels);
        categories.Add(Cat6Levels);


        Cat1Levels.Add(Cat1_Level1);
        Cat1Levels.Add(Cat1_Level2);
        Cat1Levels.Add(Cat1_Level3);
        Cat1Levels.Add(Cat1_Level4);
        Cat1Levels.Add(Cat1_Level5);
        Cat1Levels.Add(Cat1_Level6);

        Cat2Levels.Add(Cat2_Level1);
        Cat2Levels.Add(Cat2_Level2);
        Cat2Levels.Add(Cat2_Level3);
        Cat2Levels.Add(Cat2_Level4);
        Cat2Levels.Add(Cat2_Level5);
        Cat2Levels.Add(Cat2_Level6);

        Cat3Levels.Add(Cat3_Level1);
        Cat3Levels.Add(Cat3_Level2);
        Cat3Levels.Add(Cat3_Level3);
        Cat3Levels.Add(Cat3_Level4);
        Cat3Levels.Add(Cat3_Level5);
        Cat3Levels.Add(Cat3_Level6);

        Cat4Levels.Add(Cat4_Level1);
        Cat4Levels.Add(Cat4_Level2);
        Cat4Levels.Add(Cat4_Level3);
        Cat4Levels.Add(Cat4_Level4);
        Cat4Levels.Add(Cat4_Level5);
        Cat4Levels.Add(Cat4_Level6);

        Cat5Levels.Add(Cat5_Level1);
        Cat5Levels.Add(Cat5_Level2);
        Cat5Levels.Add(Cat5_Level3);
        Cat5Levels.Add(Cat5_Level4);
        Cat5Levels.Add(Cat5_Level5);
        Cat5Levels.Add(Cat5_Level6);

        Cat6Levels.Add(Cat6_Level1);
        Cat6Levels.Add(Cat6_Level2);
        Cat6Levels.Add(Cat6_Level3);
        Cat6Levels.Add(Cat6_Level4);
        Cat6Levels.Add(Cat6_Level5);
        Cat6Levels.Add(Cat6_Level6);



        //Debug.Log("cat1 " +Cat1Levels.Count);
    }

    void OnClickOpenLevel(string catNumber, string levelNumber)
    {
        //burada bolum acilacak ama bolumu gecme olmayacak
    }


    //void AssignOnClicksToButtons()
    //{
    //    for()
    //    GameObject.Find("Category" + catNumber + "/Level " + levelNumber + "/Button").GetComponent<Button>().onClick()
    //}

    public void ReadProgressionData(string childKey)
    {
        //Category1.GetComponentInChildren<Toggle>().isOn = true;


        Debug.Log("1");
        DatabaseReference progressionDatabaseRef = databaseReference.Child("Users").Child("Children").Child(childKey).Child("Progression");
        
        //burada listleri dolasmam lazim

        //kategori sayisi ve level sayilarini bir yerden cekmem lazim
        Debug.Log(categories.Count);
        Debug.Log(categories[0].Count);


        progressionDatabaseRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
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

                            button.GetComponent<Button>().onClick.AddListener(() => OnClick(child.Key));//Setting what button does when clicked
                                                                                                        //Next line assumes button has child with text as first gameobject like button created from GameObject->UI->Button
                            button.transform.GetChild(0).GetComponent<TMP_Text>().text = firstName + " " + lastName;//Changing text


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
}
