using SimpleJSON;

public class Statement{

    private string issue;
    private string category;
    private string positive;
    private string negative;

	public Statement(JSONNode node)
    {
        issue = node["issue"];
        category = node["category"];
        positive = node["positive"];
        negative = node["negative"];
    }

    public static Statement[] getStatements()
    {
        JSONNode json = JSON.Parse(System.IO.File.ReadAllText("assets/scripts/test.json"));
        Statement[] statements = new Statement[json["statement"].Count];
        for (int i = 0; i < json["statement"].Count; i++)
        {
            statements[i] = new Statement(json["statement"][i]);
        }
        return statements;
    }

    public string getIssue()
    {
        return issue;
    }
    public string getCategory()
    {
        return category;
    }
    public string getPositive()
    {
        return positive;
    }
    public string getNegative()
    {
        return negative;
    }
}
