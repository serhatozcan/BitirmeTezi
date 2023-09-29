using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection.Emit;

public class FirebaseAuthentication : MonoBehaviour
{
    // Firebase variable
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference databaseReference;

    [Space]
    [Header("Children of a Parent")]
    public GameObject childButtonPrefab;
    public GameObject childrenOfParent;
    public GameObject childrenList;

    //[Space]
    //public List<GameObject> listOfChildrenButtons;

    [Space]
    [Header("Pages")]
    [Space]
    [Header("Main Menu")]
    public GameObject mainMenu;
    [Header("Login Menu")]
    public GameObject loginMenu;
    [Header("User Type Selection Menu")]
    public GameObject userTypeSelectionMenu;
    [Space]
    [Header("Registration Pages of Child with Parent")]
    public GameObject registerParentMenu;
    public GameObject registerChildOfParentMenu;
    [Space]
    [Header("Registration Page of a Child without Parent")]
    public GameObject registerSingleChildMenu;
    [Space]
    [Header("Subjects Menu for Child")]
    public GameObject subjectsMenu;
    // Login Variables
    [Space]
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;


    // Registration Variables
    [Space]
    [Header("Registration of a Parent")]
    public TMP_InputField parentFirstNameRegisterField;
    public TMP_InputField parentLastNameRegisterField;
    public TMP_InputField parentEmailRegisterField;
    public TMP_InputField parentPasswordRegisterField;
    public TMP_InputField parentConfirmPasswordRegisterField;


    // Registration Variables
    [Space]
    [Header("Registration of a Single Child")]
    public TMP_InputField singleChildFirstNameRegisterField;
    public TMP_InputField singleChildLastNameRegisterField;
    public TMP_InputField singleChildEmailRegisterField;
    public TMP_InputField singleChildPasswordRegisterField;
    public TMP_InputField singleChildConfirmPasswordRegisterField;

    // Registration Variables
    [Space]
    [Header("Registration of Child of the Parent")]
    public TMP_InputField childFirstNameRegisterField;
    public TMP_InputField childLastNameRegisterField;
    public TMP_InputField childEmailRegisterField;
    public TMP_InputField childPasswordRegisterField;
    public TMP_InputField childConfirmPasswordRegisterField;

    [Space]
    [Header("Error messages")]
    public GameObject loginMenuErrorMessage;
    public GameObject registerParentMenuErrorMessage;
    public GameObject registerChildOfParentMenuErrorMessage;
    public GameObject registerSingleChildMenuErrorMessage;

    [Space]
    [Header("Options For Parent")]
    public GameObject parentUserOptionsButton;
    public GameObject parentUserOptionsPanel;
    public GameObject addNewChildButton;

    [Space]
    [Header("Options For Child")]
    public GameObject childUserOptionsButton;
    public GameObject childUserOptionsPanel;
    [Space]
    public GameObject subjectsPanel;

    [Space]
    private static string currentObservedChild;
    [Space]
    private string parentId;
    private string userId;

    //Firebase kullanirken yeni kullanici kaydoldugunda onceki kullanici hesabindan otomatik olarak cikiliyor ve yeni kullanicinin hesabina giris yapiliyor.
    //Bundan dolayı parent bir child kaydettiginde parent'in hesabina tekrar giris yapmak icin kullanilacaklar.
    private string emailOfParent;
    private string passwordOfParent;

    //******
    [Space]
    [Space]
    public TMP_Text nameOfObservedChild;
    [Space]
    public GameObject progressionOfChildCanvas;
    [Space]
    [Header("Levels")]
    //public string categoryNumber;

    //Direkt Category ler burada atanabilir sanirim. ÖNEMLİ ÖNEMLİ  ÖNEMLİ

    [Header("Subject 1")]
    public GameObject Subject1_Level1;
    public GameObject Subject1_Level2;
    public GameObject Subject1_Level3;
    public GameObject Subject1_Level4;
    public GameObject Subject1_Level5;
    public GameObject Subject1_Level6;
    [Header("Subject 2")]
    public GameObject Subject2_Level1;
    public GameObject Subject2_Level2;
    public GameObject Subject2_Level3;
    public GameObject Subject2_Level4;
    public GameObject Subject2_Level5;
    public GameObject Subject2_Level6;
    [Header("Subject 3")]
    public GameObject Subject3_Level1;
    public GameObject Subject3_Level2;
    public GameObject Subject3_Level3;
    public GameObject Subject3_Level4;
    public GameObject Subject3_Level5;
    public GameObject Subject3_Level6;
    [Header("Subject 4")]
    public GameObject Subject4_Level1;
    public GameObject Subject4_Level2;
    public GameObject Subject4_Level3;
    public GameObject Subject4_Level4;
    public GameObject Subject4_Level5;
    public GameObject Subject4_Level6;
    [Header("Subject 5")]
    public GameObject Subject5_Level1;
    public GameObject Subject5_Level2;
    public GameObject Subject5_Level3;
    public GameObject Subject5_Level4;
    public GameObject Subject5_Level5;
    public GameObject Subject5_Level6;
    [Header("Subject 6")]
    public GameObject Subject6_Level1;
    public GameObject Subject6_Level2;
    public GameObject Subject6_Level3;
    public GameObject Subject6_Level4;
    public GameObject Subject6_Level5;
    public GameObject Subject6_Level6;


    List<GameObject> level1Objects;
    List<GameObject> level2Objects;
    List<GameObject> level3Objects;
    List<GameObject> level4Objects;
    List<GameObject> level5Objects;
    List<GameObject> level6Objects;
    bool signedIn;

    public string observedChildFirstName;
    public string observedChildLastName;

    private void Awake()
    {
       
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
    public void Start()
    {
        
        databaseReference.Child("Users").Child("Parents").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("2");
                
            }
            else if (task.IsCompleted)
            {

                DataSnapshot snapshot = task.Result;
                if (signedIn)
                {
                    if (snapshot.HasChild(user.UserId))
                    {
                        Debug.Log("parent");

                        
                        OpenProgressionOfChildWithoutNewNames();
                      
                        ReadProgressionData(currentObservedChild);
                    }
                }

                
            }
        });

        databaseReference.Child("Users").Child("Children").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("2");
               
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (signedIn)
                {
                    if (snapshot.HasChild(user.UserId))
                    {
                        Debug.Log("child");
                        SceneManager.LoadScene("Subjects Menu");
                       
                    }
                }
                
            }
        });



    }

   

    void InitializeFirebase()
    {
        
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

   
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
         
            signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
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

    public void LoginMenuErrorMessage(string errorMessage)
    {
        loginMenuErrorMessage.SetActive(true);
        loginMenuErrorMessage.GetComponent<TMP_Text>().SetText(errorMessage);
    }
    public void RegisterSingleChildMenuErrorMessage(string errorMessage)
    {
        registerSingleChildMenuErrorMessage.SetActive(true);
        registerSingleChildMenuErrorMessage.GetComponent<TMP_Text>().SetText(errorMessage);
    }
    public void RegisterChildOfParentMenuErrorMessage(string errorMessage)
    {
        registerChildOfParentMenuErrorMessage.SetActive(true);
        registerChildOfParentMenuErrorMessage.GetComponent<TMP_Text>().SetText(errorMessage);
    }
    public void RegisterParentMenuErrorMessage(string errorMessage)
    {
        registerParentMenuErrorMessage.SetActive(true);
        registerParentMenuErrorMessage.GetComponent<TMP_Text>().SetText(errorMessage);
    }

    public void GetUserData()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            
            userId = user.UserId;
        }
    }

    public void ReadData()
    {
        databaseReference.Child("Users").Child(userId).Child("Progression").Child("Subject_1").Child("Level_1").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
               
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                
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
            LoginMenuErrorMessage(failedMessage);
        }
        else
        {
            user = loginTask.Result.User;

            
            Debug.Log("Hoş geldiniz " + user.DisplayName);

            databaseReference.Child("Users").Child("Children").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("2");
                    LoginMenuErrorMessage("Veri tabanı bağlantısında hata oldu");
                   
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChild(user.UserId))
                    {
                        Debug.Log("child");
                        SceneManager.LoadScene("Subjects Menu");
                       
                    }
                }
            });

            databaseReference.Child("Users").Child("Parents").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("2");
                    LoginMenuErrorMessage("Veri tabanı bağlantısında hata oldu.");
                    
                }
                else if (task.IsCompleted)
                {

                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChild(user.UserId))
                    {
                        Debug.Log("parent");
                        parentId = user.UserId;
                        emailOfParent = email;
                        passwordOfParent = password;
                        OpenChildrenOfaParentMenu();
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

           
            Debug.Log("Hoş geldiniz " + user.DisplayName);

            databaseReference.Child("Users").Child("Children").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("2");
                    
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
                   
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChild(user.UserId))
                    {
                        OpenChildrenOfaParentMenu();
                    }
                }
            });



        }
    }



    //-----------------------------------------------------------------------------
    public void RegisterChild()
    {

        StartCoroutine(RegisterChildAsync(singleChildFirstNameRegisterField.text, singleChildLastNameRegisterField.text, singleChildEmailRegisterField.text, singleChildPasswordRegisterField.text, singleChildConfirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterChildAsync(string firstName, string lastName, string email, string password, string confirmPassword)
    {
        if (firstName == "")
        {
            Debug.LogError("Ad boş bırakılamaz");
            RegisterSingleChildMenuErrorMessage("Ad boş bırakılamaz");
        }
        else if (lastName == "")
        {
            Debug.LogError("Soyad boş bırakılamaz");
            RegisterSingleChildMenuErrorMessage("Soyad boş bırakılamaz");
        }
        else if (email == "")
        {
            Debug.LogError("E-posta boş bırakılamaz");
            RegisterSingleChildMenuErrorMessage("E-posta boş bırakılamaz");
        }
        else if (password != confirmPassword)
        {
            Debug.LogError("Şifreler aynı değil");
            RegisterSingleChildMenuErrorMessage("Şifreler uyuşmuyor");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);
                RegisterSingleChildMenuErrorMessage("Veri tabanına kaydederken hata oldu");
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
                RegisterSingleChildMenuErrorMessage(failedMessage);
            }
            else
            {
               
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = firstName + " " + lastName };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                   
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);
                    RegisterSingleChildMenuErrorMessage("Kullanıcı kaydedilemedi.");

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
                    RegisterSingleChildMenuErrorMessage(failedMessage);
                }
                else
                {


                    Dictionary<string, object> userData = new Dictionary<string, object>();
                    userData["firstName"] = firstName;
                    userData["lastName"] = lastName;
                    userData["e-mail"] = email;



                    
                    string userId = user.UserId;

                    
                    databaseReference.Child("Users").Child("Children").Child(userId).Child("User Data").UpdateChildrenAsync(userData);

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);
                    
                    OpenLoginMenu();

                }
            }
        }

    }

    //-----------------------------------------------------------------------------
    public void RegisterParent()
    {

        StartCoroutine(RegisterParentAsync(parentFirstNameRegisterField.text, parentLastNameRegisterField.text, parentEmailRegisterField.text, parentPasswordRegisterField.text, parentConfirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterParentAsync(string firstName, string lastName, string email, string password, string confirmPassword)
    {
        if (firstName == "")
        {
            Debug.LogError("Ad boş bırakılamaz");
            RegisterParentMenuErrorMessage("Ad boş bırakılamaz");
        }
        else if (lastName == "")
        {
            Debug.LogError("Soyad boş bırakılamaz");
            RegisterParentMenuErrorMessage("Soyad boş bırakılamaz");
        }
        else if (email == "")
        {
            Debug.LogError("E-posta boş bırakılamaz");
            RegisterParentMenuErrorMessage("E-posta boş bırakılamaz");
        }
        else if (password != confirmPassword)
        {
            Debug.LogError("Şifreler aynı değil");
            RegisterParentMenuErrorMessage("Şifreler uyuşmuyor.");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);
                RegisterParentMenuErrorMessage("Veri tabanına kaydederken hata oldu.");

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
                RegisterParentMenuErrorMessage(failedMessage);
            }
            else
            {
               
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = firstName + " " + lastName };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);
                    RegisterParentMenuErrorMessage("Kullanıcı kaydedilemedi.");

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
                    RegisterParentMenuErrorMessage(failedMessage);
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
            RegisterChildOfParentMenuErrorMessage("Ad boş bırakılamaz");
        }
        else if (lastName == "")
        {
            Debug.LogError("Soyad boş bırakılamaz");
            RegisterChildOfParentMenuErrorMessage("Soyad boş bırakılamaz");
        }
        else if (email == "")
        {
            Debug.LogError("E-posta boş bırakılamaz");
            RegisterChildOfParentMenuErrorMessage("E-posta boş bırakılamaz");
        }
        else if (password != confirmPassword)
        {
            Debug.LogError("Şifreler aynı değil");
            RegisterChildOfParentMenuErrorMessage("Şifreler uyuşmuyor");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);
                RegisterChildOfParentMenuErrorMessage("Veri tabanına kaydederken hata oldu");

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
                RegisterChildOfParentMenuErrorMessage(failedMessage);
            }
            else
            {
                
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = firstName + " " + lastName };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);
                    RegisterChildOfParentMenuErrorMessage("Kullanıcı kaydedilemedi");
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
                    RegisterChildOfParentMenuErrorMessage(failedMessage);
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
                       

                        databaseReference.Child("Users").Child("Children").Child(userId).Child("Parent").SetValueAsync(parentId);


                       
                        Dictionary<string, object> childRef = new Dictionary<string, object>();
                        childRef["Id"] = userId;
                      
                        databaseReference.Child("Users").Child("Parents").Child(parentId).Child("Children").Child(userId).UpdateChildrenAsync(childRef);
                       

                    }
                    else
                    {
                        Debug.Log("parent id null");
                        
                    }


                    Debug.Log(emailOfParent);
                    Debug.Log(passwordOfParent);

                    Debug.Log("Kayıt başarıyla tamamlandı. Hoşgeldiniz " + user.DisplayName);

                    
                    LoginAgain();





                }
            }
        }
       
    }
    //-----------------------------------------------------------------------------
    public void OpenMainMenu()
    {
        TurnOffAllPages();
        mainMenu.SetActive(true);
    }

    public void OpenSubjectsMenu()
    {
        TurnOffAllPages();
        subjectsMenu.SetActive(true);
    }

    public void OpenRegisterParentMenu()
    {
        TurnOffAllPages();
        registerParentMenu.SetActive(true);
    }

    public void OpenRegisterChildWithParentMenu()
    {
        TurnOffAllPages();
        registerChildOfParentMenu.SetActive(true);
    }
    public void SignOut()
    {
        auth.SignOut();
        TurnOffAllPages();
        mainMenu.SetActive(true);
    }
    public void OpenRegisterSingleChildMenu()
    {
        TurnOffAllPages();
        registerSingleChildMenu.SetActive(true);
    }

    public void OpenLoginMenu()
    {
        TurnOffAllPages();
        loginMenu.SetActive(true);
    }
    public void OpenUserTypeSelectionMenu()
    {
        TurnOffAllPages();
        userTypeSelectionMenu.SetActive(true);
    }


    public void OpenChildrenOfaParentMenu()
    {
        
        TurnOffAllPages();
        childrenOfParent.SetActive(true);
        
        parentUserOptionsButton.GetComponent<Button>().GetComponentInChildren<TMP_Text>().SetText(user.DisplayName);
        ReadChildrenOfParentData();
    }


    public void OpenAndCloseParentUserOptionsMenu()
    {
        if (!parentUserOptionsPanel.activeSelf)
        {
            parentUserOptionsPanel.SetActive(true);
            Time.timeScale = 0;
           
            addNewChildButton.GetComponent<Button>().interactable = false;
            foreach (Transform child in childrenList.transform)
            {
                
                child.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            parentUserOptionsPanel.SetActive(false);
            Time.timeScale = 1;
            addNewChildButton.GetComponent<Button>().interactable = true;
            foreach (Transform child in childrenList.transform)
            {
                
                child.gameObject.GetComponent<Button>().interactable = true;
            }
            
        }


    }

    public void OpenAndCloseChildUserOptionsMenu()
    {
        if (!childUserOptionsPanel.activeSelf)
        {
            childUserOptionsPanel.SetActive(true);
            Time.timeScale = 0;
            
            foreach (Transform child in subjectsPanel.transform)
            {
                
                child.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            childUserOptionsPanel.SetActive(false);
            Time.timeScale = 1;
            
            foreach (Transform child in subjectsPanel.transform)
            {
                
                child.gameObject.GetComponent<Button>().interactable = true;
            }
            
        }


    }

    public void OpenProgressionOfChildWithoutNewNames()
    {
        TurnOffAllPages();
        progressionOfChildCanvas.SetActive(true);
       
    }
    public void OpenProgressionOfChildWithNewNames(string firstName, string lastName)
    {
        TurnOffAllPages();
        progressionOfChildCanvas.SetActive(true);
        nameOfObservedChild.SetText(firstName+" "+lastName+ " İlerleme Durumu");
        
    }

    public void TurnOffAllPages()
    {
        mainMenu.SetActive(false);
        loginMenu.SetActive(false);
        userTypeSelectionMenu.SetActive(false);
        registerParentMenu.SetActive(false);
        registerChildOfParentMenu.SetActive(false);
        registerSingleChildMenu.SetActive(false);
        childrenOfParent.SetActive(false);
        progressionOfChildCanvas.SetActive(false);
    }

    public void Quit()
    {
        auth.SignOut();
        Application.Quit();
    }


    //--------------------------



    public void ReadChildrenOfParentData()
    {
        Debug.Log("1");
        
       
        foreach (Transform child in childrenList.transform)
        {
            Destroy(child.gameObject);
        }
      
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
                                if (userData.Key != "firstName" && userData.Key != "lastName")
                                    continue;
                                if (userData.Key == "firstName")
                                    firstName = userData.Value.ToString();
                                if (userData.Key == "lastName")
                                    lastName = userData.Value.ToString();

                            }


                            GameObject button = Instantiate(childButtonPrefab);
                            button.transform.SetParent(childrenList.transform);
                            button.GetComponent<RectTransform>().localScale = Vector3.one;
                            

                            button.GetComponent<Button>().onClick.AddListener(() => OnClick(child.Key, firstName, lastName));
                                                                                                        
                            button.transform.GetChild(0).GetComponent<TMP_Text>().text = firstName + " " + lastName;
                            
                            Debug.Log(firstName);
                            Debug.Log(lastName);
                            



                        }
                    });
                }



            }
        });
    }
    void OnClick(string childKey, string firstName, string lastName)
    {
        currentObservedChild = childKey;
        OpenProgressionOfChildWithNewNames(firstName, lastName);
        AddOnclicksOfLevelButtons();
        ReadProgressionData(childKey);
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
    public void OnClickLevelButton(int subjectNumber, int levelNumber)
    {
        SceneManager.LoadScene("Subject" + subjectNumber + "Level" + levelNumber);
    }

    
    public void AddOnclicksOfLevelButtons()
    {
        Debug.Log("onclick1");
        //Subject 1
        Subject1_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 1));
        Subject1_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 2));
        Subject1_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 3));
        Subject1_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 4));
        Subject1_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 5));
        Subject1_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(1, 6));
        //Subject 2
        Subject2_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 1));
        Subject2_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 2));
        Subject2_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 3));
        Subject2_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 4));
        Subject2_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 5));
        Subject2_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(2, 6));
        //Subject 3
        Subject3_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 1));
        Subject3_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 2));
        Subject3_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 3));
        Subject3_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 4));
        Subject3_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 5));
        Subject3_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(3, 6));
        //Subject 4
        Subject4_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 1));
        Subject4_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 2));
        Subject4_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 3));
        Subject4_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 4));
        Subject4_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 5));
        Subject4_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(4, 6));
        //Subject 5
        Subject5_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 1));
        Subject5_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 2));
        Subject5_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 3));
        Subject5_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 4));
        Subject5_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 5));
        Subject5_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(5, 6));
        //Subject 6
        Subject6_Level1.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 1));
        Subject6_Level2.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 2));
        Subject6_Level3.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 3));
        Subject6_Level4.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 4));
        Subject6_Level5.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 5));
        Subject6_Level6.GetComponentInChildren<Button>().onClick.AddListener(() => OnClickLevelButton(6, 6));
        Debug.Log("onclick2");
    }


    public void ProgressionOfSubject1(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_1").HasChild("Level_1"))
            Subject1_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_2"))
            Subject1_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_3"))
            Subject1_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_4"))
            Subject1_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_5"))
            Subject1_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_1").HasChild("Level_6"))
            Subject1_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject2(DataSnapshot snapshot)
    {

        if (snapshot.Child("Subject_2").HasChild("Level_1"))
            Subject2_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_2"))
            Subject2_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_3"))
            Subject2_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_4"))
            Subject2_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_5"))
            Subject2_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_2").HasChild("Level_6"))
            Subject2_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject3(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_3").HasChild("Level_1"))
            Subject3_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_2"))
            Subject3_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_3"))
            Subject3_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_4"))
            Subject3_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_5"))
            Subject3_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_3").HasChild("Level_6"))
            Subject3_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject4(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_4").HasChild("Level_1"))
            Subject4_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_2"))
            Subject4_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_3"))
            Subject4_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_4"))
            Subject4_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_5"))
            Subject4_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_4").HasChild("Level_6"))
            Subject4_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject5(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_5").HasChild("Level_1"))
            Subject5_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_2"))
            Subject5_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_3"))
            Subject5_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_4"))
            Subject5_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_5"))
            Subject5_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_5").HasChild("Level_6"))
            Subject5_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }
    public void ProgressionOfSubject6(DataSnapshot snapshot)
    {
        if (snapshot.Child("Subject_6").HasChild("Level_1"))
            Subject6_Level1.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_2"))
            Subject6_Level2.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_3"))
            Subject6_Level3.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_4"))
            Subject6_Level4.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_5"))
            Subject6_Level5.GetComponentInChildren<Toggle>().isOn = true;
        if (snapshot.Child("Subject_6").HasChild("Level_6"))
            Subject6_Level6.GetComponentInChildren<Toggle>().isOn = true;
    }

}
