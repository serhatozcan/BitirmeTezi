
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Instruction : MonoBehaviour
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
    RunCodeButton runCodeButton;
    public CharacterMovementController characterMovement;
    private int iterationCount;
    //private List<Instruction> instructions;

    public For(RunCodeButton runCodeButton, CharacterMovementController characterMovement, int iterationCount, int level)
    {
        this.runCodeButton = runCodeButton;
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
            //elif ve else'lerden once ilk komutun if olmasi gerek.
            //sirasiyla bakilip calisandan sonrakiler calismamali.
            //bool didItRun = false;
            bool isThereIf = false;
            bool didPreviousConditionsRun = false;

            RunCodeButton runCodeButton = new RunCodeButton();
            runCodeButton.RunInstructions(instructions);

            //StartCoroutine(runCodeButton.WaitFor1SecondCoroutine(runCodeButton, instructions));

            //StartCoroutine(runCodeButton.RunInstructions1Second(instructions));
            //StartCoroutine(runCodeButton.RunInstructionsWithDelay(instructions));

            //runCodeButton.RunInstructions(instructions);
            //runCodeButton.WaitFor1Second(runCodeButton, instructions);
        }
    }

    //public void Add(Instruction instruction)
    //{
    //    instructions.Add(instruction);
    //}



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

    public While(CharacterMovementController characterMovement, string firstMethod, string secondMethod, string secondMethodParameter, int level)
    {
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = secondMethod;
        this.secondMethodParameter = secondMethodParameter;
        instructions = new List<Instruction>();
        this.level = level;
        //type = 2;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }

    public override void Run()
    {

        // while()
        //RunCodeButton.RunInstructions(instructions);
        RunCodeButton runCodeButton = new RunCodeButton();
        runCodeButton.RunInstructions(instructions);
        //StartCoroutine(WaitFor1SecondCoroutine(runCodeButton, instructions));
    }
}

public class If : HolderInstruction
{
    //public List<string> leftPart;
    //public string operatorType;
    //public List<string> rightPart;

    public CharacterMovementController characterMovement;
    public string firstMethod;
    public string secondMethod;
    public string secondMethodParameter;
    public int type;


    public If(CharacterMovementController characterMovement, string firstMethod, int level)
    {
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = null;
        this.secondMethodParameter = null;
        instructions = new List<Instruction>();
        this.level = level;
        type = 1;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }
    public If(CharacterMovementController characterMovement, string firstMethod, string secondMethod, string secondMethodParameter, int level)
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


    //rightpart ve operator type olup olmamas�na gore iki sekilde calisacak
    public override void Run()
    {

        Debug.Log("azxczxcxcc");
        Debug.Log(instructions.Count);
        Debug.Log(instructions[0].ToString());
        bool isThereIf = false;
        bool didPreviousConditionsRun = false;
        //RunCodeButton.RunInstructions(instructions);
        RunCodeButton runCodeButton = new RunCodeButton();
        runCodeButton.RunInstructions(instructions);
    }
}

public class Elif : HolderInstruction
{
    //public RunCodeButton runCodeButton;
    public CharacterMovementController characterMovement;
    //public List<string> leftPart;
    //public string operatorType;
    //public List<string> rightPart;
    public string firstMethod;
    public string secondMethod;
    public string secondMethodParameter;

    public int type;

    public Elif(CharacterMovementController characterMovement, string firstMethod, int level)
    {

        //----
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = null;
        this.secondMethodParameter = null;
        //this.operatorType = operatorType;
        //this.rightPart = rightPart;
        //this.id = id;
        //this.conditionRunList = conditionRunList;
        instructions = new List<Instruction>();
        this.level = level;
        type = 1;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }
    public Elif(CharacterMovementController characterMovement, string firstMethod, string secondMethod, string secondMethodParameter, int level)
    {
        this.characterMovement = characterMovement;
        this.firstMethod = firstMethod;
        this.secondMethod = secondMethod;
        this.secondMethodParameter = secondMethodParameter;
        //this.operatorType = operatorType;
        //this.rightPart = rightPart;
        //this.id = id;
        //this.conditionRunList = conditionRunList;
        instructions = new List<Instruction>();
        this.level = level;
        type = 2;

        //boolean burada hesaplan�rsa karakterin o anki g�ncel durumu de�il. en ba�taki durumuna g�re hesap yap�l�r.
    }


    //rightpart ve operator type olup olmamas�na gore iki sekilde calisacak
    public override void Run()
    {
        //burada �nce di�erlerinde ba��ms�z olarak bool de�erine g�re �al��acak m� diye kontrol edece�iz.
        //sonra �al��acaksa conditionRunListte id'ye kar��l�k gelen indexi true yap�caz.
        //SIKINTI �U: true yapt�ktan sonra for loop i�erisinde ayn� if'e tekrar gelince ne olacak???

        //if(conditionRunList == null) 
        //{ 

        //}
        //if (operatorType != null)
        //{
        //    //buralarda calisip calismadigini dondurmek gerekebilir. ya da run metodunda sadece calistirma yapilacak. run metodu cagrilmadan once if'in calisip calismadigi disarida kontrol edilecek.
        //}

        bool isThereIf = false;
        bool didPreviousConditionsRun = false;
        //S�k�nt�l� bir durum gibi.
        //RunCodeButton runCodeButton = new RunCodeButton();
        //RunCodeButton.RunInstructions(instructions);
        RunCodeButton runCodeButton = new RunCodeButton();
        runCodeButton.RunInstructions(instructions);
    }
}

public class Else : HolderInstruction
{
    //gerekli degil galiba
    public CharacterMovementController characterMovement;
    public Else(CharacterMovementController characterMovement, int level)
    {
        //this.id = id;
        //this.conditionRunList = conditionRunList;
        this.characterMovement = characterMovement;
        this.level = level;
        instructions = new List<Instruction>();
    }



    public override void Run()
    {
        bool isThereIf = false;
        bool didPreviousConditionsRun = false;
        //RunCodeButton.RunInstructions(instructions);
        //RunCodeButton runCodeButton = new RunCodeButton();
        //runCodeButton.RunInstructions(instructions);
        RunCodeButton runCodeButton = new RunCodeButton();
        runCodeButton.RunInstructions(instructions);
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
        //burada direk Move(Vector2) kullan�labilir. 
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
    private string shape;
    public GameObject character;

    public Def_SetShape(GameObject character, string shape)
    {
        this.character = character;
        this.shape = shape;
    }

    public override void Run()
    {
        if (shape == "square")
        {
            character.transform.Find("CircleBody").gameObject.SetActive(false);
            character.transform.Find("SquareBody").gameObject.SetActive(true);
        }
        else if (shape == "circle")
        {
            character.transform.Find("SquareBody").gameObject.SetActive(false);
            character.transform.Find("CircleBody").gameObject.SetActive(true);
        }
    }

}


public class Def_SetColor : HolderInstruction
{
    private string color;
    public CharacterColorAndShapeChanger characterColorAndShapeChanger;

    public Def_SetColor(CharacterColorAndShapeChanger characterColorAndShapeChanger, string color)
    {
        this.characterColorAndShapeChanger = characterColorAndShapeChanger;
        this.color = color;
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

    public bool isWaitingInstruction;

    [Header("Category and Level")]
    public int catNumber;
    public int levelNumber;
    //public bool checkIfClassFieldsPrivate;

    private List<Instruction> instructionList;
    [Space]
    [Header("GameObjects")]
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    
   
    public GameObject chest;
    public  CharacterMovementController characterMovement;
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
    //public CharacterMovementController characterMovementController;

    public List<bool> conditionRunList;
    public Vector3 characterStartingPosition;


    // Start is called before the first frame update

    //[Space]
    //[Header("BackButton")]

    [Space]
    [Header("Game Over Page")]
    public GameObject backButton;
    public GameObject runButton;
    public GameObject gameOverPanel;
    public TMP_Text errorMessageText;
    //, codeInputField_1Text, codeInputField_2Text;
    //public string codeInputField_1Text, codeInputField_2Text;
    public void GameOver(string errorMessage)
    {
        gameOverPanel.SetActive(true);
        inputField1.interactable = false;
        inputField2.interactable = false;
        backButton.GetComponent<Button>().interactable = false;
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
        characterMovement.transform.position = characterStartingPosition;
        inputField1.interactable = true;
        inputField2.interactable = true;
        backButton.GetComponent<Button>().interactable = true;
        runButton.GetComponent<Button>().interactable = true;

        //gerek var mi?
        Time.timeScale = 1;
    }
    public void LoadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void Start()
    {
        isWaitingInstruction = false;
        instructionList = new List<Instruction>();
        //ReadInputPage1(inputPage1);
        characterMovement = character.GetComponent<CharacterMovementController>();
        characterStartingPosition = characterMovement.transform.position;
        characterColorAndShapeChanger = character.GetComponent<CharacterColorAndShapeChanger>();
        chestAnimator = chest.GetComponent<Animator>();
        pythonReservedWords = new string[] { "def", "if", "else", "elif", "for", "while", "False", "True", "and", "as", "assert", "break", "class", "continue",
                                            "del",   "except", "finally",  "form", "global", "import", "in", "is", "lambda",
                                            "nonlocal", "not", "or", "pass", "raise", "return", "try",  "with", "yeld"};
        fruits = new string[] { "apple", "banana", "kiwi" };

        conditionRunList = new List<bool>();
        //simdilik burada. sonradan unity uzerinden verilecek.
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        //catNumber = 1;
        //levelNumber = 7;


        InitializeFirebase();
        //characterColorChanger.ChangeColorToBlue();
        //characterColorChanger.ChangeColorToRed();

        //chestAnimator.SetBool("opening", true);
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

    // Handle removing subscription and reference to the Auth instance.
    // Automatically called by a Monobehaviour after Destroy is called on it.
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


    public void ReadInputPage1(TMP_InputField inputField1)
    {
        this.inputField1 = inputField1;
        //inputText2 = inputPage2.text;
        //Debug.Log(inputPage1);
        //Debug.Log("Buradan sonra 2. sayfa");
        //Debug.Log(inputText2);
    }

    public void ReadInputPage2(TMP_InputField inputField2)
    {
        this.inputField2 = inputField2;
        //Debug.Log(inputText2);
    }

    public void RunCode()
    {

        string inputText1 = inputField1.text;
        string inputText2 = inputField2.text;

        string[] rows1 = inputText1.Split("\n");
        string[] rows2 = inputText2.Split("\n");

        //indentation buyuklugu
        int n = 2;


        //string className = null;
        string className = "SimpleCharacter";
        string parentClassName = "Character";
        string characterColor;
        string secondClassFileName = "SimpleCharacter";


        //List<string> classInitParameters = new List<string>();

        if (!String.IsNullOrEmpty(inputText2))
        {
            List<string> initAssignments = new List<string>();
            int indentation = 0;


            bool isFirstRow = true;
            bool isInitRow = false;
            bool isInsideInit = false;
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
                            GameOver("class nesnesi olu�turman�z gerekiyor. class kelimesini kullanman�z gerekiyor.");
                        }
                        else
                        {
                            string classPart = row.Substring(5).Trim();

                            if (classPart.Substring(0, className.Length) != className)
                            {
                                Debug.Log("hata");
                                GameOver("class ad�n� " + className + " yapman�z gerekiyor.");
                            }
                            else
                            {
                                string parentClassPart = classPart.Substring(className.Length).Trim();
                                if (parentClassPart[0] != '(')
                                {
                                    Debug.Log("hata");
                                    GameOver("Parantez hatas�: Bir class'� olu�tururken parantezlerini do�ru �ekilde kullanman�z gerekiyor. �rnekler: SimpleCharacter(Character) veya Character()");
                                }
                                else
                                {
                                    parentClassPart = parentClassPart.Substring(1).Trim();
                                    if (parentClassPart.Substring(0, parentClassName.Length) != parentClassName)
                                    {
                                        Debug.Log("hata");
                                        GameOver("Olu�turaca��n�z class'�n parent class'�n� parantez i�ine yazman�z gerekiyor. �rnek: SimpleCharacter(Character)");
                                    }
                                    else
                                    {
                                        parentClassPart = parentClassPart.Substring(parentClassName.Length).Replace(" ", "");
                                        if (parentClassPart.Length > 2)
                                        {
                                            Debug.Log("hata");
                                            GameOver("class olu�tururken parantezi do�ru yerde kapatt���n�zdan ve sonras�nda sadece : koydu�unuzdan emin olun.");
                                        }
                                        else if (parentClassPart[0] != ')')
                                        {
                                            Debug.Log("hata");
                                            GameOver("Parantez hatas�: Bir class'� olu�tururken parantezlerini do�ru �ekilde kullanman�z gerekiyor. �rnekler: SimpleCharacter(Character) veya Character()");
                                        }
                                        else if (parentClassPart[1] != ':')
                                        {
                                            Debug.Log("hata");
                                            GameOver("class olu�tururken komutun sonuna : eklemeniz gerekiyor.");
                                        }
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        GameOver("Hata");
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
                        GameOver("Indentation hatas�: Komutun �n�ne class constructor komutunun �n�ne koydu�unuzdan " + n + " fazla bo�luk koyarak yazman�z gerekiyor.");
                    }



                    //string leftTrimmedRow = rows2[i].Substring(rowIndentation);
                    string trimmedRow = rows2[i].Trim();
                    Debug.Log(rows2[i]);
                    string[] rowWords;
                    //string[] rowWords = leftTrimmedRow.Split(" ");


                    //en az gerekli karakter say�s� = 16
                    //BUNLARI DE���T�REB�L�R�M. D�REKT HATALI DEMEK SA�MA.

                    //try
                    //{
                    if (trimmedRow.Substring(0, 3) != "def")
                    {
                        Debug.Log("Hata");
                        GameOver("init komutunu yazmak i�in �nce def kelimesini yazman�z gerekiyor.");
                    }
                    else
                    {
                        rowWords = trimmedRow.Split(" ", 2);

                        string initWord = rowWords[1];
                        Debug.Log(initWord);

                        if (initWord.Substring(0, 8) != "__init__")
                        {
                            Debug.Log("Hata");
                            GameOver("init komutu i�in def yazd�ktan sonra bo�luk b�rak�p __init__ yazarak devam etmeniz gerekiyor.");
                        }
                        else
                        {
                            string initWordParametersPart = initWord.Substring(8).Trim();
                            Debug.Log(initWordParametersPart);
                            if (initWordParametersPart[0] != '(')
                            {
                                Debug.Log("hata");
                                GameOver("Parantez hatas�: __init__ yazd�ktan sonra parantez i�inde gerekli parametreleri girmeniz gerekiyor.");
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
                                    GameOver("Parantez hatas�: __init__ yazd�ktan sonra parantez i�inde gerekli parametreleri girmeniz gerekiyor.");
                                }
                                else
                                {
                                    initWordParametersPart = initWordParametersPart.Substring(0, initWordParametersPart.Length - 1).Trim();
                                    Debug.Log(initWordParametersPart);
                                    initParameters = initWordParametersPart.Split(",");
                                    
                                    for (int j = 0; j < initParameters.Length; j++)
                                    {
                                        //Debug.Log(j);
                                        Debug.Log(initParameters.Length);
                                        initParameters[j] = initParameters[j].Trim();
                                        Debug.Log(initParameters[j]);
                                        //Debug.Log(VariableCheck("ad"));
                                        Debug.Log(pythonReservedWords.Length);
                                        //Debug.Log(VariableCheck(initParameters[j]));
                                        //if (!VariableCheck(initParameters[j]))
                                        //{
                                        //    Debug.Log("hata");
                                        //    //GameOver("init i�erisine yazd���n�z parametre de�i�ken yaz�m� kurallar�na uymal�d�r. Hatal� parametre: " + initParameters[j]);
                                        //}
                                        Debug.Log("111");
                                    }
                                }
                            }

                        }

                    }
                    
                    //Burada parametrelere ulastigim icin burada kontrol ediyorum. Yukarida da edebilirim.
                    if (!initParameters.Contains("shape"))
                    {
                        GameOver("init parametrelerinden biri shape olmal�d�r.");
                    }
                    else if (!initParameters.Contains("color"))
                    {
                        GameOver("init parametrelerinden biri color olmal�d�r.");
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
                else if (isInsideInit)
                {
                    Debug.Log("inside init");
                    int c = 0;
                    while (rows2[i][c] == ' ')
                    {
                        c++;
                    }
                    rowIndentation = c;

                    if (rowIndentation != indentation + 2 * n)
                    {
                        Debug.Log("hATA");
                        GameOver("Indentation hatas�: Komutun �n�ne init komutunun �n�ne koydu�unuzdan " + n + " fazla bo�luk koyarak yazman�z gerekiyor.");
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
                                GameOver("Parametreleri atad���n�z sat�rda atama i�in = kullanman�z gerekiyor.");
                            }
                            else
                            {
                                string[] assignmentParts = trimmedRow.Split('=');
                                string leftPart = assignmentParts[0].Trim();
                                string rightPart = assignmentParts[1].Trim();

                                if (leftPart.Substring(0, selfKeyword.Length) != selfKeyword)
                                {
                                    Debug.Log("hata");
                                    GameOver("Atama sat�r�nda belirledi�iniz self keyword'�n� kullanman�z gerekiyor. Belirledi�iniz self keyword: " + selfKeyword);
                                }
                                else
                                {
                                    string leftPartAfterSelfWord = leftPart.Substring(selfKeyword.Length).Trim();
                                    if (leftPartAfterSelfWord[0] != '.')
                                    {
                                        Debug.Log("hata");
                                        GameOver("Atama sat�r�nda belirledi�iniz self keyword'den sonra . kullanman�z gerekiyor. Belirledi�iniz self keyword" + selfKeyword);
                                    }
                                    else
                                    {
                                        leftPartAfterSelfWord = leftPartAfterSelfWord.Substring(1).Trim();
                                        Debug.Log(leftPartAfterSelfWord);

                                        Debug.Log(rightPart);

                                        if (!initParameters.Contains(rightPart))
                                        {
                                            Debug.Log("+" + rightPart + "+");
                                            foreach (string str in initParameters)
                                            {
                                                Debug.Log("+" + str + "+");
                                            }
                                            Debug.Log("hata");
                                            GameOver("Atama sat�r�nda sa� tarafa yazd���n�z kelime init parametreleri aras�nda bulunmal�d�r.");
                                        }
                                        else
                                        {
                                            initAssignments.Add(rightPart);

                                        }





                                    }
                                }
                            }


                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.Message);
                        }

                    }
                }

                //}

            }



            if (initAssignments.Count != initParameters.Length - 1)
            {
                Debug.Log("atama say�lar� yetersiz");
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
                        }
                    }

                }
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //eger string bos degilse
        if (!String.IsNullOrEmpty(inputText1))
        {
            //List<Instruction> instructions = new List<Instruction>();
            int instructionLevel;

            string lastInstructionType = null;

            //bool isImportRow = true;
            bool isFirstRow = true;
            bool isClassInstanceRow = false;
            int indentation = 0;
            //bool isBodyOfSomething = false;
            //Stack<int> indentationStack = new Stack<int>();
            //int rowIndentation = 0;


            //List<Condition> tempConditions = new List<Condition>();
            //ConditionHolder conditionHolder = null;

            int conditionId = -1;
            int lastIndentation = 0;
            //Ba�ka class varsa buras� kullan�lacak. Yoksa baz� k�s�mlar� kullan�lmayacak.
            for (int i = 0; i < rows1.Length; i++)
            {
                //ayr�ca null kontrol� de gerekebilir.
                if (String.IsNullOrEmpty(rows1[i]))
                    continue;

                int rowIndentation;
                //Debug.Log(rows2[i]);
                //bos satirlar yok sayiliyor.
                //BURADA UZUNLU�U 1'SE D�YE KONTROL ETMEK BELK� DAHA �Y� OLUR AMA UZUN B�R BO�LUK DA BIRAKAB�L�RLER. B�R �EK�LDE HALLED�CEZ.
                if (!String.IsNullOrEmpty(rows1[i]))
                {
                    //if (rows1[i].Length >= 4)
                    //{
                    //birden fazla import sat�r� istenmedi�i i�in bir tane olacak �ekilde ayarland�. 
                    if (isFirstRow)
                    {
                        int c = 0;
                        while (rows1[i][c] == ' ')
                        {
                            c++;
                        }
                        indentation = c;
                        lastIndentation = c;
                        string row = rows1[i].Trim();
                        try
                        {
                            if (row.Substring(0, 4) != "from")
                            {
                                Debug.Log("hata");
                                GameOver("Import komutunun ilk kelimesi from olmal�d�r.");
                            }
                            else
                            {
                                row = row.Substring(4).Trim();
                                if (row.Substring(0, secondClassFileName.Length) != secondClassFileName)
                                {
                                    Debug.Log("hata");
                                    GameOver("Import komutunda class'�n oldu�u dosyan�n ad�n� yazman�z gerekiyor. " + className + " class'� i�in dosya ad�: " + secondClassFileName);
                                }
                                else
                                {
                                    row = row.Substring(secondClassFileName.Length).Trim();
                                    if (row.Substring(0, 6) != "import")
                                    {
                                        Debug.Log("hata");
                                        GameOver("Import komutununda class'�n bulundu�u dosyadan sonra import yazman�z gerekiyor.");
                                    }
                                    else
                                    {
                                        row = row.Substring(6).Trim();
                                        if (row != className)
                                        {
                                            Debug.Log("hata");
                                            GameOver("Import komutunda class'�n ad�n� yazman�z gerekiyor. class ad�: " + className);
                                        }

                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.Log("hata");
                        }


                        isFirstRow = false;
                        isClassInstanceRow = true;
                    }
                    //buraya eklenebilir.
                    else if (isClassInstanceRow)
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
                            GameOver("Indentation hatas�: Komutun �n�ne import komutunun �n�ne koydu�unuz kadar bo�luk koyarak yazman�z gerekiyor.");
                            Debug.Log("Indentation hatasi");
                        }


                        //string leftTrimmedRow = rows2[i].Substring(rowIndentation);
                        string trimmedRow = rows1[i].Trim();

                        string[] classInstanceParts;
                        if (!trimmedRow.Contains("="))
                        {
                            GameOver("Bir " + className + " nesnesi olu�turman�z gerekiyor.");
                            Debug.Log("Bir Character nesnesi olu�turman�z gerekiyor.");
                        }
                        else
                        {

                            classInstanceParts = trimmedRow.Split('=');


                            string classInstanceName = classInstanceParts[0].Trim();
                            string classConstructor = classInstanceParts[1].Trim();

                            if (!VariableCheck(classInstanceName))
                            {
                                GameOver("Olu�turmak istedi�iniz nesnenin ad� de�i�ken ismi kurallar�na uygun olmal�d�r.");
                                Debug.Log("Class instance name:" + classInstanceName + "+");
                                Debug.Log("hata");
                            }
                            //else if(classInstanceRightPart.Contains("="))
                            //{
                            //    Debug.Log("Hata");
                            //}
                            else if (!classConstructor.Contains("(") || !classConstructor.Contains(")"))
                            {
                                GameOver(className + " nesnesi olu�tururken parantezleri do�ru kullanman�z gerekiyor.\n�rnek: " + className + "(\"blue\")");
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
                                        GameOver("Parantez hatas�: class nesnesi olu�tururken parantezi kapatman�z gerekiyor.");
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
                                            GameOver("self keyword olmadan init komutuna yazd���n�z parametrelere s�ras�yla uygun de�erler yazman�z gerekiyor.");
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
                                                        //character.SetActive(true);
                                                    }
                                                    else if (parameter == "square")
                                                    {
                                                        characterColorAndShapeChanger.ChangeShapeToSquare();
                                                    }
                                                    else
                                                    {
                                                        Debug.Log("hata");
                                                        GameOver("shape parametresine uygun bir �ekil yazman�z gerekiyor. Uygun �ekiller: circle, square");
                                                    }
                                                }
                                                else if (initParameters[j] == "color")
                                                {
                                                    Debug.Log("+"+parameter+"+");
                                                    //Color.FromName(parameter);
                                                    if (parameter == "red")
                                                    {
                                                        //GetComponent<Renderer>().material.color = UnityEngine.Color.red;
                                                        characterColorAndShapeChanger.ChangeColorToRed();
                                                    }
                                                    else if (parameter == "green")
                                                    {
                                                        Debug.Log("gg");
                                                        //character.transform.Find("CircleBody").gameObject.GetComponent<Renderer>().material.color = UnityEngine.Color.green;
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

                                    //string[] classParameters = classConstructorParts[1].Split(',');



                                    //constructor part'larinda normalde string, integer veya double vs. deger olabilir.
                                    //ben buyuk ihtimalle sadece string olan classlar kullanacagim. yine de digerleri de implement edilebilir.
                                    //python oldugu icin init kisminda parametrelerin tipleri belli olmaz. burada kontrol etmek lazim.

                                    //b�l�mlere g�re farkl� kontroller olabilir.



                                }
                            }
                        }

                        character.SetActive(true);
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
                            GameOver("Indentation hatas�: Komutun ba��nda b�rakt���n�z bo�luk miktar�n� kontrol etmeniz gerekiyor.");
                            Debug.Log("indentation hatas�");
                        }


                        Debug.Log("rowindentaiton " + rowIndentation);
                        Debug.Log("indentaion " + indentation);


                        instructionLevel = ((rowIndentation - indentation) / n) + 1;



                        if (lastInstructionType == "holder")
                        {
                            if (rowIndentation != lastIndentation + n)
                            {
                                GameOver("Indentation hatas�: Komutun ba��nda b�rakt���n�z bo�luk miktar�n� kontrol etmeniz gerekiyor.");
                                Debug.Log("indentation hatas�2");
                            }
                        }


                        //string leftTrimmedRow = rows1[i].Substring(rowIndentation);
                        string trimmedRow = rows1[i].Trim();
                        Debug.Log(trimmedRow + " " + trimmedRow.Length);

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
                                        GameOver("if komutu yazarken if yazd�ktan sonra bo�luk b�rakman�z gerekiyor.");
                                        Debug.Log("bo�luk olmal� hata");
                                    }
                                    else
                                    {
                                        // if kismi atilacak.
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


                                        if (operatorType == null)
                                        {
                                            string[] ifParts = booleanPart.Split('.');
                                            string parameterPart = null;

                                            //if (ifParts.Length == 2 || ifParts.Length == 3)
                                            if (ifParts.Length == 3)
                                            {

                                                if (ifParts[0] != className)
                                                {
                                                    Debug.Log(ifParts[0]);
                                                    Debug.Log("classname2=" + className);
                                                    GameOver("Bu problemde if komutunda " + className + "'in metodlar�n� kullanarak kontrol yapman�z gerekiyor.\n�rnek: if " + className + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (ifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        Debug.Log("hata");
                                                        GameOver("Parantez hatas�: up_tile metoduna ula�mak i�in up_tile() yazman�z gerekiyor.");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "up_tile";
                                                        //If ifInstruction = new If(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(ifInstruction);
                                                    }

                                                }
                                                else if (ifParts[1].Substring(0, 9) == "down_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: down_tile metoduna ula�mak i�in down_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "down_tile";
                                                        //If ifInstruction = new If(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(ifInstruction);
                                                    }
                                                }
                                                else if (ifParts[1].Substring(0, 10) == "right_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(10).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: right_tile metoduna ula�mak i�in right_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "right_tile";
                                                        //If ifInstruction = new If(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(ifInstruction);
                                                    }
                                                }
                                                else if (ifParts[1].Substring(0, 9) == "left_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: left_tile metoduna ula�mak i�in left_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";
                                                        //If ifInstruction = new If(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(ifInstruction);
                                                    }
                                                }
                                                //else
                                                //{
                                                //    GameOver("Kodunuzda bir hata bulundu.");
                                                //    Debug.Log("simdilik hata");
                                                //}

                                                //if (ifParts.Length == 2)
                                                //{
                                                //    //burada 2 parcali if listeye eklenecek.
                                                //}
                                                //else if (ifParts.Length == 3)
                                                //{
                                                //kaldirilabilir ??
                                                //parameterPart = null;
                                                string secondMethod = null;
                                                //kontrol sirasi degismemeli. parameterPart dolu olan sonra okunmali. veya durum degistirilecek.


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
                                                        GameOver("Parantez hatas�: is_ground metoduna ula�mak i�in is_ground() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        secondMethod = "is_ground";
                                                        If ifInstruction = new If(characterMovement, firstMethod, secondMethod, null, instructionLevel);
                                                        AddInstruction(ifInstruction);
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
                                                        GameOver("Parantez hatas�: is_water metoduna ula�mak i�in is_water() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        secondMethod = "is_water";
                                                        If ifInstruction = new If(characterMovement, firstMethod, secondMethod, null, instructionLevel);
                                                        AddInstruction(ifInstruction);
                                                    }

                                                }
                                                else if (ifParts[2].Substring(0, 8) == "contains")
                                                {
                                                    //string s = ifParts[2].Substring(8).Replace(" ", "");
                                                    parameterPart = ifParts[2].Substring(8).Trim();
                                                    //burasi degisecek. ()'in ici dolu olacak.
                                                    if (parameterPart[parameterPart.Length - 1] != ':')
                                                    {
                                                        GameOver("if komutununun sonuna : eklemeniz gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);
                                                    //hata cikarsa zaten program duracak
                                                    if (parameterPart[0] != '(' || parameterPart[parameterPart.Length - 2] != ')')
                                                    {
                                                        GameOver("Parantez hatas�: contains metoduna ula�mak i�in �rnek olarak contains(apple) yazman�z gerekiyor.");
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
                                                            If ifInstruction = new If(characterMovement, firstMethod, secondMethod, parameterPart, instructionLevel);
                                                            AddInstruction(ifInstruction);
                                                        }
                                                    }

                                                }

                                                //burada 3 parcali if listeye eklenecek. ya da hepsi kendi yerinden listeye eklenecek.
                                                //}


                                            }


                                        }
                                    }

                                }
                            }
                            else if (trimmedRow.Substring(0, 3) == "for")
                            {

                                //string[] rowWords = rows1[i].Split(' ');
                                //Debug.Log(rowWords[1]);

                                if (trimmedRow[3] != ' ')
                                {
                                    GameOver("for komutunda for'dan sonra bo�luk b�rakman�z gerekiyor.");
                                    Debug.Log("hata");

                                }
                                else if (!trimmedRow.Contains("in"))
                                {
                                    GameOver("Bu problemde for komutunda in kelimesi kullanman�z gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else if (trimmedRow[trimmedRow.IndexOf("in") - 1] != ' ')
                                {
                                    GameOver("for komutunda in'den �nce bo�luk b�rakman�z gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else if (trimmedRow[trimmedRow.IndexOf("in") + 2] != ' ')
                                {
                                    GameOver("for komutunda in'den sonra bo�luk b�rakman�z gerekiyor.");
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
                                        GameOver("De�i�ken hatal�:" + var);
                                        Debug.Log("hata");
                                    }
                                    else if (!trimmedRow.Contains("range"))
                                    {
                                        GameOver("Bu problemde for komutunda range kullanman�z/yazman�z gerekiyor.");
                                        Debug.Log("hata");
                                    }
                                    else
                                    {
                                        string rangeParameter = trimmedRow.Substring(trimmedRow.IndexOf("range") + 5).Replace(" ", "");
                                        Debug.Log("rangeParameter1 = " + rangeParameter);

                                        if (rangeParameter[0] != '(')
                                        {
                                            GameOver("Parantez hatas�: for komutunda range kelimesinden sonra parantez i�inde bir de�er girmeniz gerekiyor.");
                                            Debug.Log("hata");
                                        }

                                        else if (rangeParameter[rangeParameter.Length - 2] != ')')
                                        {
                                            GameOver("Parantez hatas�: for komutunda range kelimesinden sonra parantez i�inde bir de�er girmeniz gerekiyor.");
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
                                                //range i�indeki de�ere ula�abiliyorum art�k.

                                                //Bunu do�ru yere eklemek gerekiyor.
                                                For forLoop = new For(this, characterMovement, parameter, instructionLevel);


                                                //indentation kontrol laz�m.
                                                if (instructionList.Count == 0)
                                                {

                                                    instructionList.Add(forLoop);
                                                }
                                                else
                                                {
                                                    for (int j = instructionList.Count - 1; j >= 0; j--)
                                                    {
                                                        if (instructionList[j] != null)
                                                        {
                                                            if (instructionList[j].level == instructionLevel - 1)
                                                            {
                                                                //burada holder olduklar�ndan emin olmak laz�m. Yani �sttekinden emin olmak laz�m. 
                                                                //((HolderInstruction)instructionList[j]).Add(forLoop);
                                                                AddInstruction(forLoop);
                                                            }
                                                        }
                                                    }
                                                }

                                                lastInstructionType = "holder";
                                                lastIndentation = rowIndentation;
                                                //instructionList.Add(forLoop);

                                            }
                                        }
                                    }



                                }

                            }
                            else if (trimmedRow.Substring(0, 4) == "move")
                            {
                                if (!trimmedRow.Contains('('))
                                {
                                    GameOver("Parantez hatas�: parametresiz methodlar sonlar�na () eklenerek, parametreli methodlar eklenen parantezin i�ine parametre de�erleri yaz�larak �a�r�l�r.");

                                    Debug.Log("hata");
                                }
                                else if (!trimmedRow.Contains(')'))
                                {
                                    GameOver("Parantez hatas�: parametresiz methodlar sonlar�na () eklenerek, parametreli methodlar eklenen parantezin i�ine parametre de�erleri yaz�larak �a�r�l�r.");
                                    Debug.Log("hata2");
                                }

                                string[] instructionParts = trimmedRow.Split('(', 2);

                                instructionParts[0] = instructionParts[0].Trim();
                                int length = instructionParts[0].Length;

                                Debug.Log(instructionParts[0]);
                                try
                                {
                                    if (instructionParts[0] == "move_up")
                                    {
                                        Move move_up = new Move(characterMovement, "up", instructionLevel);
                                        AddInstruction(move_up);
                                    }
                                    else if (instructionParts[0] == "move_left")
                                    {
                                        Move move_left = new Move(characterMovement, "left", instructionLevel);
                                        AddInstruction(move_left);
                                    }
                                    else if (instructionParts[0] == "move_down")
                                    {
                                        Move move_down = new Move(characterMovement, "down", instructionLevel);
                                        AddInstruction(move_down);
                                    }
                                    else if (instructionParts[0] == "move_right")
                                    {

                                        Move move_right = new Move(characterMovement, "right", instructionLevel);
                                        Debug.Log("mr level " + instructionLevel);
                                        AddInstruction(move_right);
                                    }
                                    else
                                    {
                                        GameOver("Method tan�m�n� do�ru yazman�z gerekiyor.");
                                        Debug.Log("hata");
                                    }
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    GameOver("Hata: " + ex.Message);

                                    Debug.Log(ex.Message);
                                }


                            }
                            else if (trimmedRow.Substring(0, 4) == "swim")
                            {
                                if (!trimmedRow.Contains('('))
                                {
                                    GameOver("Parantez hatas�: parametresiz methodlar sonlar�na () eklenerek, parametreli methodlar eklenen parantezin i�ine parametre de�erleri yaz�larak �a�r�l�r.");
                                    Debug.Log("hata");
                                }
                                else if (!trimmedRow.Contains(')'))
                                {
                                    GameOver("Parantez hatas�: parametresiz methodlar sonlar�na () eklenerek, parametreli methodlar eklenen parantezin i�ine parametre de�erleri yaz�larak �a�r�l�r.");
                                    Debug.Log("hata2");
                                }

                                string[] instructionParts = trimmedRow.Split('(', 2);

                                instructionParts[0] = instructionParts[0].Trim();
                                int length = instructionParts[0].Length;

                                Debug.Log(instructionParts[0]);
                                try
                                {
                                    if (instructionParts[0] == "swim_up")
                                    {
                                        Swim swim_up = new Swim(characterMovement, "up", instructionLevel);
                                        AddInstruction(swim_up);
                                    }
                                    else if (instructionParts[0] == "swim_left")
                                    {
                                        Swim swim_left = new Swim(characterMovement, "left", instructionLevel);
                                        AddInstruction(swim_left);
                                    }
                                    else if (instructionParts[0] == "swim_down")
                                    {
                                        Swim swim_down = new Swim(characterMovement, "down", instructionLevel);
                                        AddInstruction(swim_down);
                                    }
                                    else if (instructionParts[0] == "swim_right")
                                    {

                                        Swim swim_right = new Swim(characterMovement, "right", instructionLevel);
                                        Debug.Log("mr level " + instructionLevel);
                                        AddInstruction(swim_right);
                                    }
                                    else
                                    {
                                        GameOver("Method tan�m�n� do�ru yazman�z gerekiyor.");
                                        Debug.Log("hata");
                                    }
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    GameOver("Hata: " + ex.Message);
                                    Debug.Log(ex.Message);
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
                                        GameOver("elif komutu yazarken elif yazd�ktan sonra bo�luk b�rakman�z gerekiyor.");
                                        Debug.Log("bo�luk olmal� hata");
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


                                        if (operatorType == null)
                                        {
                                            string[] elifParts = booleanPart.Split('.');
                                            string parameterPart = null;

                                            if (elifParts.Length == 2 || elifParts.Length == 3)
                                            {

                                                if (elifParts[0] != className)
                                                {
                                                    GameOver("Bu problemde elif komutunda " + className + "'in metodlar�n� kullanarak kontrol yapman�z gerekiyor.\n�rnek: elif " + className + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (elifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: up_tile metoduna ula�mak i�in up_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "up_tile";
                                                        //Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(elifInstruction);
                                                    }

                                                }
                                                else if (elifParts[1].Substring(0, 9) == "down_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: down_tile metoduna ula�mak i�in down_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "down_tile";
                                                        //Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(elifInstruction);
                                                    }
                                                }
                                                else if (elifParts[1].Substring(0, 10) == "right_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(10).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: right_tile metoduna ula�mak i�in right_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "right_tile";
                                                        //Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(elifInstruction);
                                                    }
                                                }
                                                else if (elifParts[1].Substring(0, 9) == "left_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: left_tile metoduna ula�mak i�in left_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";
                                                        //Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(elifInstruction);
                                                    }
                                                }


                                                if (elifParts.Length == 2)
                                                {
                                                    //burada 2 parcali if listeye eklenecek.
                                                }
                                                else if (elifParts.Length == 3)
                                                {
                                                    //kaldirilabilir ??
                                                    //parameterPart = null;
                                                    string secondMethod = null;
                                                    //kontrol sirasi degismemeli. parameterPart dolu olan sonra okunmali. veya durum degistirilecek.


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
                                                            Elif elifInstruction = new Elif(characterMovement, firstMethod, secondMethod, null, instructionLevel);
                                                            AddInstruction(elifInstruction);
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
                                                            Elif elifInstruction = new Elif(characterMovement, firstMethod, secondMethod, null, instructionLevel);
                                                            AddInstruction(elifInstruction);
                                                        }

                                                    }
                                                    else if (elifParts[2].Substring(0, 8) == "contains")
                                                    {
                                                        //string s = ifParts[2].Substring(8).Replace(" ", "");
                                                        parameterPart = elifParts[2].Substring(8).Trim();
                                                        //burasi degisecek. ()'in ici dolu olacak.
                                                        if (parameterPart[parameterPart.Length - 1] != ':')
                                                        {
                                                            GameOver("elif komutunun sonuna : eklemeniz gerekiyor.");
                                                            Debug.Log("hata");
                                                        }
                                                        parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);
                                                        //hata cikarsa zaten program duracak
                                                        if (parameterPart[0] != '(' || parameterPart[parameterPart.Length - 2] != ')')
                                                        {
                                                            GameOver("Parantez hatas�: contains metoduna ula�mak i�in �rnek olarak contains(apple) yazman�z gerekiyor.");
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
                                                                Elif elifInstruction = new Elif(characterMovement, firstMethod, secondMethod, parameterPart, instructionLevel);
                                                                AddInstruction(elifInstruction);
                                                            }

                                                        }

                                                    }

                                                    //burada 3 parcali if listeye eklenecek. ya da hepsi kendi yerinden listeye eklenecek.
                                                }


                                            }


                                        }
                                    }

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
                                        GameOver("while komutu yazarken while yazd�ktan sonra bo�luk b�rakman�z gerekiyor.");
                                        Debug.Log("bo�luk olmal� hata");
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


                                        if (operatorType == null)
                                        {
                                            string[] whileParts = booleanPart.Split('.');
                                            string parameterPart = null;

                                            if (whileParts.Length == 2 || whileParts.Length == 3)
                                            {

                                                if (whileParts[0] != className)
                                                {
                                                    GameOver("Bu problemde while komutunda " + className + "'in metodlar�n� kullanarak kontrol yapman�z gerekiyor.\n�rnek: while " + className + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (whileParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: up_tile metoduna ula�mak i�in up_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "up_tile";
                                                        //Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(elifInstruction);
                                                    }

                                                }
                                                else if (whileParts[1].Substring(0, 9) == "down_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: down_tile metoduna ula�mak i�in down_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "down_tile";
                                                        //Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(elifInstruction);
                                                    }
                                                }
                                                else if (whileParts[1].Substring(0, 10) == "right_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(10).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: right_tile metoduna ula�mak i�in right_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "right_tile";
                                                        //Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(elifInstruction);
                                                    }
                                                }
                                                else if (whileParts[1].Substring(0, 9) == "left_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatas�: left_tile metoduna ula�mak i�in left_tile() yazman�z gerekiyor.");
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";
                                                        //Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(elifInstruction);
                                                    }
                                                }


                                                if (whileParts.Length == 2)
                                                {
                                                    //burada 2 parcali if listeye eklenecek.
                                                }
                                                else if (whileParts.Length == 3)
                                                {
                                                    //kaldirilabilir ??
                                                    //parameterPart = null;
                                                    string secondMethod = null;
                                                    //kontrol sirasi degismemeli. parameterPart dolu olan sonra okunmali. veya durum degistirilecek.


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
                                                            While whileInstruction = new While(characterMovement, firstMethod, secondMethod, null, instructionLevel);
                                                            AddInstruction(whileInstruction);
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
                                                            While whileInstruction = new While(characterMovement, firstMethod, secondMethod, null, instructionLevel);
                                                            AddInstruction(whileInstruction);
                                                        }

                                                    }
                                                    else if (whileParts[2].Substring(0, 8) == "contains")
                                                    {
                                                        //string s = ifParts[2].Substring(8).Replace(" ", "");
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
                                                            GameOver("Parantez hatas�: contains metoduna ula�mak i�in �rnek olarak contains(apple) yazman�z gerekiyor.");
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
                                                                While whileInstruction = new While(characterMovement, firstMethod, secondMethod, parameterPart, instructionLevel);
                                                                AddInstruction(whileInstruction);
                                                            }

                                                        }

                                                    }

                                                    //burada 3 parcali if listeye eklenecek. ya da hepsi kendi yerinden listeye eklenecek.
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
                                    GameOver("else komutu else kelimesi ve : haricinde sadece bo�luklar i�erebilir.");
                                    Debug.Log("hata");
                                }

                                else if (trimmedRow[4] != ':')
                                {
                                    GameOver("else komutunun sonuna : eklemeniz gerekiyor.");
                                    Debug.Log("hata");
                                }

                                else
                                {
                                    //AddInstruction() i�inde bunun �st�dneki ConditionHolder m� diye kontrol etmek gerek.
                                    //Else elseCondition = new Else(instructionLevel + 1);
                                    //Else elseCondition = new Else(conditionId, conditionRunList, instructionLevel);
                                    Else elseCondition = new Else(characterMovement, instructionLevel);
                                    AddInstruction(elseCondition);
                                }


                            }
                        }
                        catch (Exception e)
                        {
                            GameOver("Hata: " + e.Message);
                            Debug.Log(e.Message);
                        }

                    }
                }


            }

            Debug.Log(instructionList.Count);
            foreach (Instruction v in instructionList)
                Debug.Log(v.ToString());


            //Burada bolum ozel kontrolleri yapilabilir.

            //CheckCat1Level7Conditions(instructionList);
            CheckLevelConditions(catNumber, levelNumber, instructionList);
            //if(instructionList.Count > 0)
            //{
            //if (instructionList[0].GetType() != typeof(For))
            //{
            //    Debug.Log("�lk komutun bir for olmas� gerekiyor.");
            //}
            //}


            //instructionList burada calistiriliyor.
            //bool isThereIf = false;
            //bool didPreviousConditionsRun = false;

            RunInstructions(instructionList);

            //Buraya gelecek.
            //karakter sandigin ustundeyse 
            //chestAnimator.SetBool("opening", true);

            //BOLUM GECME KISMI
            Vector3Int characterFinalPosition = characterMovement.groundTilemap.WorldToCell(characterMovement.transform.position);
            if (characterMovement.chestPositionTilemap.HasTile(characterFinalPosition))
            {
                chestAnimator.SetBool("opening", true);
                //burada bolum gecilmis olacak.
                SubmitLevelAsPassed();
            }
        }
    }


    //----------------------------------------------------------------------------------------------------------------
    public void RunInstructions(List<Instruction> instructionList)
    {
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
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
                            {
                                didPreviousConditionsRun = true;

                                instruction.Run();

                                //Invoke("LaunchProjectile", 2.0f);
                                //System.Threading.Thread.Sleep(1000);
                                //WaitFor1Second();
                                //StartCoroutine(WaitFor1SecondCoroutine());
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
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
                            {
                                Debug.Log(":????????");
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                        else if (((If)instruction).secondMethod == "contains")
                        {
                            //burada direkt parameterPart diye yazilabilir mi??
                            if (((If)instruction).secondMethodParameter == "apple")
                            {

                            }
                            else if (((If)instruction).secondMethodParameter == "banana")
                            {

                            }
                            else if (((If)instruction).secondMethodParameter == "kiwi")
                            {

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
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(downTilePosition))
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
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(downTilePosition))
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
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition))
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
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition))
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
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition))
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
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition))
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                    }
                }



            }
            //else if (!isThereIf)
            //{
            //    Debug.Log("hata");
            //}
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
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
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
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                                if (((If)instruction).characterMovement.groundTilemap.HasTile(downTilePosition))
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
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(downTilePosition))
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
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition))
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
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition))
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
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition))
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
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition))
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
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
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
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition))
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
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition))
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
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(downTilePosition))
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
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(downTilePosition))
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
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition))
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
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition))
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
                //System.Threading.Thread.Sleep(1000);
                //timer += Time.deltaTime;
                //if (timer > delay)
                //StartCoroutine(WaitFor1SecondCoroutine(instruction));


                //instruction.Invoke("Run", 2f);

            }

        }
    }
    public IEnumerator RunInstructionsWithDelay(List<Instruction> instructions)
    {
        Debug.Log("Hey");
        foreach (Instruction instruction in instructions)
        {
            //Instantiate(gameObject, objectSpawn[i].transform.position, Quaternion.identity);
            //StartCoroutine(WeaponsCooldown(6)); //or do what you want
            instruction.Run();
            //runCodeButton.RunInstructions(instructions);
            //RunInstructions(instructions);
            yield return new WaitForSeconds(1f);
        }
    }

    public void RunInstructionsWithDelayMethod(List<Instruction> instructions)
    {
        StartCoroutine(RunInstructionsWithDelay(instructions));
    }

    //**************************************************************************

    public IEnumerator RunInstructions1Second(List<Instruction> instructionList)
    {


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
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
                            {
                                didPreviousConditionsRun = true;

                                instruction.Run();

                                //Invoke("LaunchProjectile", 2.0f);
                                //System.Threading.Thread.Sleep(1000);
                                //WaitFor1Second();
                                //StartCoroutine(WaitFor1SecondCoroutine());
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
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
                            {
                                Debug.Log(":????????");
                                didPreviousConditionsRun = true;
                                instruction.Run();

                            }
                        }
                        else if (((If)instruction).secondMethod == "contains")
                        {
                            //burada direkt parameterPart diye yazilabilir mi??
                            if (((If)instruction).secondMethodParameter == "apple")
                            {

                            }
                            else if (((If)instruction).secondMethodParameter == "banana")
                            {

                            }
                            else if (((If)instruction).secondMethodParameter == "kiwi")
                            {

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
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(downTilePosition))
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
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(downTilePosition))
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
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition))
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
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition))
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
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition))
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
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition))
                            {
                                didPreviousConditionsRun = true;
                                instruction.Run();
                            }
                        }
                    }
                }



            }
            //else if (!isThereIf)
            //{
            //    Debug.Log("hata");
            //}
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
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
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
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                                if (((If)instruction).characterMovement.groundTilemap.HasTile(downTilePosition))
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
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(downTilePosition))
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
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition))
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
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition))
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
                                if (((Elif)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition))
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
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition))
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
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
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
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(rightTilePosition))
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
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(rightTilePosition))
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
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(downTilePosition))
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
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(downTilePosition))
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
                        while (((While)instruction).characterMovement.groundTilemap.HasTile(leftTilePosition))
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
                        while (((While)instruction).characterMovement.waterTilemap.HasTile(leftTilePosition))
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
                yield return new WaitForSeconds(1);
                //timer += Time.deltaTime;
                //if (timer > delay)

                //StartCoroutine(WaitFor1SecondCoroutine(instruction));


                //instruction.Invoke("Run", 2f);

            }

        }
    }





    //***************************************************************************
    public IEnumerator WaitFor1SecondCoroutine(Instruction instruction)
    {
        yield return new WaitForSeconds(3);
        instruction.Run();
    }
    public IEnumerator WaitFor1SecondCoroutine(RunCodeButton runCodeButton, List<Instruction> instructions)
    {
        yield return new WaitForSeconds(3);
        runCodeButton.RunInstructions(instructions);
    }
    public void WaitFor1Second(RunCodeButton runCodeButton, List<Instruction> instructions)
    {
        StartCoroutine(WaitFor1SecondCoroutine(runCodeButton, instructions));

    }

    //----------------------------------------------------------------------
    //Check Level Conditions
    public void CheckCat1Level7Conditions(List<Instruction> instructionList)
    {
        if (instructionList[0].GetType() != typeof(For))
        {
            Debug.Log("�lk komutun bir for olmas� gerekiyor.");
        }
    }

    public void CheckLevelConditions(int catNumber, int levelNumber, List<Instruction> instructionList)
    {
        if (catNumber == 1)
        {
            if (levelNumber == 1)
            {

            }
            else if (levelNumber == 2)
            {

            }
            else if (levelNumber == 3)
            {

            }
            else if (levelNumber == 4)
            {

            }
            else if (levelNumber == 5)
            {

            }
            else if (levelNumber == 6)
            {

            }
            else if (levelNumber == 7)
            {
                if (instructionList[0].GetType() != typeof(For))
                {
                    Debug.Log("�lk komutun bir for olmas� gerekiyor.");
                }
            }
        }
        else if (catNumber == 2)
        {

        }
        else if (catNumber == 3)
        {

        }
        else if (catNumber == 4)
        {

        }
        else if (catNumber == 5)
        {

        }
        else if (catNumber == 6)
        {

        }

    }

    //-----------------------------------------------------------------------
    public void SubmitLevelAsPassed()
    {
        Dictionary<string, object> Level_5 = new Dictionary<string, object>();
        Level_5["Passed"] = true;

        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).UpdateChildrenAsync(Level_5);
        databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).Child("Level_" + levelNumber).UpdateChildrenAsync(Level_5);
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).Child("Level_" + levelNumber).GetValueAsync().ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsFaulted)
        //    {
        //        Debug.Log("2");
        //        // Handle the error...
        //    }
        //    else if (task.IsCompleted)
        //    {
        //        //DataSnapshot snapshot = task.Result;

        //        Dictionary<string, object> PassedLevel = new Dictionary<string, object>();
        //        PassedLevel["Passed"] = true;

        //        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).Child("Level_" + levelNumber).UpdateChildrenAsync(PassedLevel);
        //    }
        //});
    }

    public void ReadProgressionData(string catNumber)
    {
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).Child("Level_" + catNumber)
        //databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("User Data")
        databaseReference.Child("Users").Child("Children").Child(user.UserId).Child("Progression").Child("Subject_" + catNumber).Child("Level_" + levelNumber).GetValueAsync().ContinueWithOnMainThread(task =>
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
                //userData["firstName"] = firstName;
                //userData["lastName"] = lastName;
                //userData["e-mail"] = email;



                //Dictionary<string, object> childUpdates = new Dictionary<string, object>();
                string userId = user.UserId;

                //Oldu mu???
                databaseReference.Child("Users").Child("Children").Child(userId).Child("Progression").UpdateChildrenAsync(Level_X);

                //Sayfa degistirince veya yeni kullanici gelince hepsi resetlenecek mi? Kontrol etmek lazim.
                //if (snapshot.HasChild("Level_1"))
                //    Level1Button.GetComponentInChildren<Toggle>().isOn = true;
                //if (snapshot.HasChild("Level_2"))
                //    Level2Button.GetComponentInChildren<Toggle>().isOn = true;
                //if (snapshot.HasChild("Level_3"))
                //    Level3Button.GetComponentInChildren<Toggle>().isOn = true;
                //if (snapshot.HasChild("Level_4"))
                //    Level4Button.GetComponentInChildren<Toggle>().isOn = true;
                //if (snapshot.HasChild("Level_5"))
                //    Level5Button.GetComponentInChildren<Toggle>().isOn = true;
                //if (snapshot.HasChild("Level_6"))
                //    Level6Button.GetComponentInChildren<Toggle>().isOn = true;

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
                //if (instruction.ToString() == "Move: up Class")
                //    Debug.Log("Buras� 2:" + instruction.level);

                instructionList.Add(instruction);


                return;
            }
            else
            {
                //if (instructionList.Count > 0)
                //{
                //burada is instance olacak san�r�m
                //instructionList[instructionList.Count - 1].GetType() == typeof(HolderInstruction)
                //Uzun runtime buradan kaynaklan�yor.
                //instructionList[instructionList.Count - 1] is HolderInstruction
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
    //public static void METHOD_1(MonoBehaviour StartThisStatic)
    //{
    //    StartThisStatic.StartCoroutine(Test());
    //}
    //public static IEnumerator Test()
    //{
    //    yield return new WaitForSeconds(2f);
    //}


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

