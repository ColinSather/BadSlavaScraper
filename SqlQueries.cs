using System.Collections;
using System.IO;
using System;
using MySql.Data;
using MySql.Data.MySqlClient;

/*
// TEST EVENT SCRAPER BY A SINGLE CITY
string url = "http://badslava.com/open-mics.php?city=Los%20Angeles&state=CA&type=Comedy";
var micList = scraper.GetMicList(url);
Console.WriteLine(micList);
*/

public class MySqlCommands {
    private string connStr = "server=127.0.0.1; uid=root; pwd=toor; database=openmics";

    public string SelectTest() 
    {
        string data = "";

        using(MySqlConnection conn = new MySqlConnection(connStr)) 
        {
            string sql = "select Date, Name from events";

            using(MySqlCommand cmd = new MySqlCommand(sql)) 
            {
                cmd.Connection = conn;
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                while(rdr.Read())
                    data += rdr[0] + "\t" + rdr[1];

                conn.Close();
            }
        }
        return data;
    }
    
}
