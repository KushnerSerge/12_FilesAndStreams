// See https://aka.ms/new-console-template for more information

List<int> dataSource1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
List<int> dataSource2 = new List<int>() { 1, 3, 5, 8, 9, 10 };
//Method Syntax
var MS = dataSource1.Except(dataSource2).ToList();
foreach (var item in MS)
{
    Console.WriteLine(item);
}
