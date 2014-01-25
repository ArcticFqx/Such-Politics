using SimpleJSON;

public class Statement{

    private string issue;
    private string category;
    private string positive;
    private string negative;

	Statement(JSONNode node)
    {
        issue = node["issue"];
        category = node["statement"];
        positive = node["positive"];
        negative = node["negative"];
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
