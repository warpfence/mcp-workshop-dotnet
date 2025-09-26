
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
	private static readonly List<string> AsciiArts = new()
	{
		@"  (o\_/o)   ",
		@" (='.'=)    ",
		@"  ( : )     ",
		@"  (o.o)     ",
		@"  ( :3 )    ",
		@"  ( ^.^ )ノ  ",
		@"  (¬‿¬)     "
	};

	static async Task Main(string[] args)
	{
		// MCP 서버에서 원숭이 데이터를 가져오는 함수 (여기서는 샘플 데이터 사용)
		async Task<List<Monkey>> FetchMonkeysAsync()
		{
			// 실제 MCP 서버 연동 시 이 부분을 수정하세요.
			return new List<Monkey>
			{
				new Monkey { Name = "Baboon", Location = "Africa & Asia", Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.", Population = 10000, Latitude = -8.783195, Longitude = 34.508523 },
				new Monkey { Name = "Capuchin Monkey", Location = "Central & South America", Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae.", Population = 23000, Latitude = 12.769013, Longitude = -85.602364 },
				new Monkey { Name = "Blue Monkey", Location = "Central and East Africa", Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa.", Population = 12000, Latitude = 1.957709, Longitude = 37.297204 }
				// ... 필요시 더 추가
			};
		}

		await MonkeyHelper.InitializeAsync(FetchMonkeysAsync);

		bool running = true;
		var rnd = new Random();
		while (running)
		{
			Console.Clear();
			// 무작위로 ASCII art 출력
			Console.WriteLine(AsciiArts[rnd.Next(AsciiArts.Count)]);
			Console.WriteLine("\n==== Monkey Console App ====");
			Console.WriteLine("1. List all monkeys");
			Console.WriteLine("2. Get details for a specific monkey by name");
			Console.WriteLine("3. Get a random monkey");
			Console.WriteLine("4. Exit app");
			Console.Write("Select an option: ");
			var input = Console.ReadLine();

			switch (input)
			{
				case "1":
					ListAllMonkeys();
					break;
				case "2":
					GetMonkeyByName();
					break;
				case "3":
					GetRandomMonkey();
					break;
				case "4":
					running = false;
					Console.WriteLine("Goodbye!");
					break;
				default:
					Console.WriteLine("Invalid option. Press any key to continue...");
					Console.ReadKey();
					break;
			}
		}
	}

	static void ListAllMonkeys()
	{
		var monkeys = MonkeyHelper.GetMonkeys();
		Console.WriteLine("\nAvailable Monkeys:");
		Console.WriteLine("-------------------------------------------------------------");
		Console.WriteLine($"{nameof(Monkey.Name),-20} {nameof(Monkey.Location),-25} {nameof(Monkey.Population),10}");
		Console.WriteLine("-------------------------------------------------------------");
		foreach (var m in monkeys)
		{
			Console.WriteLine($"{m.Name,-20} {m.Location,-25} {m.Population,10}");
		}
		Console.WriteLine("-------------------------------------------------------------");
		Console.WriteLine("Press any key to return to menu...");
		Console.ReadKey();
	}

	static void GetMonkeyByName()
	{
		Console.Write("Enter monkey name: ");
		var name = Console.ReadLine();
		var monkey = MonkeyHelper.GetMonkeyByName(name ?? string.Empty);
		if (monkey != null)
		{
			Console.WriteLine($"\nName: {monkey.Name}\nLocation: {monkey.Location}\nPopulation: {monkey.Population}\nDetails: {monkey.Details}");
		}
		else
		{
			Console.WriteLine("Monkey not found.");
		}
		Console.WriteLine("Press any key to return to menu...");
		Console.ReadKey();
	}

	static void GetRandomMonkey()
	{
		var monkey = MonkeyHelper.GetRandomMonkey();
		if (monkey != null)
		{
			Console.WriteLine($"\nRandom Monkey: {monkey.Name}\nLocation: {monkey.Location}\nPopulation: {monkey.Population}\nDetails: {monkey.Details}");
			Console.WriteLine($"(Random monkey picked {MonkeyHelper.GetRandomPickCount()} times)");
		}
		else
		{
			Console.WriteLine("No monkeys available.");
		}
		Console.WriteLine("Press any key to return to menu...");
		Console.ReadKey();
	}
}
