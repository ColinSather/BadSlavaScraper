using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System;

// scraper section
var scraper = new SlavaScraper();
List<string> locations = scraper.ScrapeUSLinks();
string csv = scraper.ScrapeAllEvents(locations);

// csv headers
// Date, Time, Name, Venue, Address, City, State, Map, Frequency, Cost, Info, Email, Link, Phone, Reviews,

// csv formating section
var fmtr = new CsvFormatter();
fmtr.WriteData("database/csv/output.csv", csv);
//fmtr.ReadData("raw.csv");

/*
// TEST EVENT SCRAPER BY A SINGLE CITY
string url = "open-mics.php?city=Los%20Angeles&state=CA&type=Comedy";
var micList = scraper.GetMicList(url);
Console.WriteLine(micList);
*/

// <summary>
// Handles all the badslava.com web scraping actions
// </summary>
class SlavaScraper {
    private string baseUrl = "http://badslava.com/";
    //private string baseUrl = "http://localhost/";
    private HtmlWeb web = new HtmlWeb();
 
    public List<string> ScrapeUSLinks() {
        // gets all the US city urls from badslava homepage
        var doc = web.Load(baseUrl);
        
        List<string> locations = new List<string>();
        string us_xpath = "//body/div/div[3]/div[1]/div/div/div/h3/a";
        var nodes = doc.DocumentNode.SelectNodes(us_xpath);
        
        foreach (var elem in nodes) {
            string url = elem.Attributes[0].Value;
            locations.Add(url);
        }

        return locations;
    }
   
    public string GetMicList(string url) {
        // get this week's open mic list for a state or country
        string xpath = "//body/div[1]/div/div/table/tbody/tr";

        var doc = web.Load(url);
        var nodes = doc.DocumentNode.SelectNodes(xpath);
        List<string> dates = new List<string>();
        string csv = "";

        foreach (var node in nodes) {
            var tbl = node.ParentNode.ParentNode;
            var font = tbl.PreviousSibling.PreviousSibling;
            string tr = node.InnerHtml;

            tr = tr.Replace("<td>", "");
            tr = tr.Replace("\n", "");
            tr = tr.Replace(",", "&#x2C;");
            tr = tr.Replace("</td>", ", ");
            tr = tr.Remove(tr.Length - 1);

            // TODO: Check if row is null before appending to csv 
            if (tr != "") {
                csv += font.InnerText.Split(" ")[1] + ", ";
                csv += tr;
                csv += "\n";
            }
        }
      
        return csv;
    }

    public string ScrapeAllEvents(List<string> locations) {
        // Scrape all events from badslava.com and return csv string
        string csv = "";

        foreach (string url in locations) {
            csv += GetMicList(url);
            Console.WriteLine(csv);
 
            // TODO: ensure mic list d/n have null entries and is valid
          
            // TODO: save mic list to db or file.
        }
        return csv;
    }
}

class CsvFormatter {
    public void ReadData(string csvPath)  {
        // Open the file to read from.
        using (StreamReader sr = File.OpenText(csvPath))
        {
            string? line = null;
            int ln = 1;
            while ((line = sr.ReadLine()) != null)
            {   
                var lst = line.Split(",");
                Console.WriteLine($"{ln} | {lst.Length}");
                ln += 1;
            }
        }
    }

    public void WriteData(string outFile, string csv) {
        if (!File.Exists(outFile))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(outFile))
            {
                sw.Write(csv);
            }
        }
    }
}

/*
*   TODO: Find out why you cannot use the HtmlNodeCollection class directly (see link)
*   https://docs.workflowgen.com/wfgmy/v320/html/c65cebec-e770-b732-08d1-b76f2406c1f6.htm
*/
