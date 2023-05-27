using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TMPro;
using Unity.Profiling.Editor;
using UnityEditor.Experimental.GraphView;
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
    private int iterationCount;
    //private List<Instruction> instructions;

    public For(int iterationCount, int level)
    {
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

            foreach (Instruction instruction in instructions)
            {
                Debug.Log(instruction.ToString());
                instruction.Run();
            }
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

//public class Import : Instruction
//{
//    public string importClass;

//    public Import(string importClass)
//    {
//        this.importClass = importClass;
//    }

//    public override void Run()
//    {
//        if(importClass != null)
//        {

//        }
//    }
//}

public class ConditionHolder : HolderInstruction
{
    //icine sade if elif ve else eklenecek.
    //public List<Condition> conditionals;

    public ConditionHolder(int level)
    {
        this.level = level;
    }

    public override void Run()
    {
        foreach (Condition condition in instructions)
        {
            //ayrýca bir tanesi çalýþtýðýnda diðerlerini çalýþtýrmamasý gerekiyor. Nasýl yapýlacak???
            if (condition.boolean)
                condition.Run();
        }
    }

    public void Add(Instruction condition)
    {
        instructions.Add((Condition)condition);
    }

    public override string ToString()
    {
        return "ConditionHolder Class";
    }
}

public abstract class Condition : HolderInstruction
{
    //if bloðunun içindekiler eklencek.
    //public List<Instruction> instructions;


    public bool boolean;
    public bool didRun;
    //public int id;
    //public List<bool> conditionRunList;

    //burada string olarak deðil, deðeri belirlenmiþ bool olarak da atanabilir. ekleme kýsmý ayarlanmalý.
    //public Condition(string type, string booleanString, int level)
    //{
    //    this.type = type;
    //    this.level = level;

    //    if (booleanString != null)
    //    {
    //        //burada boolean deðeri belirlenecek. kontrol yapýlacak yani. 
    //        //buraya eklenmeden mi burada mý boolean belirlense daha kolay olur? eklenmeden belki daha kolay olur çünkü diðer tarafta daha çok þeye eriþim var.

    //        //boolean = true;

    //        //boolean = false;
    //    }
    //}



    //public override void Run()
    //{
    //    foreach (Instruction instruction in instructions)
    //    {
    //        instruction.Run();
    //    }
    //}

    //public void Add(Instruction instruction)
    //{
    //    instructions.Add(instruction);
    //}

    //public override void Add(Instruction instruction)
    //{
    //    instructions.Add(instruction);
    //}

    public override string ToString()
    {
        return "Condition Class";
    }
}


public class If : Condition
{
    public List<string> leftPart;
    public string operatorType;
    public List<string> rightPart;


    public If(List<string> leftPart, int level)
    {

        this.leftPart = leftPart;

        //this.id = id;
        //this.conditionRunList = conditionRunList;
        this.level = level;


        //boolean burada hesaplanýrsa karakterin o anki güncel durumu deðil. en baþtaki durumuna göre hesap yapýlýr.
    }

    public If(List<string> leftPart, string operatorType, List<string> rightPart, int level)
    {

        this.leftPart = leftPart;
        this.operatorType = operatorType;
        this.rightPart = rightPart;
        //this.id = id;
        //this.conditionRunList = conditionRunList;
        this.level = level;


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
        if (operatorType != null)
        {
            //buralarda calisip calismadigini dondurmek gerekebilir. ya da run metodunda sadece calistirma yapilacak. run metodu cagrilmadan once if'in calisip calismadigi disarida kontrol edilecek.
        }
        else
        {

        }
        foreach (Instruction instruction in instructions)
        {
            Debug.Log(instruction.ToString());
            instruction.Run();
        }
    }
}

public class Elif : Condition
{
    public string variable;
    public string operatorType;
    public string value;


    public Elif(string variable, string operatorType, string value, int level)
    {

        this.variable = variable;
        this.operatorType = operatorType;
        this.value = value;
        //this.id = id;
        //this.conditionRunList = conditionRunList;
        this.level = level;

    }



    public override void Run()
    {
        throw new NotImplementedException();
    }
}

public class Else : Condition
{

    public Else(int level)
    {
        //this.id = id;
        //this.conditionRunList = conditionRunList;
        this.level = level;

    }



    public override void Run()
    {
        throw new NotImplementedException();
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



//class Node
//{
//    public string data;
//    public List<Node> childNodes;
//    public Node parent;
//    public int level;

//    public Node(string data, int level)
//    {
//        this.data = data;
//        this.childNodes = new List<Node>();
//        this.parent = null;
//        this.level = level;
//    }

//}

//class Tree
//{
//    public Node root;


//    public Tree()
//    {
//        root = new Node(null, 0);

//    }

//    //public Tree(Node root)
//    //{
//    //    this.root = root;
//    //}


//    //kod level'ýna gore tree'ye ekleniyor
//    public void Insert(Node node)
//    {
//        int level = node.level;
//        //if (root == null)

//        //else
//        //{
//        Node current = root;
//        Node parent;

//        while (true)
//        {
//            parent = current;
//            //Kodlarýn okuma sýrasýna uymasý için tree'de surekli en sondaki node secilerek ilerleniyor. 
//            //Node n = parent.childNodes[parent.childNodes.Count - 1];
//            //if (parent.childNodes.Count == 0)



//            if (parent.level == level - 1)
//            {
//                parent.childNodes.Add(node);
//                node.parent = parent;
//                return;
//            }

//            current = parent.childNodes[parent.childNodes.Count - 1];
//        }
//        //}

//    }

//    //eklemede sorun yok. dolaþmada daha düþük seviyeliyi okumuyor.
//    public void Traverse(Node root)
//    {

//        if (root != null)
//        {
//            for (int i = 0; i < root.childNodes.Count; i++)
//            {
//                if (root.childNodes[i] != null)
//                {
//                    Node current = root.childNodes[i];
//                    //Burada son geldiðimiz node okunuyor.
//                    Debug.Log(current.data);

//                    Traverse(current);
//                }

//            }
//        }


//    }
//}



public class RunCodeButton : MonoBehaviour
{

    public List<Instruction> instructionList;

    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    [SerializeField]
    public GameObject character;
    private CharacterMovementController characterMovement;
    private CharacterColorChanger characterColorChanger;

    private string[] initParameters = null;

    private string[] pythonReservedWords;
    //public CharacterMovementController characterMovementController;

    public List<bool> conditionRunList;

    // Start is called before the first frame update
    void Start()
    {
        instructionList = new List<Instruction>();
        //ReadInputPage1(inputPage1);
        characterMovement = character.GetComponent<CharacterMovementController>();
        characterColorChanger = character.GetComponent<CharacterColorChanger>();
        pythonReservedWords = new string[] { "def", "if", "else", "elif", "for", "while", "False", "True", "and", "as", "assert", "break", "class", "continue",
                                            "del",   "except", "finally",  "form", "global", "import", "in", "is", "lambda",
                                            "nonlocal", "not", "or", "pass", "raise", "return", "try",  "with", "yeld"};

        conditionRunList = new List<bool>();
        //characterColorChanger.ChangeColorToBlue();
        //characterColorChanger.ChangeColorToRed();
    }

    // Update is called once per frame
    void Update()
    {

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
            ConditionHolder conditionHolder = null;

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
                                        string operatorType = null;

                                        if (trimmedRow.Contains("=="))
                                            operatorType = "==";
                                        else if (trimmedRow.Contains("!="))
                                            operatorType = "!=";
                                        else if (trimmedRow.Contains("<"))
                                            operatorType = "<";
                                        else if (trimmedRow.Contains(">"))
                                            operatorType = ">";


                                        if (operatorType == null)
                                        {
                                            string[] ifParts = trimmedRow.Split('.');

                                            if (ifParts.Length == 2)
                                            {
                                                if (ifParts[0] != className)
                                                {
                                                    Debug.Log("hata");
                                                }
                                                else
                                                {
                                                    //try gerekli mi??? nasýl kullanýlabilir?
                                                    try
                                                    {
                                                        string methodName = ifParts[1].Substring(0, ifParts[1].IndexOf("("));
                                                        string parameterPart = ifParts[1].Substring(ifParts[1].IndexOf("(")).Replace(" ", "");
                                                        //emin degilim 2 olabilir ): seklinde ama bakmak lazim
                                                        if (parameterPart.Length != 3)
                                                        {
                                                            Debug.Log("hata");
                                                        }
                                                        else if (parameterPart[1] != ')')
                                                        {
                                                            Debug.Log("hata");
                                                        }


                                                        else if (methodName == "is_blue")
                                                        {
                                                            //If ifInstruction = new If()
                                                        }
                                                        else if (methodName == "is_red")
                                                        {

                                                        }
                                                        else
                                                        {
                                                            Debug.Log("hata");
                                                            //if(methodParameter == "apple")
                                                            //{
                                                            //    If ifInstruction = new If()
                                                            //}
                                                            //else
                                                            //{
                                                            //    Debug.Log("hata");
                                                            //}
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Debug.Log(ex.Message);
                                                    }
                                                }

                                                //string methodParameter = ifParts[1].Substring(ifParts[1].IndexOf("("), ifParts[1].IndexOf(")"));
                                            }
                                            else if (ifParts.Length == 3)
                                            {

                                                if (ifParts[0] != className)
                                                {
                                                    Debug.Log("hata");
                                                }
                                                string firstMethod = null;
                                                if (ifParts[1].Substring(0, 7) == "up_tile")
                                                {
                                                    string s = ifParts[1].Substring(7).Replace(" ", "");
                                                    if (s != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "up_tile";
                                                    }

                                                }
                                                else if (ifParts[1].Substring(0, 9) == "down_tile")
                                                {
                                                    string s = ifParts[1].Substring(9).Replace(" ", "");
                                                    if (s != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "down_tile";
                                                    }
                                                }
                                                else if (ifParts[1].Substring(0, 10) == "right_tile")
                                                {
                                                    string s = ifParts[1].Substring(10).Replace(" ", "");
                                                    if (s != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "right_tile";
                                                    }
                                                }
                                                else if (ifParts[1].Substring(0, 9) == "left_tile")
                                                {
                                                    string s = ifParts[1].Substring(9).Replace(" ", "");
                                                    if (s != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        firstMethod = "left_tile";
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.Log("simdilik hata");
                                                }

                                                string secondMethod = null;
                                                if (ifParts[2].Substring(0, 9) == "is_ground")
                                                {
                                                    string s = ifParts[2].Substring(9).Replace(" ", "");
                                                    if (s != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        secondMethod = "is_ground";
                                                    }

                                                }
                                                else if (ifParts[2].Substring(0, 8) == "is_water")
                                                {
                                                    string s = ifParts[2].Substring(8).Replace(" ", "");
                                                    if (s != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        secondMethod = "is_water";
                                                    }

                                                }
                                                else if (ifParts[2].Substring(0, 8) == "contains")
                                                {
                                                    string s = ifParts[2].Substring(8).Replace(" ", "");
                                                    //burasi degisecek. ()'in ici dolu olacak.
                                                    if (s != "()")
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        secondMethod = "is_water";
                                                    }

                                                }


                                            }
                                        // == varsa
                                        //buraya farkli bir sey koymak lazim. 
                                        else
                                        {

                                            string leftPart = trimmedRow.Substring(2, trimmedRow.IndexOf(operatorType) - 2).Trim();
                                            //burasý VariableCheck(var) ile deðiþtirilebilir

                                            string[] leftPartArray = leftPart.Split(".");

                                            if (leftPartArray.Length != 2)
                                            {
                                                Debug.Log("hata");
                                            }

                                            else if (leftPartArray.Length == 2)
                                            {
                                                string firstMethod = null;
                                                if (leftPartArray[0] != "character")
                                                {
                                                    Debug.Log("hata");
                                                }
                                                else
                                                {
                                                    if (leftPartArray[1].Substring(0, 7) == "up_tile")
                                                    {
                                                        string s = leftPartArray[1].Substring(7).Replace(" ", "");
                                                        if (s != "()")
                                                        {
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            firstMethod = "up_tile";
                                                        }

                                                    }
                                                    else if (leftPartArray[1].Substring(0, 9) == "down_tile")
                                                    {
                                                        string s = leftPartArray[1].Substring(9).Replace(" ", "");
                                                        if (s != "()")
                                                        {
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            firstMethod = "down_tile";
                                                        }
                                                    }
                                                    else if (leftPartArray[1].Substring(0, 10) == "right_tile")
                                                    {
                                                        string s = leftPartArray[1].Substring(10).Replace(" ", "");
                                                        if (s != "()")
                                                        {
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            firstMethod = "right_tile";
                                                        }
                                                    }
                                                    else if (leftPartArray[1].Substring(0, 9) == "left_tile")
                                                    {
                                                        string s = leftPartArray[1].Substring(9).Replace(" ", "");
                                                        if (s != "()")
                                                        {
                                                            Debug.Log("hata");
                                                        }
                                                        else
                                                        {
                                                            firstMethod = "left_tile";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Debug.Log("simdilik hata");
                                                    }



                                                    string value = trimmedRow.Substring(trimmedRow.IndexOf(operatorType) + 2).Trim();

                                                    //if'li kisim <2 olarak bir ihtimal eklenebilir.

                                                    //if (value.Length < 4)  // "x":
                                                    //{
                                                    //    Debug.Log("Hata");
                                                    //}
                                                    // else if (value[0] != '"' || value[value.Length - 2] != '"')
                                                    //{
                                                    //    Debug.Log("hata");
                                                    //}
                                                    if (value[value.Length - 1] != ':')
                                                    {
                                                        Debug.Log("hata");
                                                    }
                                                    else
                                                    {
                                                        //value = value.Substring(1, value.Length - 2);
                                                        value = value.Substring(0, value.Length - 1);

                                                        //burada If class'i olusturulacak.


                                                        //Burasi ve diger if'ler icin uyumlu if class'i lazim.
                                                        //If ifCondition = new If(var, operatorType, value, instructionLevel);

                                                        //ÖNEMLÝ: operatorType == mi diye kontrol etmek lazim.

                                                        //AddInstruction(ifCondition);

                                                    }

                                                    //bu kontrole gerek yok aslinda. hata ciktiginda program duracak zaten.
                                                    //if (firstMethod != null)
                                                    //{
                                                    //if (leftPartArray[2] == null)
                                                    //{

                                                    //}
                                                    //else
                                                    //{

                                                    //}
                                                    //}
                                                }
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
                                                For forLoop = new For(parameter, instructionLevel);


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

                                if (length < 6 || length > 9)
                                {
                                    Debug.Log("hata");
                                }
                                else if (length == 6)
                                {
                                    if (instructionParts[0] == "moveUp")
                                    {
                                        Move move = new Move(characterMovement, "up", instructionLevel);
                                        AddInstruction(move);
                                    }
                                    else
                                    {
                                        Debug.Log("hata");
                                    }
                                }
                                else if (length == 8)
                                {
                                    if (instructionParts[0] == "moveLeft")
                                    {
                                        Move move = new Move(characterMovement, "left", instructionLevel);
                                        AddInstruction(move);
                                    }
                                    else if (instructionParts[0] == "moveDown")
                                    {
                                        Move move = new Move(characterMovement, "down", instructionLevel);
                                        AddInstruction(move);
                                    }
                                    else
                                    {
                                        Debug.Log("hata");
                                    }
                                }
                                else if (length == 9)
                                {
                                    if (instructionParts[0] == "moveRight")
                                    {

                                        Move move = new Move(characterMovement, "right", instructionLevel);
                                        AddInstruction(move);
                                    }
                                    else
                                    {
                                        Debug.Log("hata");
                                    }
                                }

                            }
                            else if (trimmedRow.Substring(0, 4) == "elif")
                            {
                                if (trimmedRow[trimmedRow.IndexOf("elif") + 4] != ' ')
                                {
                                    Debug.Log("boþluk olmalý hata");
                                }
                                else
                                {
                                    string operatorType = null;

                                    if (trimmedRow.Contains("=="))
                                        operatorType = "==";
                                    else if (trimmedRow.Contains("!="))
                                        operatorType = "!=";
                                    else if (trimmedRow.Contains("<"))
                                        operatorType = "<";
                                    else if (trimmedRow.Contains(">"))
                                        operatorType = ">";
                                    else if (trimmedRow.Contains("="))
                                        operatorType = "=";
                                    else
                                        Debug.Log("Hata");



                                    //if (leftTrimmedRow.Contains(operatorType))
                                    //{
                                    string var = trimmedRow.Substring(4, trimmedRow.IndexOf(operatorType) - 4).Trim();
                                    //burasý VariableCheck(var) ile deðiþtirilebilir

                                    if (VariableCheck(var))
                                    {
                                        string value = trimmedRow.Substring(trimmedRow.IndexOf(operatorType) + 4).Trim();

                                        if (value.Length < 4)  // "x":
                                        {
                                            Debug.Log("Hata");
                                        }
                                        else if (value[0] != '"' || value[value.Length - 2] != '"')
                                        {
                                            Debug.Log("hata");
                                        }
                                        else if (value[value.Length - 1] != ':')
                                        {
                                            Debug.Log("hata");
                                        }
                                        else
                                        {
                                            value = value.Substring(1, value.Length - 2);

                                            //burada var ve value elde ettik. If classýný oluþturabiliriz.
                                            //level ve tipine göre atama yapmak gerekiyor. if'se baþka elif'se baþka 
                                            //ama burasý sadece if

                                            //conditionHolder = new ConditionHolder(instructionLevel);

                                            //+1 diyerek conditionHolder'ýn içine girmesi saðlandý gibi. Ama bir yerde conditionHolder'ýn içinde olup olmadýðý kontrol edilmeli.
                                            //Elif elifCondition = new Elif(var, operatorType, value, instructionLevel + 1);
                                            //Elif elifCondition = new Elif(var, operatorType, value, conditionId, conditionRunList, instructionLevel);
                                            Elif elifCondition = new Elif(var, operatorType, value, instructionLevel);
                                            AddInstruction(elifCondition);


                                            //Oldu mu bilmiyorum. Büyük ihtimal olmadý çünkü iften sonra araya baþka þeyler girmiþ olabilir. Bundan dolayý AddInstruction'da yapmak zorundayým.
                                            //if (conditionHolder == null)
                                            //    Debug.Log("hata");
                                            //else if (instructionLevel == conditionHolder.level)
                                            //    conditionHolder.Add(elifCondition);



                                            //conditionHolder.Add(ifCondition);
                                            //AddInstruction(conditionHolder);

                                        }
                                    }
                                    //}
                                }
                            }
                            else if (trimmedRow.Substring(0, 4) == "else")
                            {
                                trimmedRow = trimmedRow.Replace(" ", "");
                                if (trimmedRow.Length != 4)
                                    Debug.Log("hata");
                                else if (trimmedRow[4] != ':')
                                    Debug.Log("hata");
                                else
                                {
                                    //AddInstruction() içinde bunun üstüdneki ConditionHolder mý diye kontrol etmek gerek.
                                    //Else elseCondition = new Else(instructionLevel + 1);
                                    //Else elseCondition = new Else(conditionId, conditionRunList, instructionLevel);
                                    Else elseCondition = new Else(instructionLevel);
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


            foreach (Instruction instruction in instructionList)
            {
                Debug.Log(instruction.ToString());
                bool didRun = false;
                if (instruction != null)
                {
                    if (instruction.GetType().IsSubclassOf(typeof(Condition)))
                    {
                        if (instruction.GetType() == typeof(If))
                        {
                            //Buralarda boolean deðerleri hesaplanarak kullanýlabilir.


                            if (((If)instruction).boolean == true)
                            {
                                instruction.Run();
                            }
                        }
                        else if (instruction.GetType() == typeof(Elif))
                        {

                        }
                        //direkt else de denebilir.
                        else if (instruction.GetType() == typeof(Else))
                        {

                        }
                    }
                    instruction.Run();
                }
            }
        }
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




        //Son geldiðimiz instructionList boþ olabilir. null error verebilir.





        //if (instruction.level == listLevel)
        //{
        //    instructionList.Add(instruction);
        //}
        //else
        //{
        //    for (int j = instructionList.Count - 1; j >= 0; j--)
        //    {
        //        if (instructionList[j] != null)
        //        {
        //            if (instructionList[j].level == instruction.level - 1)
        //            {
        //                //burada holder olduklarýndan emin olmak lazým. Yani üsttekinden emin olmak lazým. 
        //                ((HolderInstruction)instructionList[j]).Add(instruction);

        //            }
        //        }
        //    }
        //}
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

