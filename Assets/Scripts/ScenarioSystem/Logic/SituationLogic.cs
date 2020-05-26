using System;

public class SituationLogic
{
    public Action<string> OnSituationChangedSuccess;
    public Action<string> OnSituationChangedFailed;

    private readonly ISituationInterface _situationInterface;
    private readonly SituationSimulation _sitationSimulation;
    private readonly Situation _startingSituation;

    public SituationLogic(ISituationInterface situationInterface, SituationSimulation situationSimulation)
    {
        this._situationInterface = situationInterface;
        this._sitationSimulation = situationSimulation;

        _startingSituation = situationInterface.CurrentSituation;
    }   

    public void AttemptToChangeSituation(string directionNoun)
    {
        if(_sitationSimulation.IsAnAvailableChoice(directionNoun))
        {
            _situationInterface.CurrentSituation = _sitationSimulation.UpdateSituation(directionNoun);
            OnSituationChangedSuccess(directionNoun);
        }
        else
        {            
            OnSituationChangedFailed(directionNoun);
        }
    }

    public void Reset()
    {
        _situationInterface.CurrentSituation = _startingSituation;
    }

    public Situation GetCurrentSituation()
    {
        return _situationInterface.CurrentSituation;
    }
}