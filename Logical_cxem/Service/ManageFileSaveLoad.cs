using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Xml.Serialization;

namespace Logical_cxem
{
    public static class ManageFileSaveLoad
    {
        public static void SaveFile(string name, FileData Data)
        {
            var formatter = new XmlSerializer(typeof(FileData));
            using (var fs = new FileStream(name, FileMode.Create))
            {
                formatter.Serialize(fs, Data);
            }
        }

        public static FileData LoadFileFromDB(string text)
        {
            var formatter = new XmlSerializer(typeof(FileData));
            TextReader textReader = new StringReader(text);
            var newFileData = (FileData) formatter.Deserialize(textReader);
            return newFileData;
        }

        public static FileData LoadFile(string name)
        {
            var formatter = new XmlSerializer(typeof(FileData));
            //using (FileStream fs = new FileStream($"{name}.xml", FileMode.OpenOrCreate))
            using (var fs = new FileStream($"{name}", FileMode.OpenOrCreate))
            {
                try
                {
                    var binaryFormatter = new BinaryFormatter();
                    var newFileData = (FileData) formatter.Deserialize(fs);
                    return newFileData;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Файл поврежден", "Повреждение файла",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
            }
        }
    }
}