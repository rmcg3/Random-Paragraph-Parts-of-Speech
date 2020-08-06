using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Syn.WordNet;
using webDataRead;

namespace SynSetConsoleAppTest
{
    public class Program
    {
        public static string directory = Directory.GetCurrentDirectory();
        public static WordNetEngine wordNet = new WordNetEngine();

        public void Load()
        {

            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adj")), PartOfSpeech.Adjective);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adv")), PartOfSpeech.Adverb);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.noun")), PartOfSpeech.Noun);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.verb")), PartOfSpeech.Verb);

            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adj")), PartOfSpeech.Adjective);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adv")), PartOfSpeech.Adverb);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.noun")), PartOfSpeech.Noun);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.verb")), PartOfSpeech.Verb);

            Console.WriteLine("Loading data files...");
            wordNet.Load();
            Console.WriteLine("Files loaded successfully.");
        }
        public async System.Threading.Tasks.Task RunAsync()
        {


            string s = await DataScraper.getParagraphAsync();
            Console.WriteLine(s);

            string[] split = Regex.Replace(s, @"([ .,?!;:])", Environment.NewLine + "$1" + Environment.NewLine)
                                  .Split(new[] { Environment.NewLine, " " }, StringSplitOptions.RemoveEmptyEntries);
            

            foreach (var item in split)
            {
                Console.WriteLine($"Current word:'{item}'\n");
                var synSetList = wordNet.GetSynSets(item);
               
                if (synSetList.Count == 0) Console.WriteLine($"No SynSet found for '{item}'");

                foreach (var synSet in synSetList)
                {
                   Console.WriteLine($"Part of speech for {synSet}: {synSet.PartOfSpeech}\n");
                   await Task.Delay(1500);
                }
            }

            /*Console.Write("\nType first word:");

            var word = Console.ReadLine();
            var synSetList = wordNet.GetSynSets(word);
            //System.Collections.Generic.List<SynSet> synSetList
            if (synSetList.Count == 0) Console.WriteLine($"No SynSet found for '{word}'");

            foreach (var synSet in synSetList)
            {
                var words = string.Join(", ", synSet.Words);

                Console.WriteLine($"\nWords: {words}");
                Console.WriteLine($"Part Of Speech: {synSet.PartOfSpeech}");
                Console.WriteLine($"Gloss: {synSet.Gloss}");
            }*/

        }
        static async System.Threading.Tasks.Task Main()
        {
            Program prg = new Program();
            prg.Load();
            await prg.RunAsync();
        }
    }
}
