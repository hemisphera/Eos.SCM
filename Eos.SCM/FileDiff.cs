using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace eos.SCM
{

  public class FileDiff
  {
    static readonly Regex HeaderRegEx = new Regex("diff -r (?<changeset>[0-9a-f]{12}) (?<filename>.*?)");
    static readonly Regex HeaderSubRegEx = new Regex("[(+++)(---)] [ab]?/(?<filename>.*?)\\t");

    internal static FileDiff[] FromLines(IEnumerable<string> inLines)
    {
      var lines = inLines.ToArray();
      if (!lines.Any() || (lines.All(String.IsNullOrEmpty)))
        return new FileDiff[] { };

      var blockIndex = -1;
      return lines
        .Select(l =>
        {
          if (HeaderRegEx.IsMatch(l))
            blockIndex++;
          return new
          {
            Line = l,
            BlockIndex = blockIndex,
          };
        })
        .GroupBy(l => l.BlockIndex)
        .Select(grp => new FileDiff(grp.Select(l => l.Line)))
        .ToArray();
    }


    public FileDiffBlock[] DiffBlocks { get; private set; }

    public string OldFilename { get; }

    public string NewFilename { get; }


    private FileDiff()
    { }

    private FileDiff(IEnumerable<string> lines) : this()
    {
      try
      {
        var headerLines = lines.Take(3).ToArray();
        var subHeaders = headerLines.Skip(1)
          .Select(l => {
            var match = HeaderSubRegEx.Match(l);
            if (!match.Success)
              throw new Exception();
            return new
            {
              Line = l,
              Match = match
            };
          });
        var subHeadersDict = subHeaders.ToDictionary(
          item => item.Line.Substring(0, 3),
          item => item);
        OldFilename = ConvertFilename(subHeadersDict["---"].Match.Groups["filename"].Value);
        NewFilename = ConvertFilename(subHeadersDict["+++"].Match.Groups["filename"].Value);
      }
      catch
      {
        throw new FormatException("The diff format is invalid.");
      }

      var re = new Regex(@"@@ -[0-9]+,[0-9]+ \+[0-9]+,[0-9]+ @@");

      // get all blocks
      int counter = 0;
      var blocks = lines
        .Select((line, index) => new { Line = line, StartIndex = index, Sequence = counter++ })
        .Where(item => item.Line.StartsWith("@@") && re.IsMatch(item.Line))
        .ToArray();

      var blockLineSets =
        blocks
        .Select((block, index) =>
          {
            int endIndex = (index < blocks.Length - 1) ? blocks[index + 1].StartIndex - 1 : lines.Count();
            return lines.Skip(block.StartIndex).Take(endIndex - block.StartIndex);
          });

      DiffBlocks = blockLineSets.Select(blockLineSet => new FileDiffBlock(blockLineSet)).ToArray();
    }


    private static string ConvertFilename(string filename)
    {
      return
        String.Compare(filename, "dev/null", StringComparison.OrdinalIgnoreCase) == 0 ?
        null :
        filename;
    }

  }

}
