using JsonSerializer;
using JsonSerializer.Demo;

var obj = new { Name = "Hey" , Krunky = "Test" };

string[] names = { "Hey", "Name" }; //not implemented yet
var testObj = new { Name = names };
var list = new List<TestClass>();
list.Add(new TestClass()
{
    Name = "Jungli",
    Description = "Test",
});

var testObj2 = new { Class = list, Name = "Hey", Count = 10 };

var json = JSONizer.Serialize(testObj2);
Console.WriteLine(json);