using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;

public class DB_Script : MonoBehaviour
{
    public DatabaseReference databaseReference;

    public InputField userNameInput, passwordInput, phoneNumberInput, emailAddressInput;


    private void Start()
    {
        StartCoroutine(Initialization());
    }

    private IEnumerator Initialization()
    {
        var task = FirebaseApp.CheckAndFixDependenciesAsync();

        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError("Database error: " + task.Exception);
        }

        var dependencyStatus = task.Result;

        if (dependencyStatus == DependencyStatus.Available)
        {
            databaseReference = FirebaseDatabase.DefaultInstance.GetReference("Users");
            
        }
        else
        {
            Debug.LogError("Database error");
        }


    }

    public void RegisterUser()
    {
        string userName = userNameInput.text;
        string password = passwordInput.text;
        string phoneNumber = phoneNumberInput.text;
        string emailAddress = emailAddressInput.text;

        Dictionary<string, object> user = new Dictionary<string, object>();
        user["userName"] = userName;
        user["Password"] = password;
        user["phoneNumber"] = phoneNumber;
        user["email"] = emailAddress;


        string key = databaseReference.Push().Key;

        databaseReference.Child(key).UpdateChildrenAsync(user);
    }

    public void GetData()
    {
        StartCoroutine(GetUserData());
    }

    public IEnumerator GetUserData()
    {
        //burada kullaniciya ozel bir sey olmasi lazim
        string userName = userNameInput.text;

        var task = databaseReference.Child(userName).GetValueAsync();

        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError("Database error: " + task.Exception);
        }

        DataSnapshot snapshot = task.Result;

        foreach (DataSnapshot user in snapshot.Children)
        {
            if (user.Key == "userName")
            {
                Debug.Log("User name: " + user.Value.ToString());
            }

            if (user.Key == "password")
            {
                Debug.Log("Password: " + user.Value.ToString());
            }
        }

    }

}
