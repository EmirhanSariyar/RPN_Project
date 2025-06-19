using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

// Kullanıcı tanımlı istisnalar
public class InsufficientOperandsException : Exception
{
    public InsufficientOperandsException(string message) : base(message) { }
}

public class InvalidOperatorException : Exception
{
    public InvalidOperatorException(string message) : base(message) { }
}

public class TooManyOperandsException : Exception
{
    public TooManyOperandsException(string message) : base(message) { }
}

// Operand sınıfı - sayıları temsil eder
public class Operand
{
    public double Value { get; set; }

    public Operand(double value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

// Soyut Operator sınıfı
public abstract class Operator
{
    public abstract string Symbol { get; }
    public abstract int OperandCount { get; }
    public abstract Operand Execute(params Operand[] operands);
}

// Toplama operatörü
public class AddOperator : Operator
{
    public override string Symbol => "+";
    public override int OperandCount => 2;

    public override Operand Execute(params Operand[] operands)
    {
        if (operands.Length < OperandCount)
            throw new InsufficientOperandsException($"'{Symbol}' operatörü için {OperandCount} operand gerekli");

        return new Operand(operands[0].Value + operands[1].Value);
    }
}

// Çıkarma operatörü
public class SubtractOperator : Operator
{
    public override string Symbol => "-";
    public override int OperandCount => 2;

    public override Operand Execute(params Operand[] operands)
    {
        if (operands.Length < OperandCount)
            throw new InsufficientOperandsException($"'{Symbol}' operatörü için {OperandCount} operand gerekli");

        return new Operand(operands[0].Value - operands[1].Value);
    }
}

// Çarpma operatörü
public class MultiplyOperator : Operator
{
    public override string Symbol => "*";
    public override int OperandCount => 2;

    public override Operand Execute(params Operand[] operands)
    {
        if (operands.Length < OperandCount)
            throw new InsufficientOperandsException($"'{Symbol}' operatörü için {OperandCount} operand gerekli");

        return new Operand(operands[0].Value * operands[1].Value);
    }
}

// Bölme operatörü
public class DivideOperator : Operator
{
    public override string Symbol => "/";
    public override int OperandCount => 2;

    public override Operand Execute(params Operand[] operands)
    {
        if (operands.Length < OperandCount)
            throw new InsufficientOperandsException($"'{Symbol}' operatörü için {OperandCount} operand gerekli");

        if (operands[1].Value == 0)
            throw new DivideByZeroException("Sıfıra bölme hatası");

        return new Operand(operands[0].Value / operands[1].Value);
    }
}

// Stack sınıfı - yığın yapısı
public class Stack
{
    private Stack<Operand> stack;

    public Stack()
    {
        stack = new Stack<Operand>();
    }

    public void Push(Operand operand)
    {
        stack.Push(operand);
    }

    public Operand Pop()
    {
        if (stack.Count == 0)
            throw new InvalidOperationException("Yığın boş - çıkarılacak operand yok");

        return stack.Pop();
    }

    public int Count => stack.Count;

    public bool IsEmpty => stack.Count == 0;

    public void Clear()
    {
        stack.Clear();
    }
}

// GUI sınıfı - kullanıcı etkileşimi
public class CalculatorGui
{
    public string GetInput()
    {
        Console.Write("RPN ifadesi girin (çıkmak için 'exit'): ");
        return Console.ReadLine();
    }

    public void ShowResult(double result)
    {
        Console.WriteLine($"Sonuç: {result}");
    }

    public void ShowError(string errorMessage)
    {
        Console.WriteLine($"Hata: {errorMessage}");
    }

    public void ShowWelcome()
    {
        Console.WriteLine("=== RPN Hesap Makinesi ===");
        Console.WriteLine("Kullanım: Sayıları ve operatörleri boşlukla ayırarak girin");
        Console.WriteLine("Örnek: 3 4 + (sonuç: 7)");
        Console.WriteLine("Desteklenen operatörler: +, -, *, /");
        Console.WriteLine();
    }
}

// Ana hesap makinesi sınıfı
public class Calculator
{
    private Stack stack;
    private CalculatorGui gui;
    private Dictionary<string, Operator> operators;
    private const string LogFile = "calculator_log.txt";

    public Calculator()
    {
        stack = new Stack();
        gui = new CalculatorGui();
        InitializeOperators();
    }

    private void InitializeOperators()
    {
        operators = new Dictionary<string, Operator>
        {
            { "+", new AddOperator() },
            { "-", new SubtractOperator() },
            { "*", new MultiplyOperator() },
            { "/", new DivideOperator() }
        };
    }

    public double Calculate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException("Boş ifade");

        stack.Clear();
        string[] tokens = expression.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string token in tokens)
        {
            ProcessToken(token.Trim());
        }

        if (stack.Count != 1)
        {
            if (stack.Count == 0)
                throw new InvalidOperationException("Geçersiz ifade - sonuç bulunamadı");
            else
                throw new TooManyOperandsException("Eksik operatör - yığında fazla operand kaldı");
        }

        return stack.Pop().Value;
    }

    private void ProcessToken(string token)
    {
        if (operators.ContainsKey(token))
        {
            ProcessOperator(token);
        }
        else if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out double number))
        {
            stack.Push(new Operand(number));
        }
        else
        {
            throw new InvalidOperatorException($"Tanınmayan token: '{token}'");
        }
    }

    private void ProcessOperator(string operatorSymbol)
    {
        Operator op = operators[operatorSymbol];

        if (stack.Count < op.OperandCount)
        {
            throw new InsufficientOperandsException($"'{operatorSymbol}' operatörü için yeterli operand yok");
        }

        Operand[] operands = new Operand[op.OperandCount];

        // Yığından operandları al (ters sırada)
        for (int i = op.OperandCount - 1; i >= 0; i--)
        {
            operands[i] = stack.Pop();
        }

        Operand result = op.Execute(operands);
        stack.Push(result);
    }

    private void LogError(Exception ex)
    {
        try
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex.GetType().Name}: {ex.Message}";
            File.AppendAllText(LogFile, logEntry + Environment.NewLine);
        }
        catch
        {
            // Log yazma hatası durumunda sessizce devam et
        }
    }

    public void Run()
    {
        gui.ShowWelcome();

        while (true)
        {
            try
            {
                string input = gui.GetInput();

                if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "exit")
                {
                    Console.WriteLine("Çıkılıyor...");
                    break;
                }

                double result = Calculate(input);
                gui.ShowResult(result);
            }
            catch (Exception ex)
            {
                gui.ShowError(ex.Message);
                LogError(ex);
            }

            Console.WriteLine(); // Boş satır ekle
        }
    }
}

// Program giriş noktası
public class code
{
    public static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        calculator.Run();
    }
}