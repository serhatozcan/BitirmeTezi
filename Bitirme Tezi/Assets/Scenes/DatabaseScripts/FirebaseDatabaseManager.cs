using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
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


    public void Start()
    {
        InitializeFirebase();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        ReadData(categoryNumber);
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



    public void ReadData(string catNumber)
    {
        Debug.Log("1");
        databaseReference.Child("Users").Child(user.UserId).Child("Progression").Child("Subject_"+ catNumber).Child("Level_"+ catNumber).GetValueAsync().ContinueWithOnMainThread(task =>
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
                // Do something with snapshot...
                
                Debug.Log(snapshot.Value.ToString());
            }
        });
    }
}
