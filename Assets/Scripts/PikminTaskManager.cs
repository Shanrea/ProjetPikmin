using System.Collections.Generic;
using UnityEngine;

public class PikminTaskManager : MonoBehaviour
{
    public List<PikminBehavior> pikmins = new List<PikminBehavior>();

    void Update()
    {
        AssignTasks();
    }

    void AssignTasks()
    {
        // Logique pour assigner des tâches aux Pikmins
        foreach (var pikmin in pikmins)
        {
            if (pikmin.target == null)
            {
                // Assigner une nouvelle cible ou tâche
            }
        }
    }
}
