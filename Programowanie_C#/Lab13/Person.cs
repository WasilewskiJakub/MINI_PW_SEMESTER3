using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace Lab_12
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public List<Prize> Prizes { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
    [Serializable]
    public class Prize
    {
        public int Place { get; set; }
        public Contest Contest { get; set; }

        public override string ToString()
        {
            return $"{Place} at {Contest}";
        }
    }
    [Serializable]
    public class Contest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [XmlIgnore]
        public List<Person> Participants { get; set; }
        public override string ToString()
        {
            return $"{Name} took place on {Date}";
        }
    }

    public class PathAlreadyExistsException : Exception
    {
        private string file_path { get; set; }
        public PathAlreadyExistsException(string file_path):base($"ERROR: {file_path} Exist.")
        {
            this.file_path = file_path;
        }
    }

    public class Scoreboard : IEnumerable
    {
        public List<Person> participants { get; set; } = new List<Person>();
        public List<Contest> tournaments { get; set; } = new List<Contest>();

        public string File_Path { get; set; }
        DirectoryInfo DirectoyToSave = null;
        public Scoreboard(string path = "")
        {
            this.File_Path = path;
            if(path!="")
            {
                if (Directory.Exists(path))
                    throw new PathAlreadyExistsException(path);
                else
                {
                    this.DirectoyToSave = Directory.CreateDirectory(path);
                }
            }
        }
        public void Add(Contest tournament)
        {
            var nameoftournament = Path.Combine(this.File_Path, $"{tournament.Name}.xml");
            if (File.Exists(nameoftournament) == true) throw new PathAlreadyExistsException(nameoftournament);
            XmlSerializer xmlserialize = new XmlSerializer(typeof(Contest));
            using (Stream file = File.Create(nameoftournament))
                xmlserialize.Serialize(file, tournament);
            tournaments.Add(tournament);
        }
        public void Info()
        {
            foreach(var person in this.participants)
            {
                Console.WriteLine(person);
                foreach(var prize in person.Prizes)
                {
                    Console.WriteLine(prize);
                }
            }
            foreach(var tournam in tournaments)
            {
                Console.WriteLine(tournam);
            }
        }

        public void Update(Contest ContentToChange)
        {
            try
            {
                Delete(ContentToChange.Name);
            }
            catch(Exception)
            {
                return;
            }
            Add(ContentToChange);
        }
        public bool Delete(string name)
        {
            var path = Path.Combine(this.File_Path, $"{name}.xml");
            int idx = this.tournaments.FindIndex((x) => x.Name == name);
            if(idx == -1)
            {
                if (File.Exists(path) == false) throw new PathAlreadyExistsException(path);
            }
            else
            {
                this.tournaments.RemoveAt(idx);
            }
            File.Delete(path);
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            foreach(string iterfile in Directory.EnumerateFiles(this.File_Path))
            {
                using (StreamReader reader = new StreamReader(iterfile))
                {
                    var deserializer = new XmlSerializer(typeof(Contest));
                    yield return deserializer.Deserialize(reader) as Contest;
                }
            }
        }
        public static Scoreboard Create(string sourceFileContests)
        {
            if (File.Exists(sourceFileContests) == false)
                throw new FileNotFoundException();
            Scoreboard toret = new Scoreboard();
            using(FileStream file = new FileStream(sourceFileContests,FileMode.Open))
            {
                BinaryFormatter serializer = new BinaryFormatter();
#pragma warning disable SYSLIB0011
                object x = serializer.Deserialize(file);
#pragma warning restore SYSLIB0011
                toret.tournaments = x as List<Contest>;
            }
            foreach (var tourn in toret.tournaments)
            {
                foreach (var person in tourn.Participants)
                    if(toret.participants.Contains(person)==false)
                        toret.participants.Add(person);
            }
            return toret;
        }
        public void Save(string directoryPath)
        {
            using(FileStream file = new FileStream(directoryPath,FileMode.Create))
            {
                BinaryFormatter serialize = new BinaryFormatter();
                serialize.Serialize(file, this.participants);
            }
        }
    }
}
