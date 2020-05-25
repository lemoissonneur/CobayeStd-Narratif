using System;
using System.Collections.Generic;

public class SituationSimulation
{
    private Situation situation;

    private readonly List<string> interactionDescriptionsInSituation;
    private readonly Dictionary<string, Situation> choicesDictionnary;

    public SituationSimulation(Situation startingSituation)
    {
        situation = startingSituation;

        interactionDescriptionsInSituation = new List<string>();
        choicesDictionnary = new Dictionary<string, Situation>();

        UnpackExitsInRoom();
    }

    public Situation UpdateRoom(string directionNoun)
    {
        situation = choicesDictionnary[directionNoun];

        // Clear collections for new room
        interactionDescriptionsInSituation.Clear();
        choicesDictionnary.Clear();

        UnpackExitsInRoom();       

        return situation;
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < situation.choices.Length; i++)
        {
            choicesDictionnary.Add(situation.choices[i].keyString, situation.choices[i].valueRoom);
            interactionDescriptionsInSituation.Add(situation.choices[i].choiceDescription);
        }
    }

    public bool IsAnAvailableChoice(string choice)
    {
        return choicesDictionnary.ContainsKey(choice);
    }
}