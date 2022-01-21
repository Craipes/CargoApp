using StreetRegister.Models;
using System.Linq;
using System.Xml;

namespace StreetRegister;

public class XmlDataExtractor
{
    public string XmlPath { get; }

    private Action extractActions;
    private XmlElement? dataRoot;
    private IEnumerable<XmlNode> query = null!;

    public XmlDataExtractor(string xmlPath)
    {
        XmlPath = xmlPath;
        extractActions = LoadXml;
    }

    public IEnumerable<Settlement> RunAndGet()
    {
        extractActions();
        return query.Select(s => new Settlement
        (
            s["OBL_NAME"]!.InnerText,
            s["REGION_NAME"]!.InnerText,
            s["CITY_NAME"]!.InnerText,
            s["CITY_REGION_NAME"]!.InnerText
        ));
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
            foreach (var node in query)
            {
                node.RemoveChild(node.ChildNodes[^1]!);
            }
            //query = dataRoot.ChildNodes.Cast<XmlNode>()
            //    .Where(s => string.IsNullOrEmpty(s["STREET_NAME"]?.InnerText));
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
