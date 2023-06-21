using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Tilemaps;


public class FirebaseAuthentication : MonoBehaviour
{
    // Firebase variable
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference databaseReference;

    // Login Variables
    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    [Space]
    [Header("Registration Pages of Child with Parent")]
    public GameObject registerParentMenu;
    public GameObject registerChildMenu;

    // Registration Variables
    [Space]
    [Header("Registration of a Single Child or a Parent")]
    public TMP_InputField firstNameRegisterField;
    public TMP_InputField lastNameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmPasswordRegisterField;


    // Registration Variables
    [Space]
    [Header("Registration of Child of the Parent")]
    public TMP_InputField childFirstNameRegisterField;
    public TMP_InputField childLastNameRegisterField;
    public TMP_InputField childEmailRegisterField;
    public TMP_InputField childPasswordRegisterField;
    public TMP_InputField childConfirmPasswordRegisterField;



    [Space]
    private string parentId;
    private string userId;

    //Firebase kullanirken yeni kullanici kaydoldugunda onceki kullanici hesabindan otomatik olarak cikiliyor ve yeni kullanicinin hesabina giris yapiliyor.
    //Bundan dolayı parent bir child kaydettiginde parent'in hesabina tekrar giris yapmak icin kullanilacaklar.
    private string emailOfParent;
    private string passwordOfParent;


    private void Awake()
    {
        // Check that all of the necessary dependencies for firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
            }
        });
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

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    //-----------------------------------------------------------------------------

    public void GetUserData()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            //string name = user.DisplayName;
            //string email = user.Email;
            //System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            //string uid = user.UserId;
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
                Debug.Log(snapshot.Value.ToString());
            }
        });
    }










    //-----------------------------------------------------------------------------
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

            databaseReference.Child("Users").Child("Children").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("2");
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChild(user.UserId))
                    {
                        SceneManager.LoadScene("Subjects Menu");
                    }
                }
            });

            databaseReference.Child("Users").Child("Parents").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("2");
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChild(user.UserId))
                    {
                        SceneManager.LoadScene("Children of a Parent Menu");
                    }
                }
            });

            

        }
    }
    //--------------------------+++++++++++++++++++++++++++++++++++++++++++
    public void LoginAgain()
    {
        StartCoroutine(LoginAgainAsync(emailOfParent, passwordOfParent));
    }

    private IEnumerator LoginAgainAsync(string email, string password)
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

            databaseReference.Child("Users").Child("Children").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("2");
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChild(user.UserId))
                    {
                        SceneManager.LoadScene("Subjects Menu");
                    }
                }
            });

            databaseReference.Child("Users").Child("Parents").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("2");
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChild(user.UserId))
                    {
                        SceneManager.LoadScene("Children of a Parent Menu");
                    }
                }
            });



        }
    }



    //-----------------------------------------------------------------------------
    public void RegisterChild()
    {

        StartCoroutine(RegisterChildAsync(firstNameRegisterField.text, lastNameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterChildAsync(string firstName, string lastName, string email, string password, string confirmPassword)
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
            Debug.LogError("Şifreler aynı değil");
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
                    userData["e-mail"] = email;



                    //Dictionary<string, object> childUpdates = new Dictionary<string, object>();
                    string userId = user.UserId;

                    //Oldu mu???
                    databaseReference.Child("Users").Child("Children").Child(userId).Child("User Data").UpdateChildrenAsync(userData);

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);
                    SceneManager.LoadScene("Login Menu");
                    

                }
            }
        }
        
    }

    //-----------------------------------------------------------------------------
    public void RegisterParent()
    {

        StartCoroutine(RegisterParentAsync(firstNameRegisterField.text, lastNameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterParentAsync(string firstName, string lastName, string email, string password, string confirmPassword)
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
            Debug.LogError("Şifreler aynı değil");
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
                    emailOfParent = email;
                    passwordOfParent = password;

                    Dictionary<string, object> userData = new Dictionary<string, object>();
                    userData["firstName"] = firstName;
                    userData["lastName"] = lastName;
                    userData["e-mail"] = email;



                    databaseReference.Child("Users").Child("Parents").Child(user.UserId).Child("User Data").UpdateChildrenAsync(userData);
                    parentId = user.UserId;

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);
                    //SceneManager.LoadScene("Register Child of a Parent Menu");
                    OpenRegisterChildWithParentMenu();
                }
            }
        }
    }

    //-----------------------------------------------------------------------------
    public void RegisterChildOfParent()
    {
        StartCoroutine(RegisterChildOfParentAsync(childFirstNameRegisterField.text, childLastNameRegisterField.text, childEmailRegisterField.text, childPasswordRegisterField.text, childConfirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterChildOfParentAsync(string firstName, string lastName, string email, string password, string confirmPassword)
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
            Debug.LogError("Şifreler aynı değil");
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

                    string userId = user.UserId;
                    

                    
                    if (parentId != null)
                    {
                        Dictionary<string, object> userData = new Dictionary<string, object>();
                        userData["firstName"] = firstName;
                        userData["lastName"] = lastName;
                        userData["e-mail"] = email;
                        databaseReference.Child("Users").Child("Children").Child(userId).Child("User Data").UpdateChildrenAsync(userData);

                        Debug.Log(parentId);
                        //Debug.Log("not null");

                        databaseReference.Child("Users").Child("Children").Child(userId).Child("Parent").SetValueAsync(parentId);


                        //databaseReference.Child("Users").Child("Parents").Child(parentId).Child("Children").Push();
                        Dictionary<string, object> childRef = new Dictionary<string, object>();
                        childRef["Id"] = userId;
                        //databaseReference.Child("Users").Child("Parents").Child(parentId).Child("Children").Push().UpdateChildrenAsync(childRef);

                        //Cok garip bir yontem oldu ama calisabilir.
                        databaseReference.Child("Users").Child("Parents").Child(parentId).Child("Children").Child(userId).UpdateChildrenAsync(childRef);
                        //deneme amacli silinecek.
                        //db_Users_Reference.Child("Parents").Child(parentId).Child("Children").UpdateChildrenAsync(childRef);

                    }
                    else
                    {
                        //Hata oldu anlamina geliyor.
                        //Burada hem parent hem child auth silinecek. Parent database verileri de silinecek.
                    }


                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);

                    //Firebase otomatik hesap degistirdigi icin gerekli
                    LoginAgain();
                        

                    //bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
                    //if (signedIn)
                    //{
                    //    SceneManager.LoadScene("Children of a Parent Menu");
                    //}
                    //else
                    //{
                    //    SceneManager.LoadScene("Login Menu");
                    //}



                }
            }
        }
        //Gerek var mi?
        parentId = null;
    }
    //-----------------------------------------------------------------------------
    public void GoBackToRegisterParentMenu()
    {
        registerChildMenu.SetActive(false);
        registerParentMenu.SetActive(true);
    }

    public void OpenRegisterChildWithParentMenu()
    {
        registerParentMenu.SetActive(false);
        registerChildMenu.SetActive(true);
    }
    //-----------------------------------------------------------------------------

    public void GoBackToChildrenOfaParentMenu()
    {
        SceneManager.LoadScene("Children of a Parent Menu");
    }

    public void SignOut()
    {
        auth.SignOut();
        SceneManager.LoadScene("Login Menu");
    }

    public void Quit()
    {
        auth.SignOut();
        Application.Quit();
    }

}
