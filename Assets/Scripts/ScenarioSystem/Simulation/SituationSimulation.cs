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

        UnpackChoicesInSituation();
    }

    public Situation UpdateSituation(string directionNoun)
    {
        _situation = _choicesDictionnary[directionNoun];

        _interactionDescriptionsInSituation.Clear();
        _choicesDictionnary.Clear();

        UnpackChoicesInSituation();       

        return _situation;
    }

    private void UnpackChoicesInSituation()
    {
        for (int i = 0; i < _situation.Choices.Length; i++)
        {
            _choicesDictionnary.Add(_situation.Choices[i].KeyString, _situation.Choices[i].NextSituation);
            _interactionDescriptionsInSituation.Add(_situation.Choices[i].ChoiceDescription);
        }
    }

    public bool IsAnAvailableChoice(string choice)
    {
        return _choicesDictionnary.ContainsKey(choice);
    }
}