using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic
{
    public delegate void StartCoroutineDelegate(IEnumerator routine);

    private readonly StartCoroutineDelegate startCoroutine;
    private readonly RoomLogic roomLogic;
    private readonly RoomPresentation roomPresentation;

    public GameLogic(StartCoroutineDelegate startCoroutine, RoomLogic roomLogic, RoomPresentation roomPresentation)
    {
        this.startCoroutine = startCoroutine;
        this.roomLogic = roomLogic;
        this.roomPresentation = roomPresentation;

        startCoroutine(GameFlow());
    }

    private IEnumerator RoomFlow()
    {
        bool playerChoose = false;

        roomLogic.OnRoomChangedSuccessful += (directionNoun) => playerChoose = true;

        //roomPresentation.DisplayRoomText();

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
            roomLogic.Reset();
            roomPresentation.UpdateText();

            while (!partyOver)
            {
                yield return RoomFlow();
            }        
        }
    }           
}