﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Collections.Generic;
using System.Globalization;

public class FirebaseAuthentication : MonoBehaviour
{
    // Firebase variable
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference dataBaseReference;

    // Login Variables
    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    // Registration Variables
    [Space]
    [Header("Registration")]
    public string parentId;
    public TMP_InputField firstNameRegisterField;
    public TMP_InputField lastNameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField phoneRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;

    private void Awake()
    {
        // Check that all of the necessary dependencies for firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();

            }
            else
            {
                Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
            }
        });
    }


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
            dataBaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        }
        else
        {
            Debug.LogError("Database error");
        }


    }



    void InitializeFirebase()
    {
        //Set the default instance object
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
                Debug.Log("Çıkış yapıldı " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Giriş yapıldı " + user.UserId);
            }
        }
    }

    public void Login()
    {
        StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;


            string failedMessage = "Giriş başarısız oldu: ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "E-posta geçersiz";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Şifre yanlış";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "E-posta bulunamadı";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Şifre bulunamadı";
                    break;
                default:
                    failedMessage = "Giriş başarısız oldu";
                    break;
            }

            Debug.Log(failedMessage);
        }
        else
        {
            user = loginTask.Result.User;

            //Debug.LogFormat("{0} giriş yaptınız", user.DisplayName);
            Debug.Log("Hoş geldiniz " + user.DisplayName);
            SceneManager.LoadScene("Subjects Menu");
        }
    }


    public void RegisterChild()
    {

        StartCoroutine(RegisterChildAsync(parentId, firstNameRegisterField.text, lastNameRegisterField.text, phoneRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterChildAsync(string parentId, string firstName, string lastName, string phone, string email, string password, string confirmPassword)
    {
        if (firstName == "")
        {
            Debug.LogError("Ad boş bırakılamaz");
        }
        else if (lastName == "")
        {
            Debug.LogError("Soyad boş bırakılamaz");
        }
        else if (email == "")
        {
            Debug.LogError("E-posta boş bırakılamaz");
        }
        else if (password != confirmPassword)
        {
            Debug.LogError("Şifreler eşleşmiyor");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "Kayıt başarısız oldu: ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "E-posta geçersiz";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Şifre yanlış";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "E-posta bulunamadı";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Şifre bulunamadı";
                        break;
                    default:
                        failedMessage = "Kayıt başarısız oldu";
                        break;
                }

                Debug.Log(failedMessage);
            }
            else
            {
                // Get The User After Registration Success
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = firstName + " " + lastName };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    // Delete the user if user update failed
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Profil güncelleştirilmesi başarısız oldu: ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "E-posta geçersiz";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Şifre yanlış";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "E-posta bulunamadı";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Şifre bulunamadı";
                            break;
                        default:
                            failedMessage = "Profil güncelleştirilmesi başarısız oldu";
                            break;
                    }

                    Debug.Log(failedMessage);
                }
                else
                {
                    Dictionary<string, object> userData = new Dictionary<string, object>();
                    userData["firstName"] = firstName;
                    userData["lastName"] = lastName;
                    userData["phone"] = phone;
                    userData["e-mail"] = email;

                    dataBaseReference.Child("Users").Child("Children").Child(user.UserId).Child("UserData").UpdateChildrenAsync(userData);
                    dataBaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Parent").SetValueAsync(parentId);

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);
                    SceneManager.LoadScene("Login Menu");

                }
            }
        }
        parentId = null;
    }





    public void RegisterParent()
    {

        StartCoroutine(RegisterParentAsync(firstNameRegisterField.text, lastNameRegisterField.text, emailRegisterField.text, phoneRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterParentAsync(string firstName, string lastName, string email, string phone, string password, string confirmPassword)
    {
        if (firstName == "")
        {
            Debug.LogError("Ad boş bırakılamaz");
        }
        else if (lastName == "")
        {
            Debug.LogError("Soyad boş bırakılamaz");
        }
        else if (email == "")
        {
            Debug.LogError("E-posta boş bırakılamaz");
        }
        else if (password != confirmPassword)
        {
            Debug.LogError("Şifreler eşleşmiyor");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "Kayıt başarısız oldu: ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "E-posta geçersiz";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Şifre yanlış";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "E-posta bulunamadı";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Şifre bulunamadı";
                        break;
                    default:
                        failedMessage = "Kayıt başarısız oldu";
                        break;
                }

                Debug.Log(failedMessage);
            }
            else
            {
                // Get The User After Registration Success
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = firstName + " " + lastName };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    // Delete the user if user update failed
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Profil güncelleştirilmesi başarısız oldu: ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "E-posta geçersiz";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Şifre yanlış";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "E-posta bulunamadı";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Şifre bulunamadı";
                            break;
                        default:
                            failedMessage = "Profil güncelleştirilmesi başarısız oldu";
                            break;
                    }

                    Debug.Log(failedMessage);
                }
                else
                {
                    Dictionary<string, object> userData = new Dictionary<string, object>();
                    userData["firstName"] = firstName;
                    userData["lastName"] = lastName;
                    userData["phone"] = phone;
                    userData["e-mail"] = email;

                    dataBaseReference.Child("Users").Child("Parents").Child(user.UserId).Child("UserData").UpdateChildrenAsync(userData);
                    parentId = user.UserId;

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);
                    SceneManager.LoadScene("Register Child of a Parent Menu");

                }
            }
        }
    }
}