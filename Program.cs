using LINQDemo;

List<MadScientist> madScientists = new List<MadScientist> {
    new MadScientist { FirstName = "Kareem", LastName = "Dana", Gender = 'M', Age = 35, LastSeen = DateTime.Parse("12/31/2018 11:59:59 PM"), LastSeenLocation = "West Texas A&M University, Canyon, TX"},
    new MadScientist { FirstName = "Jeff", LastName = "Babb", Gender = 'M', Age = 48, LastSeen = DateTime.Parse("1/22/2019 10:00:00 AM"), LastSeenLocation = "Amarillo, TX"},
    new MadScientist { FirstName = "Sean", LastName = "Humpherys", Gender = 'M', Age = 40, LastSeen = DateTime.Parse("1/15/2019 12:30:00 AM"), LastSeenLocation = "Provo, UT"},
    new MadScientist { FirstName = "Beth", LastName = "Harmon", Gender = 'F', Age = 23, LastSeen = DateTime.Parse("1/22/2019 3:15:00 PM"), LastSeenLocation = "Las Vegas, NV"},
    new MadScientist { FirstName = "Magnus", LastName = "Carlsen", Gender = 'M', Age = 28, LastSeen = DateTime.Parse("1/23/2019 1:30:00 PM"), LastSeenLocation = "Dallas, TX"},
    new MadScientist { FirstName = "Ibrahim", LastName = "Lazrag", Gender = 'M', Age = 47, LastSeen = DateTime.Parse("1/24/2019 10:00:00 PM"), LastSeenLocation = "West Texas Sanatorium, TX"},
    new MadScientist { FirstName = "Gunter", LastName = "Muller", Gender = 'M', Age = 19, LastSeen = DateTime.Parse("1/26/2019 12:00:00 PM"), LastSeenLocation = "Las Vegas, NV"},
    new MadScientist { FirstName = "Hans", LastName = "Gruber", Gender = 'M', Age = 67, LastSeen = DateTime.Parse("1/26/2019 12:00:00 PM"), LastSeenLocation = "West Texas A&M University, Canyon, TX"},
    new MadScientist { FirstName = "Helga", LastName = "Schneider", Gender = 'F', Age = 74, LastSeen = DateTime.Parse("1/26/2019 12:00:00 PM"), LastSeenLocation = "Amarillo, TX"},
    new MadScientist { FirstName = "Brunhilda", LastName = "Abegglen", Gender = 'F', Age = 109, LastSeen = DateTime.Parse("1/22/1989 10:34:00 AM"), LastSeenLocation = "Hamburg, Germany"},
};

// Return the FIRST madscientist from our list
// Data type is MadScientist
MadScientist firstMadScientist = madScientists.First();
Console.WriteLine(firstMadScientist);

// This is a list. Data type is IEnumerable<MadScientist>
// .Skip(3) will skip the first 3 results
// .Take(2) will return the next 2 results
var firstThree = madScientists.Skip(3).Take(2);
foreach (var m in firstThree)
{
    //Console.WriteLine(m);
}

// LINQ Query Syntax
IEnumerable<MadScientist> query1 = from m in madScientists // specify data source
            where m.Age > 50 // filter our data
            select m; // what we should return

// LINQ Method Syntax
// m is range variable. Represents each individual record
// m => Lambda expression
// m is input. m.Age > 50 is the check, returns true or false
var query1MS = madScientists.Where(m => m.Age > 500);
Console.WriteLine($"How many results: {query1MS.Count()}");
foreach (MadScientist m in query1MS)
{
    Console.WriteLine(m);
}

// What happens when .First() returns no results? It throws exception that must be caught
try 
{
    var query2MS = madScientists.Where(m => m.Age > 500).First();
}
catch
{
    Console.WriteLine("No results");
}

// .FirstOrDefault() returns null instead of throwing an exception
var query3MS = madScientists.Where(m => m.Age > 500).FirstOrDefault();
if (query3MS == null)
{
    Console.WriteLine("No results");
}

// LINQ query using String manipulation
// Return all elements where the first name Contains the letter B
var query4MS = madScientists.Where(m => m.FirstName.Contains("B"));
var query4 = from m in madScientists
            where m.FirstName.Contains("B")
            select m;

foreach (var m in query4MS)
{
    //Console.WriteLine(m);
}

// You can chain multiple .Where() methods together.
var query5MS = madScientists.Where(m => m.Age > 50).Where(m => m.Gender == 'M');
var query5 = from m in madScientists
            where m.Age > 50
            where m.Gender == 'M'
            select m;

foreach (var m in query5MS)
{
    //Console.WriteLine(m);
}

Console.WriteLine();
// An example of sorting your results using OrderBy
var query6 = madScientists.OrderByDescending(m => m.LastName).ThenBy(m => m.FirstName).ThenByDescending(m => m.Age);
var query6QS = from m in madScientists
                orderby m.LastName, m.FirstName, m.Age descending
                select m;

foreach (var m in query6QS)
{
    //Console.WriteLine(m);
} 

// Using Projection with select. Select just the FirstName
var query7 = madScientists.Where(m => m.Age > 50).Select(m => m.FirstName);
var query7QS = from m in madScientists
                where m.Age > 50
                select m.FirstName;

foreach (var s in query7)       
{
//    Console.WriteLine(s);
}

// Select multiple properties using projection.
// This creates a new anonymous type we have to deal with
var query8 = madScientists.Where(m => m.Age > 50).Select(m => new {m.FirstName, m.LastName});
var query8QS = from m in madScientists
                where m.Age > 50
                select new {m.FirstName, m.LastName};

foreach (var m in query8)
{
    // Console.WriteLine($"{m.FirstName} {m.LastName}");
}

// GroupBy Example. Group all mad scientists by their gender.
var groupByQuery = madScientists.GroupBy(m => m.Gender);

var groupByQS = from m in madScientists
                group m by m.Gender;

foreach (var group in groupByQuery)
{
    Console.WriteLine($"{group.Key} -- {group.Count()}");
    foreach (var m in group)
    {
        Console.WriteLine(m);
    }
}
// Aggregate queries example.
// Return the average age of all the mad scientists
var averageAge = madScientists.Average(m => m.Age);

// Return the average age of just the men
var query9 = madScientists.Where(m => m.Gender == 'M').Average(m => m.Age);
Console.WriteLine(query9);

// Combine Aggregate with a 2nd LINQ query
// Return all mad scientists younger than the average
var query10 = madScientists.Where(m => m.Age < query9);
var query11 = madScientists.Where(m => m.Age < 
                    madScientists.Where(n => n.Gender == 'M').Average(n => n.Age));
foreach (var m in query11)
{
    Console.WriteLine(m);

}