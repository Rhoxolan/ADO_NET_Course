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
var countries = Countries.Select(c => new{c.Name, c.Population, c.Area, c.PartOfTheWorld});

foreach(var country in countries)
{
	Console.WriteLine(country);
}


var countries = Countries.Join
							(
								Capitals,
								con => con.CapitalID,
								cap => cap.Id,
								(con, cap) => new {con.Name, con.Population, con.Area, con.PartOfTheWorld, Capital = cap.Name});
								
foreach(var country in countries)
{
	Console.WriteLine(country);
}


//2.2
foreach(var country in Countries.Select(c => c.Name)) Console.WriteLine(country);


//2.3
foreach(var capital in Capitals.Select(c => c.Name)) Console.WriteLine(capital);


//2.4
var cities = Towns.Where(c => c.Population > 10000 && c.CountryID == (Countries.Where(c => c.Name == "Цар").Select(c => c.Id).First())).Select(t => new {t.Name, t.Population});

foreach(var city in cities)
{
	Console.WriteLine(city);
}


//2.5
foreach(var capital in Capitals.Where(c => c.Population > 500000).Select(c => new {c.Name, c.Population})) Console.WriteLine(capital);


//2.6
foreach(var country in Countries.Where(c => c.PartOfTheWorld == "Европа").Select(c => c.Name))
Console.WriteLine(country);


//2.7
foreach(var country in Countries.Where(c => c.Area > 500000).Select(c => new {c.Name, c.Area}))
{
	Console.WriteLine(country);
}


//3.1
var capitals = Capitals.Where(c => c.Name.Contains("а") && c.Name.Contains("р"));
foreach(var capital in capitals)
{
	Console.WriteLine(capital);
}


//3.2
var capitals = Capitals.Where(c => c.Name.StartsWith("Б"));
foreach(var capital in capitals)
{
	Console.WriteLine(capital);
}


//3.3
var countries = Countries.Where(c => c.Area > 10000 && c.Area < 1000000);
foreach(var country in countries)
Console.WriteLine(country);


//3.4
var countries = Countries.Where(c => c.Population > 1000000);
foreach(var country in countries)
Console.WriteLine(country);


//4.1
var countries = Countries.OrderByDescending(c=>c.Area).Take(2);
foreach(var country in countries)
{
	Console.WriteLine(country);
}


//4.2.
var capitals = Capitals.OrderByDescending(c=>c.Population).Take(2);
foreach(var capital in capitals) Console.WriteLine(capital);


//4.3
Console.WriteLine(Countries.OrderByDescending(c => c.Area).First().Name);


//4.4
Console.WriteLine(Capitals.OrderByDescending(c => c.Population).Select(c => c.Name).First());


//4.5
Countries country = Countries.Where(c => c.PartOfTheWorld == "Европа").OrderBy(c => c.Area).First();
Console.WriteLine(country);


//4.6
var towns = Towns.Where(t => t.CountryID == (Countries.Where(c => c.Name == "ЦАР").Select(c => c.Id).FirstOrDefault())).OrderByDescending(t => t.Population).Take(3);
foreach(Towns town in towns)
{
	Console.WriteLine(town);
}


//4.7
Console.WriteLine(Countries.Count());