using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace ConsoleApp4
{
    //Используйте CSharpScript, чтобы позволить пользователю выполнять некоторые операции с текстом:
    //подсчитывать количество слов или символов.Саму переменную с текстом, определить на уровне класса
    //Program(использовать как глобальное значение).
    public class Program
    {
        public static string text;

       static async Task Main()
        {
            text = "Hello world";
            await CountWords(text);
            await CountCharacters(text);
        }



        static async Task CountWords(string text)
        {
            var options = ScriptOptions.Default.AddReferences(typeof(Program).Assembly)
                .AddImports("System", "System.Linq", "ConsoleApp4");

            var script = CSharpScript.Create<int>("Program.text.Split(new char[] { ' ', '.', ',', '!', '?'}, StringSplitOptions.RemoweEmptyEntries).Count()", options, typeof(string));

            var result = await script.RunAsync(globals: text);
            Console.WriteLine($"Words count: {result.ReturnValue}");
        }

        static async Task CountCharacters(string text)
        {
            var options = ScriptOptions.Default.AddReferences(typeof(Program).Assembly)
                .AddImports("System", "System.Linq", "ConsoleApp4");

            var script = CSharpScript.Create<int>("Program.text.Length", options, typeof(string));

            var result = await script.RunAsync(globals: text);
            Console.WriteLine($"Symbols count: {result.ReturnValue}");
        }
    }
}
