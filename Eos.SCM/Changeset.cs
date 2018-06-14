using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Eos.SCM.Helpers;

namespace Eos.SCM
{

  public class Changeset : IEquatable<Changeset>
  {

    public static Changeset[] FromXml(string[] lines)
    {
      return FromXml(String.Join("", lines));
    }

    public static Changeset[] FromXml(string lines)
    {
      var doc = new XmlDocument();
      doc.LoadXml($"<Nodes>{lines}</Nodes>");
      return FromXml(doc.DocumentElement);
    }

    public static Changeset[] FromXml(XmlElement element)
    {
      var nodes = element.SelectNodes("Node");
      return nodes?.Cast<XmlElement>().Select(n => new Changeset(n)).ToArray() ?? new Changeset[] { };
    }


    public string Hash { get; private set; }

    public int Sequence { get; private set; }

    public DateTime Date { get; private set; }

    public string User { get; private set; }

    public string Description { get; private set; }

    public string[] Parents { get; private set; }


    private Changeset(XmlElement element)
    {
      Load(element);
    }

    private void Load(XmlElement element)
    {
      Hash = element.GetValue("ID");
      Date = element.GetValueDate("Date");
      User = element.GetValue("User");
      Description = element.GetValue("Comment");
      Parents =
        new []
        {
          element.GetValue("Parent1"),
          element.GetValue("Parent2")
        }.Where(p => !String.IsNullOrEmpty(p))
        .ToArray();
      /*
      var extrasNodes = element.SelectNodes("Extras/Ex").Cast<XmlElement>();
      foreach (var extrasNode in extrasNodes)
      {
        string[] parts = extrasNode.InnerText.Split("=".ToCharArray(), 2);
        try
        {
          if (parts[0] == "source") GraftSource = parts[1];
        }
        catch { }
      }
      */
    }

    public override int GetHashCode()
    {
      // ReSharper disable once NonReadonlyMemberInGetHashCode
      return Hash.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      var cs2 = obj as Changeset;
      return cs2 != null && Equals(cs2);
    }

    public bool Equals(Changeset other)
    {
      return other != null && other.Hash.Equals(Hash);
    }

    public override string ToString()
    {
      var str = Sequence >= 0 ? "{0}:{1}" : "{1}";
      return String.Format(str, Sequence, Hash);
    }
  }

}
