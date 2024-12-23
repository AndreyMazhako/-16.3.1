using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class CollectionPerformance
{
    public static async Task Main(string[] args)
    {
        string text = await DownloadTextAsync("https://lms-cdn.skillfactory.ru/assets/courseware/v1/dc9cf029ae4d0ae3ab9e490ef767588f/asset-v1:SkillFactory+CDEV+2021+type@asset+block/Text1.txt");

        // Тест для List<string>
        var listStopwatch = Stopwatch.StartNew();
        List<string> list = new List<string>();
        string[] words = text.Split(new char[] { ' ', '\n', '\r', ',', '.', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in words)
        {
            list.Add(word);
        }
        listStopwatch.Stop();
        Console.WriteLine($"List<string> Insertion Time: {listStopwatch.ElapsedMilliseconds} ms");

        // Тест для LinkedList<string>
        var linkedListStopwatch = Stopwatch.StartNew();
        LinkedList<string> linkedList = new LinkedList<string>();
        foreach (string word in words)
        {
            linkedList.AddLast(word);
        }
        linkedListStopwatch.Stop();
        Console.WriteLine($"LinkedList<string> Insertion Time: {linkedListStopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine($"List<string> Count: {list.Count} ");
        Console.WriteLine($"LinkedList<string> Count: {linkedList.Count} ");
    }

    static async Task<string> DownloadTextAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}