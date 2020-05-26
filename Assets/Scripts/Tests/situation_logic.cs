using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class situation_logic
    {
        [Test]
        public void change_situation_on_successful_attempt()
        {
            /*var startingSituation = ScriptableObject.CreateInstance<Situation>();
            var nextSituation = ScriptableObject.CreateInstance<Situation>();

            startingSituation.Type = Situation.SituationType.TextOnly;
            startingSituation.Description = "This is the starting situation.";
            startingSituation.NextSituation = nextSituation;

            nextSituation.Type = Situation.SituationType.TextOnly;
            nextSituation.Description = "This is the next situation.";
            nextSituation.NextSituation = startingSituation;

            var situationSimulation = new SituationSimulation(startingSituation);
            var situationInterface = Substitute.For<ISituationInterface>();
            var situationLogic = new SituationLogic(situationInterface, situationSimulation);

            situationLogic.AttemptToChangeSituation("toto");

            var currentSituation = situationLogic.GetCurrentSituation();
            Assert.AreEqual(nextSituation, currentSituation);*/

            Assert.Fail();
        }

        [Test]
        public void does_not_change_situation_on_failed_attempt()
        {
            Assert.Fail();
        }

        [Test]
        public void call_sucess_action_only_on_successful_attempt()
        {
            Assert.Fail();
        }

        [Test]
        public void call_failed_action_only_on_successful_attempt()
        {
            Assert.Fail();
        }
    }
}
