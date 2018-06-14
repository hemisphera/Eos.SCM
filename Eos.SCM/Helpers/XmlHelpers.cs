using System;
using System.Xml;

namespace Eos.SCM.Helpers
{

  internal static class XmlHelpers
  {

    public static string GetValue(this XmlElement element, string xpath)
    {
      var subNode = element.SelectSingleNode(xpath);
      return subNode?.InnerText ?? "";
    }

    public static DateTime GetValueDate(this XmlElement element, string xpath)
    {
      var subNode = element.SelectSingleNode(xpath);
      return subNode == null ? DateTime.MinValue : XmlConvert.ToDateTime(subNode.InnerText, XmlDateTimeSerializationMode.Unspecified);
    }

    public static int GetValueInt(this XmlElement element, string xpath)
    {
      var subNode = element.SelectSingleNode(xpath);
      if (subNode == null)
        return -1;
      return int.Parse(subNode.InnerText);
    }

  }

}
