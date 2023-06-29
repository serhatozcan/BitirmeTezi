
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

//sadece sayýyla dönen for loop. boolean olacaksa ayrý class oluþturulabilir.
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
        //iterasyon sayýsýný ayarlamak lazým.
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

        //boolean burada hesaplanýrsa karakterin o anki güncel durumu deðil. en baþtaki durumuna göre hesap yapýlýr.
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

        //boolean burada hesaplanýrsa karakterin o anki güncel durumu deðil. en baþtaki durumuna göre hesap yapýlýr.
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

        //boolean burada hesaplanýrsa karakterin o anki güncel durumu deðil. en baþtaki durumuna göre hesap yapýlýr.
    }


    //rightpart ve operator type olup olmamasýna gore iki sekilde calisacak
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

        //boolean burada hesaplanýrsa karakterin o anki güncel durumu deðil. en baþtaki durumuna göre hesap yapýlýr.
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

        //boolean burada hesaplanýrsa karakterin o anki güncel durumu deðil. en baþtaki durumuna göre hesap yapýlýr.
    }


    //rightpart ve operator type olup olmamasýna gore iki sekilde calisacak
    public override void Run()
    {
        //burada önce diðerlerinde baðýmsýz olarak bool deðerine göre çalýþacak mý diye kontrol edeceðiz.
        //sonra çalýþacaksa conditionRunListte id'ye karþýlýk gelen indexi true yapýcaz.
        //SIKINTI ÞU: true yaptýktan sonra for loop içerisinde ayný if'e tekrar gelince ne olacak???

        //if(conditionRunList == null) 
        //{ 

        //}
        //if (operatorType != null)
        //{
        //    //buralarda calisip calismadigini dondurmek gerekebilir. ya da run metodunda sadece calistirma yapilacak. run metodu cagrilmadan once if'in calisip calismadigi disarida kontrol edilecek.
        //}

        bool isThereIf = false;
        bool didPreviousConditionsRun = false;
        //Sýkýntýlý bir durum gibi.
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
    private CharacterColorChanger characterColorChanger;
    private string color;

    public ChangeColor(CharacterColorChanger characterColorChanger, string color)
    {
        this.characterColorChanger = characterColorChanger;
        this.color = color;
    }

    public override void Run()
    {
        if (color == "blue")
            characterColorChanger.ChangeColorToBlue();
        else if (color == "red")
            characterColorChanger.ChangeColorToRed();
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
        //burada direk Move(Vector2) kullanýlabilir. 
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
        //burada direk Move(Vector2) kullanýlabilir. 
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
    [SerializeField]
    public GameObject character;
    public GameObject chest;
    private CharacterMovementController characterMovement;
    private CharacterColorChanger characterColorChanger;

    public Animator chestAnimator;

    private string[] initParameters = null;

    private string[] pythonReservedWords;

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
        characterColorChanger = character.GetComponent<CharacterColorChanger>();
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
                            GameOver("class nesnesi oluþturmanýz gerekiyor. class kelimesini kullanmanýz gerekiyor.");
                        }
                        else
                        {
                            string classPart = row.Substring(5).Trim();

                            if (classPart.Substring(0, className.Length) != className)
                            {
                                Debug.Log("hata");
                                GameOver("class adýný " + className + " yapmanýz gerekiyor.");
                            }
                            else
                            {
                                string parentClassPart = classPart.Substring(className.Length).Trim();
                                if (parentClassPart[0] != '(')
                                {
                                    Debug.Log("hata");
                                    GameOver("Parantez hatasý: Bir class'ý oluþtururken parantezlerini doðru þekilde kullanmanýz gerekiyor. Örnekler: SimpleCharacter(Character) veya Character()");
                                }
                                else
                                {
                                    parentClassPart = parentClassPart.Substring(1).Trim();
                                    if (parentClassPart.Substring(0, parentClassName.Length) != parentClassName)
                                    {
                                        Debug.Log("hata");
                                        GameOver("Oluþturacaðýnýz class'ýn parent class'ýný parantez içine yazmanýz gerekiyor. Örnek: SimpleCharacter(Character)");
                                    }
                                    else
                                    {
                                        parentClassPart = parentClassPart.Substring(parentClassName.Length).Replace(" ", "");
                                        if (parentClassPart.Length > 2)
                                        {
                                            Debug.Log("hata");
                                            GameOver("class oluþtururken parantezi doðru yerde kapattýðýnýzdan ve sonrasýnda sadece : koyduðunuzdan emin olun.");
                                        }
                                        else if (parentClassPart[0] != ')')
                                        {
                                            Debug.Log("hata");
                                            GameOver("Parantez hatasý: Bir class'ý oluþtururken parantezlerini doðru þekilde kullanmanýz gerekiyor. Örnekler: SimpleCharacter(Character) veya Character()");
                                        }
                                        else if (parentClassPart[1] != ':')
                                        {
                                            Debug.Log("hata");
                                            GameOver("class oluþtururken komutun sonuna : eklemeniz gerekiyor.");
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
                        GameOver("Indentation hatasý: Komutun önüne class constructor komutunun önüne koyduðunuzdan " + n + " fazla boþluk koyarak yazmanýz gerekiyor.");
                    }



                    //string leftTrimmedRow = rows2[i].Substring(rowIndentation);
                    string trimmedRow = rows2[i].Trim();
                    Debug.Log(rows2[i]);
                    string[] rowWords;
                    //string[] rowWords = leftTrimmedRow.Split(" ");


                    //en az gerekli karakter sayýsý = 16
                    //BUNLARI DEÐÝÞTÝREBÝLÝRÝM. DÝREKT HATALI DEMEK SAÇMA.

                    //try
                    //{
                    if (trimmedRow.Substring(0, 3) != "def")
                    {
                        Debug.Log("Hata");
                        GameOver("init komutunu yazmak için önce def kelimesini yazmanýz gerekiyor.");
                    }
                    else
                    {
                        rowWords = trimmedRow.Split(" ", 2);

                        string initWord = rowWords[1];
                        Debug.Log(initWord);

                        if (initWord.Substring(0, 8) != "__init__")
                        {
                            Debug.Log("Hata");
                            GameOver("init komutu için def yazdýktan sonra boþluk býrakýp __init__ yazarak devam etmeniz gerekiyor.");
                        }
                        else
                        {
                            string initWordParametersPart = initWord.Substring(8).Trim();
                            Debug.Log(initWordParametersPart);
                            if (initWordParametersPart[0] != '(')
                            {
                                Debug.Log("hata");
                                GameOver("Parantez hatasý: __init__ yazdýktan sonra parantez içinde gerekli parametreleri girmeniz gerekiyor.");
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
                                    GameOver("Parantez hatasý: __init__ yazdýktan sonra parantez içinde gerekli parametreleri girmeniz gerekiyor.");
                                }
                                else
                                {
                                    initWordParametersPart = initWordParametersPart.Substring(0, initWordParametersPart.Length - 1).Trim();
                                    Debug.Log(initWordParametersPart);
                                    initParameters = initWordParametersPart.Split(",");
                                    for (int j = 0; j < initParameters.Length; j++)
                                    {
                                        initParameters[j] = initParameters[j].Trim();
                                        Debug.Log(initParameters[j]);
                                        if (!VariableCheck(initParameters[j]))
                                        {
                                            Debug.Log("hata");
                                            GameOver("init içerisine yazdýðýnýz parametre deðiþken yazýmý kurallarýna uymalýdýr. Hatalý parametre: " + initParameters[j]);
                                        }
                                    }
                                }
                            }

                        }

                    }

                    //Burada parametrelere ulastigim icin burada kontrol ediyorum. Yukarida da edebilirim.
                    if (!initParameters.Contains("shape"))
                    {
                        GameOver("init parametrelerinden biri shape olmalýdýr.");
                    }
                    else if (!initParameters.Contains("color"))
                    {
                        GameOver("init parametrelerinden biri color olmalýdýr.");
                    }

                    //self yerine farkli kelimeler kullanilabilir.
                    selfKeyword = initParameters[0];

                    Debug.Log("burasý");
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
                        GameOver("Indentation hatasý: Komutun önüne init komutunun önüne koyduðunuzdan " + n + " fazla boþluk koyarak yazmanýz gerekiyor.");
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
                                GameOver("Parametreleri atadýðýnýz satýrda atama için = kullanmanýz gerekiyor.");
                            }
                            else
                            {
                                string[] assignmentParts = trimmedRow.Split('=');
                                string leftPart = assignmentParts[0].Trim();
                                string rightPart = assignmentParts[1].Trim();

                                if (leftPart.Substring(0, selfKeyword.Length) != selfKeyword)
                                {
                                    Debug.Log("hata");
                                    GameOver("Atama satýrýnda belirlediðiniz self keyword'ünü kullanmanýz gerekiyor. Belirlediðiniz self keyword: " + selfKeyword);
                                }
                                else
                                {
                                    string leftPartAfterSelfWord = leftPart.Substring(selfKeyword.Length).Trim();
                                    if (leftPartAfterSelfWord[0] != '.')
                                    {
                                        Debug.Log("hata");
                                        GameOver("Atama satýrýnda belirlediðiniz self keyword'den sonra . kullanmanýz gerekiyor. Belirlediðiniz self keyword" + selfKeyword);
                                    }
                                    else
                                    {
                                        leftPartAfterSelfWord = leftPartAfterSelfWord.Substring(1).Trim();
                                        Debug.Log(leftPartAfterSelfWord);

                                        Debug.Log(rightPart);

                                        if (!initParameters.Contains(rightPart))
                                        {
                                            Debug.Log("hata");
                                            GameOver("Atama satýrýnda sað tarafa yazdýðýnýz kelime init parametreleri arasýnda bulunmalýdýr.");
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
                Debug.Log("atama sayýlarý yetersiz");
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
            //Baþka class varsa burasý kullanýlacak. Yoksa bazý kýsýmlarý kullanýlmayacak.
            for (int i = 0; i < rows1.Length; i++)
            {
                //ayrýca null kontrolü de gerekebilir.
                if (String.IsNullOrEmpty(rows1[i]))
                    continue;

                int rowIndentation;
                //Debug.Log(rows2[i]);
                //bos satirlar yok sayiliyor.
                //BURADA UZUNLUÐU 1'SE DÝYE KONTROL ETMEK BELKÝ DAHA ÝYÝ OLUR AMA UZUN BÝR BOÞLUK DA BIRAKABÝLÝRLER. BÝR ÞEKÝLDE HALLEDÝCEZ.
                if (!String.IsNullOrEmpty(rows1[i]))
                {
                    //if (rows1[i].Length >= 4)
                    //{
                    //birden fazla import satýrý istenmediði için bir tane olacak þekilde ayarlandý. 
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
                                GameOver("Import komutunun ilk kelimesi from olmalýdýr.");
                            }
                            else
                            {
                                row = row.Substring(4).Trim();
                                if (row.Substring(0, secondClassFileName.Length) != secondClassFileName)
                                {
                                    Debug.Log("hata");
                                    GameOver("Import komutunda class'ýn olduðu dosyanýn adýný yazmanýz gerekiyor. " + className + " class'ý için dosya adý: " + secondClassFileName);
                                }
                                else
                                {
                                    row = row.Substring(secondClassFileName.Length).Trim();
                                    if (row.Substring(0, 6) != "import")
                                    {
                                        Debug.Log("hata");
                                        GameOver("Import komutununda class'ýn bulunduðu dosyadan sonra import yazmanýz gerekiyor.");
                                    }
                                    else
                                    {
                                        row = row.Substring(6).Trim();
                                        if (row != className)
                                        {
                                            Debug.Log("hata");
                                            GameOver("Import komutunda class'ýn adýný yazmanýz gerekiyor. class adý: " + className);
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
                            GameOver("Indentation hatasý: Komutun önüne import komutunun önüne koyduðunuz kadar boþluk koyarak yazmanýz gerekiyor.");
                            Debug.Log("Indentation hatasi");
                        }


                        //string leftTrimmedRow = rows2[i].Substring(rowIndentation);
                        string trimmedRow = rows1[i].Trim();

                        string[] classInstanceParts;
                        if (!trimmedRow.Contains("="))
                        {
                            GameOver("Bir " + className + " nesnesi oluþturmanýz gerekiyor.");
                            Debug.Log("Bir Character nesnesi oluþturmanýz gerekiyor.");
                        }
                        else
                        {

                            classInstanceParts = trimmedRow.Split('=');


                            string classInstanceName = classInstanceParts[0].Trim();
                            string classConstructor = classInstanceParts[1].Trim();

                            if (!VariableCheck(classInstanceName))
                            {
                                GameOver("Oluþturmak istediðiniz nesnenin adý deðiþken ismi kurallarýna uygun olmalýdýr.");
                                Debug.Log("Class instance name:" + classInstanceName + "+");
                                Debug.Log("hata");
                            }
                            //else if(classInstanceRightPart.Contains("="))
                            //{
                            //    Debug.Log("Hata");
                            //}
                            else if (!classConstructor.Contains("(") || !classConstructor.Contains(")"))
                            {
                                GameOver(className + " nesnesi oluþtururken parantezleri doðru kullanmanýz gerekiyor.\nÖrnek: " + className + "(\"blue\")");
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
                                        GameOver("Parantez hatasý: class nesnesi oluþtururken parantezi kapatmanýz gerekiyor.");
                                    }
                                    else
                                    {
                                        classParametersPart = classParametersPart.Substring(0, classParametersPart.Length - 1);

                                        string[] classParameters = classParametersPart.Split(',');

                                        if (classParameters.Length != initParameters.Length - 1)
                                        {
                                            Debug.Log("hata");
                                            GameOver("self keyword olmadan init komutuna yazdýðýnýz parametrelere sýrasýyla uygun deðerler yazmanýz gerekiyor.");
                                        }
                                        else
                                        {
                                            for (int j = 1; j < initParameters.Length; j++)
                                            {
                                                //classParameters[j].Trim();
                                                string parameter = classParameters[j - 1].Trim();
                                                if (initParameters[j] == "shape")
                                                {
                                                    if (parameter.Trim() == "circle")
                                                    {

                                                    }
                                                    else if (parameter == "square")
                                                    {

                                                    }
                                                    else
                                                    {
                                                        Debug.Log("hata");
                                                        GameOver("shape parametresine uygun bir þekil yazmanýz gerekiyor. Uygun þekiller: circle, square");
                                                    }
                                                }
                                                else if (initParameters[j] == "color")
                                                {

                                                    //Color.FromName(parameter);
                                                    if (parameter == "red")
                                                    {

                                                    }
                                                    else if (parameter == "green")
                                                    {

                                                    }
                                                    else if (parameter == "blue")
                                                    {

                                                    }
                                                    else if (parameter == "yellow")
                                                    {

                                                    }
                                                    else
                                                    {
                                                        Debug.Log("hata");
                                                        GameOver("color parametresine uygun bir renk yazmanýz gerekiyor. Uygun renkler: red, green, blue, yellow");
                                                    }

                                                }

                                            }
                                        }



                                    }

                                    //string[] classParameters = classConstructorParts[1].Split(',');



                                    //constructor part'larinda normalde string, integer veya double vs. deger olabilir.
                                    //ben buyuk ihtimalle sadece string olan classlar kullanacagim. yine de digerleri de implement edilebilir.
                                    //python oldugu icin init kisminda parametrelerin tipleri belli olmaz. burada kontrol etmek lazim.

                                    //bölümlere göre farklý kontroller olabilir.



                                }
                            }
                        }

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
                            GameOver("Indentation hatasý: Komutun baþýnda býraktýðýnýz boþluk miktarýný kontrol etmeniz gerekiyor.");
                            Debug.Log("indentation hatasý");
                        }


                        Debug.Log("rowindentaiton " + rowIndentation);
                        Debug.Log("indentaion " + indentation);


                        instructionLevel = ((rowIndentation - indentation) / n) + 1;



                        if (lastInstructionType == "holder")
                        {
                            if (rowIndentation != lastIndentation + n)
                            {
                                GameOver("Indentation hatasý: Komutun baþýnda býraktýðýnýz boþluk miktarýný kontrol etmeniz gerekiyor.");
                                Debug.Log("indentation hatasý2");
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
                                        GameOver("if komutu yazarken if yazdýktan sonra boþluk býrakmanýz gerekiyor.");
                                        Debug.Log("boþluk olmalý hata");
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
                                                    GameOver("Bu problemde if komutunda " + className + "'in metodlarýný kullanarak kontrol yapmanýz gerekiyor.\nÖrnek: if " + className + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (ifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        Debug.Log("hata");
                                                        GameOver("Parantez hatasý: up_tile metoduna ulaþmak için up_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: down_tile metoduna ulaþmak için down_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: right_tile metoduna ulaþmak için right_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: left_tile metoduna ulaþmak için left_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: is_ground metoduna ulaþmak için is_ground() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: is_water metoduna ulaþmak için is_water() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: contains metoduna ulaþmak için örnek olarak contains(apple) yazmanýz gerekiyor.");
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
                                    GameOver("for komutunda for'dan sonra boþluk býrakmanýz gerekiyor.");
                                    Debug.Log("hata");

                                }
                                else if (!trimmedRow.Contains("in"))
                                {
                                    GameOver("Bu problemde for komutunda in kelimesi kullanmanýz gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else if (trimmedRow[trimmedRow.IndexOf("in") - 1] != ' ')
                                {
                                    GameOver("for komutunda in'den önce boþluk býrakmanýz gerekiyor.");
                                    Debug.Log("hata");
                                }
                                else if (trimmedRow[trimmedRow.IndexOf("in") + 2] != ' ')
                                {
                                    GameOver("for komutunda in'den sonra boþluk býrakmanýz gerekiyor.");
                                    Debug.Log("hata");
                                }

                                else
                                {
                                    //burada sanýrým trim kullanmak gerekiyor ve sonrasýnda kelimenin içinde boþluk var mý diye bakmak gerekiyor
                                    string var = trimmedRow.Substring(3, trimmedRow.IndexOf("in") - 3).Trim();
                                    Debug.Log("var = " + var);
                                    //
                                    if (!VariableCheck(var))
                                    {
                                        GameOver("Deðiþken hatalý:" + var);
                                        Debug.Log("hata");
                                    }
                                    else if (!trimmedRow.Contains("range"))
                                    {
                                        GameOver("Bu problemde for komutunda range kullanmanýz/yazmanýz gerekiyor.");
                                        Debug.Log("hata");
                                    }
                                    else
                                    {
                                        string rangeParameter = trimmedRow.Substring(trimmedRow.IndexOf("range") + 5).Replace(" ", "");
                                        Debug.Log("rangeParameter1 = " + rangeParameter);

                                        if (rangeParameter[0] != '(')
                                        {
                                            GameOver("Parantez hatasý: for komutunda range kelimesinden sonra parantez içinde bir deðer girmeniz gerekiyor.");
                                            Debug.Log("hata");
                                        }

                                        else if (rangeParameter[rangeParameter.Length - 2] != ')')
                                        {
                                            GameOver("Parantez hatasý: for komutunda range kelimesinden sonra parantez içinde bir deðer girmeniz gerekiyor.");
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
                                                //range içindeki deðere ulaþabiliyorum artýk.

                                                //Bunu doðru yere eklemek gerekiyor.
                                                For forLoop = new For(this, characterMovement, parameter, instructionLevel);


                                                //indentation kontrol lazým.
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
                                                                //burada holder olduklarýndan emin olmak lazým. Yani üsttekinden emin olmak lazým. 
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
                                    GameOver("Parantez hatasý: parametresiz methodlar sonlarýna () eklenerek, parametreli methodlar eklenen parantezin içine parametre deðerleri yazýlarak çaðrýlýr.");

                                    Debug.Log("hata");
                                }
                                else if (!trimmedRow.Contains(')'))
                                {
                                    GameOver("Parantez hatasý: parametresiz methodlar sonlarýna () eklenerek, parametreli methodlar eklenen parantezin içine parametre deðerleri yazýlarak çaðrýlýr.");
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
                                        GameOver("Method tanýmýný doðru yazmanýz gerekiyor.");
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
                                    GameOver("Parantez hatasý: parametresiz methodlar sonlarýna () eklenerek, parametreli methodlar eklenen parantezin içine parametre deðerleri yazýlarak çaðrýlýr.");
                                    Debug.Log("hata");
                                }
                                else if (!trimmedRow.Contains(')'))
                                {
                                    GameOver("Parantez hatasý: parametresiz methodlar sonlarýna () eklenerek, parametreli methodlar eklenen parantezin içine parametre deðerleri yazýlarak çaðrýlýr.");
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
                                        GameOver("Method tanýmýný doðru yazmanýz gerekiyor.");
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
                                        GameOver("elif komutu yazarken elif yazdýktan sonra boþluk býrakmanýz gerekiyor.");
                                        Debug.Log("boþluk olmalý hata");
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
                                                    GameOver("Bu problemde elif komutunda " + className + "'in metodlarýný kullanarak kontrol yapmanýz gerekiyor.\nÖrnek: elif " + className + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (elifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatasý: up_tile metoduna ulaþmak için up_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: down_tile metoduna ulaþmak için down_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: right_tile metoduna ulaþmak için right_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: left_tile metoduna ulaþmak için left_tile() yazmanýz gerekiyor.");
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
                                                            GameOver("Parantez hatasý: contains metoduna ulaþmak için örnek olarak contains(apple) yazmanýz gerekiyor.");
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
                                        GameOver("while komutu yazarken while yazdýktan sonra boþluk býrakmanýz gerekiyor.");
                                        Debug.Log("boþluk olmalý hata");
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
                                                    GameOver("Bu problemde while komutunda " + className + "'in metodlarýný kullanarak kontrol yapmanýz gerekiyor.\nÖrnek: while " + className + ".up_tile().is_ground()");
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (whileParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = whileParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        GameOver("Parantez hatasý: up_tile metoduna ulaþmak için up_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: down_tile metoduna ulaþmak için down_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: right_tile metoduna ulaþmak için right_tile() yazmanýz gerekiyor.");
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
                                                        GameOver("Parantez hatasý: left_tile metoduna ulaþmak için left_tile() yazmanýz gerekiyor.");
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
                                                            GameOver("Parantez hatasý: contains metoduna ulaþmak için örnek olarak contains(apple) yazmanýz gerekiyor.");
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
                                    GameOver("else komutu else kelimesi ve : haricinde sadece boþluklar içerebilir.");
                                    Debug.Log("hata");
                                }

                                else if (trimmedRow[4] != ':')
                                {
                                    GameOver("else komutunun sonuna : eklemeniz gerekiyor.");
                                    Debug.Log("hata");
                                }

                                else
                                {
                                    //AddInstruction() içinde bunun üstüdneki ConditionHolder mý diye kontrol etmek gerek.
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
            //    Debug.Log("Ýlk komutun bir for olmasý gerekiyor.");
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
            Debug.Log("Ýlk komutun bir for olmasý gerekiyor.");
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
                    Debug.Log("Ýlk komutun bir for olmasý gerekiyor.");
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


    //SORUN BURADA HERHALDE. Forun içine eklemiyor moveu.
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
                //    Debug.Log("Burasý 2:" + instruction.level);

                instructionList.Add(instruction);


                return;
            }
            else
            {
                //if (instructionList.Count > 0)
                //{
                //burada is instance olacak sanýrým
                //instructionList[instructionList.Count - 1].GetType() == typeof(HolderInstruction)
                //Uzun runtime buradan kaynaklanýyor.
                //instructionList[instructionList.Count - 1] is HolderInstruction
                Debug.Log(instructionList.Count);
                Debug.Log(instructionList[instructionList.Count - 1].GetType().IsSubclassOf(typeof(HolderInstruction)));
                if (instructionList[instructionList.Count - 1].GetType().IsSubclassOf(typeof(HolderInstruction)))
                {
                    Debug.Log("holderxxxx");
                    //burada son listin sonundaki instructionýn listi alýnýyor
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
            Regex regex = new Regex(@"^[a-zA-Z0-9_]*$");
            Match match = regex.Match(var);
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
        return true;
    }

}

