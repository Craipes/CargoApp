using CargoApp.Models;
using System.Xml;

namespace CargoApp;

public class XmlDataExtractor
{
    public string XmlPath { get; }

    private readonly bool hideNoneCities;
    private Action extractActions;
    private XmlElement? dataRoot;
    private IEnumerable<XmlNode> query = null!;

    public XmlDataExtractor(string xmlPath, bool hideNoneCities = true)
    {
        this.hideNoneCities = hideNoneCities;
        XmlPath = xmlPath;
        extractActions = LoadXml;
    }

    public IEnumerable<Settlement> RunAndGet()
    {
        extractActions();
        if (hideNoneCities)
        {
            return query.Select(s => new Settlement
            (
                s["OBL_NAME"]!.InnerText,
                s["REGION_NAME"]!.InnerText,
                s["CITY_NAME"]!.InnerText,
                s["CITY_REGION_NAME"]!.InnerText,
                !string.IsNullOrEmpty(s["CITY_NAME"]!.InnerText)
            ));
        }
        else
        {
            return query.Select(s => new Settlement
            (
               s["OBL_NAME"]!.InnerText,
               s["REGION_NAME"]!.InnerText,
               s["CITY_NAME"]!.InnerText,
               s["CITY_REGION_NAME"]!.InnerText
            ));
        }
    }

    public void RunAndSave(string savePath)
    {
        extractActions();
        var values = query.Select(s => s.OuterXml);

        string resultXml = string.Join("", values);
        resultXml = "<DATA FORMAT_VERSION=\"1.0\">" + resultXml + "</DATA>";
        XmlDocument result = new();
        result.LoadXml(resultXml);

        using StreamWriter sw = new(savePath);
        result.Save(sw);
    }

    private void LoadXml()
    {
        XmlDocument xmlDocument = new();
        xmlDocument.Load(XmlPath);
        dataRoot = xmlDocument.DocumentElement;
        if (dataRoot != null)
        {
            query = dataRoot.ChildNodes.Cast<XmlNode>();
        }
        else
        {
            throw new Exception("Invalid Xml file exception");
        }
    }

    public void AddDistinct()
    {
        extractActions += () =>
        {
            query = query.DistinctBy(s => s.InnerText);
        };
    }

    public void Dispose()
    {
        query = null!;
        dataRoot = null;
        extractActions = null!;
    }
}
