
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.RegularExpressions;

using TMPro;

using UnityEngine;

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

//sadece sayýyla dönen for loop. boolean olacaksa ayrý class oluþturulabilir.
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
        //iterasyon sayýsýný ayarlamak lazým.
        Debug.Log("iteration" + iterationCount);

        for (int i = 0; i < iterationCount; i++)
        {
            //elif ve else'lerden once ilk komutun if olmasi gerek.
            //sirasiyla bakilip calisandan sonrakiler calismamali.
            //bool didItRun = false;
            bool isThereIf = false;
            bool didPreviousConditionsRun = false;

            RunCodeButton.RunInstructions(instructions);
            

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
        RunCodeButton.RunInstructions(instructions);
        
    }
}

public class Elif : HolderInstruction
{
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
        RunCodeButton.RunInstructions(instructions);
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
        RunCodeButton.RunInstructions(instructions);
        
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

    [Header("Category and Level")]
    public int catNumber;
    public int levelNumber;


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

    // Start is called before the first frame update
    void Start()
    {
        instructionList = new List<Instruction>();
        //ReadInputPage1(inputPage1);
        characterMovement = character.GetComponent<CharacterMovementController>();
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
        //characterMovement.MoveRight();
        //List<Instruction> instructions = new List<Instruction>();

        //bu kýsým çalýþabiliyor.
        //Move moveLeft = new Move(characterMovement, "right" , 1);

        //instructionList.Add(moveLeft);
        //instructionList[0].Run();

        //characterMovement.MoveRight();
        //characterMovement.MoveRight();

        //bunlar silinmeyecek
        //List<List<string>> rows1 = new List<List<string>>();
        //List<List<string>> rows2 = new List<List<string>>();

        string inputText1 = inputField1.text;
        string inputText2 = inputField2.text;

        string[] rows1 = inputText1.Split("\n");
        string[] rows2 = inputText2.Split("\n");

        //indentation buyuklugu
        int n = 2;


        string className = null;
        string characterColor;
        string secondClassFileName = "Character";

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

                //Debug.Log(rows2[i]);
                //bos satirlar yok sayiliyor.
                //if (rows2[i] != "\n")
                //{

                //aslinda burada kelimeleri almis oluyoruz. indentation kontrolu yapariz sadece. ??????
                //string[] words = rows2[i].Split(" ");
                if (isFirstRow)
                {

                    int c = 0;
                    while (rows2[i][c] == ' ')
                    {
                        c++;
                    }
                    indentation = c;

                    string row = rows2[i].Replace(" ", "");

                    if (row.Length < 7)
                    {
                        Debug.Log("hata");
                    }
                    else
                    {
                        Debug.Log(row.Length + " " + row);
                        string word = row.Substring(5, row.Length - 5 - 1);
                        if (row.Substring(0, 5) != "class")
                        {
                            Debug.Log(row.Substring(0, 5));
                            Debug.Log("hata");
                        }
                        else if (row[row.Length - 1] != ':')
                        {
                            Debug.Log(row[row.Length - 1]);
                            Debug.Log("hata");
                        }
                        else if (word.Contains(":"))
                        {
                            Debug.Log("hata");
                        }
                        else if (word.Contains("(") || word.Contains(")"))
                        {
                            Debug.Log("hata");
                        }
                        else
                        {
                            className = word;
                            Debug.Log("classname1=" + className);
                        }
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
                    }



                    //string leftTrimmedRow = rows2[i].Substring(rowIndentation);
                    string trimmedRow = rows2[i].Trim();
                    Debug.Log(rows2[i]);
                    string[] rowWords;
                    //string[] rowWords = leftTrimmedRow.Split(" ");


                    //en az gerekli karakter sayýsý = 16
                    //BUNLARI DEÐÝÞTÝREBÝLÝRÝM. DÝREKT HATALI DEMEK SAÇMA.
                    if (trimmedRow.Length < 16)
                    {
                        Debug.Log("hata");
                    }
                    else if (trimmedRow.Substring(0, 3) != "def")
                    {
                        Debug.Log("Hata");

                    }
                    else
                    {
                        rowWords = trimmedRow.Split(" ", 2);


                        // burada class mý, sonu ':' mý vs. kontrol edilecek.
                        //string lastWord = rowWords[rowWords.Length - 1];

                        string initWord = rowWords[1];
                        Debug.Log(initWord);

                        //burada hata var. ( 6 da olmak zorunda degil.


                        //if (initWord.Length >= 8)
                        //{
                        //once ( ) buna gore bolerim sonra icinde baska ( ) var mý bakarým.
                        //Debug.Log(initWord.Length);
                        //initWord = initWord.TrimEnd();

                        if (initWord.Substring(0, 8) != "__init__")
                        {

                            Debug.Log("Hata");

                        }
                        else
                        {
                            //Bu kýsýmda boþluklarýn önemi olmadýðý için boþluksuz kelime elde ediliyor.
                            initWord = initWord.Replace(" ", "");
                            //Debug.Log(str);
                            //if (initWord.Length >= 11)
                            //{
                            Debug.Log(initWord[8]);
                            Debug.Log(initWord[initWord.Length - 2]);

                            //indexler doðru mu??
                            if (initWord[8] != '(' || initWord[initWord.Length - 2] != ')')
                            {
                                Debug.Log("hata");
                            }
                            else if (initWord[initWord.Length - 1] != ':')
                            {
                                Debug.Log("hata");
                            }
                            //12 cikarmanin sebebi sondaki ekstra karakter
                            else if (initWord.Substring(9, initWord.Length - 11).Contains("(") || initWord.Substring(9, initWord.Length - 11).Contains(")"))
                            {
                                Debug.Log("Fazla parantez");
                            }
                            else if (initWord.Substring(9, initWord.Length - 11).Contains(":"))
                            {
                                Debug.Log("yanlýþ yerde : ");
                            }
                            else
                            {
                                string initParametersString = initWord.Substring(9, initWord.Length - 11);
                                initParameters = initParametersString.Split(",");
                            }


                        }

                        //}



                        //burada className'i olmasi gerekenle karsilastir.
                        //kodda init olup olmamasina gore kodlarin kontrol edilmesi gerekiyor. ikisi icin if else yazilabilir.



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



                    int c = 0;
                    while (rows2[i][c] == ' ')
                    {
                        c++;
                    }
                    rowIndentation = c;

                    if (rowIndentation != indentation + 2 * n)
                    {
                        Debug.Log("hATA");
                    }

                    else
                    {
                        //string leftTrimmedRow = rows2[i].Substring(rowIndentation);
                        string row = rows2[i].Replace(" ", "");
                        if (row.Length < 5)
                        {
                            Debug.Log("hata");
                        }
                        else
                        {
                            //Debug.Log("hata");
                            //burasý da deðiþecek...
                            //if (row.Substring(0, 4) != "self")
                            //{
                            //    Debug.Log("hata");
                            //}
                            //else
                            if (!row.Contains("="))
                            {
                                Debug.Log("hata");
                            }
                            else
                            {
                                string[] assignmentParts = row.Split('=');

                                if (assignmentParts.Length > 2)
                                {
                                    Debug.Log("birden fazla = var");
                                }
                                else if (!assignmentParts[0].Contains("."))
                                {
                                    Debug.Log("hata");
                                }
                                else
                                {
                                    string[] leftSide = assignmentParts[0].Split('.');
                                    if (leftSide.Length > 2)
                                    {
                                        Debug.Log("birden fazla . var");
                                    }
                                    //Burasý deðiþecek çünkü kelimenin self olmasý gerekmiyor. 
                                    //Bununla birlikte uzunluk kontrolü de deðiþecek. Çünkü self olmasýna göre yapýldý ama 1 karakterlik bir þey de olabilir. 
                                    //else if (leftSide[0] != "self")
                                    //{
                                    //  Debug.Log("hata");
                                    //}
                                    else
                                    {
                                        if (leftSide[0] != selfKeyword)
                                        {
                                            Debug.Log("keyword doðru deðil");
                                        }
                                        else
                                        {
                                            //string selfWord = leftSide[0];
                                            //string rightSideWord = assignmentParts[1].Substring(0, assignmentParts[1].Length - 1);
                                            string rightSideWord = assignmentParts[1].Replace("\n", "");
                                            Debug.Log(rightSideWord);
                                            Debug.Log(leftSide[1]);
                                            if (leftSide[1] != rightSideWord)
                                            {
                                                Debug.Log(rightSideWord + " " + rightSideWord.Length);
                                                Debug.Log(leftSide[1] + " " + leftSide[1].Length);
                                                for (int m = 0; m < rightSideWord.Length; m++)
                                                {
                                                    Debug.Log((int)rightSideWord[m]);
                                                }
                                                Debug.Log("kelimeler ayný degil");
                                            }
                                            else
                                            {
                                                //burada kaydetmeye baþlanabilir
                                                //init row dakilerle de ayrýca karþýlaþtýrmak gerekiyor.
                                                Debug.Log(leftSide[1] + "," + rightSideWord);

                                                initAssignments.Add(leftSide[1]);

                                            }
                                        }

                                    }

                                }
                                //init icindeki parametreler burada sirasiz yazilabiliyor. ama nesne olusuturulurken sirayla degerlerin girilmesi lazim ve self yazilmamasi lazim.

                            }

                        }

                    }
                }

                //}

            }

            if (initAssignments.Count != initParameters.Length - 1)
            {
                Debug.Log("atama sayýlarý yetersiz");
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
                        string row = rows1[i].Replace(" ", "");

                        if (rows1[i].Length < 10 + secondClassFileName.Length + className.Length)
                        {
                            Debug.Log("hata");

                        }
                        else
                        {

                            if (row.Substring(0, 4) == "from")
                            {
                                if (row.Substring(4, secondClassFileName.Length) != secondClassFileName)
                                {
                                    Debug.Log(row.Substring(4, secondClassFileName.Length));
                                    Debug.Log("hata");
                                }
                                else if (row.Substring(4 + secondClassFileName.Length, 6) != "import")
                                {
                                    Debug.Log("hata");
                                }
                                else if (row.Substring(10 + secondClassFileName.Length, row.Length - (10 + secondClassFileName.Length)) != className)
                                {
                                    Debug.Log(row.Substring(10 + secondClassFileName.Length, row.Length - (10 + secondClassFileName.Length)));
                                    Debug.Log("hata");
                                }

                            }


                        }

                        isFirstRow = false;
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
                            Debug.Log("indentation hatasý");
                        }


                        Debug.Log("rowindentaiton " + rowIndentation);
                        Debug.Log("indentaion " + indentation);


                        instructionLevel = ((rowIndentation - indentation) / n) + 1;



                        if (lastInstructionType == "holder")
                        {
                            if (rowIndentation != lastIndentation + n)
                            {
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
                                    Debug.Log("hata");
                                }
                                else
                                {
                                    if (trimmedRow[trimmedRow.IndexOf("if") + 2] != ' ')
                                    {
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
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (ifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = ifParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        Debug.Log("hata");
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
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";
                                                        //If ifInstruction = new If(characterMovement, firstMethod, instructionLevel);
                                                        //AddInstruction(ifInstruction);
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.Log("simdilik hata");
                                                }

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
                                                        Debug.Log("hata");
                                                    }
                                                    else if (parameterPart != "():")
                                                    {
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
                                                        Debug.Log("hata");
                                                    }
                                                    else if (parameterPart != "():")
                                                    {
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
                                                        Debug.Log("hata");
                                                    }
                                                    parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);
                                                    //hata cikarsa zaten program duracak
                                                    if (parameterPart[0] != '(' || parameterPart[parameterPart.Length - 2] != ')')
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {

                                                        if (!fruits.Contains(parameterPart))
                                                        {
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
                                            // == varsa
                                            //buraya farkli bir sey koymak lazim. 
                                            else
                                            {





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
                                    Debug.Log("hata");

                                }
                                else if (!trimmedRow.Contains("in"))
                                {
                                    Debug.Log("hata");
                                }
                                else if (trimmedRow[trimmedRow.IndexOf("in") - 1] != ' ')
                                {
                                    Debug.Log("hata");
                                }
                                else if (trimmedRow[trimmedRow.IndexOf("in") + 2] != ' ')
                                {
                                    Debug.Log("hata");
                                }

                                else
                                {
                                    //burada sanýrým trim kullanmak gerekiyor ve sonrasýnda kelimenin içinde boþluk var mý diye bakmak gerekiyor
                                    string var = trimmedRow.Substring(3, trimmedRow.IndexOf("in") - 3).Trim();
                                    Debug.Log("var = " + var);
                                    //
                                    if (pythonReservedWords.Contains(var))
                                    {
                                        Debug.Log("hata");
                                    }
                                    else if (!trimmedRow.Contains("range"))
                                    {
                                        Debug.Log("hata");
                                    }
                                    else
                                    {
                                        string rangeParameter = trimmedRow.Substring(trimmedRow.IndexOf("range") + 5).Replace(" ", "");
                                        Debug.Log("rangeParameter1 = " + rangeParameter);

                                        if (rangeParameter[0] != '(')
                                            Debug.Log("hata");
                                        else if (rangeParameter[rangeParameter.Length - 2] != ')')
                                        {
                                            Debug.Log("hata");
                                        }
                                        else if (rangeParameter[rangeParameter.Length - 1] != ':')
                                        {
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
                                                For forLoop = new For(characterMovement, parameter, instructionLevel);


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
                                    Debug.Log("hata");
                                }
                                else if (!trimmedRow.Contains(')'))
                                {
                                    Debug.Log("hata2");
                                }

                                string[] instructionParts = trimmedRow.Split('(', 2);

                                instructionParts[0] = instructionParts[0].Trim();
                                int length = instructionParts[0].Length;
                                //if (instructionParts[0].Length<6 || instructionParts[0].Length > 9)
                                //{
                                //    Debug.Log("hata");
                                //}else if()

                                //if (length < 6 || length > 9)
                                //{
                                //    Debug.Log("hata");
                                //}
                                //else if (length == 6)
                                //{
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
                                        Debug.Log("hata");
                                    }
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    Debug.Log(ex.Message);
                                }


                            }
                            else if (trimmedRow.Substring(0, 4) == "swim")
                            {
                                if (!trimmedRow.Contains('('))
                                {
                                    Debug.Log("hata");
                                }
                                else if (!trimmedRow.Contains(')'))
                                {
                                    Debug.Log("hata2");
                                }

                                string[] instructionParts = trimmedRow.Split('(', 2);

                                instructionParts[0] = instructionParts[0].Trim();
                                int length = instructionParts[0].Length;
                                //if (instructionParts[0].Length<6 || instructionParts[0].Length > 9)
                                //{
                                //    Debug.Log("hata");
                                //}else if()

                                //if (length < 6 || length > 9)
                                //{
                                //    Debug.Log("hata");
                                //}
                                //else if (length == 6)
                                //{
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
                                        Debug.Log("hata");
                                    }
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    Debug.Log(ex.Message);
                                }


                            }
                            else if (trimmedRow.Substring(0, 4) == "elif")
                            {
                                if (trimmedRow[trimmedRow.Length - 1] != ':')
                                {
                                    Debug.Log("hata");
                                }
                                else
                                {
                                    if (trimmedRow[trimmedRow.IndexOf("elif") + 4] != ' ')
                                    {
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
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;

                                                if (elifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(7).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "up_tile";
                                                        Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        AddInstruction(elifInstruction);
                                                    }

                                                }
                                                else if (elifParts[1].Substring(0, 9) == "down_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "down_tile";
                                                        Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        AddInstruction(elifInstruction);
                                                    }
                                                }
                                                else if (elifParts[1].Substring(0, 10) == "right_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(10).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "right_tile";
                                                        Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        AddInstruction(elifInstruction);
                                                    }
                                                }
                                                else if (elifParts[1].Substring(0, 9) == "left_tile")
                                                {
                                                    parameterPart = elifParts[1].Substring(9).Replace(" ", "");
                                                    if (parameterPart != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";
                                                        Elif elifInstruction = new Elif(characterMovement, firstMethod, instructionLevel);
                                                        AddInstruction(elifInstruction);
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.Log("simdilik hata");
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
                                                            Debug.Log("hata");
                                                        }
                                                        parameterPart = parameterPart.Substring(0, parameterPart.Length - 1);
                                                        //hata cikarsa zaten program duracak
                                                        if (parameterPart[0] != '(' || parameterPart[parameterPart.Length - 2] != ')')
                                                        {
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            if (!fruits.Contains(parameterPart))
                                                            {
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
                                            // == varsa
                                            //buraya farkli bir sey koymak lazim. 
                                            else
                                            {





                                            }

                                        }
                                    }

                                }
                            }

                            else if (trimmedRow.Substring(0, 4) == "else")
                            {
                                trimmedRow = trimmedRow.Replace(" ", "");
                                if (trimmedRow.Length != 5)
                                    Debug.Log("hata");
                                else if (trimmedRow[4] != ':')
                                    Debug.Log("hata");
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


    public static void RunInstructions(List<Instruction> instructionList)
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
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
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
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
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
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
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
                            Vector3Int upTilePosition = ((If)instruction).characterMovement.groundTilemap.WorldToCell(((If)instruction).characterMovement.transform.position + (Vector3)direction);
                            //Oldu mu???
                            if (((If)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                                Vector3Int upTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((If)instruction).characterMovement.groundTilemap.HasTile(upTilePosition))
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
                                Vector3Int upTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                                Vector2 direction = new Vector2(1, 0);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int upTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
                                Vector2 direction = new Vector2(-1, 0);
                                //groundTilemap vs waterTilemap ???
                                Vector3Int upTilePosition = ((Elif)instruction).characterMovement.groundTilemap.WorldToCell(((Elif)instruction).characterMovement.transform.position + (Vector3)direction);
                                //Oldu mu???
                                if (((Elif)instruction).characterMovement.waterTilemap.HasTile(upTilePosition))
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
            else
            {
                isThereIf = false;
                instruction.Run();
            }

        }
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

