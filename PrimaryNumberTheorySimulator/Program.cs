using DynamicExpresso;
using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;

namespace PrimaryNumberTheory
{
    public partial class InfiniteSet<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
				yield return currentNumber = nextNumberGenerator(currentNumber);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
			while (true)
			{
				yield return currentNumber = nextNumberGenerator(currentNumber);
			}
		}

        public InfiniteSet(T firstNumber, Func<T, T> getNextNumber)
        {
			currentNumber = firstNumber;
			nextNumberGenerator = getNextNumber;
        }
		private static T currentNumber;
		private static Func<T, T> nextNumberGenerator;
    }
    public class PrimaryNumberTheorySimulator
	{
		public static void REPL()
		{
			string[] outList = new string[256];
			Interpreter interpreter = new Interpreter();
			//foreach(double i in )
			interpreter.SetVariable("odds", new HashSet<double>());
			
			for (int case_count = 0; ; case_count++)
			{
				Console.Write($"In[{case_count}]: ");
				string? line = Console.ReadLine();
				Console.Write($"Out[{case_count}]: ");
				if (line == null) continue;
				line = line.Trim();
				for(int i = 1; i < line.Length - 1; i++)
                {
					bool l = char.IsLetter(line[i - 1]);
					bool r = char.IsLetter(line[i + 1]);
					if (line[i] == ' ' && !(l && r))
					{
                        line = line.Remove(i, 1);
                    }
                }
				string[] tokens = line.Split(" ".ToCharArray());
				if (tokens[0] == "def")
				{
					string functionName = tokens[1].Substring(0, tokens[1].IndexOf('('));
					string functionBody = tokens[1].Substring(tokens[1].IndexOf('=') + 1);
					string parameterName = tokens[1].Substring(functionName.Length + 1, tokens[1].IndexOf(')') - functionName.Length - 1);
					//Console.WriteLine($"{functionName} {parameterName} {functionBody}");
					interpreter.SetFunction(functionName, interpreter.ParseAsDelegate<Func<double, double>>(functionBody, new string[1] { parameterName }));
					Console.WriteLine($"Function: {functionName}, Parameters: {parameterName}, Function expression: {functionBody}, ok.");
					outList.Append(functionName);
                }
                else
                {
					object result = interpreter.Eval(line);
					Console.WriteLine(result);
					outList.Append(result);
                }
				interpreter.SetVariable("Out", outList);
			}
		}
		public static void Main(string[] args)
		{
			
			REPL();
		}
	}
}