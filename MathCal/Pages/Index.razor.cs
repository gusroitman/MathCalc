using Microsoft.VisualBasic;
using MudBlazor;
using System.ComponentModel;
using System.Linq;
using String = System.String;

namespace MathCal.Pages
{
    public partial class Index
    {
        public string? input { get; set; }

        private List<string>? output = new List<string>();
        public string? outputVisor { get; set; }

        public char[] operators = { '+', '-', '*', '/', '\t' };

        /*
        public void ChangeValue()
        {
            if (!String.IsNullOrEmpty(input))
            {
                _ = (input.StartsWith("-")) ? input = input[1..] : input = input.Insert(0, "-");
            }
        }
        */
        public void DelOutput()
        {
            if (output is not null)
            {
                if (output.Any()) //prevent IndexOutOfRangeException for empty list
                {
                    output.RemoveAt(output.Count - 1);
                }
                outputVisor = string.Format("[{0}]", string.Join("; ", output));
            }
        }
        public void Add(int numero)
        {
            if (!String.IsNullOrEmpty(input))
            {
                _ = (input.Length < 13) ? input += numero.ToString() : "";
            }
            else
            {
                input = numero.ToString();
            }
        }
        void AddOutput()
        {
            if (!String.IsNullOrEmpty(input) && !operators.Any(input.Contains))
            {

                if (outputVisor is null)
                {
                    output.Add(input);
                    outputVisor = string.Format("[{0}]", string.Join("; ", output));
                    input = string.Empty;
                }
                else if (outputVisor.Length < 29)
                {
                    output.Add(input);
                    outputVisor = string.Format("[{0}]", string.Join("; ", output));
                    input = string.Empty;
                }
                else
                {
                    input = "Max reached";
                    ChangeStringAfterDelay();
                }
            }
        }
        public async void ChangeStringAfterDelay()
        {
            await Task.Delay(500); // Delay for 0,5 second
            input = string.Empty;
        }
        void AddPoint()
        {
            if (input is not null && !input.Contains(','))
            {
                input += ",";
            }
            else if (input is not null && input.Contains(','))
            {
                var checkPoint = input.Split(operators, StringSplitOptions.TrimEntries);

                if (!checkPoint.Last().Contains(','))
                {
                    input += ",";
                }
            }
        }
        void Clear()
        {
            input = string.Empty;

            if (outputVisor is not null)
            {
                outputVisor = string.Empty;
                output?.Clear();
            }
        }
        void Del()
        {
            if (!String.IsNullOrEmpty(input))
            {
                input = input.Remove(input.Length - 1, 1);
            }
        }
        void Mas()
        {
            if (!String.IsNullOrEmpty(input) && !operators.Any(input.Contains))
            {
                if (char.IsDigit(input, input.Length - 1))
                {
                    input += "+";
                }
            }
        }
        void Menos()
        {
            if (!String.IsNullOrEmpty(input) && !operators.Any(input.Contains))
            {
                if (char.IsDigit(input, input.Length - 1))
                {
                    input += "-";
                }
            }
        }
        void Multi()
        {
            if (!String.IsNullOrEmpty(input) && !operators.Any(input.Contains))
            {
                if (char.IsDigit(input, input.Length - 1))
                {
                    input += "*";
                }
            }
        }
        void Division()
        {
            if (!String.IsNullOrEmpty(input) && !operators.Any(input.Contains))
            {
                if (char.IsDigit(input, input.Length - 1))
                {
                    input += "/";
                }
            }
        }
        void Igual()
        {
            if (!System.String.IsNullOrEmpty(input))
            {
                var array = input.Split(operators, StringSplitOptions.RemoveEmptyEntries);

                array = array.Select(x => x.Replace(".", ",")).ToArray();

                foreach (char c in operators)
                {
                    if (input.Contains(c))
                    {
                        var resulta = c switch
                        {
                            '+' => IndexHelpers.Plus(TConverter.ChangeType<double>(array.First()), TConverter.ChangeType<double>(array.Last())),
                            '-' => IndexHelpers.Minus(TConverter.ChangeType<double>(array.First()), TConverter.ChangeType<double>(array.Last())),
                            '*' => IndexHelpers.Times(TConverter.ChangeType<double>(array.First()), TConverter.ChangeType<double>(array.Last())),
                            '/' => IndexHelpers.Divide(TConverter.ChangeType<double>(array.First()), TConverter.ChangeType<double>(array.Last())),
                            _ => throw new NotImplementedException(),
                        };

                        input = resulta.ToString("#.##");
                        AddOutput();
                        ChangeStringAfterDelay();
                    }
                }
            }
        }
        void Suma()
        {
            if (output is not null)
            {
                var arr = output.Select(Double.Parse).ToArray();

                var result = IndexHelpers.AddAll(arr);

                input = result.ToString();
            }
        }
        void Average()
        {
            if (output is not null)
            {
                var arr = output.Select(Double.Parse).ToArray();

                var result = IndexHelpers.AvgAll(arr);

                input = result.ToString();
            }
        }
        void Minim()
        {
            if (output is not null)
            {
                var arr = output.Select(Double.Parse).ToArray();

                var result = IndexHelpers.Min(arr);

                input = result.ToString();
            }
        }
        void Maxim()
        {
            if (output is not null)
            {
                var arr = output.Select(Double.Parse).ToArray();

                var result = IndexHelpers.Max(arr);

                input = result.ToString();
            }
        }
        void Rand()
        {
            if (output is not null)
            {
                var arr = output.Select(Double.Parse).ToArray();

                var result = IndexHelpers.Rand(arr);

                input = result.ToString();
            }
        }
    }
    /*
    public static TResult SumOperator<T, TResult>(IEnumerable<T> values)
        where T : INumber<T>
        where TResult : INumber<TResult>
    {
        TResult result = TResult.Zero;

        foreach (var value in values)
        {
            result += TResult.CreateChecked(value);
        }

        return result;
    }

    public static TResult AverageOperator<T, TResult>(IEnumerable<T> values)
        where T : INumber<T>
        where TResult : INumber<TResult>
    {
        TResult sum = SumOperator<T, TResult>(values);
        return TResult.CreateChecked(sum) / TResult.CreateChecked(values.Count());
    }

            T IsBiggerOrSum<T>(T[] values, T cmpMax) where T : INumber<T>
        {
            T sum = T.Zero;
            T max = T.Zero;

            foreach (T value in values)
            {
                sum += value;
                if (value.CompareTo(max) > 0)
                    max = value;
            }
            return sum > cmpMax ? sum : max;
        }
    */

}
