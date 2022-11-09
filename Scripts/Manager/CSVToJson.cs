using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class CSVToJson
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static void Convert(string _csvName)
    {
        // CSV파일 읽기
        var list = new List<Dictionary<string, string>>();
        TextAsset data = Resources.Load("CSVFile/" + _csvName) as TextAsset;
        if(data == null)
        {
            Debug.Log("데이터가 없습니다");
            return;
        }

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for(var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, string>();
            for(var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                string finalvalue = value;

                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        
        ConvertCSVFileToJson(list, _csvName, Application.dataPath + "/Resources/JsonFile");
    }
    public static void ConvertCSVFileToJson(List<Dictionary<string, string>> csv, string filePath, string outputPath)
    {
        Debug.Log("Excel To Json Converter: Processing: " + filePath);


        if (csv == null)
        {
            Debug.LogError("CSV To Json Converter: Failed to process file: " + filePath);
            return;
        }

        string spreadSheetJson = "";

        // Process Each SpreadSheet in the excel file

        spreadSheetJson = Newtonsoft.Json.JsonConvert.SerializeObject(csv);
        if (String.IsNullOrEmpty(spreadSheetJson))
        {
            Debug.LogError("CSV To Json Converter: Failed to covert Spreadsheet '" + filePath + "' to json.");
            return;
        }
        else
        {
            // The file name is the sheet name with spaces removed
            string fileName = filePath.Replace(" ", string.Empty);
            WriteTextToFile(spreadSheetJson, outputPath + "/" + fileName + ".json");
            Debug.Log("CSV To Json Converter: " + fileName + " successfully written to file.");
        }

    }
    private static string GetSpreadSheetJson(List<Dictionary<string, string>> csv, int _index)
    {

        // 나중에 없애거나 수정할 거 있으면 이걸로 해보자 ㄱㄱ
        // Get the specified table
        Dictionary<string, string> dataTable = csv[_index];

        // Remove empty columns
        for (int col = dataTable.Count - 1; col >= 0; col--)
        {
            bool removeColumn = true;
            string _row = null;
            foreach (string row in dataTable.Keys)
            {
                if (row != null)
                {
                    removeColumn = false;
                    _row = row;
                    
                    Debug.Log("행은 " + _row);
                    break;
                }
            }

            if (removeColumn)
            {
                dataTable[_row] = null;
            }
        }

        // Remove columns which start with '~'
        Regex columnNameRegex = new Regex(@"^~.*$");
        for (int i = dataTable.Count - 1; i >= 0; i--)
        {
            if (columnNameRegex.IsMatch(dataTable.Values.ToString()))
            {
                string _row = dataTable.Keys.ToString();
                dataTable[_row] = null;
            }
        }

        // Serialze the data table to json string
        return Newtonsoft.Json.JsonConvert.SerializeObject(csv);
    }
    private static void WriteTextToFile(string text, string filePath)
    {
        System.IO.File.WriteAllText(filePath, text);
    }
}
