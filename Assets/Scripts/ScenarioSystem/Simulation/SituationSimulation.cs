using System;
using System.Collections.Generic;

public class SituationSimulation
{
    private Situation _situation;

    private readonly List<string> _interactionDescriptionsInSituation;
    private readonly Dictionary<string, Situation> _choicesDictionnary;

    public SituationSimulation(Situation startingSituation)
    {
        _situation = startingSituation;

        _interactionDescriptionsInSituation = new List<string>();
        _choicesDictionnary = new Dictionary<string, Situation>();

        UnpackExitsInRoom();
    }

    public Situation UpdateSituation(string directionNoun)
    {
        _situation = _choicesDictionnary[directionNoun];

        _interactionDescriptionsInSituation.Clear();
        _choicesDictionnary.Clear();

        UnpackExitsInRoom();       

        return _situation;
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < _situation._choices.Length; i++)
        {
            _choicesDictionnary.Add(_situation._choices[i].KeyString, _situation._choices[i]._nextSituation);
            _interactionDescriptionsInSituation.Add(_situation._choices[i].ChoiceDescription);
        }
    }

    public bool IsAnAvailableChoice(string choice)
    {
        return _choicesDictionnary.ContainsKey(choice);
    }
}