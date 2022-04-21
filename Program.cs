using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System;

// scraper section
/*
var scraper = new BadSlavaScraper();
List<string> locations = scraper.ScrapeLinks();
string csv = scraper.ScrapeAllEvents(locations);
*/

// csv formating section
/*
var fmtr = new CsvFormatter();
fmtr.WriteData("database/csv/output.csv", csv);
fmtr.ReadData("raw.csv");
*/

// mysql query section
var sql = new MySqlCommands();
string test = sql.SelectTest();
Console.WriteLine(test);
