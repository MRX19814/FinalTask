using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string binaryFilePath = "C:\\Users\\kurga\\Desktop\\qwe\\Students.dat";
            string outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Students";

            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                using (var fs = new FileStream(binaryFilePath, FileMode.OpenOrCreate))
                {
                    Student[] students = (Student[])formatter.Deserialize(fs);
                    Console.WriteLine("Объекты десериализованы");

                    foreach (var student in students)
                    {
                        string groupDirectory = outputDirectory + "\\" + student.Group;

                        if (!Directory.Exists(groupDirectory))
                            Directory.CreateDirectory(groupDirectory);

                        string studentFile = groupDirectory + "\\" + student.Name + ".txt";

                        using (var writer = new StreamWriter(studentFile))
                        {
                            writer.WriteLine($"Имя: {student.Name}");
                            writer.WriteLine($"Дата рождения: {student.DateOfBirth}");
                        }
                    }

                    Console.WriteLine("Данные сохранены в текстовых файлах по группам студентов.");
                }
            }
            catch (SerializationException ex)
            {
                Console.WriteLine("Ошибка десериализации: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}