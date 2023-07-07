
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Instruction
{
    public int level;
    public abstract void Run();
}

public abstract class HolderInstruction : Instruction
{
    public List<Instruction> instructions;
    //public abstract void Add(Instruction instruction);
}

//sadece say�yla d�nen for loop. boolean olacaksa ayr� class olu�turulabilir.
public class For : HolderInstruction
{
    public CharacterMovementController characterMovement;
    private int iterationCount;
    //private List<Instruction> instructions;

    public For(CharacterMovementController characterMovement, int iterationCount, int level)
    {
        this.characterMovement = characterMovement;
        this.iterationCount = iterationCount;
        instructions = new List<Instruction>();
        this.level = level;
    }

    public override void Run()
    {
        //iterasyon say�s�n� ayarlamak laz�m.
        Debug.Log("iteration" + iterationCount);

        for (int i = 0; i < iterationCount; i++)
        {

            RunCodeButton.RunInstructions(instructions);

        }
    }


    public override string ToString()
    {
        return "For Class";
    }
}

public class While : HolderInstruction
{
    public CharacterMovementController characterMovement;
    public string firstMethod;
    public string secondMethod;
    public string secondMethodParameter;
    public bool isThereNotOperator;

    public While(CharacterMovementController characterMovement, string firstMethod, string secondMethod, string secondMethodParameter, bool isThereNotOperator, int level)
    {
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = secondMethod;
        this.secondMethodParameter = secondMethodParameter;
        instructions = new List<Instruction>();
        this.isThereNotOperator = isThereNotOperator;
        this.level = level;
        //type = 2;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }

    public override void Run()
    {
        RunCodeButton.RunInstructions(instructions);
    }
}

public class If : HolderInstruction
{

    public CharacterMovementController characterMovement;
    public string firstMethod;
    public string secondMethod;
    public string secondMethodParameter;
    public int type;
    public bool isThereNotOperator;


    public If(CharacterMovementController characterMovement, string firstMethod, bool isThereNotOperator, int level)
    {
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = null;
        this.secondMethodParameter = null;
        instructions = new List<Instruction>();
        this.isThereNotOperator = isThereNotOperator;
        this.level = level;
        type = 1;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }
    public If(CharacterMovementController characterMovement, string firstMethod, string secondMethod, string secondMethodParameter, bool isThereNotOperator, int level)
    {
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = secondMethod;
        this.secondMethodParameter = secondMethodParameter;
        instructions = new List<Instruction>();
        this.isThereNotOperator = isThereNotOperator;
        this.level = level;
        type = 2;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }


    //rightpart ve operator type olup olmamas�na gore iki sekilde calisacak
    public override void Run()
    {

        RunCodeButton.RunInstructions(instructions);
    }
}

public class Elif : HolderInstruction
{

    public CharacterMovementController characterMovement;

    public string firstMethod;
    public string secondMethod;
    public string secondMethodParameter;
    public bool isThereNotOperator;
    public int type;

    public Elif(CharacterMovementController characterMovement, string firstMethod, bool isThereNotOperator, int level)
    {

        //----
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = null;
        this.secondMethodParameter = null;

        instructions = new List<Instruction>();
        this.level = level;
        type = 1;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }
    public Elif(CharacterMovementController characterMovement, string firstMethod, string secondMethod, string secondMethodParameter, bool isThereNotOperator, int level)
    {
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = secondMethod;
        this.secondMethodParameter = secondMethodParameter;

        instructions = new List<Instruction>();
        this.level = level;
        type = 2;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }



    public override void Run()
    {

        RunCodeButton.RunInstructions(instructions);
    }
}

public class Else : HolderInstruction
{
    //gerekli degil galiba
    public CharacterMovementController characterMovement;
    public Else(CharacterMovementController characterMovement, int level)
    {

        this.characterMovement = characterMovement;
        this.level = level;
        instructions = new List<Instruction>();
    }



    public override void Run()
    {

        RunCodeButton.RunInstructions(instructions);
    }
}

public class ChangeColor : Instruction
{
    private CharacterColorAndShapeChanger characterColorAndShapeChanger;
    private string color;

    public ChangeColor(CharacterColorAndShapeChanger characterColorAndShapeChanger, string color)
    {
        this.characterColorAndShapeChanger = characterColorAndShapeChanger;
        this.color = color;
    }

    public override void Run()
    {
        if (color == "blue")
            characterColorAndShapeChanger.ChangeColorToBlue();
        else if (color == "red")
            characterColorAndShapeChanger.ChangeColorToRed();
        else
            Debug.Log("hata");
    }
}

public class Move : Instruction
{
    private CharacterMovementController characterMovement;
    private string direction;


    public Move(CharacterMovementController characterMovement, string direction, int level)
    {
        this.direction = direction;
        this.characterMovement = characterMovement;
        this.level = level;
    }

    public override void Run()
    {

        if (direction == "left")
            characterMovement.MoveLeft();
        else if (direction == "right")
            characterMovement.MoveRight();
        //characterMovement.Invoke("MoveRight", 1f);
        else if (direction == "up")
            characterMovement.MoveUp();
        else if (direction == "down")
            characterMovement.MoveDown();

    }

    public void RunMethod()
    {
        Debug.Log("000");
        if (direction == "left")
            characterMovement.MoveLeft();
        else if (direction == "right")
            characterMovement.MoveRight();
        else if (direction == "up")
            characterMovement.MoveUp();
        else if (direction == "down")
            characterMovement.MoveDown();
    }


    public override string ToString()
    {
        return "Move: " + direction + " Class";
    }

}

public class Swim : Instruction
{
    private CharacterMovementController characterMovement;
    private string direction;

    public Swim(CharacterMovementController characterMovement, string direction, int level)
    {
        this.direction = direction;
        this.characterMovement = characterMovement;
        this.level = level;
    }

    public override void Run()
    {
        //burada direk Move(Vector2) kullan�labilir. 
        if (direction == "left")
            characterMovement.SwimLeft();
        else if (direction == "right")
            characterMovement.SwimRight();
        else if (direction == "up")
            characterMovement.SwimUp();
        else if (direction == "down")
            characterMovement.SwimDown();

    }

    public override string ToString()
    {
        return "Move: " + direction + " Class";
    }

}

public class Def_SetShape : HolderInstruction
{
    public string shape;
    public CharacterColorAndShapeChanger characterColorAndShapeChanger;

    public Def_SetShape(CharacterColorAndShapeChanger characterColorAndShapeChanger, string shape, int level)
    {
        this.characterColorAndShapeChanger = characterColorAndShapeChanger;
        this.shape = shape;
        this.level = level;
    }

    public override void Run()
    {
        if (shape == "square")
        {
            characterColorAndShapeChanger.ChangeShapeToSquare();
        }
        else if (shape == "circle")
        {
            characterColorAndShapeChanger.ChangeShapeToCircle();
        }
    }

}


public class Def_SetColor : HolderInstruction
{
    public string color;
    public CharacterColorAndShapeChanger characterColorAndShapeChanger;

    public Def_SetColor(CharacterColorAndShapeChanger characterColorAndShapeChanger, string color, int level)
    {
        this.characterColorAndShapeChanger = characterColorAndShapeChanger;
        this.color = color;
        this.level = level;
    }

    public override void Run()
    {
        if (color == "red")
        {
            characterColorAndShapeChanger.ChangeColorToRed();
        }
        else if (color == "green")
        {
            characterColorAndShapeChanger.ChangeColorToGreen();
        }
        else if (color == "blue")
        {
            characterColorAndShapeChanger.ChangeColorToBlue();
        }
        else if (color == "yellow")
        {
            characterColorAndShapeChanger.ChangeColorToYellow();
        }
    }

}

public class RunCodeButton : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;
    DatabaseReference databaseReference;

    [Header("Kod Sayfa Sayısı")]
    public int codePageCount;


    public bool isWaitingInstruction;

    [Header("Subject and Level")]
    public int subjectNumber;
    public int levelNumber;
    //public bool checkIfClassFieldsPrivate;

    private List<Instruction> instructionList;
    [Space]
    [Header("GameObjects")]
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;


    public GameObject chest;
    public CharacterMovementController characterMovement;
    public CharacterColorAndShapeChanger characterColorAndShapeChanger;
    [Space]
    [Header("Character")]
    [SerializeField]
    public GameObject character;



    public Animator chestAnimator;

    private string[] initParameters = null;
    //[HideInInspector]
    public string[] pythonReservedWords;

    private string[] fruits;

    public List<bool> conditionRunList;
    public Vector3 characterStartingPosition;


    public float timer = 0;
    private string[] colorsOfCharacter;
    private string[] shapesOfCharacter;

    [Space]
    [Header("Game Over Panel")]

    public GameObject runButton;
    public GameObject gameOverPanel;
    public TMP_Text errorMessageText;

    [Space]
    [Header("Options Menu/Panel")]
    public GameObject optionsButton;
    public GameObject optionsPanel;
    [Space]
    [Header("Back Button for Parent")]
    public GameObject backButtonForParent;

    public void GameOver(string errorMessage)
    {
        gameOverPanel.SetActive(true);
        inputField1.interactable = false;
        if (codePageCount == 2)
            inputField2.interactable = false;
        //inputField2.interactable = false;
        optionsButton.GetComponent<Button>().interactable = false;
        runButton.GetComponent<Button>().interactable = false;
        Time.timeScale = 0;
        //catNumber = CatNumberInput;
        //levelNumber = levelNumberInput;
        errorMessageText.text = errorMessage;
        ////codeInputField_1Text.text = codeInputField_1;
        //PlayerPrefs.SetString("codeInput1", codeInputField_1);
        //PlayerPrefs.SetString("codeInput2", codeInputField_2);
        ////codeInputField_2Text.text = codeInputField_2;

    }
    public void ResetLevel()
    {
        //meyve veya farkli seyler eklenirse her sey basta oldugu yere konmali
        //LoadScene ile scene'i yeniden yuklersem cocuklarin yazdiklari kod kaybolur. 
        gameOverPanel.SetActive(false);
        instructionList.Clear();

        characterMovement.transform.position = characterStartingPosition;
        inputField1.interactable = true;
        if (codePageCount == 2)
            inputField2.interactable = true;
        optionsButton.GetComponent<Button>().interactable = true;
        runButton.GetComponent<Button>().interactable = true;

        //gerek var mi?
        Time.timeScale = 1;
    }
    public void LoadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void OpenAndCloseOptionsMenu()
    {
        if (!optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(true);
            Time.timeScale = 0;
            inputField1.interactable = false;
            if (codePageCount == 2)
                inputField2.interactable = false;
            runButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            optionsPanel.SetActive(false);
            Time.timeScale = 1;
            inputField1.interactable = true;
            if (codePageCount == 2)
                inputField2.interactable = true;
            runButton.GetComponent<Button>().interactable = true;
        }


    }

    public void BackToCategoryMenu()
    {
        SceneManager.LoadScene("Subject" + subjectNumber);
    }

    void Start()
    {
        colorsOfCharacter = new string[] { "red", "blue", "green", "yellow" };
        shapesOfCharacter = new string[] { "circle", "square" };
        isWaitingInstruction = false;
        instructionList = new List<Instruction>();
        characterMovement = character.GetComponent<CharacterMovementController>();
        characterStartingPosition = characterMovement.transform.position;
        characterColorAndShapeChanger = character.GetComponent<CharacterColorAndShapeChanger>();
        chestAnimator = chest.GetComponent<Animator>();
        pythonReservedWords = new string[] { "def", "if", "else", "elif", "for", "while", "False", "True", "and", "as", "assert", "break", "class", "continue",
                                            "del",   "except", "finally",  "form", "global", "import", "in", "is", "lambda",
                                            "nonlocal", "not", "or", "pass", "raise", "return", "try",  "with", "yeld"};
        fruits = new string[] { "apple", "banana", "kiwi" };

        conditionRunList = new List<bool>();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        if (subjectNumber == 6)
        {
            if (levelNumber == 2 || levelNumber == 3)
            {
                inputField2.text = "class SimpleCharacter(Character) : \r\n  def __init__ (self, shape, color) :\r\n    self.shape = shape\r\n    self.color = color";
                
            }
        }


        InitializeFirebase();


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
                    Debug.Log("child");
                    backButtonForParent.SetActive(false);
                    optionsButton.SetActive(true);
                    runButton.GetComponent<Button>().interactable = true;
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
                    Debug.Log("parent");
                    optionsButton.SetActive(false);
                    runButton.GetComponent<Button>().interactable = false;
                    backButtonForParent.SetActive(true);

                }
            }
        });
    }

    public void BackButtonForParent()
    {
        //login'e atıyor. düzeltilecek.
        SceneManager.LoadScene("Real Main Menu");
    }

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


    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


    public void ReadInputPage1(TMP_InputField inputField1)
    {
        this.inputField1 = inputField1;

    }

    public void ReadInputPage2(TMP_InputField inputField2)
    {
        this.inputField2 = inputField2;
    }

    public void RunCode()
    {
        //GameOver("Örnek kod hatası");
        //aslinda gerek olmayabilir
        instructionList.Clear();


        string inputText1 = inputField1.text;
        string inputText2 = null;
        if (codePageCount == 2)
            inputText2 = inputField2.text;

        string[] rows1 = inputText1.Split("\n");
        string[] rows2 = null;
        if (codePageCount == 2)
            rows2 = inputText2.Split("\n");

        //indentation buyuklugu
        int n = 2;


        //string className = null;
        string className = "SimpleCharacter";
        string parentClassName = "Character";
        string secondClassFileName = "SimpleCharacter";
        string classInstanceName = null;

        //List<string> classInitParameters = new List<string>();

        if (codePageCount == 1)
            classInstanceName = "character";

        if (codePageCount == 2 && !String.IsNullOrEmpty(inputText2))
        {
            List<string> initAssignments = new List<string>();
            int indentation = 0;
            

            bool isFirstRow = true;
            bool isInitRow = false;
            bool isInsideInit = false;
            bool isInsideSetColor = false;
            bool isInsideSetShape = false;
            string selfKeyword = null;
            int rowIndentation = 0;
            for (int i = 0; i < rows2.Length; i++)
            {


                if (String.IsNullOrEmpty(rows2[i]))
                    continue;

                if (isFirstRow)
                {

                    int c = 0;
                    while (rows2[i][c] == ' ')
                    {
                        c++;
                    }
                    indentation = c;

                    string row = rows2[i].Trim();

                    try
                    {
                        if (row.Substring(0, 5) != "class")
                        {
                            Debug.Log("hata");
                            GameOver("class nesnesi oluşturmanız gerekiyor. class kelimesini kullanmanız gerekiyor.");
                        }
                        else
                        {
                            string classPart = row.Substring(5).Trim();

                            if (classPart.Substring(0, className.Length) != className)
                            {
                                Debug.Log("hata");
                                GameOver("class adını " + className + " yapmanız gerekiyor.");
                            }
                            else
                            {
                                string parentClassPart = classPart.Substring(className.Length).Trim();
                                if (parentClassPart[0] != '(')
                                {
                                    Debug.Log("hata");
                                    GameOver("Parantez hatası: Bir class'ı olu�tururken parantezlerini doğru şekilde kullanmanız gerekiyor. Örnekler: SimpleCharacter(Character) veya Character()");
                                }
                                else
                                {
                                    parentClassPart = parentClassPart.Substring(1).Trim();
                                    if (parentClassPart.Substring(0, parentClassName.Length) != parentClassName)
                                    {
                                        Debug.Log("hata");
                                        GameOver("Oluşturacağınız class'ın parent class'ını parantez içine yazmanız gerekiyor. Örnek: SimpleCharacter(Character)");
                                    }
                                    else
                                    {
                                        parentClassPart = parentClassPart.Substring(parentClassName.Length).Replace(" ", "");
                                        if (parentClassPart.Length > 2)
                                        {
                                            Debug.Log("hata");
                                            GameOver("class oluştururken parantezi doğru yerde kapattığınızdan ve sonrasında sadece : koyduğunuzdan emin olun.");
                                        }
                                        else if (parentClassPart[0] != ')')
                                        {
                                            Debug.Log("hata");
                                            GameOver("Parantez hatası: Bir class'ı oluştururken parantezlerini doğru şekilde kullanmanız gerekiyor. Örnekler: SimpleCharacter(Character) veya Character()");
                                        }
                                        else if (parentClassPart[1] != ':')
                                        {
                                            Debug.Log("hata");
                                            GameOver("class oluştururken komutun sonuna : eklemeniz gerekiyor.");
                                        }
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        GameOver("Kodunuzda eksik veya hatalı nokta/lar var.");
                    }


                    isFirstRow = false;
                    isInitRow = true;
                }
                else if (isInitRow)
                {

                    int c = 0;
                    while (rows2[i][c] == ' ')
                    {
                        c++;
                    }
                    rowIndentation = c;


                    //ilk satirin class oldugu varsayiliyor.
                    if (rowIndentation != indentation + n)
                    {
                        Debug.Log("Indentation hatasi");
                        GameOver("Indentation hatası: Komutun önüne class constructor komutunun önüne koyduğunuzdan " + n + " fazla boşluk koyarak yazmanız gerekiyor.");
                    }



                    //string leftTrimmedRow = rows2[i].Substring(rowIndentation);
                    string trimmedRow = rows2[i].Trim();
                    Debug.Log(rows2[i]);
                    string[] rowWords;
                    //string[] rowWords = leftTrimmedRow.Split(" ");


                    try
                    {
                        if (trimmedRow.Substring(0, 3) != "def")
                        {
                            Debug.Log("Hata");
                            GameOver("init komutunu yazmak için önce def kelimesini yazmanız gerekiyor.");
                        }
                        else
                        {
                            rowWords = trimmedRow.Split(" ", 2);

                            string initWord = rowWords[1];
                            Debug.Log(initWord);

                            if (initWord.Substring(0, 8) != "__init__")
                            {
                                Debug.Log("Hata");
                                GameOver("init komutu için def yazdıktan sonra boşluk bırakıp __init__ yazarak devam etmeniz gerekiyor.");
                            }
                            else
                            {
                                string initWordParametersPart = initWord.Substring(8).Trim();
                                Debug.Log(initWordParametersPart);
                                if (initWordParametersPart[0] != '(')
                                {
                                    Debug.Log("hata");
                                    GameOver("Parantez hatası: __init__ yazdıktan sonra parantez içinde gerekli parametreleri girmeniz gerekiyor.");
                                }
                                else if (initWordParametersPart[initWordParametersPart.Length - 1] != ':')
                                {
                                    Debug.Log("hata");
                                    GameOver("init komutunun sonuna : eklemeniz gerekiyor.");
                                }
                                else
                                {
                                    initWordParametersPart = initWordParametersPart.Substring(1, initWordParametersPart.Length - 2).Trim();
                                    Debug.Log(initWordParametersPart);
                                    if (initWordParametersPart[initWordParametersPart.Length - 1] != ')')
                                    {
                                        Debug.Log("hata");
                                        GameOver("Parantez hatası: __init__ yazd�ktan sonra parantez içinde gerekli parametreleri girmeniz gerekiyor.");
                                    }
                                    else
                                    {
                                        initWordParametersPart = initWordParametersPart.Substring(0, initWordParametersPart.Length - 1).Trim();
                                        Debug.Log(initWordParametersPart);
                                        initParameters = initWordParametersPart.Split(",");

                                        for (int j = 0; j < initParameters.Length; j++)
                                        {
                                            Debug.Log(initParameters.Length);
                                            initParameters[j] = initParameters[j].Trim();
                                            Debug.Log(initParameters[j]);
                                            Debug.Log(pythonReservedWords.Length);

                                            Debug.Log("111");
                                        }
                                    }
                                }

                            }

                        }

                    }
                    catch (Exception e)
                    {
                        Debug.Log("hata");
                        GameOver("Kodunuzda eksik veya hatalı nokta/lar var.");
                    }


                    //Burada parametrelere ulastigim icin burada kontrol ediyorum. Yukarida da edebilirim.
                    if (!initParameters.Contains("shape"))
                    {
                        GameOver("init parametrelerinden biri shape olmalıdır.");
                    }
                    else if (!initParameters.Contains("color"))
                    {
                        GameOver("init parametrelerinden biri color olmalıdır.");
                    }
                    Debug.Log("222");
                    //self yerine farkli kelimeler kullanilabilir.
                    selfKeyword = initParameters[0];

                    Debug.Log("buras�");
                    foreach (string parameter in initParameters)
                        Debug.Log(parameter + " " + parameter.Length);


                    isInitRow = false;
                    isInsideInit = true;


                }

                else if (isInsideSetColor || isInsideSetShape)
                {
                    Debug.Log(isInsideSetColor + " " + rows2[i] + " " + isInsideSetShape);
                    //burada kod şöyle olacak
                    //self.color = color
                    int c = 0;
                    while (rows2[i][c] == ' ')
                    {
                        c++;
                    }
                    rowIndentation = c;

                    if (rowIndentation != indentation + 2 * n)
                    {
                        Debug.Log("hATA");
                        GameOver("");
                    }
                    else
                    {
                        string trimmedRow = rows2[i].Trim();
                        Debug.Log(trimmedRow);
                        try
                        {
                            if (!trimmedRow.Contains('='))
                            {
                                Debug.Log("hata");
                                GameOver("");
                            }
                            else
                            {
                                string[] assignmentParts = trimmedRow.Split('=');
                                string leftPart = assignmentParts[0].Trim();
                                string rightPart = assignmentParts[1].Trim();

                                if (leftPart.Substring(0, selfKeyword.Length) != selfKeyword)
                                {
                                    Debug.Log("hata");
                                    GameOver("Atama satırında belirlediğiniz self keyword'ünü kullanmanız gerekiyor. Belirlediğiniz self keyword: " + selfKeyword);
                                }
                                else
                                {
                                    string leftPartAfterSelfWord = leftPart.Substring(selfKeyword.Length).Trim();
                                    if (leftPartAfterSelfWord[0] != '.')
                                    {
                                        Debug.Log("hata");
                                        GameOver("Atama satırında belirlediğiniz self keyword'den sonra . kullanmanız gerekiyor. Belirlediğiniz self keyword" + selfKeyword);
                                    }
                                    else
                                    {
                                        leftPartAfterSelfWord = leftPartAfterSelfWord.Substring(1).Trim();
                                        Debug.Log(leftPartAfterSelfWord);

                                        Debug.Log(rightPart);

                                        if (leftPartAfterSelfWord != rightPart)
                                        {
                                            Debug.Log("hata");
                                        }
                                        else
                                        {
                                            if (isInsideSetColor)
                                            {
                                                if (rightPart != "color")
                                                {
                                                    Debug.Log("hata");
                                                }
                                                else
                                                {
                                                    isInsideSetColor = false;
                                                }
                                            }
                                            else if (isInsideSetShape)
                                            {
                                                if (rightPart != "shape")
                                                {
                                                    Debug.Log("hata");
                                                }
                                                else
                                                {
                                                    isInsideSetShape = false;
                                                }
                                            }

                                        }

                                    }
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                            GameOver("Kodunuzda eksik veya hatalı nokta/lar var.");
                        }
                    }


                }

                else
                {
                    //burada def set_color ve def set_shape okunacak.
                    int c = 0;
                    while (rows2[i][c] == ' ')
                    {
                        c++;
                    }
                    rowIndentation = c;
                    //------------------------------------
                    if (rowIndentation == indentation + 2 * n && isInsideInit)
                    {
                        //Debug.Log("hATA");
                        //GameOver("Indentation hatas�: Komutun �n�ne init komutunun �n�ne koydu�unuzdan " + n + " fazla bo�luk koyarak yazman�z gerekiyor.");

                        string trimmedRow = rows2[i].Trim();
                        Debug.Log(trimmedRow);
                        try
                        {
                            if (!trimmedRow.Contains('='))
                            {
                                Debug.Log("hata");
                                GameOver("Parametreleri atadığınız satırda atama için = kullanmanız gerekiyor.");
                            }
                            else
                            {
                                string[] assignmentParts = trimmedRow.Split('=');
                                string leftPart = assignmentParts[0].Trim();
                                string rightPart = assignmentParts[1].Trim();

                                if (leftPart.Substring(0, selfKeyword.Length) != selfKeyword)
                                {
                                    Debug.Log("hata");
                                    GameOver("Atama satırında belirlediğiniz self keyword'ünü kullanmanız gerekiyor. Belirlediğiniz self keyword: " + selfKeyword);
                                }
                                else
                                {
                                    string leftPartAfterSelfWord = leftPart.Substring(selfKeyword.Length).Trim();
                                    if (leftPartAfterSelfWord[0] != '.')
                                    {
                                        Debug.Log("hata");
                                        GameOver("Atama satırında belirlediğiniz self keyword'den sonra . kullanmanız gerekiyor. Belirlediğiniz self keyword" + selfKeyword);
                                    }
                                    else
                                    {
                                        leftPartAfterSelfWord = leftPartAfterSelfWord.Substring(1).Trim();
                                        Debug.Log(leftPartAfterSelfWord);

                                        Debug.Log(rightPart);

                                        //aslında sol taraf _name veya __name gibi olabiliyor. bakılacak.
                                        if (leftPartAfterSelfWord != rightPart)
                                        {
                                            Debug.Log("hata");
                                        }
                                        else
                                        {
                                            if (!initParameters.Contains(rightPart))
                                            {
                                                Debug.Log("+" + rightPart + "+");
                                                foreach (string str in initParameters)
                                                {
                                                    Debug.Log("+" + str + "+");
                                                }
                                                Debug.Log("hata");
                                                GameOver("Atama satırında sağ tarafa yazdığınız kelime init parametreleri arasında bulunmalıdır.");
                                            }
                                            else
                                            {
                                                initAssignments.Add(rightPart);

                                            }
                                        }



                                    }
                                }
                            }


                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                            GameOver("Kodunuzda eksik veya hatalı nokta/lar var.");
                        }
                    }

                    else if (rowIndentation == indentation + n)
                    {
                        isInsideInit = false;


                        Debug.Log("*" + rows2[i] + "*");
                        string trimmedRow = rows2[i].Trim();
                        Debug.Log(trimmedRow);
                        try
                        {
                            if (trimmedRow.Substring(0, 3) != "def")
                            {
                                Debug.Log("hata");
                            }
                            else
                            {
                                string methodPart = trimmedRow.Substring(3).Trim();
                                //bu kısım metodlaştırılıp soruya göre çağrılabilir.
                                if (methodPart.Substring(0, 9) != "set_color" && methodPart.Substring(0, 9) != "set_shape")
                                {
                                    Debug.Log("hata");
                                }
                                else
                                {
                                    string methodParameterPart = methodPart.Substring(9).Trim();
                                    if (methodParameterPart[0] != '(')
                                    {
                                        Debug.Log("hata");

                                    }
                                    else if (methodParameterPart[methodParameterPart.Length - 1] != ':')
                                    {
                                        Debug.Log("hata");
                                    }
                                    else
                                    {
                                        Debug.Log("methodParameterPart1:" + methodParameterPart + "/");
                                        methodParameterPart = methodParameterPart.Substring(1, methodParameterPart.Length - 2).Trim();
                                        Debug.Log("methodParameterPart2:" + methodParameterPart + "/");
                                        if (methodParameterPart[methodParameterPart.Length - 1] != ')')
                                        {
                                            Debug.Log("hata");
                                        }
                                        else
                                        {
                                            methodParameterPart = methodParameterPart.Substring(0, methodParameterPart.Length - 1);
                                            Debug.Log("methodParameterPart3:" + methodParameterPart + "/");
                                            string[] methodParameters = methodParameterPart.Split(',');
                                            for (int j = 0; j < methodParameters.Length; j++)
                                            {
                                                methodParameters[j] = methodParameters[j].Trim();
                                            }


                                            if (methodParameters[0] != selfKeyword)
                                            {
                                                Debug.Log("hata");

                                            }
                                            else
                                            {
                                                if (methodPart.Substring(0, 9) == "set_color")
                                                {
                                                    if (methodParameters[1] != "color")
                                                    {
                                                        Debug.Log("pp" + methodParameters[1] + "pp");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        Debug.Log("inside color");
                                                        isInsideSetColor = true;
                                                    }
                                                }
                                                else if (methodPart.Substring(0, 9) == "set_shape")
                                                {
                                                    if (methodParameters[1] != "shape")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        Debug.Log("inside shape");
                                                        isInsideSetShape = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            Debug.Log("hata");
                            GameOver("Kodunuzda eksik veya hatalı nokta/lar var.");
                        }
                    }

                    else
                    {
                        isInsideInit = false;
                    }

                }

            }



            if (initAssignments.Count != initParameters.Length - 1)
            {
                Debug.Log("Atama sayıları yetersiz");
                GameOver("Atama sayıları yetersiz. init parametrelerinin self keyword hariç tamamını atamanız gerekiyor.");
            }
            else
            {
                foreach (string parameter in initParameters)
                {
                    if (parameter != selfKeyword)
                    {
                        if (!initAssignments.Contains(parameter))
                        {
                            Debug.Log(parameter);
                            Debug.Log("hata");
                            GameOver("Atamalarınızda hata/lar var.");
                        }
                    }

                }
            }

        }

        if (!String.IsNullOrEmpty(inputText1))
        {
            //List<Instruction> instructions = new List<Instruction>();
            int instructionLevel;

            string lastInstructionType = null;

            //bool isImportRow = true;
            bool isFirstRow = true;
            bool isClassInstanceRow = false;
            int indentation = 0;


            int conditionId = -1;
            int lastIndentation = 0;
            //Ba�ka class varsa buras� kullan�lacak. Yoksa baz� k�s�mlar� kullan�lmayacak.
            for (int i = 0; i < rows1.Length; i++)
            {
                //ayr�ca null kontrol� de gerekebilir.
                if (String.IsNullOrEmpty(rows1[i]))
                    continue;

                int rowIndentation;

                if (!String.IsNullOrEmpty(rows1[i]))
                {
                    
                

                    //birden fazla import sat�r� istenmedi�i i�in bir tane olacak �ekilde ayarland�. 
                    if (codePageCount == 2 && isFirstRow)
                    {
                        int c = 0;
                        while (rows1[i][c] == ' ')
                        {
                            c++;
                        }
                        indentation = c;
                        lastIndentation = c;
                        if (codePageCount == 2)
                        {
                            string row = rows1[i].Trim();
                            try
                            {
                                if (row.Substring(0, 4) != "from")
                                {
                                    Debug.Log("hata");
                                    GameOver("Import komutunun ilk kelimesi from olmalıdır.");
                                }
                                else
                                {
                                    row = row.Substring(4).Trim();
                                    if (row.Substring(0, secondClassFileName.Length) != secondClassFileName)
                                    {
                                        Debug.Log("hata");
                                        GameOver("Import komutunda class'ın olduğu dosyanın adını yazmanız gerekiyor. " + className + " class'ı için dosya adı: " + secondClassFileName);
                                    }
                                    else
                                    {
                                        row = row.Substring(secondClassFileName.Length).Trim();
                                        if (row.Substring(0, 6) != "import")
                                        {
                                            Debug.Log("hata");
                                            GameOver("Import komutununda class'ın bulunduğu dosyadan sonra import yazmanız gerekiyor.");
                                        }
                                        else
                                        {
                                            row = row.Substring(6).Trim();
                                            if (row != className)
                                            {
                                                Debug.Log("hata");
                                                GameOver("Import komutunda class'ın adını yazmanız gerekiyor. class adı: " + className);
                                            }

                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Debug.Log("hata");
                                GameOver("Kodunuzda eksik veya hatalı nokta/lar var.");

                            }
                        }






                        isFirstRow = false;
                        isClassInstanceRow = true;
                    }
                    //buraya eklenebilir.
                    else if (codePageCount == 2 && isClassInstanceRow)
                    {

                        int c = 0;
                        while (rows1[i][c] == ' ')
                        {
                            c++;
                        }
                        rowIndentation = c;


                        //ilk satirin class oldugu varsayiliyor.
                        if (rowIndentation != indentation)
                        {
                            GameOver("Indentation hatası: Komutun önüne import komutunun önüne koyduğunuz kadar boşluk koyarak yazmanız gerekiyor.");
                            Debug.Log("Indentation hatasi");
                        }


                        string trimmedRow = rows1[i].Trim();

                        string[] classInstanceParts;
                        try
                        {
                            if (!trimmedRow.Contains("="))
                            {
                                GameOver("Bir " + className + " nesnesi oluşturmanız gerekiyor.");
                                Debug.Log("Bir Character nesnesi oluşturmanız gerekiyor.");
                            }
                            else
                            {

                                classInstanceParts = trimmedRow.Split('=');

                                classInstanceName = classInstanceParts[0].Trim();
                                string classConstructor = classInstanceParts[1].Trim();


                                if (!VariableCheck(classInstanceName))
                                {
                                    GameOver("Oluşturmak istediğiniz nesnenin adı değişken ismi kurallarına uygun olmalıdır.");
                                    Debug.Log("Class instance name:" + classInstanceName + "+");
                                    Debug.Log("hata");
                                }

                                else if (!classConstructor.Contains("(") || !classConstructor.Contains(")"))
                                {
                                    GameOver(className + " nesnesi oluştururken parantezleri doğru kullanmanız gerekiyor.\nÖrnek: " + className + "(\"blue\")");
                                    Debug.Log("hata");
                                }
                                else
                                {
                                    string[] classConstructorParts = classConstructor.Split("(");
                                    string classNamePart = classConstructorParts[0].Trim();
                                    string classParametersPart = classConstructorParts[1].Trim();
                                    if (!VariableCheck(classNamePart))
                                    {
                                        //GameOver("");
                                        Debug.Log("HATA");
                                    }
                                    else
                                    {
                                        if (classParametersPart[classParametersPart.Length - 1] != ')')
                                        {
                                            Debug.Log("hata");
                                            GameOver("Parantez hatası: class nesnesi oluştururken parantezi kapatmanız gerekiyor.");
                                        }
                                        else
                                        {
                                            Debug.Log("hey");
                                            classParametersPart = classParametersPart.Substring(0, classParametersPart.Length - 1);
                                            Debug.Log(classParametersPart);
                                            string[] classParameters = classParametersPart.Split(',');

                                            if (classParameters.Length != initParameters.Length - 1)
                                            {
                                                Debug.Log("hata");
                                                GameOver("self keyword olmadan init komutuna yazdığınız parametrelere sırasıyla uygun değerler yazmanız gerekiyor.");
                                            }
                                            else
                                            {
                                                Debug.Log("hey");
                                                for (int j = 1; j < initParameters.Length; j++)
                                                {
                                                    //classParameters[j].Trim();
                                                    string parameter = classParameters[j - 1].Trim();
                                                    if (initParameters[j] == "shape")
                                                    {
                                                        if (parameter.Trim() == "circle")
                                                        {
                                                            characterColorAndShapeChanger.ChangeShapeToCircle();
                                                            Debug.Log("circleeee");

                                                        }
                                                        else if (parameter == "square")
                                                        {
                                                            characterColorAndShapeChanger.ChangeShapeToSquare();
                                                        }
                                                        else
                                                        {
                                                            Debug.Log("hata");
                                                            GameOver("shape parametresine uygun bir şekil yazmanız gerekiyor. Uygun şekiller: circle, square");
                                                        }
                                                    }
                                                    else if (initParameters[j] == "color")
                                                    {
                                                        Debug.Log("+" + parameter + "+");
                                                        if (parameter == "red")
                                                        {
                                                            characterColorAndShapeChanger.ChangeColorToRed();
                                                        }
                                                        else if (parameter == "green")
                                                        {
                                                            Debug.Log("gg");
                                                            characterColorAndShapeChanger.ChangeColorToGreen();
                                                            Debug.Log("greeeeen");
                                                        }
                                                        else if (parameter == "blue")
                                                        {
                                                            characterColorAndShapeChanger.ChangeColorToBlue();
                                                        }
                                                        else if (parameter == "yellow")
                                                        {
                                                            characterColorAndShapeChanger.ChangeColorToYellow();
                                                        }
                                                        else
                                                        {
                                                            Debug.Log("hata");
                                                            GameOver("color parametresine uygun bir renk yazman�z gerekiyor. Uygun renkler: red, green, blue, yellow");
                                                        }

                                                    }

                                                }
                                                Debug.Log("burada");
                                            }

                                        }

                                    }
                                }
                            }

                            character.SetActive(true);
                        }
                        catch (Exception e)
                        {
                            Debug.Log("hata");
                            GameOver("Kodunuzda eksik veya hatalı nokta/lar var.");

                        }

                        Debug.Log("activve");
                        isClassInstanceRow = false;
                    }
                    else
                    {
                        int c = 0;
                        while (rows1[i][c] == ' ')
                        {
                            c++;
                        }
                        rowIndentation = c;

                        if ((rowIndentation - indentation) % n != 0)
                        {
                            GameOver("Indentation hatası: Komutun başında bıraktığınız boşluk miktarını kontrol etmeniz gerekiyor.");
                            Debug.Log("indentation hatası");
                        }


                        Debug.Log("rowindentaiton " + rowIndentation);
                        Debug.Log("indentaion " + indentation);


                        instructionLevel = ((rowIndentation - indentation) / n) + 1;

                        

                        if (lastInstructionType == "holder")
                        {
                            Debug.Log("holder");
                            Debug.Log("instructionlevel: "+instructionLevel);
                            Debug.Log("rowindentation:"+rowIndentation);
                            Debug.Log("lastindentation"+lastIndentation);
                            if (rowIndentation != lastIndentation + n)
                            {
                                Debug.Log(lastIndentation);
                                Debug.Log("indentation hatası2");
                                Debug.Log(lastIndentation);
                                GameOver("Indentation hatası: Komutun başında bıraktığınız boşluk miktarını kontrol etmeniz gerekiyor.");
                                
                            }
                        }

                        //lastIndentation = rowIndentation;
                        string trimmedRow = rows1[i].Trim();
                        Debug.Log(trimmedRow + " " + trimmedRow.Length);
                        Debug.Log(classInstanceName);

                        try
                        {
                            if (trimmedRow.Substring(0, 2) == "if")
                            {
                                if (trimmedRow[trimmedRow.Length - 1] != ':')
                                {
                                    GameOver("if komutunun sonuna : eklemeniz gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else
                                {
                                    if (trimmedRow[trimmedRow.IndexOf("if") + 2] != ' ')
                                    {
                                        GameOver("if komutu yazarken if yazdıktan sonra boşluk bırakmanız gerekiyor.");
                                        Debug.Log("boşluk olmalı hata");
                                    }
                                    else
                                    {
                                        string booleanPart = trimmedRow.Substring(2).Trim();
                                        string operatorType = null;

                                        if (booleanPart.Contains("=="))
                                            operatorType = "==";
                                        else if (booleanPart.Contains("!="))
                                            operatorType = "!=";
                                        else if (booleanPart.Contains("<"))
                                            operatorType = "<";
                                        else if (booleanPart.Contains(">"))
                                            operatorType = ">";

                                        bool isThereNotOperator = false;

                                        if (booleanPart.Substring(0, 3) == "not")
                                        {
                                            isThereNotOperator = true;
                                            booleanPart = booleanPart.Substring(3).Trim();

                                        }



                                        if (operatorType == null)
                                        {
                                            string[] ifParts = booleanPart.Split('.');
                                            string parameterPart = null;

                                            //if (ifParts.Length == 2 || ifParts.Length == 3)
                                            if (ifParts.Length == 3)
                                            {

                                                if (ifParts[0].Trim() != classInstanceName)
                                                {
                                                    Debug.Log(ifParts[0]);
                                                    Debug.Log("classname2=" + classInstanceName);
                                                    GameOver("Bu problemde if komutunda " + classInstanceName + "'in metodlarını kullanarak kontrol yapmanız gerekiyor.\nÖrnek: if " + classInstanceName + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (ifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        Debug.Log("hata");
                                                        GameOver("Parantez hatası: up_tile metoduna ulaşmak için up_tile() yazmanız gerekiyor.");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "up_tile";

                                                    }

                                                }
                                                else if (ifParts[1].Substring(0, 9) == "down_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: down_tile metoduna ulaşmak için down_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "down_tile";

                                                    }
                                                }
                                                else if (ifParts[1].Substring(0, 10) == "right_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(10).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: right_tile metoduna ulaşmak için right_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "right_tile";

                                                    }
                                                }
                                                else if (ifParts[1].Substring(0, 9) == "left_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: left_tile metoduna ulaşmak için left_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";

                                                    }
                                                }


                                                string secondMethod = null;


                                                if (ifParts[2].Substring(0, 9) == "is_ground")
                                                {
                                                    parameterPart = ifParts[2].Substring(9).Replace(" ", "");
                                                    if (parameterPart[parameterPart.Length - 1] != ':')
                                                    {
                                                        GameOver("if komutununun sonuna : eklemeniz gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else if (parameterPart != "():")
                                                    {
                                                        GameOver("Parantez hatası: is_ground metoduna ulaşmak için is_ground() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        secondMethod = "is_ground";
                                                        If ifInstruction = new If(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                        AddInstruction(ifInstruction);
                                                        lastInstructionType = "holder";
                                                        lastIndentation = rowIndentation;
                                                    }

                                                }
                                                else if (ifParts[2].Substring(0, 8) == "is_water")
                                                {
                                                    parameterPart = ifParts[2].Substring(8).Replace(" ", "");
                                                    if (parameterPart[parameterPart.Length - 1] != ':')
                                                    {
                                                        GameOver("if komutununun sonuna : eklemeniz gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else if (parameterPart != "():")
                                                    {
                                                        GameOver("Parantez hatası: is_water metoduna ulaşmak için is_water() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        secondMethod = "is_water";
                                                        If ifInstruction = new If(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                        AddInstruction(ifInstruction);
                                                        lastInstructionType = "holder";
                                                        lastIndentation = rowIndentation;
                                                    }

                                                }
                                                else if (ifParts[2].Substring(0, 11) == "is_obstacle")
                                                {
                                                    parameterPart = ifParts[2].Substring(11).Replace(" ", "");
                                                    if (parameterPart[parameterPart.Length - 1] != ':')
                                                    {
                                                        GameOver("if komutununun sonuna : eklemeniz gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else if (parameterPart != "():")
                                                    {
                                                        GameOver("Parantez hatası: is_water metoduna ulaşmak için is_water() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        secondMethod = "is_obstacle";
                                                        If ifInstruction = new If(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                        AddInstruction(ifInstruction);
                                                        lastInstructionType = "holder";
                                                        lastIndentation = rowIndentation;
                                                    }

                                                }
                                                else if (ifParts[2].Substring(0, 8) == "contains")
                                                {
                                                    parameterPart = ifParts[2].Substring(8).Trim();
                                                    if (parameterPart[parameterPart.Length - 1] != ':')
                                                    {
                                                        GameOver("if komutununun sonuna : eklemeniz gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);
                                                    //hata cikarsa zaten program duracak
                                                    if (parameterPart[0] != '(' || parameterPart[parameterPart.Length - 2] != ')')
                                                    {
                                                        GameOver("Parantez hatası: contains metoduna ulaşmak için örnek olarak contains(apple) yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {

                                                        if (!fruits.Contains(parameterPart))
                                                        {
                                                            GameOver("contains metoduna parametre olarak sadece apple, banana veya kiwi yazabilirsiniz.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            secondMethod = "contains";
                                                            If ifInstruction = new If(characterMovement, firstMethod, secondMethod, parameterPart, isThereNotOperator, instructionLevel);
                                                            AddInstruction(ifInstruction);
                                                            lastInstructionType = "holder";
                                                            lastIndentation = rowIndentation;
                                                        }
                                                    }

                                                }


                                            }


                                        }
                                    }

                                }
                            }
                            else if (trimmedRow.Substring(0, 3) == "for")
                            {

                                if (trimmedRow[3] != ' ')
                                {
                                    GameOver("for komutunda for'dan sonra boşluk bırakmanız gerekiyor.");
                                    Debug.Log("hata");

                                }
                                else if (!trimmedRow.Contains("in"))
                                {
                                    GameOver("Bu problemde for komutunda in kelimesi kullanmanız gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else if (trimmedRow[trimmedRow.IndexOf("in") - 1] != ' ')
                                {
                                    GameOver("for komutunda in'den önce boşluk bırakmanız gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else if (trimmedRow[trimmedRow.IndexOf("in") + 2] != ' ')
                                {
                                    GameOver("for komutunda in'den sonra boşluk bırakmanız gerekiyor.");
                                    Debug.Log("hata");
                                }

                                else
                                {
                                    //burada san�r�m trim kullanmak gerekiyor ve sonras�nda kelimenin i�inde bo�luk var m� diye bakmak gerekiyor
                                    string var = trimmedRow.Substring(3, trimmedRow.IndexOf("in") - 3).Trim();
                                    Debug.Log("var = " + var);
                                    //
                                    if (!VariableCheck(var))
                                    {
                                        GameOver("Değişken hatalı:" + var);
                                        Debug.Log("hata");
                                    }
                                    else if (!trimmedRow.Contains("range"))
                                    {
                                        GameOver("Bu problemde for komutunda range kullanmanız/yazmanız gerekiyor.");
                                        Debug.Log("hata");
                                    }
                                    else
                                    {
                                        string rangeParameter = trimmedRow.Substring(trimmedRow.IndexOf("range") + 5).Replace(" ", "");
                                        Debug.Log("rangeParameter1 = " + rangeParameter);

                                        if (rangeParameter[0] != '(')
                                        {
                                            GameOver("Parantez hatası: for komutunda range kelimesinden sonra parantez içinde bir değer girmeniz gerekiyor.");
                                            Debug.Log("hata");
                                        }

                                        else if (rangeParameter[rangeParameter.Length - 2] != ')')
                                        {
                                            GameOver("Parantez hatası: for komutunda range kelimesinden sonra parantez içinde bir değer girmeniz gerekiyor.");
                                            Debug.Log("hata");
                                        }
                                        else if (rangeParameter[rangeParameter.Length - 1] != ':')
                                        {
                                            GameOver("for komutunun sonuna : eklemeniz gerekiyor.");
                                            Debug.Log("hata");
                                        }
                                        else
                                        {
                                            rangeParameter = rangeParameter.Substring(1, rangeParameter.Length - 3);
                                            Debug.Log("rangeParameter2 = " + rangeParameter);
                                            int parameter;
                                            if (int.TryParse(rangeParameter, out parameter))
                                            {
                                                Debug.Log(parameter);

                                                For forLoop = new For(characterMovement, parameter, instructionLevel);
                                                AddInstruction(forLoop);

                                                lastInstructionType = "holder";
                                                lastIndentation = rowIndentation;

                                            }
                                        }
                                    }

                                }

                            }
                            else if (trimmedRow.Substring(0, 4) == "elif")
                            {
                                if (trimmedRow[trimmedRow.Length - 1] != ':')
                                {
                                    GameOver("elif komutunun sonuna : eklemeniz gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else
                                {
                                    if (trimmedRow[trimmedRow.IndexOf("elif") + 4] != ' ')
                                    {
                                        GameOver("elif komutu yazarken elif yazdıktan sonra boşluk bırakmanız gerekiyor.");
                                        Debug.Log("boşluk olmalı hata");
                                    }
                                    else
                                    {
                                        string booleanPart = trimmedRow.Substring(4).Trim();
                                        string operatorType = null;

                                        if (booleanPart.Contains("=="))
                                            operatorType = "==";
                                        else if (booleanPart.Contains("!="))
                                            operatorType = "!=";
                                        else if (booleanPart.Contains("<"))
                                            operatorType = "<";
                                        else if (booleanPart.Contains(">"))
                                            operatorType = ">";


                                        bool isThereNotOperator = false;

                                        if (booleanPart.Substring(0, 3) == "not")
                                        {
                                            isThereNotOperator = true;
                                            booleanPart = booleanPart.Substring(3).Trim();

                                        }


                                        if (operatorType == null)
                                        {
                                            string[] elifParts = booleanPart.Split('.');
                                            string parameterPart = null;

                                            if (elifParts.Length == 2 || elifParts.Length == 3)
                                            {

                                                if (elifParts[0].Trim() != classInstanceName)
                                                {
                                                    GameOver("Bu problemde elif komutunda " + classInstanceName + "'in metodlarını kullanarak kontrol yapmanız gerekiyor.\nÖrnek: elif " + classInstanceName + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (elifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: up_tile metoduna ulaşmak için up_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "up_tile";

                                                    }

                                                }
                                                else if (elifParts[1].Substring(0, 9) == "down_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: down_tile metoduna ulaşmak için down_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "down_tile";

                                                    }
                                                }
                                                else if (elifParts[1].Substring(0, 10) == "right_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(10).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: right_tile metoduna ulaşmak için right_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "right_tile";

                                                    }
                                                }
                                                else if (elifParts[1].Substring(0, 9) == "left_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: left_tile metoduna ulaşmak için left_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";

                                                    }
                                                }


                                                if (elifParts.Length == 2)
                                                {
                                                    //burada 2 parcali if listeye eklenecek.
                                                }
                                                else if (elifParts.Length == 3)
                                                {

                                                    string secondMethod = null;


                                                    if (elifParts[2].Substring(0, 9) == "is_ground")
                                                    {
                                                        parameterPart = elifParts[2].Substring(9).Replace(" ", "");
                                                        if (parameterPart != "():")
                                                        {
                                                            GameOver("elif komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            secondMethod = "is_ground";
                                                            Elif elifInstruction = new Elif(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                            AddInstruction(elifInstruction);
                                                            lastInstructionType = "holder";
                                                            lastIndentation = rowIndentation;
                                                        }

                                                    }
                                                    else if (elifParts[2].Substring(0, 8) == "is_water")
                                                    {
                                                        parameterPart = elifParts[2].Substring(8).Replace(" ", "");
                                                        if (parameterPart != "():")
                                                        {
                                                            GameOver("elif komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            secondMethod = "is_water";
                                                            Elif elifInstruction = new Elif(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                            AddInstruction(elifInstruction);
                                                            lastInstructionType = "holder";
                                                            lastIndentation = rowIndentation;
                                                        }

                                                    }
                                                    else if (elifParts[2].Substring(0, 11) == "is_obstacle")
                                                    {
                                                        parameterPart = elifParts[2].Substring(11).Replace(" ", "");
                                                        if (parameterPart != "():")
                                                        {
                                                            GameOver("elif komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            secondMethod = "is_obstacle";
                                                            Elif elifInstruction = new Elif(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                            AddInstruction(elifInstruction);
                                                            lastInstructionType = "holder";
                                                            lastIndentation = rowIndentation;
                                                        }

                                                    }
                                                    else if (elifParts[2].Substring(0, 8) == "contains")
                                                    {

                                                        parameterPart = elifParts[2].Substring(8).Trim();
                                                        if (parameterPart[parameterPart.Length - 1] != ':')
                                                        {
                                                            GameOver("elif komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);
                                                        //hata cikarsa zaten program duracak
                                                        if (parameterPart[0] != '(' || parameterPart[parameterPart.Length - 2] != ')')
                                                        {
                                                            GameOver("Parantez hatası: contains metoduna ulaşmak için örnek olarak contains(apple) yazmanız gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            if (!fruits.Contains(parameterPart))
                                                            {
                                                                GameOver("contains metoduna parametre olarak sadece apple, banana veya kiwi yazabilirsiniz.");
                                                                Debug.Log("hata");
                                                            }
                                                            else
                                                            {
                                                                secondMethod = "contains";
                                                                Elif elifInstruction = new Elif(characterMovement, firstMethod, secondMethod, parameterPart, isThereNotOperator, instructionLevel);
                                                                AddInstruction(elifInstruction);
                                                            }

                                                        }

                                                    }


                                                }


                                            }


                                        }
                                    }

                                }
                            }
                            else if (trimmedRow.Substring(0, 4) == "else")
                            {
                                trimmedRow = trimmedRow.Replace(" ", "");
                                if (trimmedRow.Length != 5)
                                {
                                    GameOver("else komutu else kelimesi ve : haricinde sadece boşluklar içerebilir.");
                                    Debug.Log("hata");
                                }

                                else if (trimmedRow[4] != ':')
                                {
                                    GameOver("else komutunun sonuna : eklemeniz gerekiyor.");
                                    Debug.Log("hata");
                                }

                                else
                                {

                                    Else elseCondition = new Else(characterMovement, instructionLevel);
                                    AddInstruction(elseCondition);
                                    lastInstructionType = "holder";
                                    lastIndentation = rowIndentation;
                                }


                            }

                            else if (trimmedRow.Substring(0, 5) == "while")
                            {
                                if (trimmedRow[trimmedRow.Length - 1] != ':')
                                {
                                    GameOver("while komutunun sonuna : eklemeniz gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else
                                {
                                    if (trimmedRow[trimmedRow.IndexOf("while") + 5] != ' ')
                                    {
                                        GameOver("while komutu yazarken while yazdıktan sonra boşluk bırakmanız gerekiyor.");
                                        Debug.Log("boşluk olmalı hata");
                                    }
                                    else
                                    {
                                        string booleanPart = trimmedRow.Substring(5).Trim();
                                        string operatorType = null;

                                        if (booleanPart.Contains("=="))
                                            operatorType = "==";
                                        else if (booleanPart.Contains("!="))
                                            operatorType = "!=";
                                        else if (booleanPart.Contains("<"))
                                            operatorType = "<";
                                        else if (booleanPart.Contains(">"))
                                            operatorType = ">";


                                        bool isThereNotOperator = false;
                                        Debug.Log(booleanPart);
                                        if (booleanPart.Substring(0, 3) == "not")
                                        {
                                            isThereNotOperator = true;
                                            booleanPart = booleanPart.Substring(3).Trim();

                                        }
                                        

                                        if (operatorType == null)
                                        {
                                            string[] whileParts = booleanPart.Split('.');
                                            string parameterPart = null;

                                            if (whileParts.Length == 2 || whileParts.Length == 3)
                                            {

                                                if (whileParts[0].Trim() != classInstanceName)
                                                {
                                                    GameOver("Bu problemde while komutunda " + classInstanceName + "'in metodlarını kullanarak kontrol yapmanız gerekiyor.\nÖrnek: while " + classInstanceName + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (whileParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: up_tile metoduna ulaşmak için up_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "up_tile";

                                                    }

                                                }
                                                else if (whileParts[1].Substring(0, 9) == "down_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: down_tile metoduna ulaşmak için down_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "down_tile";

                                                    }
                                                }
                                                else if (whileParts[1].Substring(0, 10) == "right_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(10).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: right_tile metoduna ulaşmak için right_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "right_tile";

                                                    }
                                                }
                                                else if (whileParts[1].Substring(0, 9) == "left_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatası: left_tile metoduna ulaşmak için left_tile() yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";

                                                    }
                                                }


                                                if (whileParts.Length == 2)
                                                {
                                                    //burada 2 parcali if listeye eklenecek.
                                                }
                                                else if (whileParts.Length == 3)
                                                {

                                                    string secondMethod = null;

                                                    if (whileParts[2].Substring(0, 9) == "is_ground")
                                                    {
                                                        parameterPart = whileParts[2].Substring(9).Replace(" ", "");
                                                        if (parameterPart != "():")
                                                        {
                                                            GameOver("while komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            secondMethod = "is_ground";
                                                            While whileInstruction = new While(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                            AddInstruction(whileInstruction);
                                                            lastInstructionType = "holder";
                                                            lastIndentation = rowIndentation;
                                                        }

                                                    }
                                                    else if (whileParts[2].Substring(0, 8) == "is_water")
                                                    {
                                                        parameterPart = whileParts[2].Substring(8).Replace(" ", "");
                                                        if (parameterPart != "():")
                                                        {
                                                            GameOver("while komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            secondMethod = "is_water";
                                                            While whileInstruction = new While(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                            AddInstruction(whileInstruction);
                                                            lastInstructionType = "holder";
                                                            lastIndentation = rowIndentation;
                                                        }

                                                    }
                                                    else if (whileParts[2].Substring(0, 11) == "is_obstacle")
                                                    {
                                                        parameterPart = whileParts[2].Substring(11).Replace(" ", "");
                                                        if (parameterPart != "():")
                                                        {
                                                            GameOver("while komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            secondMethod = "is_obstacle";
                                                            While whileInstruction = new While(characterMovement, firstMethod, secondMethod, null, isThereNotOperator, instructionLevel);
                                                            AddInstruction(whileInstruction);
                                                            lastInstructionType = "holder";
                                                            lastIndentation = rowIndentation;
                                                        }

                                                    }
                                                    else if (whileParts[2].Substring(0, 8) == "contains")
                                                    {

                                                        parameterPart = whileParts[2].Substring(8).Trim();
                                                        //burasi degisecek. ()'in ici dolu olacak.
                                                        if (parameterPart[parameterPart.Length - 1] != ':')
                                                        {
                                                            GameOver("while komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);
                                                        //hata cikarsa zaten program duracak
                                                        if (parameterPart[0] != '(' || parameterPart[parameterPart.Length - 2] != ')')
                                                        {
                                                            GameOver("Parantez hatası: contains metoduna ulaşmak için örnek olarak contains(apple) yazmanız gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            if (!fruits.Contains(parameterPart))
                                                            {
                                                                GameOver("contains metoduna parametre olarak sadece apple, banana veya kiwi yazabilirsiniz.");
                                                                Debug.Log("hata");
                                                            }
                                                            else
                                                            {
                                                                secondMethod = "contains";
                                                                While whileInstruction = new While(characterMovement, firstMethod, secondMethod, parameterPart, isThereNotOperator, instructionLevel);
                                                                AddInstruction(whileInstruction);
                                                                lastInstructionType = "holder";
                                                                lastIndentation = rowIndentation;
                                                            }

                                                        }

                                                    }


                                                }


                                            }


                                        }
                                    }

                                }
                            }
                            else if (trimmedRow.Substring(0, classInstanceName.Length) == classInstanceName)
                            {
                                if (!trimmedRow.Contains('.'))
                                {
                                    Debug.Log("hata");
                                    GameOver("Bir class nesnesinin metodunu kullanacağınız zaman class nesnesinin adını yazıp . koyduktan sonra metodu yazmanız gerekiyor. Örnek: character.move_up()");
                                }
                                else
                                {
                                    string[] moveParts = trimmedRow.Split('.');
                                    string classInstancePart = moveParts[0].Trim();
                                    string methodPart = moveParts[1].Trim();
                                    //Debug.Log("+" + classInstancePart + "+");
                                    //Debug.Log("+" + methodPart + "+");

                                    if (classInstancePart != classInstanceName)
                                    {
                                        Debug.Log("hata");
                                        GameOver("Bir class nesnesinin metodunu kullanacağınız zaman class nesnesinin adını yazıp . koyduktan sonra metodu yazmanız gerekiyor. Örnek: character.move_up()");
                                    }
                                    else
                                    {

                                        if (!methodPart.Contains('('))
                                        {
                                            GameOver("Parantez hatası: parametresiz methodlar sonlarına () eklenerek, parametreli methodlar eklenen parantezin içine parametre değerleri yazılarak çağrılır.");

                                            Debug.Log("hata");
                                        }
                                        else if (!methodPart.Contains(')'))
                                        {
                                            GameOver("Parantez hatası: parametresiz methodlar sonlarına () eklenerek, parametreli methodlar eklenen parantezin içine parametre değerleri yazılarak çağrılır.");
                                            Debug.Log("hata2");
                                        }

                                        string[] instructionParts = methodPart.Split('(', 2);

                                        string methodName = instructionParts[0].Trim();
                                        string methodParameterPart = instructionParts[1].Trim();
                                        //instructionParts[1] = instructionParts[1].Trim();
                                        int length = methodName.Length;

                                        //Debug.Log(methodName);
                                        try
                                        {
                                            if (methodName.Substring(0, 4) == "move")
                                            {
                                                methodParameterPart = methodParameterPart.Replace(" ", "");
                                                if (methodParameterPart != ")")
                                                {
                                                    Debug.Log("hata");
                                                    //Debug.Log(methodPart);
                                                    GameOver("move methodlarını parametre eklemeden yazmanız gerekiyor. Örnek: move_up()");
                                                }
                                                else
                                                {
                                                    if (methodName == "move_up")
                                                    {
                                                        Move move_up = new Move(characterMovement, "up", instructionLevel);
                                                        AddInstruction(move_up);
                                                        Debug.Log("MOVE UP : " + instructionLevel);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else if (methodName == "move_left")
                                                    {
                                                        Move move_left = new Move(characterMovement, "left", instructionLevel);
                                                        AddInstruction(move_left);
                                                        Debug.Log("MOVE LEFT : " + instructionLevel);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else if (methodName == "move_down")
                                                    {
                                                        Move move_down = new Move(characterMovement, "down", instructionLevel);
                                                        AddInstruction(move_down);
                                                        Debug.Log("MOVE DOWN : " + instructionLevel);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else if (methodName == "move_right")
                                                    {

                                                        Move move_right = new Move(characterMovement, "right", instructionLevel);
                                                        Debug.Log("mr level " + instructionLevel);
                                                        AddInstruction(move_right);
                                                        Debug.Log("MOVE RIGHT : " + instructionLevel);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else
                                                    {
                                                        GameOver("Method tanımını doğru yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                }

                                            }
                                            else if (methodName.Substring(0, 4) == "swim")
                                            {
                                                methodParameterPart = methodParameterPart.Replace(" ", "");
                                                if (methodParameterPart != ")")
                                                {
                                                    Debug.Log("hata");
                                                    GameOver("swim methodlarını parametre eklemeden yazmanız gerekiyor. Örnek: swim_up()");
                                                }
                                                else
                                                {
                                                    if (methodName == "swim_up")
                                                    {
                                                        Swim swim_up = new Swim(characterMovement, "up", instructionLevel);
                                                        AddInstruction(swim_up);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else if (methodName == "swim_left")
                                                    {
                                                        Swim swim_left = new Swim(characterMovement, "left", instructionLevel);
                                                        AddInstruction(swim_left);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else if (methodName == "swim_down")
                                                    {
                                                        Swim swim_down = new Swim(characterMovement, "down", instructionLevel);
                                                        AddInstruction(swim_down);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else if (methodName == "swim_right")
                                                    {

                                                        Swim swim_right = new Swim(characterMovement, "right", instructionLevel);
                                                        Debug.Log("mr level " + instructionLevel);
                                                        AddInstruction(swim_right);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else
                                                    {
                                                        GameOver("Method tanımını doğru yazmanız gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                }

                                            }
                                            else if (codePageCount == 2 && methodName.Substring(0, 9) == "set_color")
                                            {
                                                methodParameterPart = methodParameterPart.Trim();

                                                if (methodParameterPart[methodParameterPart.Length - 1] != ')')
                                                {
                                                    Debug.Log("hata");
                                                    GameOver("Parantez hatası: parametreli metod komutlarını yazarken parametreli parantez içine yazmanız gerekiyor. Örnek: set_color(blue)");
                                                }
                                                else
                                                {
                                                    string methodParameter = methodParameterPart.Substring(0, methodParameterPart.Length - 1).Trim();
                                                    if (colorsOfCharacter.Contains(methodParameter))
                                                    {
                                                        Def_SetColor setColor = new Def_SetColor(characterColorAndShapeChanger, methodParameter, instructionLevel);
                                                        AddInstruction(setColor);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else
                                                    {
                                                        Debug.Log("hata");
                                                        GameOver("set_color metoduna uygun bir renk yazmanız gerekiyor. Uygun şekiller: red, blue, green, yellow");
                                                    }

                                                }

                                            }
                                            else if (codePageCount == 2 && methodName.Substring(0, 9) == "set_shape")
                                            {
                                                methodParameterPart = methodParameterPart.Trim();
                                                if (methodParameterPart[methodParameterPart.Length - 1] != ')')
                                                {
                                                    Debug.Log("hata");
                                                    GameOver("Parantez hatası: parametreli metod komutlarını yazarken parametreli parantez içine yazmanız gerekiyor. Örnek: set_shape(circle)");
                                                }
                                                else
                                                {
                                                    string methodParameter = methodParameterPart.Substring(0, methodParameterPart.Length - 1).Trim();
                                                    if (shapesOfCharacter.Contains(methodParameter))
                                                    {
                                                        Def_SetShape setShape = new Def_SetShape(characterColorAndShapeChanger, methodParameter, instructionLevel);
                                                        AddInstruction(setShape);
                                                        lastInstructionType = "not_holder";
                                                        lastIndentation = rowIndentation;
                                                    }
                                                    else
                                                    {
                                                        Debug.Log("hata");
                                                        GameOver("set_shape metoduna uygun bir şekil yazmanız gerekiyor. Uygun şekiller: square, circle");
                                                    }
                                                }
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            //GameOver("Hata: " + ex.Message);
                                            GameOver("Kodunuzda eksik veya hatalı nokta/lar var.");
                                            Debug.Log(ex.Message);
                                        }
                                    }


                                }
                            }


                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                            GameOver("Kodunuzda eksik kısımlar var.");
                        }



                    }
                }


            }


        }




        Debug.Log(instructionList.Count);
        foreach (Instruction v in instructionList)
            Debug.Log(v.ToString());


        CheckLevelConditions(subjectNumber, levelNumber, instructionList);


        RunInstructions(instructionList);



        //BOLUM GECME KISMI
        Vector3Int characterFinalPosition = characterMovement.groundTilemap.WorldToCell(characterMovement.transform.position);
        if (characterMovement.chestPositionTilemap.HasTile(characterFinalPosition))
        {
            chestAnimator.SetBool("opening", true);
            //burada bolum gecilmis olacak.
            SubmitLevelAsPassed();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //eger string bos degilse

    }


    //----------------------------------------------------------------------------------------------------------------
    public static void RunInstructions(List<Instruction> instructionList)
    {
        //Thread.Sleep(1000);

        bool isThereIf = false;
        bool didPreviousConditionsRun = false;
        foreach (Instruction instruction in instructionList)
        {
            if (instruction.GetType() == typeof(If))
            {
                Debug.Log("Burasi");
                isThereIf = true;
                if (((If)instruction).type == 1)
                {
                    Debug.Log("?????????");
                    //if(((If)instruction).firstMethod) == "";
                }
                else if (((If)instruction).type == 2)
                {
                    Debug.Log("Burasi2");
                    Debug.Log("+" + ((If)instruction).firstMethod + "+");
                    Debug.Log("+" + ((If)instruction).secondMethod + "+");
                    if (((If)instruction).firstMethod == "up_tile")
                    {
                        Debug.Log("uP");
                        if (((If)instruction).secondMethod == "is_ground")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(0, 1);
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(upTilePosition) && !((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;

                                instruction.Run();


                            }
                        }
                        else if (((If)instruction).secondMethod == "is_water")
                        {
                            Debug.Log("wATER");
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(0, 1);
                            //groundTilemap vs waterTilemap ???
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(upTilePosition) && !((If)instruction).isThereNotOperator)
                            {
                                Debug.Log(":????????");
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                        else if (((If)instruction).secondMethod == "is_obstacle")
                        {
                            Debug.Log("wATER");
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(0, 1);
                            //groundTilemap vs waterTilemap ???
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (!((If)instruction).characterMovement.obstaclesTilemap.HasTile(upTilePosition) && ((If)instruction).isThereNotOperator)
                            {
                                Debug.Log(":????????");
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                      
                    }
                    //HEPSINE CONTAINS EKLENECEK.
                    else if (((If)instruction).firstMethod == "down_tile")
                    {
                        if (((If)instruction).secondMethod == "is_ground")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(0, -1);
                            Vector3Int downTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(downTilePosition) && !((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                        else if (((If)instruction).secondMethod == "is_water")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(0, -1);
                            //groundTilemap vs waterTilemap ???
                            Vector3Int downTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(downTilePosition) && !((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                        else if (((If)instruction).secondMethod == "is_obstacle")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(0, -1);
                            //groundTilemap vs waterTilemap ???
                            Vector3Int downTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (!((If)instruction).characterMovement.obstaclesTilemap.HasTile(downTilePosition) && ((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }

                    }
                    else if (((If)instruction).firstMethod == "right_tile")
                    {
                        Debug.Log("Burasi3");
                        if (((If)instruction).secondMethod == "is_ground")
                        {
                            Debug.Log("Burasi4");
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(1, 0);
                            Vector3Int rightTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition) && !((If)instruction).isThereNotOperator)
                            {
                                Debug.Log("Burasi5");
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                        else if (((If)instruction).secondMethod == "is_water")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(1, 0);
                            //groundTilemap vs waterTilemap ???
                            Vector3Int rightTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition) && !((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                        else if (((If)instruction).secondMethod == "is_obstacle")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(1, 0);
                            //groundTilemap vs waterTilemap ???
                            Vector3Int rightTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (!((If)instruction).characterMovement.obstaclesTilemap.HasTile(rightTilePosition) && ((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                    }
                    else if (((If)instruction).firstMethod == "left_tile")
                    {
                        if (((If)instruction).secondMethod == "is_ground")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(-1, 0);
                            Vector3Int leftTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition) && !((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                        else if (((If)instruction).secondMethod == "is_water")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(-1, 0);
                            //groundTilemap vs waterTilemap ???
                            Vector3Int leftTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition) && !((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                        else if (((If)instruction).secondMethod == "is_obstacle")
                        {
                            //burada ground mu diye kontrol etmek gerekiyor
                            Vector2 direction = new Vector2(-1, 0);
                            //groundTilemap vs waterTilemap ???
                            Vector3Int leftTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (!((If)instruction).characterMovement.obstaclesTilemap.HasTile(leftTilePosition) && ((If)instruction).isThereNotOperator)
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                    }
                }



            }

            else if (instruction.GetType() == typeof(Elif))
            {
                if (!isThereIf)
                {
                    Debug.Log("hata");

                }
                else if (!didPreviousConditionsRun)
                {
                    if (((Elif)instruction).type == 1)
                    {
                        //if(((If)instruction).firstMethod) == "";
                    }
                    else if (((Elif)instruction).type == 2)
                    {
                        if (((Elif)instruction).firstMethod == "up_tile")
                        {
                            if (((Elif)instruction).secondMethod == "is_ground")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(0, 1);
                                Vector3Int upTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(upTilePosition) && !((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "is_water")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(0, 1);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int upTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(upTilePosition) && !((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "is_obstacle")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(0, 1);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int upTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (!((Elif)instruction).characterMovement.obstaclesTilemap.HasTile(upTilePosition) && ((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "contains")
                            {
                                //burada direkt parameterPart diye yazilabilir mi??
                                if (((Elif)instruction).secondMethodParameter == "apple")
                                {

                                }
                                else if (((Elif)instruction).secondMethodParameter == "banana")
                                {

                                }
                                else if (((Elif)instruction).secondMethodParameter == "kiwi")
                                {

                                }
                            }
                        }
                        //HEPSINE CONTAINS EKLENECEK.
                        else if (((Elif)instruction).firstMethod == "down_tile")
                        {
                            if (((Elif)instruction).secondMethod == "is_ground")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(0, -1);
                                Vector3Int downTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((If)instruction).characterMovement.groundTilemap.HasTile(downTilePosition) && !((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "is_water")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(0, -1);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int downTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(downTilePosition) && !((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "is_obstacle")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(0, -1);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int downTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (!((Elif)instruction).characterMovement.obstaclesTilemap.HasTile(downTilePosition) && ((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }

                        }
                        else if (((Elif)instruction).firstMethod == "right_tile")
                        {
                            if (((Elif)instruction).secondMethod == "is_ground")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(1, 0);
                                Vector3Int rightTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition) && !((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "is_water")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(1, 0);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int rightTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition) && !((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "is_obstacle")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(1, 0);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int rightTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (!((Elif)instruction).characterMovement.obstaclesTilemap.HasTile(rightTilePosition) && ((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                        }
                        else if (((Elif)instruction).firstMethod == "left_tile")
                        {
                            if (((Elif)instruction).secondMethod == "is_ground")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(-1, 0);
                                Vector3Int leftTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition) && !((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "is_water")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(-1, 0);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int leftTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition) && !((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                            else if (((Elif)instruction).secondMethod == "is_obstacle")
                            {
                                //burada ground mu diye kontrol etmek gerekiyor
                                Vector2 direction = new Vector2(-1, 0);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int leftTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (!((Elif)instruction).characterMovement.obstaclesTilemap.HasTile(leftTilePosition) && ((Elif)instruction).isThereNotOperator)
                                {
                                    didPreviousConditionsRun = true;
                                    instruction.Run();
                                }
                            }
                        }
                    }


                }

            }
            else if (instruction.GetType() == typeof(Else))
            {
                if (!isThereIf)
                {
                    Debug.Log("hata");
                }
                else if (!didPreviousConditionsRun)
                {
                    instruction.Run();
                }
            }
            else if (instruction.GetType() == typeof(While))
            {
                isThereIf = false;
                if (((While)instruction).firstMethod == "up_tile")
                {
                    if (((While)instruction).secondMethod == "is_ground")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(0, 1);
                        Vector3Int upTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(upTilePosition) && !((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            upTilePosition = upTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                    else if (((While)instruction).secondMethod == "is_water")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(0, 1);
                        //groundTilemap vs waterTilemap ???
                        Vector3Int upTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(upTilePosition) && !((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            upTilePosition = upTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                    else if (((While)instruction).secondMethod == "is_obstacle")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(0, 1);
                        //groundTilemap vs waterTilemap ???
                        Vector3Int upTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (!((While)instruction).characterMovement.obstaclesTilemap.HasTile(upTilePosition) && ((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            upTilePosition = upTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                }
                else if (((While)instruction).firstMethod == "right_tile")
                {
                    if (((While)instruction).secondMethod == "is_ground")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(1, 0);
                        Vector3Int rightTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition) && !((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            rightTilePosition = rightTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                    else if (((While)instruction).secondMethod == "is_water")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(1, 0);
                        //groundTilemap vs waterTilemap ???
                        Vector3Int rightTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition) && !((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            rightTilePosition = rightTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                    else if (((While)instruction).secondMethod == "is_obstacle")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(1, 0);
                        //groundTilemap vs waterTilemap ???
                        Vector3Int rightTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (!((While)instruction).characterMovement.obstaclesTilemap.HasTile(rightTilePosition) && ((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            rightTilePosition = rightTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                }
                else if (((While)instruction).firstMethod == "down_tile")
                {
                    if (((While)instruction).secondMethod == "is_ground")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(0, -1);
                        Vector3Int downTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(downTilePosition) && !((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            downTilePosition = downTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                    else if (((While)instruction).secondMethod == "is_water")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(0, -1);
                        //groundTilemap vs waterTilemap ???
                        Vector3Int downTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(downTilePosition) && !((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            downTilePosition = downTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                    else if (((While)instruction).secondMethod == "is_obstacle")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(0, -1);
                        //groundTilemap vs waterTilemap ???
                        Vector3Int downTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (!((While)instruction).characterMovement.obstaclesTilemap.HasTile(downTilePosition) && ((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            downTilePosition = downTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                }
                else if (((While)instruction).firstMethod == "left_tile")
                {
                    if (((While)instruction).secondMethod == "is_ground")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(-1, 0);
                        Vector3Int leftTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition) && !((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            leftTilePosition = leftTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                    else if (((While)instruction).secondMethod == "is_water")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(-1, 0);
                        //groundTilemap vs waterTilemap ???
                        Vector3Int leftTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition) && !((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            leftTilePosition = leftTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                    else if (((While)instruction).secondMethod == "is_obstacle")
                    {
                        //burada ground mu diye kontrol etmek gerekiyor
                        Vector2 direction = new Vector2(-1, 0);
                        //groundTilemap vs waterTilemap ???
                        Vector3Int leftTilePosition = ((While)instruction).characterMovement.groundTilemap.WorldToCell(((While)instruction).characterMovement.transform.position + (Vector3)direction);
                        //Oldu mu???
                        while (!((While)instruction).characterMovement.obstaclesTilemap.HasTile(leftTilePosition) && ((While)instruction).isThereNotOperator)
                        {
                            instruction.Run();
                            leftTilePosition = leftTilePosition + Vector3Int.FloorToInt((Vector3)direction);
                        }
                    }
                }
            }
            else
            {
                isThereIf = false;

                instruction.Run();


            }

        }
    }

    //----------------------------------------------------------------------
    //Check Level Conditions
    public void CheckSubject1Level7Conditions(List<Instruction> instructionList)
    {
        if (instructionList[0].GetType() != typeof(For))
        {
            Debug.Log("�lk komutun bir for olmas� gerekiyor.");
        }
    }

    public void CheckLevelConditions(int subjectNumber, int levelNumber, List<Instruction> instructionList)
    {
        if (subjectNumber == 1)
        {

            for (int i = 0; i < instructionList.Count; i++)
            {
                if (instructionList[i].GetType() != typeof(Move))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda sadece move metodlarını kullanmanız gerekiyor.");
                }
            }

        }
        else if (subjectNumber == 2)
        {
            if (levelNumber == 1)
            {
                if (instructionList[0].GetType() != typeof(For))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda for döngüsü kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 2)
            {
                if (instructionList[0].GetType() != typeof(For))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda for döngüsü kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 3)
            {
                if (instructionList[0].GetType() != typeof(For))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda for döngüsü kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 4)
            {
                int forLoopCount = 0;
                for (int i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].GetType() == typeof(For))
                    {
                        forLoopCount++;
                    }
                }
                if (forLoopCount == 0)
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda en az bir tane for döngüsü kullanmanız gerekiyor.");
                }

            }
            else if (levelNumber == 5)
            {
                for (int i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].GetType() != typeof(For))
                    {
                        Debug.Log("hata");
                        GameOver("Bu soruda tüm komutlarınızı for döngüleri içine yazmanız gerekiyor.");
                    }
                }
            }
            else if (levelNumber == 6)
            {
                int forLoopCount = 0;
                for (int i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].GetType() == typeof(For))
                    {
                        forLoopCount++;
                    }
                }
                if (forLoopCount == 0)
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda en az bir tane for döngüsü kullanmanız gerekiyor.");
                }

            }

        }
        else if (subjectNumber == 3)
        {
            if (levelNumber == 1)
            {
                if (instructionList[0].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda if yapısını kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).firstMethod != "up_tile")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda karakterin üstündeki kontrol etmek için up_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).secondMethod != "is_water")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda bir bloğun su olup olmadığını kontrol etmek için is_water() metodunu kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 2)
            {
                if (instructionList[0].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda if else yapısını kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).firstMethod != "right_tile")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda karakterin sağındaki bloğu kontrol etmek için right_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda bir bloğun kara olup olmadığını kontrol etmek için is_water() metodunu kullanmanız gerekiyor.");
                }
                else if (instructionList[1].GetType() != typeof(Else))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda if else yapısını kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 3)
            {
                if (instructionList[0].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda if ve elif komutlarını  kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).firstMethod != "right_tile")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda karakterin sağındaki bloğu kontrol etmek için right_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda bir bloğun kara olup olmadığını kontrol etmek için is_water() metodunu kullanmanız gerekiyor.");
                }

                else if (instructionList[1].GetType() != typeof(Elif))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda if ve elif komutlarını kullanmanız gerekiyor.");
                }
                else if (((Elif)instructionList[1]).firstMethod != "right_tile")
                {
                    Debug.Log("hata");
                    GameOver("elif komutunda karakterin sağındaki bloğu kontrol etmek için right_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((Elif)instructionList[1]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("elif komutunda bir bloğun kara olup olmadığını kontrol etmek için is_water() metodunu kullanmanız gerekiyor.");
                }

            }
            else if (levelNumber == 4)
            {
                if (instructionList[0].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda if yapısını kullanmanız gerekiyor.");
                }
                else if (!((If)instructionList[0]).isThereNotOperator)
                {
                    Debug.Log("hata");
                    GameOver("Bir engel olmadığından emin olmak için not operatörünü kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).firstMethod != "down_tile")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda karakterin altındaki bloğu kontrol etmek down_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).secondMethod != "is_obstacle")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda bir blokta engel olup olmadığını kontrol etmek için is_obstacle() metodunu kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 5)
            {
                if (instructionList[0].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane if yapısı kullanmanız gerekiyor.");
                }
                else if (!((If)instructionList[0]).isThereNotOperator)
                {
                    Debug.Log("hata");
                    GameOver("Bir engel olmadığından emin olmak için not operatörünü kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).firstMethod != "left_tile")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda karakterin soldundaki bloğu kontrol etmek için left_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).secondMethod != "is_obstacle")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda bir blokta engel olup olmadığını kontrol etmek için is_obstacle() metodunu kullanmanız gerekiyor.");
                }


                else if (instructionList[1].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane if yapısı kullanmanız gerekiyor.");
                }

                else if (((If)instructionList[1]).firstMethod != "up_tile")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda karakterin üstündeki bloğu kontrol etmek için up_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[1]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda bir bloğun kara olup olmadığını kontrol etmek için is_ground() metodunu kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 6)
            {
                if (instructionList[0].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane if yapısı kullanmanız gerekiyor.");
                }
                else if (!((If)instructionList[0]).isThereNotOperator)
                {
                    Debug.Log("hata");
                    GameOver("Bir engel olmadığından emin olmak için not operatörünü kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).firstMethod != "left_tile")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda karakterin soldundaki bloğu kontrol etmek için left_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[0]).secondMethod != "is_obstacle")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda bir blokta engel olup olmadığını kontrol etmek için is_obstacle() metodunu kullanmanız gerekiyor.");
                }


                else if (instructionList[1].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane if yapısı kullanmanız gerekiyor.");
                }

                else if (((If)instructionList[1]).firstMethod != "up_tile")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda karakterin üstündeki bloğu kontrol etmek için up_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((If)instructionList[1]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("if komutunda bir bloğun kara olup olmadığını kontrol etmek için is_obstacle() metodunu kullanmanız gerekiyor.");
                }

                else if (instructionList[2].GetType() != typeof(Else))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane if yapısı kullanmanız gerekiyor.");
                }
            }
        }
        else if (subjectNumber == 4)
        {
            if (levelNumber == 1)
            {
                if (instructionList[0].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane while yapısı kullanmanız gerekiyor.");
                }

                else if (((While)instructionList[0]).firstMethod != "up_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin üstündeki bloğu kontrol etmek için up_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[0]).secondMethod != "is_water")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun su olup olmadığını kontrol etmek için is_obstacle() metodunu kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 2)
            {
                if (instructionList[0].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane while yapısı kullanmanız gerekiyor.");
                }

                else if (((While)instructionList[0]).firstMethod != "right_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin sağındaki bloğu kontrol etmek için right_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[0]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun kara olup olmadığını kontrol etmek için is_ground() metodunu kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 3)
            {
                if (instructionList[0].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane while yapısı kullanmanız gerekiyor.");
                }

                else if (((While)instructionList[0]).firstMethod != "right_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin sağındaki bloğu kontrol etmek için right_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[0]).secondMethod != "is_water")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun su olup olmadığını kontrol etmek için is_water() metodunu kullanmanız gerekiyor.");
                }

                else if (instructionList[1].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane while yapısı kullanmanız gerekiyor.");
                }

                else if (((While)instructionList[1]).firstMethod != "down_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin altındaki bloğu kontrol etmek için down_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[1]).secondMethod != "is_water")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun su olup olmadığını kontrol etmek için is_water() metodunu kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 4)
            {
                if (instructionList[0].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane while yapısı kullanmanız gerekiyor.");
                }
                else if (!((While)instructionList[0]).isThereNotOperator)
                {
                    Debug.Log("hata");
                    GameOver("Bir engel olmadığından emin olmak için not operatörünü kullanmanız gerekiyor.");
                }

                else if (((While)instructionList[0]).firstMethod != "down_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin altındaki bloğu kontrol etmek için down_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[0]).secondMethod != "is_obstacle")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir blokta engel olup olmadığını kontrol etmek için is_obstacle() metodunu kullanmanız gerekiyor.");
                }


                else if (instructionList[1].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane while yapısı kullanmanız gerekiyor.");
                }
                

                else if (((While)instructionList[1]).firstMethod != "right_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin sağındaki bloğu kontrol etmek için right_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[1]).secondMethod != "is_water")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun su olup olmadığını kontrol etmek için is_obstacle() metodunu kullanmanız gerekiyor.");
                }
            }
            else if (levelNumber == 5)
            {
                if (instructionList[0].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane while yapısı kullanmanız gerekiyor.");
                }


                else if (((While)instructionList[0]).firstMethod != "left_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin solundaki bloğu kontrol etmek için left_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[0]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun kara olup olmadığını kontrol etmek için is_ground() metodunu kullanmanız gerekiyor.");
                }

                else if (instructionList[0].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda iki tane while yapısı kullanmanız gerekiyor.");
                }


                else if (((While)instructionList[1]).firstMethod != "up_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin üstündeki bloğu kontrol etmek için up_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[1]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun kara olup olmadığını kontrol etmek için is_ground() metodunu kullanmanız gerekiyor.");
                }


            }
            else if (levelNumber == 6)
            {
                if (instructionList[0].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda birden fazla while yapısı kullanmanız gerekiyor.\"");
                }


                else if (((While)instructionList[0]).firstMethod != "right_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin solundaki bloğu kontrol etmek için right_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[0]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun kara olup olmadığını kontrol etmek için is_ground() metodunu kullanmanız gerekiyor.");
                }

                else if (instructionList[1].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda birden fazla while yapısı kullanmanız gerekiyor.");
                }

                else if (((While)instructionList[1]).firstMethod != "down_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin altındaki bloğu kontrol etmek için down_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[1]).secondMethod != "is_water")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun su olup olmadığını kontrol etmek için is_water() metodunu kullanmanız gerekiyor.");
                }


                if (instructionList[2].GetType() != typeof(While))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda birden fazla while yapısı kullanmanız gerekiyor.\"");
                }


                else if (((While)instructionList[2]).firstMethod != "right_tile")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda karakterin solundaki bloğu kontrol etmek için right_tile() metodunu kullanmanız gerekiyor.");
                }
                else if (((While)instructionList[2]).secondMethod != "is_ground")
                {
                    Debug.Log("hata");
                    GameOver("while komutunda bir bloğun kara olup olmadığını kontrol etmek için is_ground() metodunu kullanmanız gerekiyor.");
                }
            }
        }
        else if (subjectNumber == 5)
        {
            if (levelNumber == 1)
            {
                if (instructionList[0].GetType() != typeof(For))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda bir for döngüsünün içinde bir for döngüsü kullanmanız gerekiyor.");
                }
                else
                {
                    Instruction insideInstruction = ((For)instructionList[0]).instructions[0];
                    if (insideInstruction != null)
                    {
                        if (insideInstruction.GetType() != typeof(For))
                        {
                            Debug.Log("hata");
                            GameOver("Bu soruda bir for döngüsünün içinde bir if yapısı kullanmanız gerekiyor.");
                        }
                    }
                }
            }
            else if (levelNumber == 2)
            {
                if (instructionList[0].GetType() != typeof(For))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda bir for döngüsünün içinde bir for döngüsü kullanmanız gerekiyor.");
                }
                else
                {
                    Instruction insideInstruction = ((For)instructionList[0]).instructions[0];
                    if (insideInstruction != null)
                    {
                        if (insideInstruction.GetType() != typeof(For))
                        {
                            Debug.Log("hata");
                            GameOver("Bu soruda bir for döngüsünün içinde bir if yapısı kullanmanız gerekiyor.");
                        }
                    }
                }


            }
            else if (levelNumber == 3)
            {
                if (instructionList[0].GetType() != typeof(For))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda bir for döngüsünün içinde bir for döngüsü kullanmanız gerekiyor.");
                }
                else
                {
                    Instruction insideInstruction = ((For)instructionList[0]).instructions[0];
                    if (insideInstruction != null)
                    {
                        if (insideInstruction.GetType() != typeof(If))
                        {
                            Debug.Log("hata");
                            GameOver("Bu soruda bir for döngüsünün içinde bir if yapısı kullanmanız gerekiyor.");
                        }

                    }
                }

            }
            else if (levelNumber == 4)
            {
                if (instructionList[0].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda bir if yapısının içinde bir if yapısı kullanmanız gerekiyor.");
                }
                else
                {
                    int ifInstructionCount = 0;
                    for (int i = 0; i < ((If)instructionList[0]).instructions.Count; i++)
                    {
                        if (((If)instructionList[0]).instructions[i].GetType() == typeof(If))
                            ifInstructionCount++;
                    }
                    if (ifInstructionCount == 0)
                    {
                        Debug.Log("hata");
                        GameOver("Bu soruda bir if yapısının içinde bir if yapısı kullanmanız gerekiyor.");
                    }

                }

            }
            else if (levelNumber == 5)
            {
                if (instructionList[0].GetType() != typeof(If))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda bir if yapısının içinde bir if yapısı kullanmanız gerekiyor.");
                }
                else
                {
                    int ifInstructionCount = 0;
                    for (int i = 0; i < ((If)instructionList[0]).instructions.Count; i++)
                    {
                        if (((If)instructionList[0]).instructions[i].GetType() == typeof(If))
                            ifInstructionCount++;
                    }
                    if (ifInstructionCount == 0)
                    {
                        Debug.Log("hata");
                        GameOver("Bu soruda bir if yapısının içinde bir if yapısı kullanmanız gerekiyor.");
                    }

                }

            }
            else if (levelNumber == 6)
            {
                if (instructionList[0].GetType() != typeof(For))
                {
                    Debug.Log("hata");
                    GameOver("Bu soruda bir if yapısının içinde bir if yapısı kullanmanız gerekiyor.");
                }
                else
                {
                    List<Instruction> insideInstructions = ((For)instructionList[0]).instructions;
                    if (insideInstructions.Count != 2)
                    {
                        Debug.Log("hata");
                        GameOver("For döngüsünün içinde if ve elif komutlarını kullanmanız gerekiyor.");
                    }
                    else if (insideInstructions[0].GetType() != typeof(If))
                    {
                        Debug.Log("hata");
                        GameOver("For döngüsünün içinde if ve elif komutlarını kullanmanız gerekiyor.");
                    }
                    else if (insideInstructions[1].GetType() != typeof(Elif))
                    {
                        Debug.Log("hata");
                        GameOver("For döngüsünün içinde if ve elif komutlarını kullanmanız gerekiyor.");
                    }
                }

            }
        }
        else if (subjectNumber == 6)
        {
            //if (levelNumber == 1)
            //{

            //}
            if (levelNumber == 2)
            {
                if (instructionList[0].GetType() != typeof(Def_SetColor))
                {
                    Debug.Log("hata");
                    GameOver("set_color metodunu kullanmanız gerekiyor.");
                }
                else if (((Def_SetColor)instructionList[0]).color != "red")
                {
                    Debug.Log("hata");
                    GameOver("set_color metodu ile karakteri kırmızı yapmanız gerekiyor.");
                }
            }
            else if (levelNumber == 3)
            {
                if (instructionList[0].GetType() != typeof(Def_SetShape))
                {
                    Debug.Log("hata");
                    GameOver("set_color metodunu kullanmanız gerekiyor.");
                }
                else if (((Def_SetShape)instructionList[0]).shape != "circle")
                {
                    Debug.Log("hata");
                    GameOver("set_color metodu ile karakteri kırmızı yapmanız gerekiyor.");
                }
            }
            else if (levelNumber == 4)
            {
                int setColorMethodCount = 0;
                int setShapeMethodCount = 0;

                for (int i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].GetType() == typeof(Def_SetColor))
                    {
                        setColorMethodCount++;
                    }
                    else if (instructionList[i].GetType() == typeof(Def_SetShape))
                    {
                        setShapeMethodCount++;
                    }
                }

                if (setColorMethodCount > 0)
                {
                    Debug.Log("hata");
                    GameOver("set_color metodu kullanmamanız gerekiyor.");
                }
                else if (setShapeMethodCount > 0)
                {
                    Debug.Log("hata");
                    GameOver("set_shape metodu kullanmamanız gerekiyor.");
                }
                else
                {
                    if (!transform.Find("SquareBody").gameObject.activeSelf)
                    {
                        Debug.Log("hata");
                        GameOver("Karakteri kare (square) şeklinde oluşturmanız gerekiyor.");
                    }
                    else if (transform.Find("SquareBody").gameObject.GetComponent<Renderer>().material.color != UnityEngine.Color.yellow)
                    {
                        Debug.Log("hata");
                        GameOver("Karakteri sarı (yellow) renkte oluşturmanız gerekiyor.");
                    }

                }
            }

        }

    }

    //-----------------------------------------------------------------------
    public void SubmitLevelAsPassed()
    {
        Dictionary<string, object> Level_Passed = new Dictionary<string, object>();
        Level_Passed["Passed"] = true;

        databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + subjectNumber).Child("Level_" + levelNumber).UpdateChildrenAsync(Level_Passed);

    }

    public void ReadProgressionData(string subjectNumber)
    {

        databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + subjectNumber).Child("Level_" + levelNumber).GetValueAsync().ContinueWithOnMainThread(task =>
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

                Dictionary<string, object> Level_X = new Dictionary<string, object>();
                Level_X["passed"] = true;

                string userId = user.UserId;

                //Oldu mu???
                databaseReference.Child("Users").Child("Children").Child(userId).Child("Progression").UpdateChildrenAsync(Level_X);

            }
        });
    }


    //SORUN BURADA HERHALDE. Forun i�ine eklemiyor moveu.
    public void AddInstruction(Instruction instruction)
    {

        List<Instruction> instructionList = this.instructionList;
        Debug.Log(instruction.ToString());

        for (int i = 1; true; i++)
        {
            Debug.Log("level " + instruction.level);

            //if (instruction.GetType() == typeof(Condition))
            //{

            //}

            if (i == instruction.level)
            {
                instructionList.Add(instruction);

                return;
            }
            else
            {

                Debug.Log(instructionList.Count);
                Debug.Log(instructionList[instructionList.Count - 1].GetType().IsSubclassOf(typeof(HolderInstruction)));
                if (instructionList[instructionList.Count - 1].GetType().IsSubclassOf(typeof(HolderInstruction)))
                {
                    Debug.Log("holderxxxx");
                    //burada son listin sonundaki instruction�n listi al�n�yor
                    instructionList = ((HolderInstruction)instructionList[instructionList.Count - 1]).instructions;
                    Debug.Log(instructionList.Count);
                    //instruction = instructionList[instructionList.Count - 1];

                }
                //}
                else
                {
                    Debug.Log("hata???");
                    return;
                }
            }

        }

    }


    public bool VariableCheck(string var)
    {
        Debug.Log("noluyor");
        if (var == null)
        {
            Debug.Log("hata");
            return false;
        }
        else if (!(Char.IsLetter(var[0]) || var[0] == '_'))
        {
            Debug.Log("hata");
            return false;
        }
        else
        {
            Debug.Log("mm");

            Regex regex = new Regex(@"^[a-zA-Z0-9_]*$");
            Match match = regex.Match(var);
            Debug.Log("bul");
            Debug.Log(pythonReservedWords.Length);

            if (!match.Success)
            {
                Debug.Log("invalid variable hata");
                return false;
            }
            else if (pythonReservedWords.Contains(var))
            {
                Debug.Log("reserved word hata");
                return false;
            }
        }
        Debug.Log("yyy");
        return true;
    }

}

