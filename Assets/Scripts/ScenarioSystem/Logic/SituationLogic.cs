using System;

public class SituationLogic
{
    public Action<string> OnSituationChangedSuccess;
    public Action<string> OnSituationChangedFailed;

    private readonly ISituationInterface situationInterface;
    private readonly SituationSimulation sitationSimulation;
    private readonly Situation startingSimulation;

    public SituationLogic(ISituationInterface situationInterface, SituationSimulation situationSimulation)
    {
        this.situationInterface = situationInterface;
        this.sitationSimulation = situationSimulation;

        startingSimulation = situationInterface.CurrentSituation;
    }   

    public void AttemptToChangeSituation(string directionNoun)
    {
        if(sitationSimulation.IsAnAvailableChoice(directionNoun))
        {
            situationInterface.CurrentSituation = sitationSimulation.UpdateRoom(directionNoun);
            OnSituationChangedSuccess(directionNoun);
        }
        else
        {            
            OnSituationChangedFailed(directionNoun);
        }
    }

    public void Reset()
    {
        situationInterface.CurrentSituation = startingSimulation;
    }

    public Situation GetCurrentSituation()
    {
        return situationInterface.CurrentSituation;
    }
}