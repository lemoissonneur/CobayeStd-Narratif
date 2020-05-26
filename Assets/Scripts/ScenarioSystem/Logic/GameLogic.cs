using System.Collections;

public class GameLogic
{
    public delegate void StartCoroutineDelegate(IEnumerator routine);

    private readonly StartCoroutineDelegate _startCoroutine;
    private readonly SituationLogic _situationLogic;
    private readonly SituationPresentation _situationPresentation;

    public GameLogic(StartCoroutineDelegate startCoroutine, SituationLogic situationLogic, SituationPresentation situationPresentation)
    {
        this._startCoroutine = startCoroutine;
        this._situationLogic = situationLogic;
        this._situationPresentation = situationPresentation;

        startCoroutine(GameFlow());
    }

    private IEnumerator SituationFlow()
    {
        bool playerChoose = false;

        _situationLogic.OnSituationChangedSuccess += (directionNoun) => playerChoose = true;

        while (!playerChoose)
        {
            // TO DO
            yield return null;
        }
    }

    private IEnumerator GameFlow()
    {
        bool partyOver = false;

        while (true)
        {
            _situationLogic.Reset();
            _situationPresentation.LogSituationText();                  

            while (!partyOver)
            {
                yield return SituationFlow();
            }        
        }
    }           
}