using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterConsole
{
    internal class CharacterReader
    {
        public string?[] lines;
        public string? FileName { get; set; }

        public List<Character>? Characters { get; set; }

        public CharacterReader()
        {
            FileName = "Files/input.csv";
        }

        //public void ReadFile()
        //{
        //    //lines = File.ReadAllLines(FileName);
        //}

        public List<Character> PopulateCharacters()
        {
            lines = File.ReadAllLines(FileName);


            // Skip the header row and parse individual lines
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] quotedNameArray = line.Split(',');
                var quotedNameTest = quotedNameArray[0];
                
                var character = new Character();

                // Check if the name is quoted
                if (quotedNameTest.StartsWith('"'))
                {
                    // Parse each line into individual character attributes
                    // Remove quotes from the name if present
                    // TODO put Ewuipment into an array and add to EquipmentList

                    
                    String[] CharacterFields = line.Split(',');
                    var characterLastName = CharacterFields[0].Trim('"');
                    var characterFirstName = CharacterFields[1].Trim('"');

                    //  Add character to List using one of many optional Syntax
                    Characters.Add(new Character()
                    {
                        Name = ($"{characterFirstName} {characterLastName}"),
                        ClassType = CharacterFields[2],
                        Level = CharacterFields[3],
                        HitPoints = CharacterFields[4],
                        EquipmentList = CharacterFields[5].Replace("|", ", ")
                    });
                    
                }
                else
                {
                    // Parse each line into individual character attributes
                    // Remove quotes from the name if present
                    // Replace | in quipment list with , and space for better readability

                    String[] CharacterFields = line.Split(',');
                    

                    character.Name = CharacterFields[0].Trim('"');
                    character.ClassType = CharacterFields[1];
                    character.Level = CharacterFields[2];
                    character.HitPoints = CharacterFields[3];
                    character.EquipmentList = CharacterFields[4].Replace("|", ", ");

                    //  ******   Can undo to here before altering toInt fields



                }
                

            }
            return Characters;

            
        }


    }
}
