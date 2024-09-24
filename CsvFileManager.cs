using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterConsole
{
    internal class CsvFileManager
    {
        public string?[] lines;
        public string? FileName { get; set; }

        private readonly List<Character> _charactersList;

        public CsvFileManager(List<Character> CharactersList)
        {
            FileName = "Files/input.csv";
            _charactersList = CharactersList;
        }

        public void PopulateCharacters()
        {
            _charactersList.Clear();
            lines = File.ReadAllLines(FileName);

            // Skip the header row and parse individual lines
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                String[] CharacterFields = line.Split(',');
                var character = new Character();

                // Check if the name is quoted
                var quotedNameTest = CharacterFields[0];
                if (quotedNameTest.StartsWith('"'))
                {
                    // Parse each line into individual character attributes
                    // Remove quotes from the name if present
                    // TODO put Ewuipment into an array and add to EquipmentList

                    var characterLastName = CharacterFields[0].Trim('"');
                    var characterFirstName = CharacterFields[1].Trim('"');

                    //  Add character to List using one of many optional Syntax

                    character.Name = ($"{characterFirstName} {characterLastName}");
                    character.ClassType = CharacterFields[2];
                    character.Level = CharacterFields[3];
                    character.HitPoints = CharacterFields[4];
                    character.EquipmentList = CharacterFields[5];   //.Replace("|", ", ");
                }
                else
                {
                    // Parse each line into individual character attributes
                    // Remove quotes from the name if present
                    // Replace | in quipment list with , and space for better readability

                    character.Name = CharacterFields[0];    //.Trim('"');
                    character.ClassType = CharacterFields[1];
                    character.Level = CharacterFields[2];
                    character.HitPoints = CharacterFields[3];
                    character.EquipmentList = CharacterFields[4];   // .Replace("|", ", ");
                }
                //  Add Character to the CharacterList
                _charactersList.Add(character);
            }

        }//  End of Populate Character Method

        public void UpdateCharacterFile()
        {
            // Create list for CSV formated character objects
            List<string> CsvFormattedCharacters = new List<string>();
            //  Add header to CsvFormattedCharacters List in CSV format
            CsvFormattedCharacters.Add("Name,Class,Level,HP,Equipment");

            //  Convert characters objects into CSV format line by line and add to list
            foreach (var character in _charactersList)
            {
                string csvLine = ($"{character.Name},{character.ClassType},{character.Level},{character.HitPoints},{character.EquipmentList}");

                CsvFormattedCharacters.Add(csvLine);
            }
            //  Overwrite CSV File with CsvFormattedCharacters List
            //File.Delete(FileName);
            File.WriteAllLines(FileName, CsvFormattedCharacters);

        }  //  End of updateCharacterFile Method

    }
}
