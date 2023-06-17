using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Collections.Generic;


public class FirebaseAuthentication : MonoBehaviour
{
    // Firebase variable
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference db_Users_Reference;

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


    

    private string parentId;

    private void Awake()
    {
        // Check that all of the necessary dependencies for firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
                db_Users_Reference = FirebaseDatabase.DefaultInstance.GetReference("Users");
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
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
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
                    db_Users_Reference.Child("Children").Child(userId).Child("User Data").UpdateChildrenAsync(userData);

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);
                    SceneManager.LoadScene("Login Menu");
                    

                }
            }
        }
        
    }





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
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
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



                    db_Users_Reference.Child("Parents").Child(user.UserId).Child("User Data").UpdateChildrenAsync(userData);
                    parentId = user.UserId;

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);
                    //SceneManager.LoadScene("Register Child of a Parent Menu");
                    OpenRegisterChildWithParentMenu();
                }
            }
        }
    }

    


    public void RegisterChildOfParent()
    {
        StartCoroutine(RegisterChildOfParentAsync(firstNameRegisterField.text, lastNameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
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
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
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

                    string userId = user.UserId;
                    if (parentId != null)
                    {
                        Debug.Log(parentId);
                        //Debug.Log("not null");

                        db_Users_Reference.Child("Children").Child(userId).Child("Parent").SetValueAsync(parentId);

                        Dictionary<string, object> childRef = new Dictionary<string, object>();
                        childRef["Id"] = userId;
                        db_Users_Reference.Child("Parents").Child(parentId).Child("Children").UpdateChildrenAsync(childRef);

                        //deneme amacli silinecek.
                        //db_Users_Reference.Child("Parents").Child(parentId).Child("Children").UpdateChildrenAsync(childRef);


                    }


                    //dataBaseReference.Child("Users").Child("Parents").Child(parentId).Child("Children").Push();
                    //dataBaseReference.Child("Users").Child("Parents").Child(parentId).Child("Children").SetValueAsync(user.UserId);



                    db_Users_Reference.Child("Parents").Child(user.UserId).Child("User Data").UpdateChildrenAsync(userData);
                    parentId = user.UserId;

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);
                    SceneManager.LoadScene("Login Menu");


                }
            }
        }
        //Gerek var mi?
        parentId = null;
    }

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
}
