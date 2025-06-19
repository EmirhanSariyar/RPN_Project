# RPN Hesap Makinesi

Bu proje, Reverse Polish Notation (RPN) kullanarak matematiksel i�lemleri ger�ekle�tiren bir hesap makinesi uygulamas�d�r. 
RPN, operat�rlerin operandlardan sonra yaz�ld��� bir matematiksel notasyon sistemidir.

## ��indekiler

- [RPN Nedir?](#rpn-nedir)
- [�zellikler](#�zellikler)
- [Kurulum](#kurulum)
- [Kullan�m](#kullan�m)
- [S�n�f Yap�s�](#s�n�f-yap�s�)
- [�rnekler](#�rnekler)
- [Hata Y�netimi](#hata-y�netimi)
- [Log Dosyas�](#log-dosyas�)

## RPN Nedir?

Reverse Polish Notation (RPN) veya Postfix Notation, matematiksel ifadelerin operat�rlerin operandlardan sonra yaz�ld��� bir g�sterim bi�imidir.

�rnek:
- Normal notasyon: '3 + 4'
- RPN notasyonu: '3 4 +'

Avantajlar�:
- Parantez kullan�m�na gerek yok
- ��lem �nceli�i belirsizli�i yok
- Y���n (stack) tabanl� basit hesaplama

## �zellikler

- Temel Matematiksel ��lemler: Toplama (+), ��karma (-), �arpma (*), B�lme (/)
- Y���n Tabanl� Hesaplama: RPN standard�na uygun algoritma
- Kapsaml� Hata Y�netimi: Kullan�c� tan�ml� ve sistem hatalar�
- Log Sistemi: T�m hatalar�n dosyaya kaydedilmesi
- Konsol Tabanl� Aray�z: Kullan�c� dostu etkile�im
- Geni�letilebilir Yap�: Yeni operat�rler kolayca eklenebilir

## Kurulum

### Gereksinimler
- .NET Framework 4.7.2 veya �zeri
- Visual Studio 2019+ veya .NET CLI

### Kurulum Ad�mlar�

1. Proje klas�r�n� olu�turun:
   ```bash
   mkdir RPN-Calculator
   cd RPN-Calculator
   ```

2. Yeni konsol uygulamas� olu�turun:
   ```bash
   dotnet new console
   ```

3. Program.cs dosyas�n�n i�eri�ini verilen kod ile de�i�tirin

4. Uygulamay� derleyin:
   ```bash
   dotnet build
   ```

5. Uygulamay� �al��t�r�n:
   ```bash
   dotnet run
   ```

## Kullan�m

### Temel Kullan�m

1. Uygulamay� ba�latt���n�zda kar��lama mesaj� g�r�n�r
2. RPN format�nda ifadenizi girin (say�lar ve operat�rleri bo�lukla ay�r�n)
3. Enter tu�una bas�n
4. Sonucu g�r�n
5. ��kmak i�in 'exit' yaz�n

### Girdi Format�

- Say�lar: Ondal�k say�lar desteklenir (�rn: 3.14, -5, 100)
- Operat�rler: +, -, *, /
- Ay�r�c�: Bo�luk veya virg�l
- �rnek: '3 4 +' veya '3,4,+'

## S�n�f Yap�s�

### UML S�n�f Diyagram� Uygulamas�

```
Calculator (Ana S�n�f)
??? Stack (Y���n Yap�s�)
??? CalculatorGui (Kullan�c� Aray�z�)
??? Operand (Say� Temsili)
??? Operator (Soyut Operat�r S�n�f�)
    ??? AddOperator (+)
    ??? SubtractOperator (-)
    ??? MultiplyOperator (*)
    ??? DivideOperator (/)
```

### S�n�f A��klamalar�

- Calculator: Ana hesaplama mant���n� y�neten s�n�f
- Stack: Operandlar� saklayan y���n yap�s�
- Operand: Say�sal de�erleri temsil eden s�n�f
- Operator: T�m operat�rler i�in soyut temel s�n�f
- CalculatorGui: Kullan�c� etkile�imi i�in aray�z s�n�f�

## �rnekler

### Basit ��lemler

|   Girdi   | Normal Notasyon |  Sonu�  |
|-----------|-----------------|---------|
| '3 4 +'   |     '3 + 4'     |    7    |
| '10 5 -'  |     '10 - 5'    |    5    |
| '6 7 *'   |     '6 � 7'     |    42   |
| '20 4 /'  |     '20 � 4'    |    5    |

### Karma��k ��lemler

| Girdi | Normal Notasyon | Sonu� |
|-------|----------------|--------|
| '3 4 2 + -' | '3 - (4 + 2)' | -3 |
| '2 3 4 5 * + -' | '2 - (3 + (4 � 5))' | -21 |
| '15 7 1 1 + - / 3 * 2 1 1 + + -' | '((15 � (7 - (1 + 1))) � 3) - (2 + (1 + 1))' | 5 |

### Ad�ml� Hesaplama �rne�i

Girdi: '15 7 1 1 + - / 3 * 2 1 1 + + -'

```
Ad�m 1: 15 7 1 1 +     ? 15 7 1 2      (1+1=2)
Ad�m 2: 15 7 2 -       ? 15 5          (7-2=5)
Ad�m 3: 15 5 /         ? 3             (15/5=3)
Ad�m 4: 3 3 *          ? 9             (3*3=9)
Ad�m 5: 9 2 1 1 +      ? 9 2 2         (1+1=2)
Ad�m 6: 9 2 2 +        ? 9 4           (2+2=4)
Ad�m 7: 9 4 -          ? 5             (9-4=5)
```

## Hata Y�netimi

### Kullan�c� Tan�ml� Hatalar

- InsufficientOperandsException: Operat�r i�in yeterli operand yok
- InvalidOperatorException: Tan�nmayan operat�r veya token
- TooManyOperandsException: ��lem sonunda y���nda fazla operand kald�

### Sistem Hatalar�

- DivideByZeroException: S�f�ra b�lme hatas�
- FormatException: Ge�ersiz say� format�
- ArgumentException: Bo� veya ge�ersiz girdi

### Hata �rnekleri

```
Girdi: "3 +"           ? Hata: '+' operat�r� i�in yeterli operand yok
Girdi: "3 4 &"         ? Hata: Tan�nmayan token: '&'
Girdi: "3 4"           ? Hata: Eksik operat�r - y���nda fazla operand kald�
Girdi: "5 0 /"         ? Hata: S�f�ra b�lme hatas�
```

## Log Dosyas�

T�m hatalar 'calculator_log.txt' dosyas�na otomatik olarak kaydedilir:

```
[2025-06-19 20:56] DivideByZeroException: S�f�ra b�lme hatas�
[2025-06-19 20:56] InvalidOperatorException: Tan�nmayan token: '&'
[2025-06-19 20:56] InsufficientOperandsException: '+' operat�r� i�in yeterli operand yok
```

## Geli�tirme

### Yeni Operat�r Ekleme

Yeni bir operat�r eklemek i�in 'Operator' s�n�f�ndan t�retin:

```csharp
public class PowerOperator : Operator
{
    public override string Symbol => "^";
    public override int OperandCount => 2;

    public override Operand Execute(params Operand[] operands)
    {
        return new Operand(Math.Pow(operands[0].Value, operands[1].Value));
    }
}
```

Sonra 'Calculator' s�n�f�n�n 'InitializeOperators' metoduna ekleyin:

```csharp
operators.Add("^", new PowerOperator());
```

### GUI Geli�tirme

'CalculatorGui' s�n�f�n� geni�leterek Windows Forms veya WPF aray�z� ekleyebilirsiniz.

## Lisans

Bu proje e�itim ama�l� geli�tirilmi�tir ve MIT lisans� alt�nda da��t�lmaktad�r.

## Katk�da Bulunma

1. Projeyi fork edin
2. Feature branch olu�turun ('git checkout -b feature/YeniOzellik')
3. De�i�ikliklerinizi commit edin ('git commit -am 'Yeni �zellik eklendi'')
4. Branch'inizi push edin ('git push origin feature/YeniOzellik')
5. Pull Request olu�turun

## �leti�im

Sorular�n�z i�in l�tfen ileti�ime ge�in veya issue a��n.

---

Not: Bu README dosyas� projenin tam dok�mantasyonunu i�ermektedir. Herhangi bir sorunla kar��la�t���n�zda �nce bu dok�mantasyonu kontrol edin.