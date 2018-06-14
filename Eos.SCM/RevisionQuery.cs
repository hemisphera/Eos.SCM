namespace Eos.SCM
{

  public class RevisionQuery
  {

    public string FromRevisionId { get; set; }

    public string ToRevisionId { get; set; }


    public static implicit operator RevisionQuery(string value)
    {
      return new RevisionQuery
      {
        FromRevisionId = value
      };
    }

  }

}
