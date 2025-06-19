# RPN Hesap Makinesi

Bu proje, Reverse Polish Notation (RPN) kullanarak matematiksel iþlemleri gerçekleþtiren bir hesap makinesi uygulamasýdýr. 
RPN, operatörlerin operandlardan sonra yazýldýðý bir matematiksel notasyon sistemidir.

## Ýçindekiler

- [RPN Nedir?](#rpn-nedir)
- [Özellikler](#özellikler)
- [Kurulum](#kurulum)
- [Kullaným](#kullaným)
- [Sýnýf Yapýsý](#sýnýf-yapýsý)
- [Örnekler](#örnekler)
- [Hata Yönetimi](#hata-yönetimi)
- [Log Dosyasý](#log-dosyasý)

## RPN Nedir?

Reverse Polish Notation (RPN) veya Postfix Notation, matematiksel ifadelerin operatörlerin operandlardan sonra yazýldýðý bir gösterim biçimidir.

Örnek:
- Normal notasyon: '3 + 4'
- RPN notasyonu: '3 4 +'

Avantajlarý:
- Parantez kullanýmýna gerek yok
- Ýþlem önceliði belirsizliði yok
- Yýðýn (stack) tabanlý basit hesaplama

## Özellikler

- Temel Matematiksel Ýþlemler: Toplama (+), Çýkarma (-), Çarpma (*), Bölme (/)
- Yýðýn Tabanlý Hesaplama: RPN standardýna uygun algoritma
- Kapsamlý Hata Yönetimi: Kullanýcý tanýmlý ve sistem hatalarý
- Log Sistemi: Tüm hatalarýn dosyaya kaydedilmesi
- Konsol Tabanlý Arayüz: Kullanýcý dostu etkileþim
- Geniþletilebilir Yapý: Yeni operatörler kolayca eklenebilir

## Kurulum

### Gereksinimler
- .NET Framework 4.7.2 veya üzeri
- Visual Studio 2019+ veya .NET CLI

### Kurulum Adýmlarý

1. Proje klasörünü oluþturun:
   ```bash
   mkdir RPN-Calculator
   cd RPN-Calculator
   ```

2. Yeni konsol uygulamasý oluþturun:
   ```bash
   dotnet new console
   ```

3. Program.cs dosyasýnýn içeriðini verilen kod ile deðiþtirin

4. Uygulamayý derleyin:
   ```bash
   dotnet build
   ```

5. Uygulamayý çalýþtýrýn:
   ```bash
   dotnet run
   ```

## Kullaným

### Temel Kullaným

1. Uygulamayý baþlattýðýnýzda karþýlama mesajý görünür
2. RPN formatýnda ifadenizi girin (sayýlar ve operatörleri boþlukla ayýrýn)
3. Enter tuþuna basýn
4. Sonucu görün
5. Çýkmak için 'exit' yazýn

### Girdi Formatý

- Sayýlar: Ondalýk sayýlar desteklenir (örn: 3.14, -5, 100)
- Operatörler: +, -, *, /
- Ayýrýcý: Boþluk veya virgül
- Örnek: '3 4 +' veya '3,4,+'

## Sýnýf Yapýsý

### UML Sýnýf Diyagramý Uygulamasý

```
Calculator (Ana Sýnýf)
??? Stack (Yýðýn Yapýsý)
??? CalculatorGui (Kullanýcý Arayüzü)
??? Operand (Sayý Temsili)
??? Operator (Soyut Operatör Sýnýfý)
    ??? AddOperator (+)
    ??? SubtractOperator (-)
    ??? MultiplyOperator (*)
    ??? DivideOperator (/)
```

### Sýnýf Açýklamalarý

- Calculator: Ana hesaplama mantýðýný yöneten sýnýf
- Stack: Operandlarý saklayan yýðýn yapýsý
- Operand: Sayýsal deðerleri temsil eden sýnýf
- Operator: Tüm operatörler için soyut temel sýnýf
- CalculatorGui: Kullanýcý etkileþimi için arayüz sýnýfý

## Örnekler

### Basit Ýþlemler

|   Girdi   | Normal Notasyon |  Sonuç  |
|-----------|-----------------|---------|
| '3 4 +'   |     '3 + 4'     |    7    |
| '10 5 -'  |     '10 - 5'    |    5    |
| '6 7 *'   |     '6 × 7'     |    42   |
| '20 4 /'  |     '20 ÷ 4'    |    5    |

### Karmaþýk Ýþlemler

| Girdi | Normal Notasyon | Sonuç |
|-------|----------------|--------|
| '3 4 2 + -' | '3 - (4 + 2)' | -3 |
| '2 3 4 5 * + -' | '2 - (3 + (4 × 5))' | -21 |
| '15 7 1 1 + - / 3 * 2 1 1 + + -' | '((15 ÷ (7 - (1 + 1))) × 3) - (2 + (1 + 1))' | 5 |

### Adýmlý Hesaplama Örneði

Girdi: '15 7 1 1 + - / 3 * 2 1 1 + + -'

```
Adým 1: 15 7 1 1 +     ? 15 7 1 2      (1+1=2)
Adým 2: 15 7 2 -       ? 15 5          (7-2=5)
Adým 3: 15 5 /         ? 3             (15/5=3)
Adým 4: 3 3 *          ? 9             (3*3=9)
Adým 5: 9 2 1 1 +      ? 9 2 2         (1+1=2)
Adým 6: 9 2 2 +        ? 9 4           (2+2=4)
Adým 7: 9 4 -          ? 5             (9-4=5)
```

## Hata Yönetimi

### Kullanýcý Tanýmlý Hatalar

- InsufficientOperandsException: Operatör için yeterli operand yok
- InvalidOperatorException: Tanýnmayan operatör veya token
- TooManyOperandsException: Ýþlem sonunda yýðýnda fazla operand kaldý

### Sistem Hatalarý

- DivideByZeroException: Sýfýra bölme hatasý
- FormatException: Geçersiz sayý formatý
- ArgumentException: Boþ veya geçersiz girdi

### Hata Örnekleri

```
Girdi: "3 +"           ? Hata: '+' operatörü için yeterli operand yok
Girdi: "3 4 &"         ? Hata: Tanýnmayan token: '&'
Girdi: "3 4"           ? Hata: Eksik operatör - yýðýnda fazla operand kaldý
Girdi: "5 0 /"         ? Hata: Sýfýra bölme hatasý
```

## Log Dosyasý

Tüm hatalar 'calculator_log.txt' dosyasýna otomatik olarak kaydedilir:

```
[2025-06-19 20:56] DivideByZeroException: Sýfýra bölme hatasý
[2025-06-19 20:56] InvalidOperatorException: Tanýnmayan token: '&'
[2025-06-19 20:56] InsufficientOperandsException: '+' operatörü için yeterli operand yok
```

## Geliþtirme

### Yeni Operatör Ekleme

Yeni bir operatör eklemek için 'Operator' sýnýfýndan türetin:

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

Sonra 'Calculator' sýnýfýnýn 'InitializeOperators' metoduna ekleyin:

```csharp
operators.Add("^", new PowerOperator());
```

### GUI Geliþtirme

'CalculatorGui' sýnýfýný geniþleterek Windows Forms veya WPF arayüzü ekleyebilirsiniz.

## Lisans

Bu proje eðitim amaçlý geliþtirilmiþtir ve MIT lisansý altýnda daðýtýlmaktadýr.

## Katkýda Bulunma

1. Projeyi fork edin
2. Feature branch oluþturun ('git checkout -b feature/YeniOzellik')
3. Deðiþikliklerinizi commit edin ('git commit -am 'Yeni özellik eklendi'')
4. Branch'inizi push edin ('git push origin feature/YeniOzellik')
5. Pull Request oluþturun

## Ýletiþim

Sorularýnýz için lütfen iletiþime geçin veya issue açýn.

---

Not: Bu README dosyasý projenin tam dokümantasyonunu içermektedir. Herhangi bir sorunla karþýlaþtýðýnýzda önce bu dokümantasyonu kontrol edin.