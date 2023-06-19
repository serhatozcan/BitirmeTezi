using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;
//using Firebase.Extensions.TaskExtension; // for ContinueWithOnMainThread

public class FirebaseDatabaseManager : MonoBehaviour
{
    //FirebaseAuth auth;
    //FirebaseUser user;

    //public DatabaseReference databaseReference;
    //private string userId;
    //// Start is called before the first frame update
    ////void Start()
    ////{
    ////    databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    ////    FirebaseUser user = auth.CurrentUser;
    ////    if (user != null)
    ////    {
    ////        //string name = user.DisplayName;
    ////        //string email = user.Email;
    ////        //System.Uri photo_url = user.PhotoUrl;
    ////        // The user's Id, unique to the Firebase project.
    ////        // Do NOT use this value to authenticate with your backend server, if you
    ////        // have one; use User.TokenAsync() instead.
    ////        userId = user.UserId;
    ////    }

    ////    ReadData();
    ////}

    //void AuthStateChanged(object sender, System.EventArgs eventArgs)
    //{
    //    if (auth.CurrentUser != user)
    //    {
    //        bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
    //        if (!signedIn && user != null)
    //        {
    //            Debug.Log("Signed out " + user.UserId);
    //        }
    //        user = auth.CurrentUser;
    //        if (signedIn)
    //        {
    //            Debug.Log("Signed in " + user.UserId);
    //        }
    //    }
    //}



    FirebaseAuth auth;
    FirebaseUser user;
    DatabaseReference databaseReference;

    
    public string categoryNumber;
    [Space]
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
                    Debug.Log(child.Value);
                    
                }
                //List<string> childKeys = new List<string>();
                //foreach (DataSnapshot childRef in snapshot.Children)
                //{
                //    childKeys.Add(childRef.Key);


                //    //Debug.Log(childRef.Key);
                //    //DataSnapshot child = databaseReference.Child("Users").Child("Children").Child(childRef.Key).Child("User Data");
                //}



            }
        });
    }
}
