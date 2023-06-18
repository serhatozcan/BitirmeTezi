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
    FirebaseAuth auth;
    FirebaseUser user;

    public DatabaseReference databaseReference;
    private string userId;
    // Start is called before the first frame update
    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            //string name = user.DisplayName;
            //string email = user.Email;
            //System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            userId = user.UserId;
        }
    }

    public void ReadData()
    {
        databaseReference.Child("Users").Child(userId).Child("Progression").Child("Subject_1").Child("Level_1").GetValueAsync().ContinueWithOnMainThread(task =>
      {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              // Do something with snapshot...
          }
      });
    }
}
