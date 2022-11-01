<Query Kind="Statements">
  <Connection>
    <ID>4f166263-059b-472c-a4fc-f4179e1ea7b7</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Countries</Database>
  </Connection>
</Query>

//2.1
foreach(var country in Countries)
{
	Console.WriteLine(country);
}

var countries = from c in Countries
				select new {c.Name, c.Capital, c.Population, c.Area, c.PartOfTheWorld};
				
foreach(var country in countries)
{
	Console.WriteLine(country);
}				


//2.2
var countryNames = from c in Countries
					select c.Name;
					
foreach(var country in countryNames)
{
	Console.WriteLine(country);
}


//2.3
var capitals = from c in Countries
				select c.Capital;
				
foreach(var capital in capitals)
{
	Console.WriteLine(capital);
}


//2.4
var europeCountries = from c in Countries
						where c.PartOfTheWorld == "Европа"
						select c.Name;
						
foreach(var counrty in europeCountries)
{
	Console.WriteLine(counrty);
}


//2.5
var countries = from c in Countries
				where c.Area > 250000
				select c.Name;
				
foreach(var counrty in countries)
{
	Console.WriteLine(counrty);
}


//3.1
var countries = from c in Countries
				where c.Name.Contains("ф") || c.Name.Contains("х")
				select c.Name;
				
foreach(var counrty in countries)
{
	Console.WriteLine(counrty);
}


//3.2
var countries = from c in Countries
				where c.Name.StartsWith("А")
				select c.Name;
				
foreach(var counrty in countries)
{
	Console.WriteLine(counrty);
}


//3.3
var countries = from c in Countries
				where c.Area > 250000 && c.Area < 500000
				select new { c.Name, c.Area};
				
foreach(var counrty in countries)
{
	Console.WriteLine(counrty);
}


//3.4
var countries = from c in Countries
				where c.Population > 1000000
				select new { c.Name, c.Population};
				
foreach(var counrty in countries)
{
	Console.WriteLine(counrty);
}


//4.1
var countries = (from c in Countries
				orderby c.Area descending
				select new { c.Name, c.Area}).Take(5);
				
foreach(var counrty in countries)
{
	Console.WriteLine(counrty);
}	


//4.2
var countries = (from c in Countries
				orderby c.Population descending
				select new { c.Name, c.Population}).Take(5);
				
foreach(var counrty in countries)
{
	Console.WriteLine(counrty);
}			


//4.3
var counrty = (from c in Countries
				orderby c.Population descending
				select new { c.Name, c.Population}).First();
				
Console.WriteLine(counrty);	


//4.4
var counrty = (from c in Countries
				where c.PartOfTheWorld == "Африка"
				orderby c.Area
				select new {c.Name, c.Area}).First();
				
Console.WriteLine(counrty);


//4.5
var avgArea = (from c in Countries
				where c.PartOfTheWorld == "Европа"
				select c.Area).Average();
				
Console.WriteLine(avgArea);