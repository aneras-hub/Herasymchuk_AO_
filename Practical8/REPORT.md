
# REPORT.md
1. Які класи використовуються для роботи з файловою системою в C#? Наведіть приклади
У C# для роботи з файловою системою використовуються класи з простору імен System.IO.
Основні класи:
File — робота з файлами;
FileInfo — інформація про конкретний файл;
Directory — робота з папками;
DirectoryInfo — інформація про конкретну папку;
StreamReader — потокове читання тексту;
StreamWriter — потоковий запис тексту.
У моїй програмі використовувались:
File.WriteAllText(filePath, json, Encoding.UTF8);
File.ReadAllText(filePath, Encoding.UTF8);
Directory.CreateDirectory("Backups");
File.Copy(sourcePath, backupPath, true);

2. У чому різниця між File/FileInfo та Directory/DirectoryInfo?
File і Directory — це статичні класи. Їх зручно використовувати, коли потрібно швидко виконати одну дію: створити файл, прочитати файл, створити папку, отримати список файлів.
FileInfo і DirectoryInfo — це класи-об’єкти. Їх зручно використовувати, коли потрібно багато разів працювати з одним конкретним файлом або папкою.
У моїй програмі FileInfo використовувався при очищенні старих резервних копій:
FileInfo info = new FileInfo(file);
if (info.CreationTime < DateTime.Now.AddDays(-daysOld))
{
    info.Delete();
}

3. Поясніть роботу StreamReader та StreamWriter. Коли їх краще використовувати?
StreamReader використовується для потокового читання текстових файлів. Його краще використовувати, коли файл великий і його не потрібно повністю завантажувати в пам’ять.
StreamWriter використовується для потокового запису тексту у файл. Його зручно використовувати для створення звітів, логів або поступового запису даних.
У моїй програмі StreamWriter використано для збереження текстового звіту:
public void SaveToText(string content, string filePath)
{
    using StreamWriter writer =
        new StreamWriter(filePath, false, Encoding.UTF8);

    writer.Write(content);
}

4. Як працює серіалізація об’єктів через System.Text.Json? Які атрибути ви використовували?
Серіалізація — це перетворення об’єкта C# у текстовий формат JSON. Десеріалізація — це зворотний процес, коли JSON-файл перетворюється назад в об’єкт.
У програмі для цього використовувався JsonSerializer:
string json = JsonSerializer.Serialize(data, options);
return JsonSerializer.Deserialize<T>(json, options);
Також використовувались налаштування:
JsonSerializerOptions options = new JsonSerializerOptions
{
    WriteIndented = true
};
WriteIndented = true робить JSON-файл відформатованим і читабельним.
У попередніх частинах програми також використовувався атрибут:
[JsonIgnore]
Він потрібний, щоб не серіалізувати складні або службові властивості, наприклад поліморфні колекції.

5. Які винятки можуть виникати при роботі з файлами? Як ви їх обробляли?
При роботі з файлами можуть виникати такі винятки:
FileNotFoundException — файл не знайдено;
IOException — загальна помилка роботи з файлом;
UnauthorizedAccessException — немає доступу до файлу;
JsonException — помилка серіалізації або десеріалізації JSON;
InvalidFileFormatException — власний виняток для неправильного формату файлу.
У програмі було створено власний виняток:
public class InvalidFileFormatException : Exception
Приклад перевірки формату:
if (Path.GetExtension(filePath).ToLower() != ".json")
{
    throw new InvalidFileFormatException(
        "Файл повинен мати формат .json"
    );
}

6. Як ви реалізували резервне копіювання даних?
Резервне копіювання реалізовано у методі CreateBackup(). Спочатку перевіряється, чи існує файл. Потім створюється папка Backups, формується ім’я резервної копії з датою та часом, після чого файл копіюється.
public void CreateBackup(string sourcePath)
{
    if (!File.Exists(sourcePath))
        throw new FileNotFoundException(
            "Файл для резервної копії не знайдено.",
            sourcePath
        );
    Directory.CreateDirectory("Backups");
    string fileName = Path.GetFileNameWithoutExtension(sourcePath);
    string extension = Path.GetExtension(sourcePath);
    string backupPath = Path.Combine(
        "Backups",
        $"{fileName}_backup_{DateTime.Now:yyyyMMdd_HHmmss}{extension}"
    );
    File.Copy(sourcePath, backupPath, true);
}
Також було реалізовано очищення старих бекапів через CleanOldBackups().

7. Які труднощі виникали під час роботи з файлами?
Під час виконання роботи виникали такі труднощі:
програма не знаходила файл через неправильний шлях;
виникали проблеми з кирилицею у назвах файлів і шляхах;
потрібно було правильно відрізняти шлях до папки від шляху до файлу;
при тестуванні винятків FileNotFoundException спочатку перехоплювався як IOException;
потрібно було правильно розмістити .txt та .csv файли у папці запуску програми.
Проблеми були вирішені через:
використання повних шляхів до файлів;
перенесення файлів у bin/Debug/net8.0;
виведення типу помилки через ex.GetType().Name;
корекцію порядку обробки винятків.

8. Що нового ви дізналися під час виконання цієї практичної роботи?
Під час виконання практичної роботи я навчилася працювати з файловою системою в C#.
Було опрацьовано:
збереження та завантаження JSON-файлів;
створення текстових звітів;
експорт та імпорт CSV-файлів;
створення резервних копій;
очищення старих бекапів;
створення папок через Directory;
копіювання, переміщення та видалення файлів;
обробка винятків при роботі з файлами.
Також я краще зрозуміла, чому важливо правильно задавати шлях до файлу та обробляти можливі помилки під час читання й запису даних.