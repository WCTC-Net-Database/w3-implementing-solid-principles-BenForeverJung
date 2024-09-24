using System.Threading.Channels;

namespace CharacterConsole;

public class CharacterManager
{
    private readonly IInput _input;
    private readonly IOutput _output;
    //From Template
    //  private readonly string _filePath = "input.csv";
    //  private string[] lines;
    public List<Character>? CharactersList { get; set; }
    public Character FoundCharacter { get; set; }
    private string findCharacter;

    public CharacterManager(IInput input, IOutput output)
    {
        _input = input;
        _output = output;
        CharactersList = new List<Character>();
    }

    public void Run()
    {
        //  Populate CharacterList from file
        CsvFileManager csvFileManager = new CsvFileManager(CharactersList);
        csvFileManager.PopulateCharacters();

        _output.WriteLine("Welcome to Character Management");

        //From Template
        //  lines = File.ReadAllLines(_filePath);

        while (true)
        {
            _output.WriteLine("Menu:");
            _output.WriteLine("1. Display All Characters");
            _output.WriteLine("2. Find And Display A Characters");
            _output.WriteLine("3. Add Character");
            _output.WriteLine("4. Level Up Character");
            _output.WriteLine("5. Exit");
            _output.Write("Enter your choice: ");
            var choice = _input.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayCharacters();
                    break;
                case "2":
                    DisplayFoundCharacter();
                    break;
                case "3":
                    AddCharacter();
                    break;
                case "4":
                    LevelUpCharacter();
                    break;
                case "5":
                    return;
                default:
                    _output.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void DisplayCharacters()
    {
        // Implement displaying characters from the CSV file

        // If CharacterList is null use PopulateCharacter method to create list   
        if (CharactersList != null)
        {
            CsvFileManager csvFileManager = new CsvFileManager(CharactersList);
            csvFileManager.PopulateCharacters();
        }
        // Display characters from the CharacterList
        foreach (var character in CharactersList)
        {
            _output.WriteLine("");
            _output.WriteLine($"Name= {character.Name}; Class= {character.ClassType}; Level= {character.Level}; Hit Points= {character.HitPoints}; Equipment= {character.EquipmentList}");
            _output.WriteLine("");
        }
        _output.WriteLine("");
        _output.WriteLine("------------------------------------");
        _output.WriteLine("");

    }

    public void DisplayFoundCharacter()
    {
        //  Display Found Character  or If found Character is Null display could not find Message 
        CharacterFinder();
        if (FoundCharacter == null)
        {
            _output.WriteLine("");
            _output.WriteLine($"Could Not Find {findCharacter} in the Character List");
            _output.WriteLine("");
            _output.WriteLine("------------------------------------");
            _output.WriteLine("");
        }
        else
        {
            _output.WriteLine("");
            _output.WriteLine($"Name= {FoundCharacter.Name}; Class= {FoundCharacter.ClassType}; Level= {FoundCharacter.Level}; Hit Points= {FoundCharacter.HitPoints}; Equipment= {FoundCharacter.EquipmentList}");
            _output.WriteLine("");
            _output.WriteLine("------------------------------------");
            _output.WriteLine("");
        }
    }

    public void AddCharacter()
    {
        // If CharacterList is null use PopulateCharacter method to create list   
        if (CharactersList != null)
        {
            CsvFileManager csvFileManager = new CsvFileManager(CharactersList);
            csvFileManager.PopulateCharacters();
        }
        //  Get User inputs to build Character
        _output.WriteLine("");
        _output.WriteLine("Enter Your Character's Name and Press Enter: ");
        var nameInput = _input.ReadLine();
        _output.WriteLine("");
        _output.WriteLine("Enter Your Character's Class and Press Enter: ");
        var classTypeInput = _input.ReadLine();
        _output.WriteLine("");
        _output.WriteLine("Enter Your Character's Level and Press Enter: ");
        var levelInput = _input.ReadLine();
        _output.WriteLine("");
        _output.WriteLine("Enter Your Character's Hit Power and Press Enter: ");
        var hitPointsInput = _input.ReadLine();

        _output.WriteLine("");
        _output.WriteLine("Enter Your Character's First Piece of Equipment and Press Enter: ");
        var equipmentInput = _input.ReadLine();

        //  Use Do While loop to continu askiing for additional equipment until complete
        // Add to equipment string with Pipe delineated format
        //TODO Add input validation to Do While Inputs  - No Null for adding equipment and Y/N Questions
        bool addEquipment = true;

        do
        {
            _output.WriteLine("");
            _output.WriteLine("Would You Like to Add Another Piece of Equipment? Enter Y for Yes or N for No. ");
            var addEquipmentresponse = _input.ReadLine();
            if (addEquipmentresponse == "Y" || addEquipmentresponse == "y")
            {
                _output.WriteLine("");
                _output.WriteLine("Enter Your Character's Next Piece of Equipment and Press Enter: ");
                var nextEquipment = _input.ReadLine();
                equipmentInput = (equipmentInput + "|" + nextEquipment);
            }
            else addEquipment = false;
        }
        while (addEquipment == true);

        // Display character to user
        _output.WriteLine("");
        _output.WriteLine($"Your Character Has Been Added to the Game!");
        _output.WriteLine($"Name = {nameInput}");
        _output.WriteLine($"Class = {classTypeInput}");
        _output.WriteLine($"Level = {levelInput}");
        _output.WriteLine($"HP = {hitPointsInput}");
        _output.WriteLine($"Equipment List = {equipmentInput}");
        _output.WriteLine("");
        _output.WriteLine("------------------------------------");
        _output.WriteLine("");

        //  Convert inputed character attributes to Character Class Objests and Add to Characterlist
        var character = new Character();
        character.Name = nameInput;
        character.ClassType = classTypeInput;
        character.Level = levelInput;
        character.HitPoints = hitPointsInput;
        character.EquipmentList = equipmentInput;
        CharactersList.Add(character);


        // Update Character File From updated CharacterList
        CsvFileManager characterReader = new CsvFileManager(CharactersList);
        characterReader.UpdateCharacterFile();
        // ReRead the updated input.csv file via the methods in CsvFileManager
        characterReader.PopulateCharacters();

    }

    public void LevelUpCharacter()
    {
        //  Display Found Character  or If found Character is Null display could not find Message 
        CharacterFinder();
        if (FoundCharacter == null)
        {
            _output.WriteLine("");
            _output.WriteLine($"Could Not Find {findCharacter} in the Character List");
            _output.WriteLine("");
            _output.WriteLine("------------------------------------");
            _output.WriteLine("");
        }
        else
        {
            _output.WriteLine("");
            _output.WriteLine($"Name= {FoundCharacter.Name}; Class= {FoundCharacter.ClassType}; Level= {FoundCharacter.Level}; Hit Points= {FoundCharacter.HitPoints}; Equipment= {FoundCharacter.EquipmentList}");
            _output.WriteLine("");
            _output.WriteLine($"Enter New {FoundCharacter.ClassType} Level for Character {FoundCharacter.Name}.");
            var newCharacterLevel = _input.ReadLine();

            // Update FoundCharacter in CharacterList and Display Updated Character Level

            foreach (Character character in CharactersList)
            {
                if (character.Name == FoundCharacter.Name)
                {
                    character.Level = newCharacterLevel;

                    _output.WriteLine($"Character {character.Name} Has Been Upgraded To a Level {character.Level} {character.ClassType}!");
                }
            }
            _output.WriteLine("------------------------------------");
            _output.WriteLine("");
        }

        // Update Character File From updated CharacterList
        CsvFileManager characterReader = new CsvFileManager(CharactersList);
        characterReader.UpdateCharacterFile();
        // ReRead the updated input.csv file via the methods in CsvFileManager
        characterReader.PopulateCharacters();
    }

    public Character CharacterFinder()
    {
        // If CharactersList is null use PopulateCharacter method to create list   
        if (CharactersList != null)
        {
            CsvFileManager csvFileManager = new CsvFileManager(CharactersList);
            csvFileManager.PopulateCharacters();
        }

        _output.WriteLine("");
        _output.WriteLine("Enter the Name of the Character You Want to Find");
        //Find Character using linq
        findCharacter = _input.ReadLine();
        findCharacter = findCharacter.ToLower();
        var foundCharacter = CharactersList?.FirstOrDefault(foundC => foundC.Name.ToLower() == findCharacter);
        //  Return the Found Character
        FoundCharacter = foundCharacter;
        return FoundCharacter;
    }
}