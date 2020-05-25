using System.Collections;

public class GameLogic
{
    public delegate void StartCoroutineDelegate(IEnumerator routine);

    private readonly StartCoroutineDelegate startCoroutine;
    private readonly SituationLogic situationLogic;
    private readonly SituationPresentation situationPresentation;

    public GameLogic(StartCoroutineDelegate startCoroutine, SituationLogic situationLogic, SituationPresentation situationPresentation)
    {
        this.startCoroutine = startCoroutine;
        this.situationLogic = situationLogic;
        this.situationPresentation = situationPresentation;

        startCoroutine(GameFlow());
    }

    private IEnumerator SituationFlow()
    {
        bool playerChoose = false;

        situationLogic.OnSituationChangedSuccess += (directionNoun) => playerChoose = true;

        while (!playerChoose)
        {
            yield return null;
        }
    }

    private IEnumerator GameFlow()
    {
        bool partyOver = false;

        while (true)
        {
            situationLogic.Reset();
            situationPresentation.LogSituationText();                  

            while (!partyOver)
            {
                yield return SituationFlow();
            }        
        }
    }           
}