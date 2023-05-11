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

//sadece say�yla d�nen for loop. boolean olacaksa ayr� class olu�turulabilir.
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
        //iterasyon say�s�n� ayarlamak laz�m.
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
            //ayr�ca bir tanesi �al��t���nda di�erlerini �al��t�rmamas� gerekiyor. Nas�l yap�lacak???
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
    //if blo�unun i�indekiler eklencek.
    //public List<Instruction> instructions;


    public bool boolean;
    public int id;
    public List<bool> conditionRunList;

    //burada string olarak de�il, de�eri belirlenmi� bool olarak da atanabilir. ekleme k�sm� ayarlanmal�.
    //public Condition(string type, string booleanString, int level)
    //{
    //    this.type = type;
    //    this.level = level;

    //    if (booleanString != null)
    //    {
    //        //burada boolean de�eri belirlenecek. kontrol yap�lacak yani. 
    //        //buraya eklenmeden mi burada m� boolean belirlense daha kolay olur? eklenmeden belki daha kolay olur ��nk� di�er tarafta daha �ok �eye eri�im var.

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
    public string variable;
    public string operatorType;
    public string value;


    public If(string variable, string operatorType, string value, int id, List<bool> conditionRunList, int level)
    {

        this.variable = variable;
        this.operatorType = operatorType;
        this.value = value;
        this.id = id;
        this.conditionRunList = conditionRunList;
        this.level = level;
        
    }



    public override void Run()
    {
        //burada �nce di�erlerinde ba��ms�z olarak bool de�erine g�re �al��acak m� diye kontrol edece�iz.
        //sonra �al��acaksa conditionRunListte id'ye kar��l�k gelen indexi true yap�caz.
        //SIKINTI �U: true yapt�ktan sonra for loop i�erisinde ayn� if'e tekrar gelince ne olacak???
        
        //if(conditionRunList == null) 
        //{ 
            
        //}

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


    public Elif(string variable, string operatorType, string value, int id, List<bool> conditionRunList, int level)
    {

        this.variable = variable;
        this.operatorType = operatorType;
        this.value = value;
        this.id = id;
        this.conditionRunList = conditionRunList;
        this.level = level;

    }



    public override void Run()
    {
        throw new NotImplementedException();
    }
}

public class Else : Condition
{

    public Else(int id, List<bool> conditionRunList,  int level)
    {
        this.id = id;
        this.conditionRunList = conditionRunList;
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


//    //kod level'�na gore tree'ye ekleniyor
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
//            //Kodlar�n okuma s�ras�na uymas� i�in tree'de surekli en sondaki node secilerek ilerleniyor. 
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

//    //eklemede sorun yok. dola�mada daha d���k seviyeliyi okumuyor.
//    public void Traverse(Node root)
//    {

//        if (root != null)
//        {
//            for (int i = 0; i < root.childNodes.Count; i++)
//            {
//                if (root.childNodes[i] != null)
//                {
//                    Node current = root.childNodes[i];
//                    //Burada son geldi�imiz node okunuyor.
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

        //bu k�s�m �al��abiliyor.
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



                    string leftTrimmedRow = rows2[i].Substring(rowIndentation);
                    Debug.Log(rows2[i]);
                    string[] rowWords;
                    //string[] rowWords = leftTrimmedRow.Split(" ");


                    //en az gerekli karakter say�s� = 16
                    //BUNLARI DE���T�REB�L�R�M. D�REKT HATALI DEMEK SA�MA.
                    if (leftTrimmedRow.Length < 16)
                    {
                        Debug.Log("hata");
                    }
                    else if (leftTrimmedRow.Substring(0, 3) != "def")
                    {
                        Debug.Log("Hata");

                    }
                    else
                    {
                        rowWords = leftTrimmedRow.Split(" ", 2);


                        // burada class m�, sonu ':' m� vs. kontrol edilecek.
                        //string lastWord = rowWords[rowWords.Length - 1];

                        string initWord = rowWords[1];
                        Debug.Log(initWord);

                        //burada hata var. ( 6 da olmak zorunda degil.


                        //if (initWord.Length >= 8)
                        //{
                        //once ( ) buna gore bolerim sonra icinde baska ( ) var m� bakar�m.
                        //Debug.Log(initWord.Length);
                        //initWord = initWord.TrimEnd();

                        if (initWord.Substring(0, 8) != "__init__")
                        {

                            Debug.Log("Hata");

                        }
                        else
                        {
                            //Bu k�s�mda bo�luklar�n �nemi olmad��� i�in bo�luksuz kelime elde ediliyor.
                            initWord = initWord.Replace(" ", "");
                            //Debug.Log(str);
                            //if (initWord.Length >= 11)
                            //{
                            Debug.Log(initWord[8]);
                            Debug.Log(initWord[initWord.Length - 2]);

                            //indexler do�ru mu??
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
                                Debug.Log("yanl�� yerde : ");
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

                    Debug.Log("buras�");
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
                            //buras� da de�i�ecek...
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
                                    //Buras� de�i�ecek ��nk� kelimenin self olmas� gerekmiyor. 
                                    //Bununla birlikte uzunluk kontrol� de de�i�ecek. ��nk� self olmas�na g�re yap�ld� ama 1 karakterlik bir �ey de olabilir. 
                                    //else if (leftSide[0] != "self")
                                    //{
                                    //  Debug.Log("hata");
                                    //}
                                    else
                                    {
                                        if (leftSide[0] != selfKeyword)
                                        {
                                            Debug.Log("keyword do�ru de�il");
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
                                                Debug.Log("kelimeler ayn� degil");
                                            }
                                            else
                                            {
                                                //burada kaydetmeye ba�lanabilir
                                                //init row dakilerle de ayr�ca kar��la�t�rmak gerekiyor.
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
                Debug.Log("atama say�lar� yetersiz");
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
                            Debug.Log("indentation hatas�");
                        }


                        Debug.Log("rowindentaiton " + rowIndentation);
                        Debug.Log("indentaion " + indentation);


                        instructionLevel = ((rowIndentation - indentation) / n) + 1;



                        if (lastInstructionType == "holder")
                        {
                            if (rowIndentation != lastIndentation + n)
                            {
                                Debug.Log("indentation hatas�2");
                            }
                        }


                        string leftTrimmedRow = rows1[i].Substring(rowIndentation);
                        Debug.Log(leftTrimmedRow + " " + leftTrimmedRow.Length);



                        //BU length kontrolleri DE���ECEK!!!!!!!!!!!!
                        if (leftTrimmedRow.Length >= 2)
                        {
                            if (leftTrimmedRow.Substring(0, 2) == "if")
                            {
                                if (leftTrimmedRow[leftTrimmedRow.IndexOf("if") + 2] != ' ')
                                {
                                    Debug.Log("bo�luk olmal� hata");
                                }
                                else
                                {
                                    string operatorType = null;

                                    if (leftTrimmedRow.Contains("=="))
                                        operatorType = "==";
                                    else if (leftTrimmedRow.Contains("!="))
                                        operatorType = "!=";
                                    else if (leftTrimmedRow.Contains("<"))
                                        operatorType = "<";
                                    else if (leftTrimmedRow.Contains(">"))
                                        operatorType = ">";
                                    else if (leftTrimmedRow.Contains("="))
                                        operatorType = "=";
                                    else
                                        Debug.Log("Hata");



                                    //if (leftTrimmedRow.Contains(operatorType))
                                    //{
                                    string var = leftTrimmedRow.Substring(2, leftTrimmedRow.IndexOf(operatorType) - 2).Trim();
                                    //buras� VariableCheck(var) ile de�i�tirilebilir

                                    if (VariableCheck(var))
                                    {
                                        string value = leftTrimmedRow.Substring(leftTrimmedRow.IndexOf(operatorType) + 2).Trim();

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

                                            //burada var ve value elde ettik. If class�n� olu�turabiliriz.
                                            //level ve tipine g�re atama yapmak gerekiyor. if'se ba�ka elif'se ba�ka 
                                            //ama buras� sadece if

                                            //conditionHolder = new ConditionHolder(instructionLevel);
                                            //If ifCondition = new If(var, operatorType, value, instructionLevel + 1);
                                            If ifCondition = new If(var, operatorType, value, ++conditionId, conditionRunList, instructionLevel);
                                            //conditionHolder.Add(ifCondition);
                                            //AddInstruction(conditionHolder);
                                            AddInstruction(ifCondition);

                                        }
                                    }
                                    //}
                                }

                            }


                        }

                        //BO�LUK VAR MI D�YE KONTROL ETMEK LAZIM. GEREKL� BO�LUKLAR YOKSA HATA VAR DEMEK LAZIM.
                        if (leftTrimmedRow.Length >= 3)
                        {
                            Debug.Log("�");
                            if (leftTrimmedRow.Substring(0, 3) == "for")
                            {

                                //string[] rowWords = rows1[i].Split(' ');
                                //Debug.Log(rowWords[1]);

                                if (leftTrimmedRow[3] != ' ')
                                {
                                    Debug.Log("hata");

                                }
                                else if (!leftTrimmedRow.Contains("in"))
                                {
                                    Debug.Log("hata");
                                }
                                else if (leftTrimmedRow[leftTrimmedRow.IndexOf("in") - 1] != ' ')
                                {
                                    Debug.Log("hata");
                                }
                                else if (leftTrimmedRow[leftTrimmedRow.IndexOf("in") + 2] != ' ')
                                {
                                    Debug.Log("hata");
                                }

                                else
                                {
                                    //burada san�r�m trim kullanmak gerekiyor ve sonras�nda kelimenin i�inde bo�luk var m� diye bakmak gerekiyor
                                    string var = leftTrimmedRow.Substring(3, leftTrimmedRow.IndexOf("in") - 3).Trim();
                                    Debug.Log("var = " + var);
                                    //
                                    if (pythonReservedWords.Contains(var))
                                    {
                                        Debug.Log("hata");
                                    }
                                    else if (!leftTrimmedRow.Contains("range"))
                                    {
                                        Debug.Log("hata");
                                    }
                                    else
                                    {
                                        string rangeParameter = leftTrimmedRow.Substring(leftTrimmedRow.IndexOf("range") + 5).Replace(" ", "");
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
                                                //range i�indeki de�ere ula�abiliyorum art�k.

                                                //Bunu do�ru yere eklemek gerekiyor.
                                                For forLoop = new For(parameter, instructionLevel);


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

                        }
                        if (leftTrimmedRow.Length >= 4)
                        {
                            Debug.Log("Moveeee");
                            if (leftTrimmedRow.Substring(0, 4) == "move")
                            {
                                if (!leftTrimmedRow.Contains('('))
                                {
                                    Debug.Log("hata");
                                }
                                else if (!leftTrimmedRow.Contains(')'))
                                {
                                    Debug.Log("hata2");
                                }

                                string[] instructionParts = leftTrimmedRow.Split('(', 2);

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
                            else if (leftTrimmedRow.Substring(0, 4) == "elif")
                            {
                                if (leftTrimmedRow[leftTrimmedRow.IndexOf("elif") + 4] != ' ')
                                {
                                    Debug.Log("bo�luk olmal� hata");
                                }
                                else
                                {
                                    string operatorType = null;

                                    if (leftTrimmedRow.Contains("=="))
                                        operatorType = "==";
                                    else if (leftTrimmedRow.Contains("!="))
                                        operatorType = "!=";
                                    else if (leftTrimmedRow.Contains("<"))
                                        operatorType = "<";
                                    else if (leftTrimmedRow.Contains(">"))
                                        operatorType = ">";
                                    else if (leftTrimmedRow.Contains("="))
                                        operatorType = "=";
                                    else
                                        Debug.Log("Hata");



                                    //if (leftTrimmedRow.Contains(operatorType))
                                    //{
                                    string var = leftTrimmedRow.Substring(4, leftTrimmedRow.IndexOf(operatorType) - 4).Trim();
                                    //buras� VariableCheck(var) ile de�i�tirilebilir

                                    if (VariableCheck(var))
                                    {
                                        string value = leftTrimmedRow.Substring(leftTrimmedRow.IndexOf(operatorType) + 4).Trim();

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

                                            //burada var ve value elde ettik. If class�n� olu�turabiliriz.
                                            //level ve tipine g�re atama yapmak gerekiyor. if'se ba�ka elif'se ba�ka 
                                            //ama buras� sadece if

                                            //conditionHolder = new ConditionHolder(instructionLevel);

                                            //+1 diyerek conditionHolder'�n i�ine girmesi sa�land� gibi. Ama bir yerde conditionHolder'�n i�inde olup olmad��� kontrol edilmeli.
                                            //Elif elifCondition = new Elif(var, operatorType, value, instructionLevel + 1);
                                            Elif elifCondition = new Elif(var, operatorType, value, conditionId, conditionRunList, instructionLevel);
                                            AddInstruction(elifCondition);


                                            //Oldu mu bilmiyorum. B�y�k ihtimal olmad� ��nk� iften sonra araya ba�ka �eyler girmi� olabilir. Bundan dolay� AddInstruction'da yapmak zorunday�m.
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
                            else if (leftTrimmedRow.Substring(0, 4) == "else")
                            {
                                leftTrimmedRow = leftTrimmedRow.Replace(" ", "");
                                if (leftTrimmedRow.Length != 4)
                                    Debug.Log("hata");
                                else if (leftTrimmedRow[4] != ':')
                                    Debug.Log("hata");
                                else
                                {
                                    //AddInstruction() i�inde bunun �st�dneki ConditionHolder m� diye kontrol etmek gerek.
                                    //Else elseCondition = new Else(instructionLevel + 1);
                                    Else elseCondition = new Else(conditionId, conditionRunList, instructionLevel);
                                    AddInstruction(elseCondition);
                                }


                            }
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
                if (instruction != null)
                {
                    instruction.Run();
                }
            }
        }
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




        //Son geldi�imiz instructionList bo� olabilir. null error verebilir.





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
        //                //burada holder olduklar�ndan emin olmak laz�m. Yani �sttekinden emin olmak laz�m. 
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

