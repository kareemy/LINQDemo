using System;

namespace LINQDemo
{
    public class MadScientist
    {
        public int MadScientistID {get; set;}
        public string FirstName {get; set;} = string.Empty;
        public string LastName {get; set;} = string.Empty;
        public int Age {get; set;}
        public DateTime LastSeen {get; set;}
        public string LastSeenLocation {get; set;} = string.Empty;
        public char Gender {get; set;}

        public override string ToString()
        {
            return $"{FirstName} {LastName} - {Age} years old.";
        }
    }
}