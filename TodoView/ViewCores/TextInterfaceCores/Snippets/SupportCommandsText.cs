namespace TodoView.ViewCores.TextInterfaceCores.Snippets;

public struct SupportCommandsText
{
    public string CommandsTextList => 
        "\n" +
        "/ read all            - Show all tasks history without filtering.\n" +
        "/ read [filter]       - Show tasks for a specific period:\n" +
        "   \u251c\u2500 today            - Tasks created today.\n" +
        "   \u251c\u2500 yesterday        - Tasks created yesterday.\n" +
        "   \u251c\u2500 thisWeek         - Tasks created this week.\n" +
        "   \u251c\u2500 lastWeek         - Tasks created last week.\n" +
        "   \u251c\u2500 thisMonth        - Tasks created this month.\n" +
        "   \u251c\u2500 lastMonth        - Tasks created last month.\n" +
        "   \u251c\u2500 thisYear         - Tasks created this year.\n" +
        "   \u2514\u2500 lastYear         - Tasks created last year.\n" +
        "/ create                - Create a new task.\n" +
        "/ update [id]           - Update task content.\n" +
        "/ mark                  - Mark task as done/undone.\n" +
        "/ delete [id]           - Delete task by id.\n" +
        "/ delete all            - Delete all tasks.\n" +
        "/ exit                 - Exit the application.\n"
    ;
}