public class Mission
{
    public string description; // Mission description
    public int goal; // Goal value (e.g., number of coins)
    public int currentProgress; // Current progress
    public bool isCompleted; // Is the mission completed

    public Mission(string description, int goal)
    {
        this.description = description;
        this.goal = goal;
        this.currentProgress = 0;
        this.isCompleted = false;
    }

    public void AddProgress(int amount)
    {
        currentProgress += amount;
        if (currentProgress >= goal)
        {
            isCompleted = true;
        }
    }
}
