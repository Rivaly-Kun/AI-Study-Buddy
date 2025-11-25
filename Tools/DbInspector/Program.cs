using Microsoft.Data.Sqlite;
using System;
using System.IO;

var dbPath = args.Length > 0 ? args[0] : "../../studybuddydb.db";
if (!File.Exists(dbPath))
{
    Console.Error.WriteLine($"Database file not found: {dbPath}");
    return 1;
}

var connString = $"Data Source={dbPath}";
using var conn = new SqliteConnection(connString);
conn.Open();

Console.WriteLine($"Opened DB: {Path.GetFullPath(dbPath)}\n");

// list tables
using (var cmd = conn.CreateCommand())
{
    cmd.CommandText = "SELECT name, type FROM sqlite_master WHERE type IN ('table','view') ORDER BY name;";
    using var reader = cmd.ExecuteReader();
    var tables = new System.Collections.Generic.List<string>();
    while (reader.Read())
    {
        var name = reader.GetString(0);
        var type = reader.GetString(1);
        Console.WriteLine($"{type}: {name}");
        tables.Add(name);
    }

    Console.WriteLine();

    foreach (var t in tables)
    {
        Console.WriteLine($"--- Rows from {t} ---");
        using var cmd2 = conn.CreateCommand();
        cmd2.CommandText = $"SELECT * FROM \"{t}\" LIMIT 10;";
        try
        {
            using var r2 = cmd2.ExecuteReader();
            // print column names
            for (int i = 0; i < r2.FieldCount; i++)
            {
                Console.Write(r2.GetName(i) + (i + 1 < r2.FieldCount ? " | " : "\n"));
            }
            while (r2.Read())
            {
                for (int i = 0; i < r2.FieldCount; i++)
                {
                    var val = r2.IsDBNull(i) ? "NULL" : r2.GetValue(i)?.ToString();
                    Console.Write(val + (i + 1 < r2.FieldCount ? " | " : "\n"));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading {t}: {ex.Message}");
        }
        Console.WriteLine();
    }
}

return 0;
